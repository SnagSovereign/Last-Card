using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LastCardManager : MonoBehaviour {

	[SerializeField] List<Player> players;
	[SerializeField] PickupDeck pickupDeck;
	[SerializeField] GameObject playDirectionArrows;
	[SerializeField] GameObject pausePanel;
	[SerializeField] GameObject gameOverPanel;
	[SerializeField] TMP_Text gameOverText;

	int playDirection = 1;
	int currentPlayerTurn = 0;

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
		ConfigurePlayers();
		pickupDeck.GenerateDeck();
		pickupDeck.ShuffleDeck();
		pickupDeck.DealCards(players);

		// tell the first player that it is there turn
		players[currentPlayerTurn].StartTurn();
	}

	private void ConfigurePlayers()
    {
		if(PlayerPrefs.GetInt("players") == 2)
        {
            Destroy(players[1].gameObject);
            players.RemoveAt(1);

            Destroy(players[2].gameObject);
            players.RemoveAt(2);
        }
		else if(PlayerPrefs.GetInt("players") == 3)
        {
			Destroy(players[2].gameObject);
			players.RemoveAt(2);
		}
	}

	public void GameOver()
    {
		gameOverPanel.SetActive(true);
		if(currentPlayerTurn == 0)
        {
			// You win
			gameOverText.text = "You Win!";
        }
		else
        {
			// You lose
			gameOverText.text = "You Lose!";
		}
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
			nextPlayer = players.Count - 1;
		}
		else if(nextPlayer > players.Count - 1)
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