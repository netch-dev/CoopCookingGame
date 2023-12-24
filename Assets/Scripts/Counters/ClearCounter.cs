using UnityEngine;

public class ClearCounter : BaseCounter {

	[SerializeField] private KitchenObjectSO kitchObjectSO;

	public override void Interact(Player player) {
		if (!HasKitchenObject()) {
			// There is no object
			if (player.HasKitchenObject()) {
				player.GetKitchenObject().SetKitchenObjectParent(this);
			}
		} else {
			// there is a kitchen object here
			if (player.HasKitchenObject()) {

			} else {
				GetKitchenObject().SetKitchenObjectParent(player);
			}
		}
	}

}
