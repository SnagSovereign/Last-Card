using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : Player {

	// Use this for initialization
	void Start () 
	{
		spacing = 65f;
	}

	public void PickupButton()
    {
		if(myTurn)
        {
			AddCard(pickupDeck.PickupCard());
        }
    }
}
