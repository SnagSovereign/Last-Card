using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LastCardManager : MonoBehaviour {

	// Object references
	[SerializeField] List<Player> players;
	[SerializeField] PickupDeck pickupDeck;
	[SerializeField] GameObject playDirectionArrows;
	[SerializeField] GameObject pausePanel;
	[SerializeField] GameObject gameOverPanel;
	[SerializeField] TMP_Text gameOverText;

	int currentPlayerTurn = 0;
	int playDirection = 1; // 1 = clockwise & -1 = anticlockwise

	// Skip boolean ensures that the game does not infinitely skip all players after an 8 is played
	bool skip = false;

	// Pickup Count keeps track of how many cards a player must pick up
	int pickupCount = 0;

	int suit;

	private void Start()
    {
		// time scale set to 1 in case the user pauses the game, quits, then plays again
		Time.timeScale = 1f;
		SetupGame();
	}

	private void SetupGame()
    {
		SetNumOfPlayers();
		pickupDeck.GenerateDeck();
		pickupDeck.ShuffleDeck();
		pickupDeck.DealCards(players);

		// tell the first player that it is there turn
		players[currentPlayerTurn].StartTurn();
	}

	private void SetNumOfPlayers()
    {
		if(PlayerPrefs.GetInt("players") == 2)
        {
			// destroy and remove the left player
            Destroy(players[1].gameObject);
            players.RemoveAt(1);

			// destroy and remove the right player
            Destroy(players[2].gameObject);
            players.RemoveAt(2);

			// disable the play direction arrows because they are unnecessary with 2 players
			playDirectionArrows.SetActive(false);
        }
		else if(PlayerPrefs.GetInt("players") == 3)
        {
			// destroy and remove the top player
			Destroy(players[2].gameObject);
			players.RemoveAt(2);
		}
	}

	public void GameOver() // this method is called when a player has 0 cards
    {
		gameOverPanel.SetActive(true);

		// if the player who won is the user
		if(currentPlayerTurn == 0)
        {
			// You win
			gameOverText.text = "You Win!";
        }
		else // an AI won the game
        {
			// You lose
			gameOverText.text = "You Lose!";
		}
	}

	public void NextPlayer()
	{
		// set the current player to the next player
		currentPlayerTurn = GetNextPlayer();

		// tell the current player that it is their turn
		players[currentPlayerTurn].StartTurn();
	}

	private int GetNextPlayer()
	{
		// calculate the next player by adding the play direction (1 or -1)
		int nextPlayer = currentPlayerTurn + playDirection;

		// if the next player has gone too low
		if(nextPlayer < 0)
		{
			//loop back to the last player
			nextPlayer = players.Count - 1;
		}
		//if the the next player has gone too hight
		else if(nextPlayer > players.Count - 1)
		{
			// loop back to the first player
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
			playDirection, // flip the playdirection arrows
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

	public void Skip() // method is called to alternate the value of skip
    {
		skip = !skip;
    }

	public bool CheckSkip()
    {
		return skip;
    }

	public void ChangeSuit(int newSuit)
    {
		suit = newSuit;
    }

	public int GetSuit()
    {
		return suit;
    }

	public int GetPlayerCount()
    {
		return players.Count;
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