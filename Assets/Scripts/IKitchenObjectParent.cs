using UnityEngine;

public interface IKitchenObjectParent {
	
	public void SetKitchenObject(KitchenObject kitchenObject);

	public KitchenObject GetKitchenObject();
	
	public Transform GetKitchObjectFollowTransform();

	public void ClearKitchenObject();

	public bool HasKitchenObject();
}