using UnityEngine;

public class User : Player {

    [SerializeField] GameObject suitSelectPanel;

	public void PickupButton()
    {
		if(myTurn)
        {
			AddCard(pickupDeck.PickupCard());
			EndTurn();
        }
    }

    protected override void SelectSuit()
    {
        suitSelectPanel.SetActive(true);
    }

    public void SuitButton(int suit)
    {
        manager.ChangeSuit(suit);
        discardPile.SuitChange(suit);
        suitSelectPanel.SetActive(false);
        EndTurn();
    }
}