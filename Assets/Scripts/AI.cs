using System.Collections;
using UnityEngine;

public class AI : Player {

    protected override void CalcValidCards(int powerCard)
	{
        base.CalcValidCards(powerCard);
		
		// the only time this will be false is if the AI has already
		// picked up cards from a 2 or Black Jack
		if(myTurn)
        {
			// if the AI cannot use any cards
			if (validCards.Count == 0)
			{
				StartCoroutine(Pickup());
			}
			else // the player can use a card
			{
				StartCoroutine(UseCard());
			}
		}
		
	}

    protected override void SelectSuit()
    {
		// the AI picks a new random suit
		int newSuit = Random.Range(0, 4);

		StartCoroutine(ChangeSuit(newSuit));
    }

    IEnumerator Pickup()
	{
		yield return new WaitForSeconds(1.5f);

		AddCard(pickupDeck.PickupCard());
		EndTurn();
	}

	IEnumerator UseCard()
    {
		// chooses a random valid card
		int cardIndex = Random.Range(0, validCards.Count);
		Card card = validCards[cardIndex];

		// find the card object in the hand list that matches the chosen card
		foreach (CardObject cardObject in hand)
        {
			if(cardObject.GetCard().value == card.value
			&& cardObject.GetCard().suit == card.suit)
            {
				cardIndex = cardObject.transform.GetSiblingIndex();
				break;
            }
        }

		// wait 1.5 seconds
		yield return new WaitForSeconds(1.5f);

		// discard the card
		Discard(cardIndex);
	}

	IEnumerator ChangeSuit(int newSuit)
    {
		// wait 0.75 seconds
		yield return new WaitForSeconds(0.75f);

		// tell the LastCardManager what the new suit is
		manager.ChangeSuit(newSuit);
		// change the sprite of the discard pile
		discardPile.SuitChange(newSuit);

		EndTurn();
	}
}