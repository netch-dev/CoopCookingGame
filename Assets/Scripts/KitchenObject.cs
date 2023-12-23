using UnityEngine;

public class KitchenObject : MonoBehaviour {
	[SerializeField] private KitchenObjectSO kitchObjectSO;

	private IKitchenObjectParent kitchenObjectParent;
	public KitchenObjectSO GetKitchenObjectSO() {
		return kitchObjectSO;
	}

	public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {
		if (this.kitchenObjectParent != null) {
			this.kitchenObjectParent.ClearKitchenObject();
		}

		this.kitchenObjectParent = kitchenObjectParent;

		if (kitchenObjectParent.HasKitchenObject()) {
			Debug.LogError("IKitchenObjectParent already has a kitchen object");
			return;
		}
		kitchenObjectParent.SetKitchenObject(this);

		transform.parent = kitchenObjectParent.GetKitchObjectFollowTransform();
		transform.localPosition = Vector3.zero;
;	}

	public IKitchenObjectParent GetKitchObjectParent() {
		return kitchenObjectParent;
	}
}
