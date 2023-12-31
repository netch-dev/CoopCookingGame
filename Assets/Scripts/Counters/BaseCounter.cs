using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {
	public static event EventHandler OnAnyObjectPlacedHere;
	public static void ResetStaticData() {
		OnAnyObjectPlacedHere = null;
	}

	[SerializeField] private GameObject counterTopPoint;

	private KitchenObject kitchenObject;

	public virtual void Interact(Player player) {

	}
	public virtual void InteractAlternate(Player player) {

	}
	public Transform GetKitchObjectFollowTransform() {
		return counterTopPoint.transform;
	}

	public void SetKitchenObject(KitchenObject kitchenObject) {
		this.kitchenObject = kitchenObject;

		if (kitchenObject != null) {
			OnAnyObjectPlacedHere.Invoke(this, EventArgs.Empty);
		}
	}

	public KitchenObject GetKitchenObject() {
		return kitchenObject;
	}

	public void ClearKitchenObject() {
		kitchenObject = null;
	}

	public bool HasKitchenObject() {
		return kitchenObject != null;
	}
}
