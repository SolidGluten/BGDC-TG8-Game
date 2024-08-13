using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

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
    public event Action<int> OnPlayCard;
    public event Action<Card> OnDrawCard;

    public int handSize = 10;
    public int initialDraw = 6;

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
        foreach (Card card in knightDeck)
        {
            drawPile.Add(card);
        }

        foreach (Card card in mageDeck)
        {
            drawPile.Add(card);
        }

        DrawInitialHand();
    }

    public IEnumerator PlayCard(int index)
    {
        Card card = hand[index];
        if (!card) yield break;

        Character caster = CharacterManager.Instance.ActiveCharacters[0];
        var highlightedCells = CellsHighlighter.HighlightArea(caster.occupiedCell.index, card.rangeFromCaster, HighlightShape.Diamond);
        highlightedCells.ForEach((cell) =>
        {
            cell.Types = EnumFlags.SetFlag(cell.Types, CellType.Range, true);
        });

        Cell prevHoveredCell = null;
        List<Cell> cardEffectArea = new List<Cell>();

        while (true)
        {
            Vector2 normDir = (GameManager.MousePos - (Vector2)caster.transform.position).normalized;
            int x = Math.Abs(normDir.x) > Math.Abs(normDir.y) ? (int)Math.Round(normDir.x) : 0;
            int y = Math.Abs(normDir.x) < Math.Abs(normDir.y) ? (int)Math.Round(normDir.y) : 0;
            Vector2Int castDirVector = new Vector2Int(x, y);

            Direction castDir = Direction.Right;

            if(castDirVector == Vector2Int.left)
                castDir = Direction.Left;
            else if(castDirVector == Vector2Int.right)
                castDir = Direction.Right;
            else if(castDirVector == Vector2Int.up)
                castDir = Direction.Up;
            else
                castDir = Direction.Down;

            Cell hoveredCell = CellSelector.Instance.HoveredCell;

            //Debug.Log(hoveredCell);
            if (prevHoveredCell != hoveredCell)
            {
                cardEffectArea.ForEach((cell) => {
                    cell.Types = EnumFlags.SetFlag(cell.Types, CellType.Effect, false);
                });
                cardEffectArea.Clear();

                if(highlightedCells.Contains(hoveredCell))
                {
                    cardEffectArea = CellsHighlighter.HighlightArea(hoveredCell.index, card.width, card.effectShape, card.range, castDir);
                    cardEffectArea.ForEach((cell) =>
                    {
                        cell.Types = EnumFlags.SetFlag(cell.Types, CellType.Effect, true);
                    });
                }

                prevHoveredCell = hoveredCell;
            }


            if (Input.GetMouseButtonDown(0))
            {
                Cell selectedCell = CellSelector.Instance.SelectedCell; 

                // Plays the card
                if (selectedCell && highlightedCells.Contains(selectedCell))
                {
                    hand.Remove(card);
                    DiscardCard(card);

                    OnPlayCard?.Invoke(index);
                } else //Cancel playing the card
                {
                    OnCancelCard?.Invoke();
                }
                CellsHighlighter.ClearAll();
                yield break;
            }

            yield return null;
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

    public void DrawInitialHand()
    {
        for (int i = 0; i < initialDraw; i++)
        {
            DrawCard();
        }
    }

    public void DrawCard()
    {
        if (drawPile.Count == 0)
        {
            ReshuffleDiscardIntoDrawPile();
            return;
        }

        Card drawnCard = drawPile[0];
        drawPile.RemoveAt(0);
        hand.Add(drawnCard);
        OnDrawCard?.Invoke(drawnCard);
    }

    public void DiscardCard(Card card)
    {
        discardPile.Add(card);
    }

    public void ExhaustCard(Card card)
    {
        exhaustPile.Add(card);
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

    public void DiscardHand()
    {
        foreach (Card card in hand)
        {
            DiscardCard(card);
        }
        hand.Clear();
    }
}
