public class CuttingCounter : BaseCounter
{
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

	public override void InteractAlternate(Player player) {
		if (HasKitchenObject()) {
			// There is an object, cut it

			// destroy object that's on it
			GetKitchenObject().DestroySelf();

			// spawn the cut version

		}
	}
}
