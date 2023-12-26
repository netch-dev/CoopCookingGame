using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
	[Serializable]
	public struct KitchenObjectSO_GameObject {
		public KitchenObjectSO kitchenObjectSO;
		public GameObject gameObject;
	}

	[SerializeField] private PlateKitchenObject plateKitchenObject;
	[SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;

	void Start() {
		plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
		foreach (KitchenObjectSO_GameObject item in kitchenObjectSOGameObjectList) {
			item.gameObject.SetActive(false);
		}
	}

	private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
		foreach (KitchenObjectSO_GameObject item in kitchenObjectSOGameObjectList) {
			if (e.kitchenObjectSO == item.kitchenObjectSO) {
				item.gameObject.SetActive(true);
			}
		}
	}

}
