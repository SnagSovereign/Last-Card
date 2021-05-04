using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastCardManager : MonoBehaviour {

	[SerializeField] Player[] players;
	[SerializeField] PickupDeck pickupDeck;

	int playDirection = 1;
	int currentPlayerTurn = 0;
	bool gameOver = false;

	private void Start()
    {
		SetupGame();
	}

	private void SetupGame()
    {
		pickupDeck.GenerateDeck();
		pickupDeck.ShuffleDeck();
		pickupDeck.DealCards(players);

		// tell the first player that it is there turn
		players[currentPlayerTurn].myTurn = true;
	}

	public void NextPlayer()
    {
		currentPlayerTurn = GetNextPlayer();

		// tell the current player that it is their turn
		players[currentPlayerTurn].myTurn = true;
	}

	int GetNextPlayer()
	{
		// calculate the next player by adding the play direction (1 or -1)
		int nextPlayer = currentPlayerTurn + playDirection;

		if(nextPlayer < 0)
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