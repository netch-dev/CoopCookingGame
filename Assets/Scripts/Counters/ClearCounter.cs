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
				// player is carrying something
				if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
					// player is holding a plate
					if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
						GetKitchenObject().DestroySelf();
					}
				} else {
					// player is not holding a plate but something else
					if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
						// counter is holding a plate
						if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
							player.GetKitchenObject().DestroySelf();
						}
					}
				}
			} else {
				GetKitchenObject().SetKitchenObjectParent(player);
			}
		}
	}

}
