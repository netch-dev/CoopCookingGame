using System;
using UnityEngine;

public class PlatesCounter : BaseCounter {

	[SerializeField] private KitchenObjectSO plateKitchenObjectSO;

	public event EventHandler OnPlateAdded;
	public event EventHandler OnPlateRemoved;
	 
	private float spawnPlateTimer;
	private float spawnPlateTimerMax = 4f;

	private int currentPlateCount = 0;
	private int maxPlateCount = 4;


	private void Update() {
		if (currentPlateCount < maxPlateCount) {
			spawnPlateTimer += Time.deltaTime;
			if (KitchenGameManager.Instance.IsGamePlaying() && spawnPlateTimer > spawnPlateTimerMax) {
				currentPlateCount++;
				OnPlateAdded?.Invoke(this, EventArgs.Empty);
				spawnPlateTimer = 0f;
			}
		}
	}

	public override void Interact(Player player) {
		if (currentPlateCount > 0 && !player.HasKitchenObject()) {
			currentPlateCount--;
			OnPlateRemoved?.Invoke(this, EventArgs.Empty);

			KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
		} 
	}
}
	
