using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscardPile : MonoBehaviour {

    [SerializeField] LastCardManager manager;

	public List<Card> discardPile = new List<Card>(52);

    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

	public void Discard(Card card)
    {
        // add the card to the list
        discardPile.Add(card);
        // change the suit in play
        manager.ChangeSuit(card.suit);
        image.sprite = Resources.Load<Sprite>("Sprites/Deck/" + card.value + "_" + card.suit);
    }
    
    public Card GetTopCard()
    {
        return discardPile[discardPile.Count - 1];
    }

    public void SuitChange(int newSuit)
    {
        image.sprite = Resources.Load<Sprite>("Sprites/Suits/" + newSuit);
    }
}