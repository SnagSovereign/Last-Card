using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UserCard : CardObject, IPointerDownHandler {

	// Use this for initialization
	void Start()
	{
		DisplayCardImage();
	}

	private void DisplayCardImage()
    {
		// gets the sibling index of the card
		int siblingIndex = transform.GetSiblingIndex();
		Card cardToDisplay = transform.GetComponentInParent<User>().GetCardAtIndex(siblingIndex);
		
		GetComponent<Image>().sprite = Resources.Load<Sprite>
		(
            "Sprites/Deck/" + cardToDisplay.value + "_" + cardToDisplay.suit
        );
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Discard();
	}
}
