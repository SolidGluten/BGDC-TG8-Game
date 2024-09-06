using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    public const int MAX_ENERGY = 5;
    public int currentEnergy;

    public List<Card> knightDeck = new List<Card>();
    public List<Card> mageDeck = new List<Card>();
    public List<Card> deck = new List<Card>();

    private List<CardInstance> drawPile = new List<CardInstance>();
    private List<CardInstance> discardPile = new List<CardInstance>();
    private List<CardInstance> exhaustPile = new List<CardInstance>();

    private List<CardInstance> hand = new List<CardInstance>();

    public bool isPlaying = false;

    public int DrawPileCount => drawPile.Count;
    public int DiscardPileCount => discardPile.Count;
    public int ExhaustPileCount => exhaustPile.Count;

    public event Action OnCancelCard;
    public event Action OnDiscardHand;
    public event Action<CardInstance> OnDiscardCard;
    public event Action<CardInstance> OnPlayCard;
    public event Action<CardInstance> OnDrawCard;

    public int maxHandSize = 10;
    public int initialDraw = 7;

    private List<Cell> highlightedCells = new List<Cell>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            instance = this;
        }
    }

    private void Start()
    {
        foreach (Card card in knightDeck) deck.Add(card);
        foreach (Card card in mageDeck) deck.Add(card);


        CharacterManager.instance.OnCharacterInitialize += AddDeckToDrawPile;
        TurnController.instance.OnEndTurn += ResetHand;
    }

    public void ResetHand()
    {
        ResetEnergy();
        DiscardHand();
        DrawHand();
    }

    public void ResetEnergy() => currentEnergy = MAX_ENERGY;

    public IEnumerator PlayCard(CardInstance cardInstance)
    {
        isPlaying = true;

        Character caster = cardInstance.caster;
        Card card = cardInstance.cardScriptable;

        if (cardInstance == null)
        {
            Debug.Log("No card to play");
            yield break;
        }
        if (currentEnergy < cardInstance.cost)
        {
            Debug.Log("Not enough energy");
            yield break;
        }


        if (highlightedCells.Any())
            CellsHighlighter.LowerLayerType(highlightedCells, CellType.Range);

        highlightedCells = CellsHighlighter.HighlightArea(caster.occupiedCell.index, card.rangeFromCaster, HighlightShape.Square);
        CellsHighlighter.RaiseLayerType(highlightedCells, CellType.Range);

        Cell prevHoveredCell = null;
        Direction prevDir = Direction.Right;
        List<Cell> cardEffectArea = new List<Cell>();

        while (true)
        {
            var castDir = CharacterManager.DirectionFromCharacter(caster);
            Cell hoveredCell = CellSelector.Instance.HoveredCell;

            if (card.rangeFromCaster == 0)
            {
                if (prevDir != castDir)
                {
                    CellsHighlighter.LowerLayerType(cardEffectArea, CellType.Effect);
                    cardEffectArea.Clear();

                    cardEffectArea = CellsHighlighter.HighlightArea(
                        caster.occupiedCell.index,
                        card.width,
                        card.effectShape,
                        card.range,
                        castDir);

                    CellsHighlighter.RaiseLayerType(cardEffectArea, CellType.Effect);

                    prevDir = castDir;
                }

            } else
            {
                if (prevHoveredCell != hoveredCell)
                {
                    CellsHighlighter.LowerLayerType(cardEffectArea, CellType.Effect);
                    cardEffectArea.Clear();


                    if (highlightedCells.Contains(hoveredCell))
                    {
                        cardEffectArea = CellsHighlighter.HighlightArea(hoveredCell.index, card.width, card.effectShape, card.range, castDir);
                        CellsHighlighter.RaiseLayerType(cardEffectArea, CellType.Effect);
                    }

                    prevHoveredCell = hoveredCell;
                }
            }

            if (Input.anyKeyDown)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Cell selectedCell = CellSelector.Instance.SelectedCell;

                    var targetCells = cardEffectArea.Where((cell) => cell && cell.isOccupied);
                    Entity[] target = targetCells.Select((cell) => cell.occupiedEntity).ToArray();

                    if (cardInstance.PlayCard(target))
                    {
                        currentEnergy -= card.cost;
                        if (card.exhaust) ExhaustCard(cardInstance);
                        else DiscardCard(cardInstance);
                        OnPlayCard?.Invoke(cardInstance);
                    }
                    else
                    {
                        OnCancelCard?.Invoke();
                    }

                    isPlaying = false;
                }
                else
                {
                    OnCancelCard();
                    isPlaying = false;
                }

                CellsHighlighter.LowerLayerType(highlightedCells, CellType.Range);
                CellsHighlighter.LowerLayerType(cardEffectArea, CellType.Effect);

                yield break;
            }

            yield return null;
        }
    }

    public void DrawHand()
    {
        for (int i = 0; i < initialDraw; i++) 
            DrawCard();
    }

    public void DrawCard()
    {
        if (hand.Count >= maxHandSize) return;
        if (drawPile.Count == 0) ReshuffleDiscardIntoDrawPile();
        if (drawPile.Count == 0) return;

        CardInstance drawnCard = drawPile[0];
        if (drawnCard == null) return;

        drawPile.RemoveAt(0);
        AddCardToHand(drawnCard);
    }

    public void AddCardToHand(CardInstance card)
    {
        hand.Add(card);
        OnDrawCard?.Invoke(card);
    }

    // Discard
    [ContextMenu("Discard Hand")]
    public void DiscardHand()
    {
        OnDiscardHand?.Invoke();
        for(int i = hand.Count - 1; i >= 0; i--)
        {
            var cardInstance = hand[i];
            if(cardInstance != null) 
                DiscardCard(cardInstance);
        }
    }
    public void DiscardCard(CardInstance cardInstance) {
        discardPile.Add(cardInstance);
        hand.Remove(cardInstance);
        OnDiscardCard?.Invoke(cardInstance);
    }
    public void ExhaustCard(CardInstance cardInstance)
    {
        exhaustPile.Add(cardInstance);
        hand.Remove(cardInstance);
        OnDiscardCard?.Invoke(cardInstance);
    }

    private void ReshuffleDiscardIntoDrawPile()
    {
        drawPile.AddRange(discardPile);
        discardPile.Clear();
        Shuffle(drawPile);
    }

    private void Shuffle(List<CardInstance> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            CardInstance temp = list[i];
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void ResetDeck()
    {
        hand.Clear();
        drawPile.Clear();
        discardPile.Clear();
        exhaustPile.Clear();
        AddDeckToDrawPile();
        Shuffle(drawPile);
    }

    public void AddDeckToDrawPile()
    {
        foreach (Card card in deck)
        {
            var caster = CharacterManager.instance.GetCharacterByType(card.caster);
            var cardInstance = new CardInstance(card, caster);
            drawPile.Add(cardInstance);
        }

        ResetHand();
    }

    public List<CardInstance> GetAllPlayingCards() 
    {
        var cards = new List<CardInstance>();
        cards.AddRange(hand);
        cards.AddRange(drawPile);
        cards.AddRange(discardPile);
        cards.AddRange(exhaustPile);
        return cards;
    }

    public void OnDisable()
    {
        TurnController.instance.OnEndTurn -= ResetHand;
        CharacterManager.instance.OnCharacterInitialize -= AddDeckToDrawPile;
    }

    public void OnDestroy()
    {
        deck.Clear();
        hand.Clear();
        exhaustPile.Clear();
        discardPile.Clear();
        drawPile.Clear();
    }
}
