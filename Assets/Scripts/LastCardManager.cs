using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastCardManager : MonoBehaviour {

	[SerializeField] Player[] players;
	[SerializeField] PickupDeck pickupDeck;
	[SerializeField] GameObject playDirectionArrows;
	[SerializeField] GameObject pausePanel;

	int playDirection = 1;
	int currentPlayerTurn = 0;
	bool gameOver = false;

	int pickupCount = 0;
	[HideInInspector]
	public bool skip = false;
	int suit;

	private void Start()
    {
		Time.timeScale = 1f;
		SetupGame();
	}

	private void SetupGame()
    {
		pickupDeck.GenerateDeck();
		pickupDeck.ShuffleDeck();
		pickupDeck.DealCards(players);

		// tell the first player that it is there turn
		players[currentPlayerTurn].StartTurn();
	}

	public void NextPlayer()
	{
		print("NEXT PLAYER");

		currentPlayerTurn = GetNextPlayer();

		// tell the current player that it is their turn
		players[currentPlayerTurn].StartTurn();
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

	public void ReversePlayDirection()
	{
		playDirection *= -1;
		playDirectionArrows.transform.localScale = new Vector3
		(
			1f, 
			playDirectionArrows.transform.localScale.y * -1f, 
			1f
		);
	}

	public void AddToPickupCount(int amount)
    {
		pickupCount += amount;
    }

	public int GetPickupCount()
    {
		return pickupCount;
    }

	public void ResetPickupCount()
    {
		pickupCount = 0;
    }

	public void Skip()
    {
		skip = true;
    }

	public void ChangeSuit(int newSuit)
    {
		suit = newSuit;
    }

	public int GetSuit()
    {
		return suit;
    }

	public void PauseButton()
	{
		Time.timeScale = 0f;
		pausePanel.SetActive(true);
	}

	public void ResumeButton()
    {
		Time.timeScale = 1f;
		pausePanel.SetActive(false);
    }
}