using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {
	public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
	public class OnIngredientAddedEventArgs : EventArgs {
		public KitchenObjectSO kitchenObjectSO;
	}

	[SerializeField] private List<KitchenObjectSO> validKitchenSOList;
	private List<KitchenObjectSO> kitchenObjectSOList;

	private void Awake() {
		kitchenObjectSOList = new List<KitchenObjectSO>();
	}
	public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) {
		if (!validKitchenSOList.Contains(kitchenObjectSO)) {
			// Not a valid ingredient
			return false;
		}

		if (kitchenObjectSOList.Contains(kitchenObjectSO)) {
			// Already has this type of ingredient
			return false;
		} else {
			kitchenObjectSOList.Add(kitchenObjectSO);
			OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs { kitchenObjectSO = kitchenObjectSO });
			return true;
		}
	}
}
