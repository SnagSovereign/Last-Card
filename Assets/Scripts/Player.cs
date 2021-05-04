using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	protected float spacing;

	[HideInInspector] public bool myTurn = false;

	LastCardManager manager;
	protected DiscardPile discardPile;
	protected PickupDeck pickupDeck;

	// protected List<Card> hand = new List<Card>();
	protected List<CardObject> hand = new List<CardObject>();

	private void Awake()
    {
		manager = FindObjectOfType<LastCardManager>();
		discardPile = FindObjectOfType<DiscardPile>();
		pickupDeck = FindObjectOfType<PickupDeck>();
	}

	public void AddCard(Card card)
    {
		// spawn the card
		CardObject newCard = Instantiate
		(
			Resources.Load<CardObject>("Prefabs/" + tag + "Card"),
			new Vector3(spacing * hand.Count, 0f, 0f),
			Quaternion.identity
		);

        // add card to player hand list
		hand.Add(newCard);

		// set the parent of the card to this player object
		newCard.transform.SetParent(gameObject.transform, false);

		//Set the value and suit of the card
		newCard.SetValueAndSuit(card);
	}

	public List<Card> ValidCards()
    {
		List<Card> validCards = new List<Card>();
		Card discard = discardPile.GetTopCard();

		foreach(CardObject card in hand)
        {
			if (card.GetCard().value == discard.value || card.GetCard().suit == discard.suit)
			{
				validCards.Add(card.GetCard());
			}
		}

		return validCards;
    }
}