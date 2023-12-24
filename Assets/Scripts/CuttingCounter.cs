using UnityEngine;

public class CuttingCounter : BaseCounter {
	[SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
	public override void Interact(Player player) {
		if (!HasKitchenObject()) {
			// There is no object
			if (player.HasKitchenObject()) {

				if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
					// player is carrying something that can be cut here
					player.GetKitchenObject().SetKitchenObjectParent(this);
				}
			}
		} else {
			// there is a kitchen object here
			if (player.HasKitchenObject()) {

			} else {
				GetKitchenObject().SetKitchenObjectParent(player);
			}
		}

	}

	public override void InteractAlternate(Player player) {
		if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())) {
			// There is an object, cut it

			KitchenObjectSO cutVersion = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
			GetKitchenObject().DestroySelf();

			// spawn the cut version
			KitchenObject.SpawnKitchenObject(cutVersion, this);
		}
	}
	private bool HasRecipeWithInput(KitchenObjectSO input) {
		foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {

			if (cuttingRecipeSO.input == input) {

				return true;
			}
		}

		return false;
	}
	private KitchenObjectSO GetOutputForInput(KitchenObjectSO input) {
		foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
			if (cuttingRecipeSO.input == input) {
				return cuttingRecipeSO.output;
			}
		}

		return null;
	}
}
