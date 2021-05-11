using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour {

	[HideInInspector]
	public bool myTurn = false;
	protected bool pickedUp = false;

	protected LastCardManager manager;
	protected DiscardPile discardPile;
	protected PickupDeck pickupDeck;

	protected List<CardObject> hand = new List<CardObject>();
	public List<Card> validCards = new List<Card>();

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

		// if the previous card was an offensive power card
		if(powerCard != 0)
        {
			// if the prevous card was an Ace
			if (powerCard == 1)
			{
				foreach (CardObject card in hand)
				{
					if (card.GetCard().suit == manager.GetSuit() || card.GetCard().suit == 1)
					{
						validCards.Add(card.GetCard());
					}
				}
			}
			else // if the previous card was a 2 or Black Jack
			{
				foreach (CardObject card in hand)
				{
					if (card.GetCard().value == powerCard)
					{
						validCards.Add(card.GetCard());
					}
				}
				if(validCards.Count == 0)
                {
					for(int pickedUp = 0; pickedUp < manager.GetPickupCount(); pickedUp++)
                    {
						AddCard(pickupDeck.PickupCard());
                    }
					pickedUp = true;
					manager.ResetPickupCount();
					EndTurn();
                }
			}
        }
		else // the previous card was not an offensive power card
        {
			foreach (CardObject card in hand)
			{
				if (card.GetCard().value == discard.value || 
					card.GetCard().suit == discard.suit || 
					card.GetCard().value == 1)
				{
					validCards.Add(card.GetCard());
				}
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
			// Reverse
			manager.ReversePlayDirection();
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
		if(discardPile.GetTopCard().value == 1) // Ace
        {
			// only valid cards are ones that have a suit that matches manager.suit
			CalcValidCards(1);
		}
		else if(discardPile.GetTopCard().value == 2 && manager.GetPickupCount() != 0) // 2
		{
			//Pickup 2
			// only valid cards to play is a 2
			CalcValidCards(2);
		}
		else if (discardPile.GetTopCard().value == 11 &&
			    (discardPile.GetTopCard().suit == 0 ||
			     discardPile.GetTopCard().suit == 3) &&
				 manager.GetPickupCount() != 0)
		{
			// Black Jack
			// Only valid cards to play are a Jack (black or red)
			CalcValidCards(11);
		}
		else if(manager.skip) // 8
		{
            // Skip
            manager.skip = false;
			EndTurn();
		}
		else
        {
			// if the previous card was not a power card
			CalcValidCards(0);
        }
    }
}