using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UserCard : CardObject, IPointerDownHandler {

	void Start()
	{
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
			if(playerParent.validCards.Contains(thisCard))
            {
				playerParent.Discard(transform.GetSiblingIndex());
			}
		}
	}
}
