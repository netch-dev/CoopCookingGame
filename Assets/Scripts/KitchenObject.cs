using UnityEngine;

public class KitchenObject : MonoBehaviour {
	[SerializeField] private KitchenObjectSO kitchObjectSO;
	public KitchenObjectSO GetKitchenObjectSO() {
		return kitchObjectSO;
	}
}
