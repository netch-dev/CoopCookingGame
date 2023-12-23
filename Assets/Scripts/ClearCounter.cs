using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent {

	[SerializeField] private KitchenObjectSO kitchObjectSO;
	[SerializeField] private GameObject counterTopPoint;
	[SerializeField] private ClearCounter secondClearCounter;

	private KitchenObject kitchenObject;

	public void Interact(Player player) {
		if (kitchenObject == null) {
			GameObject newKitchenObject = Instantiate(kitchObjectSO.prefab, counterTopPoint.transform);
			newKitchenObject.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
		} else {
			// give to the player
			kitchenObject.SetKitchenObjectParent(player);
		}
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
