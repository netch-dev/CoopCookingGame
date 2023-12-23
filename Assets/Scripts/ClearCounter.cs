using UnityEngine;

public class ClearCounter : MonoBehaviour {

	[SerializeField] private KitchenObjectSO kitchObjectSO;
	[SerializeField] private GameObject counterTopPoint;

	public void Interact() {
		Debug.Log("interact");
		GameObject tomato = Instantiate(kitchObjectSO.prefab, counterTopPoint.transform);
		tomato.transform.localPosition = Vector3.zero;

		Debug.Log(tomato.GetComponent<KitchenObject>().GetKitchenObjectSO().name);
	}

}
