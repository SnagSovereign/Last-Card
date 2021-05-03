using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : MonoBehaviour {

	protected void Discard()
    {
		Destroy(gameObject);
		print("Discard");
    }
}

public struct Card
{
	public int value, suit;

	/*
	value: 1-13 (Ace - King)

	suit: 0 = clubs
		  1 = diamonds
		  2 = hearts
		  3 = spades
	*/
}