using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	protected float spacing;
	protected bool myTurn;

	protected List<Card> hand = new List<Card>();

	public void AddCard(Card card)
    {
		// add card to player hand
		hand.Add(card);

		// spawn the card
		GameObject newCard = Instantiate
		(
			Resources.Load("Prefabs/" + tag + "Card"),
			new Vector3(spacing * hand.Count, 0f, 0f),
			Quaternion.identity
		) as GameObject;

		// set the parent of the card to this player object
		newCard.transform.SetParent(gameObject.transform, false);

    }

	void CentreHand()
	{

	}

	protected void DiscardCard()
    {

    }
}