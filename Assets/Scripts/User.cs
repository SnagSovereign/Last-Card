using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : Player {

	// Use this for initialization
	void Start () 
	{
		spacing = 65f;
	}

	public Card GetCardAtIndex(int cardIndex)
    {
		return hand[cardIndex];
    }
	
}
