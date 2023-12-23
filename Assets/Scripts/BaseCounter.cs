using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {
	[SerializeField] private GameObject counterTopPoint;

	private KitchenObject kitchenObject;

	public virtual void Interact(Player player) {

	}

	public Transform GetKitchObjectFollowTransform() {
		return counterTopPoint.transform;
	}

	public void SetKitchenObject(KitchenObject kitchenObject) {
		this.kitchenObject = kitchenObject;
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
