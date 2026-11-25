using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour{
    
	[SerializeField] private PlateCounter plateCounter;
	[SerializeField] private Transform counterTopPoint;
	[SerializeField] private Transform plateVisualGameObject;
	
	private List<GameObject> plateVisualGameObjectList;
	
	private void Awake(){
		plateVisualGameObjectList = new List<GameObject>();
	}

	private void Start(){
		plateCounter.OnPlateSpawned += PlateCounter_OnPlateSpawned;
		plateCounter.OnPlatePicked += PlateCounter_OnPlatePicked;
	}
	
	private void PlateCounter_OnPlatePicked(object sender, System.EventArgs e){
		GameObject lastSpawnedPlateVisual = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
		plateVisualGameObjectList.Remove(lastSpawnedPlateVisual);
		Destroy(lastSpawnedPlateVisual);
	}
	
	private void PlateCounter_OnPlateSpawned(object sender, System.EventArgs e){
		Transform spawnedPlateVisualTransform = Instantiate(plateVisualGameObject, counterTopPoint);
		
		float plateVisualOffset = 0.1f;
		spawnedPlateVisualTransform.localPosition = new Vector3(0, plateVisualOffset * plateVisualGameObjectList.Count, 0);
		
		plateVisualGameObjectList.Add(spawnedPlateVisualTransform.gameObject);
	}
	
}
