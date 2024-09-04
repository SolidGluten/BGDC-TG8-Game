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
    [SerializeField] private List<Card> drawPile = new List<Card>();
    [SerializeField] private List<Card> discardPile = new List<Card>();
    [SerializeField] private List<Card> exhaustPile = new List<Card>();

    public List<Card> hand = new List<Card>();

    public int DrawPileCount => drawPile.Count;
    public int DiscardPileCount => discardPile.Count;
    public int ExhaustPileCount => exhaustPile.Count;


    public event Action OnCancelCard;
    public event Action OnDiscardHand;
    public event Action<int> OnDiscardCard;
    public event Action<int> OnPlayCard;
    public event Action<Card> OnDrawCard;

    public int maxHandSize = 10;
    public int initialDraw = 7;

    private List<Cell> highlightedCells = new List<Cell>();

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            instance = this;
        }
    }

    private void Start()
    {
        foreach (Card card in knightDeck) drawPile.Add(card);
        foreach (Card card in mageDeck) drawPile.Add(card);

        ResetEnergy();
        DrawHand();

        TurnController.instance.OnEndTurn += ResetHand;
    }

    public void ResetHand()
    {
        ResetEnergy();
        DiscardHand();
        DrawHand();
    }

    public void ResetEnergy() => currentEnergy = MAX_ENERGY;

    public IEnumerator PlayCard(int index)
    {
        Card card = hand[index];
        if (!card) yield break;
        if (currentEnergy < card.cost) yield break;

        Character caster = CharacterManager.Instance.GetCharacterByType(card.caster);

        if(highlightedCells.Any())
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

                    cardEffectArea = CellsHighlighter.HighlightArea(caster.occupiedCell.index, card.width, card.effectShape, card.range, castDir);

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

                    if (card.Play(caster, target))
                    {
                        currentEnergy -= card.cost;
                        DiscardCard(index);
                        OnPlayCard?.Invoke(index);
                    }
                    else
                    {
                        OnCancelCard?.Invoke();
                    }
                }
                else
                {
                    OnCancelCard();
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
        
        Card drawnCard = drawPile[0];
        if (!drawnCard) return;

        drawPile.RemoveAt(0);
        AddCardToHand(drawnCard);
    }

    public void AddCardToHand(Card card)
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
            DiscardCard(i);
    }
    public void DiscardCard(int index) {
        if (index < 0 || index > hand.Count - 1) return;
        var card = hand[index];
        discardPile.Add(card);
        hand.RemoveAt(index);
        OnDiscardCard?.Invoke(index);
    }
    public void ExhaustCard(int index)
    {
        if (index < 0 || index > hand.Count - 1) return;
        var card = hand[index];
        exhaustPile.Add(card);
        hand.RemoveAt(index);
        OnDiscardCard?.Invoke(index);
    }

    private void ReshuffleDiscardIntoDrawPile()
    {
        drawPile.AddRange(discardPile);
        discardPile.Clear();
        Shuffle(drawPile);
    }

    private void Shuffle(List<Card> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Card temp = list[i];
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
        drawPile.AddRange(deck);
        Shuffle(drawPile);
    }

    public void OnDisable()
    {
        TurnController.instance.OnEndTurn -= ResetHand;
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
