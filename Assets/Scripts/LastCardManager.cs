using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastCardManager : MonoBehaviour {

	[SerializeField] Player[] players;
	[SerializeField] PickupDeck pickupDeck;

	int playDirection = 1;
	int currentPlayerTurn = 0;
	bool gameOver = false;

	void Start()
    {
		pickupDeck.GenerateDeck();
		pickupDeck.ShuffleDeck();
		pickupDeck.DealCards(players);
    }

	int NextPlayer()
	{
		int nextPlayer = currentPlayerTurn + playDirection;

		if (nextPlayer < 0)
		{
			nextPlayer = players.Length - 1;
		}
		else if(nextPlayer > players.Length - 1)
		{
			nextPlayer = 0;
		}

		return nextPlayer;
	}
}