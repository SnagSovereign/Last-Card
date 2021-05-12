using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour {

	[HideInInspector]
	public bool myTurn = false;

	protected LastCardManager manager;
	protected DiscardPile discardPile;
	protected PickupDeck pickupDeck;

	protected List<CardObject> hand = new List<CardObject>();
	public List<Card> validCards = new List<Card>();

	// this method is called when an Ace is played
	protected abstract void SelectSuit();

	private void Awake()
    {
		manager = FindObjectOfType<LastCardManager>();
		discardPile = FindObjectOfType<DiscardPile>();
		pickupDeck = FindObjectOfType<PickupDeck>();
	}

	public void StartTurn()
	{
		myTurn = true;

		// Check if the previous card was a power card
		CheckForPowerCard();
	}

	public void EndTurn()
    {
		myTurn = false;

		if(hand.Count == 0)
        {
			manager.GameOver();
			// win the game
        }
		else
        {
			// if player has their last card
			if(hand.Count == 1)
            {
				// audio and visual cues would go here
            }

			// continue the game
			manager.NextPlayer();
		}
	}


	public void AddCard(Card card)
    {
		// spawn the card
		CardObject newCard = Instantiate
		(
			Resources.Load<CardObject>("Prefabs/" + tag + "Card")
		);

		// add card to player hand list
		hand.Add(newCard);

		// set the parent of the card to this player object
		newCard.transform.SetParent(gameObject.transform, false);
		newCard.playerParent = this;

		//Set the value and suit of the card
		newCard.SetValueAndSuit(card);
	}

	public void Discard(int cardIndex)
    {
		CardObject cardToDiscard = hand[cardIndex];

		// add the card to the discard pile
		discardPile.Discard(cardToDiscard.GetCard());

		//Check if the card was a power card
		UsePowerCard(cardIndex);

		// remove the card from the hand list
		hand.RemoveAt(cardIndex);

		// destroy the gameobject
		Destroy(cardToDiscard.gameObject);

		//if the card being discarded is not an Ace
        if (discardPile.GetTopCard().value != 1)
        {
			EndTurn();
		}
	}

	// valid cards are calculated at the start of each players turn
	protected virtual void CalcValidCards(int powerCard)
    {
		validCards = new List<Card>();

		Card discard = discardPile.GetTopCard();

		// if the previous card was not an offensive power card
		if(powerCard == 0)
        {
			foreach (CardObject card in hand)
			{
				// valid cards must match the value and suit of the previous card
				if (card.GetCard().value == discard.value ||
					card.GetCard().suit == manager.GetSuit() ||
					card.GetCard().value == 1) // An Ace can be played regardless of previous suit
				{
					validCards.Add(card.GetCard());
				}
			}
		}
		else // if the previous card was a 2 or a Black Jack
        {
			// loop through all the players cards
			foreach (CardObject card in hand)
			{
				// the only valid cards are a 2 or Jack
				if (card.GetCard().value == powerCard)
				{
					validCards.Add(card.GetCard());
				}
			}

			// if the player does not have any valid cards to defend against a 2 or Black Jack
			if (validCards.Count == 0)
			{
				// the player is forced to pickup
				for (int pickedUp = 0; pickedUp < manager.GetPickupCount(); pickedUp++)
				{
					AddCard(pickupDeck.PickupCard());
				}

				manager.ResetPickupCount();
				EndTurn();
			}
		}
	}

	void UsePowerCard(int cardIndex)
    {
		Card card = hand[cardIndex].GetCard();

		if(card.value == 2)
        {
			// Pickup 2
			manager.AddToPickupCount(2);
        }
		else if(card.value == 8)
        {
			// Skip
			manager.Skip();
        }
		else if(card.value == 10)
        {
			// if there are only 2 players, the 10 acts as a skip
			if(manager.GetPlayerCount() == 2)
            {
				// Skip
				manager.Skip();
			}
			else
            {
				// Reverse
				manager.ReversePlayDirection();
			}

        }
		else if(card.value == 11 && (card.suit == 0 || card.suit == 3))
        {
			// Black Jack
			// Next player picks up however many cards were dealt
			manager.AddToPickupCount(PlayerPrefs.GetInt("cards"));
        }
		else if(card.value == 11 && (card.suit == 1 || card.suit == 2))
        {
			// Red Jack
			// Blocks a black Jack
			manager.ResetPickupCount();
        }
		else if(card.value == 1)
        {
			// Ace
			// Chooses suit
			SelectSuit();
        }
    }

	void CheckForPowerCard()
    {
		if(discardPile.GetTopCard().value == 2 && manager.GetPickupCount() != 0)
		{
			//Pickup 2
			CalcValidCards(2);
		}
		else if (discardPile.GetTopCard().value == 11 &&
			    (manager.GetSuit() == 0 ||
			     manager.GetSuit() == 3) &&
				 manager.GetPickupCount() != 0)
		{
			// Black Jack
			CalcValidCards(11);
		}
		else if(manager.CheckSkip()) //8
		{
            // If the last card played was skip and no one else has been skipped yet
            manager.Skip();
			EndTurn();
		}
		else
        {
			// if the previous card was not an offensive power card
			CalcValidCards(0);
        }
    }
}