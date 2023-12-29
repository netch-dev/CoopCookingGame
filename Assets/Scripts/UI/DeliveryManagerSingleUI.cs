using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI recipeNameText;
	[SerializeField] private Transform iconContainer;
	[SerializeField] private Transform itemTemplate;

	private void Awake() {
		itemTemplate.gameObject.SetActive(false);
	}

	public void SetRecipeSO(RecipeSO recipeSO) {
		recipeNameText.text = recipeSO.recipeName;

		foreach (Transform child in iconContainer) {
			if (child == itemTemplate) continue;
			Destroy(child.gameObject);
		}

		foreach (KitchenObjectSO kitchObjectSO in recipeSO.kitchenObjectSOList) {
			 Transform iconTransform = Instantiate(itemTemplate, iconContainer);
			iconTransform.gameObject.SetActive(true);
			iconTransform.GetComponent<Image>().sprite = kitchObjectSO.sprite;

		}
	}
   
}
