using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PickupDeck : MonoBehaviour {

	[SerializeField] DiscardPile discardPile;

	// Empty list of cards that will be filled in GenerateDeck()
	List<Card> pickupDeck = new List<Card>();

	public Card PickupCard()
    {
		// if the pickup deck is empty
		if(pickupDeck.Count == 0)
        {
			// set the pickup deck equal to the discard pile
			pickupDeck = discardPile.discardPile;
			// set the discard deck equal to just the topmost deck
			discardPile.discardPile = new List<Card>();
			discardPile.discardPile.Add(pickupDeck[pickupDeck.Count - 1]);
			// shuffle the pickup deck
			ShuffleDeck();
        }
		
		Card cardToPickup = pickupDeck[pickupDeck.Count - 1];

		pickupDeck.RemoveAt(pickupDeck.Count - 1);

		return cardToPickup;
    }
	
	public void GenerateDeck()
    {
		Card newCard;
        for (int value = 1; value <= 13; value++)
        {
            for (int suit = 0; suit <= 3; suit++)
            {
				newCard.value = value;
				newCard.suit = suit;
				pickupDeck.Add(newCard);
            }
        }
    }

	public void ShuffleDeck()
    {
		// a temporary list that stores the shuffledDeck
		List<Card> shuffledDeck = new List<Card>();

		// count how many cards are in the deck before they are taken away
		int cardsInDeck = pickupDeck.Count;

        for (int count = 0; count <= cardsInDeck - 1; count++)
        {
			// pick a random card from the deck
			int randomIndex = Random.Range(0, pickupDeck.Count);
			// add that random card to the shuffled deck
			shuffledDeck.Add(pickupDeck[randomIndex]);
			// remove the card from the deck so it cannnot be chosen again
			pickupDeck.RemoveAt(randomIndex);
        }

		pickupDeck = shuffledDeck;
		shuffledDeck = null; // free up memory
    }

	public void DealCards(List<Player> players)
    {
		foreach (Player player in players)
		{
			for (int card = 0; card < PlayerPrefs.GetInt("cards"); card++)
			{
                player.AddCard(pickupDeck[pickupDeck.Count - 1]);
                pickupDeck.RemoveAt(pickupDeck.Count - 1);
            }
		}

		// add card to discard pile that is not a power card
		int[] powerCards = { 1, 2, 8, 10, 11 };

        for (int cardIndex = pickupDeck.Count - 1; cardIndex > 0; cardIndex--)
		{
			if(!powerCards.Contains(pickupDeck[cardIndex].value))
            {
				FindObjectOfType<DiscardPile>().Discard(pickupDeck[cardIndex]);
				pickupDeck.RemoveAt(cardIndex);
				break;
			}
		}
	}
}