using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : MonoBehaviour {

	protected Card thisCard;
	public void SetValueAndSuit(Card card)
	{
		thisCard.value = card.value;
		thisCard.suit = card.suit;
	}

	public Card GetCard()
	{
		return thisCard;
	}

	protected void Discard()
    {
		// add the card to the discard pile
		FindObjectOfType<DiscardPile>().Discard(thisCard);
		Destroy(gameObject);
    }
}

public struct Card
{
	public int value, suit;

	/*
	value: 1-13 (Ace - King)

	suit: 0 = clubs
		  1 = diamonds
		  2 = hearts
		  3 = spades
	*/
}