using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscardPile : MonoBehaviour {

	List<Card> discardPile = new List<Card>();

    Image image;

    private void Start()
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
}
