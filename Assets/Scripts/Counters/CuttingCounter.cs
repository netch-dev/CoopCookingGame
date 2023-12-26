using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress {
	public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

	public event EventHandler OnCut;

	[SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

	private int cuttingProgress = 0;
	public override void Interact(Player player) {
		if (!HasKitchenObject()) {
			// There is no object
			if (player.HasKitchenObject()) {
				if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
					// player is carrying something that can be cut here
					player.GetKitchenObject().SetKitchenObjectParent(this);
					cuttingProgress = 0;

					CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { 
						progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax // cast to float so the result is float
					});
				}
			}
		} else {
			// there is a kitchen object here
			if (player.HasKitchenObject()) {

			} else {
				GetKitchenObject().SetKitchenObjectParent(player);
			}
		}

	}

	public override void InteractAlternate(Player player) {
		if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())) {
			// There is an object, cut it
			cuttingProgress++;

			OnCut?.Invoke(this, EventArgs.Empty);

			CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
			OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
				progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
			});

			if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax) {
				KitchenObjectSO cutVersion = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
				GetKitchenObject().DestroySelf();

				// spawn the cut version
				KitchenObject.SpawnKitchenObject(cutVersion, this);
			}
			
		}
	}
	private bool HasRecipeWithInput(KitchenObjectSO input) {
		KitchenObjectSO outputSO = GetOutputForInput(input);
		return outputSO != null;
	}
	private KitchenObjectSO GetOutputForInput(KitchenObjectSO input) {
		CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(input);
		if (cuttingRecipeSO) return cuttingRecipeSO.output;
		else return null;
	}


	private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO input) {
		foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
			if (cuttingRecipeSO.input == input) {
				return cuttingRecipeSO;
			}
		}

		return null;
	}
}
