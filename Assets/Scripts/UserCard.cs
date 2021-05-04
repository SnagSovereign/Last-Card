using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UserCard : CardObject, IPointerDownHandler {

	[HideInInspector] public Player playerParent;

	void Start()
	{
		playerParent = transform.parent.GetComponent<Player>();
		DisplayCardImage();
	}

	private void DisplayCardImage()
    {	
		GetComponent<Image>().sprite = Resources.Load<Sprite>
		(
            "Sprites/Deck/" + thisCard.value + "_" + thisCard.suit
        );
	}

	// when the user clicks on one of their cards
	public void OnPointerDown(PointerEventData eventData)
	{
		// check if it is the user's turn
		if(GetComponentInParent<User>().myTurn)
        {
			if(playerParent.ValidCards().Contains(thisCard))
            {
				Discard();
			}
		}
	}
}
