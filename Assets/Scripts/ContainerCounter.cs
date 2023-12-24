using System;
using UnityEngine;

public class ContainerCounter : BaseCounter {
	public event EventHandler OnPlayerGrabbedObject;

	[SerializeField] private KitchenObjectSO kitchObjectSO;

	
	public override void Interact(Player player) {
		if (!player.HasKitchenObject()) {
			// player is not carrying anything
			GameObject newKitchenObject = Instantiate(kitchObjectSO.prefab);
			newKitchenObject.GetComponent<KitchenObject>().SetKitchenObjectParent(player);

			OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
		}
	}

	

}
