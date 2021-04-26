using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingStack : MonoBehaviour {

	// Empty list of cards that will be filled in GenerateDeck()
	List<Card> deck = new List<Card>();

	void Start () 
	{
		GenerateDeck();
		ShuffleDeck();
	}
	
	void GenerateDeck()
    {
		Card newCard;
        for (int value = 1; value <= 13; value++)
        {
            for (int suit = 0; suit <= 3; suit++)
            {
				newCard.value = value;
				newCard.suit = suit;
				deck.Add(newCard);
            }
        }
    }

	void ShuffleDeck()
    {
		// a temporary list that stores the shuffledDeck
		List<Card> shuffledDeck = new List<Card>();

		// count how many cards are in the deck before they are taken away
		int cardsInDeck = deck.Count;

        for (int count = 0; count <= cardsInDeck - 1; count++)
        {
			// pick a random card from the deck
			int randomIndex = Random.Range(0, deck.Count);
			// add that random card to the shuffled deck
			shuffledDeck.Add(deck[randomIndex]);
			// remove the card from the deck so it cannnot be chosen again
			deck.RemoveAt(randomIndex);
        }

		deck = shuffledDeck;
		shuffledDeck = null; // free up memory
    }
}

