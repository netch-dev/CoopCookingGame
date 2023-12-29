using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress {
	public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
	public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

	public class OnStateChangedEventArgs : EventArgs {
		public State state;
	}

	public enum State {
		Idle,
		Frying,
		Fried,
		Burned
	}

	[SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
	[SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

	private State state;
	private float fryingTimer;
	private FryingRecipeSO fryingRecipeSO;
	private float burningTimer;
	private BurningRecipeSO burningRecipeSO;


	private void Start() {
		state = State.Idle;
	}
	private void Update() {
		if (HasKitchenObject()) {

			switch (state) {
				case State.Idle:
				break;

				case State.Frying:
				fryingTimer += Time.deltaTime;
				if (fryingTimer >= fryingRecipeSO.fryingTimerMax) {
					// Fried
					GetKitchenObject().DestroySelf();

					// spawn the fried version
					KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

					burningRecipeSO = GeBurningRecipeSOWithInput(fryingRecipeSO.output);
					state = State.Fried;
					OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
					burningTimer = 0f;
				}
				OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
					progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax // cast to float so the result is float
				});

				break;

				case State.Fried:
				burningTimer += Time.deltaTime;

				OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
					progressNormalized = burningTimer / burningRecipeSO.burningTimerMax // cast to float so the result is float
				});

				if (burningTimer >= burningRecipeSO.burningTimerMax) {
					// Burnt
					GetKitchenObject().DestroySelf();

					// spawn the fried version
					KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

					state = State.Burned;
					OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });

					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
						progressNormalized = 0f // cast to float so the result is float
					});
				}
				
				break;

				case State.Burned:

				break;
			}
		}
	}

	public override void Interact(Player player) {
		if (!HasKitchenObject()) {
			// The counter is empty. drop players item if they have one
			if (player.HasKitchenObject()) {
				if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
					// player is carrying something that can be fried here
					player.GetKitchenObject().SetKitchenObjectParent(this);
					fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

					state = State.Frying;
					OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
					fryingTimer = 0f;
				}
			}
		} else {
			// there is a kitchen object here. give it to the player
			if (player.HasKitchenObject()) {
				// player is carrying something
				if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
					// player is holding a plate
					if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
						GetKitchenObject().DestroySelf();

						state = State.Idle;
						OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });

						OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
							progressNormalized = 0f
						});
					}
				}
			} else {
				GetKitchenObject().SetKitchenObjectParent(player);
				
				state = State.Idle;
				OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });

				OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
					progressNormalized = 0f 
				});
			}
		}
	}
	private bool HasRecipeWithInput(KitchenObjectSO input) {
		FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(input);
		return fryingRecipeSO != null;
	}
	private KitchenObjectSO GetOutputForInput(KitchenObjectSO input) {
		FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(input);
		if (fryingRecipeSO) return fryingRecipeSO.output;
		else return null;
	}


	private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO input) {
		foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray) {
			if (fryingRecipeSO.input == input) {
				return fryingRecipeSO;
			}
		}

		return null;
	}

	private BurningRecipeSO GeBurningRecipeSOWithInput(KitchenObjectSO input) {
		foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray) {
			if (burningRecipeSO.input == input) {
				return burningRecipeSO;
			}
		}

		return null;
	}
}
