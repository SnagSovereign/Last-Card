using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscardPile : MonoBehaviour {

	public List<Card> discardPile = new List<Card>();

    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

	public void Discard(Card card)
    {
        print(card.value + "_" + card.suit);
        // add the card to the list
        discardPile.Add(card);
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
