using System;
using UnityEngine;

public class ContainerCounter : BaseCounter {
	public event EventHandler OnPlayerGrabbedObject;

	[SerializeField] private KitchenObjectSO kitchObjectSO;

	
	public override void Interact(Player player) {
		if (!player.HasKitchenObject()) {
			KitchenObject.SpawnKitchenObject(kitchObjectSO, player);

			OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
		}
	}

	

}
