using UnityEngine;

public class CardObject : MonoBehaviour {

	protected Card thisCard;
	[HideInInspector]
	public Player playerParent;


	public void SetValueAndSuit(Card card)
	{
		thisCard.value = card.value;
		thisCard.suit = card.suit;
	}

	public Card GetCard()
	{
		return thisCard;
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