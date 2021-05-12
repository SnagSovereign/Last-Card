using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PickupDeck : MonoBehaviour {

	[SerializeField] DiscardPile discardPile;

	// Empty list of cards that will be filled in GenerateDeck()
	List<Card> pickupDeck = new List<Card>(52);

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

		// loop through all the values (1 to 13)
        for (int value = 1; value <= 13; value++)
        {
			//loop through all the suits (0 to 3)
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
		//loop through each player
		foreach (Player player in players)
		{
			// Add 5-8 cards to the players hand
			for (int card = 0; card < PlayerPrefs.GetInt("cards"); card++)
			{
                player.AddCard(pickupDeck[pickupDeck.Count - 1]); // add card to hand
                pickupDeck.RemoveAt(pickupDeck.Count - 1); // remove card from pickup deck
            }
		}

		int[] powerCards = { 1, 2, 8, 10, 11 };

		//start looping through the pickup deck
        for (int cardIndex = pickupDeck.Count - 1; cardIndex > 0; cardIndex--)
		{
			// if the card is not a power card
			if (!powerCards.Contains(pickupDeck[cardIndex].value))
            {
				// add the card to the discard pile
				discardPile.Discard(pickupDeck[cardIndex]);

				// remove the card from the pickup deck
				pickupDeck.RemoveAt(cardIndex);
				break;
			}
		}
	}
}