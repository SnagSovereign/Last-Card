using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDeck : MonoBehaviour {

	// Empty list of cards that will be filled in GenerateDeck()
	List<Card> pickupDeck = new List<Card>();

	public void PickupButton()
    {
		print("Pickup");
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

	public void DealCards(Player[] players)
    {
		foreach (Player player in players)
		{
			for (int i = 0; i < PlayerPrefs.GetInt("cards"); i++)
			{
                player.AddCard(pickupDeck[pickupDeck.Count - 1]);
                pickupDeck.RemoveAt(pickupDeck.Count - 1);
            }
		}

		// add card to discard pile
		FindObjectOfType<DiscardPile>().Discard(pickupDeck[pickupDeck.Count - 1]);
		pickupDeck.RemoveAt(pickupDeck.Count - 1);
	}
}