using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour {
	[SerializeField] private PlatesCounter platesCounter;
	[SerializeField] private GameObject counterTopPoint;
	[SerializeField] private GameObject plateVisualGameObject;

	private List<GameObject> plateVisualGameObjectList = new List<GameObject>();

	private void Start() {
		platesCounter.OnPlateAdded += PlatesCounter_OnPlateAdded;
		platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
	}

	private void PlatesCounter_OnPlateAdded(object sender, EventArgs e) {
		GameObject newPlate = Instantiate(plateVisualGameObject, counterTopPoint.transform);
		
		float plateHeightOffsetY = 0.1f;
		newPlate.transform.localPosition = new Vector3(0f, plateHeightOffsetY * plateVisualGameObjectList.Count, 0f);
		
		plateVisualGameObjectList.Add(newPlate);
	}

	private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e) {
		GameObject topPlate = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
		plateVisualGameObjectList.Remove(topPlate);
		Destroy(topPlate);
	}

	
}
