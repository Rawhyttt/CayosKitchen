using System;
using UnityEngine;

public class PlateCounter : BaseCounter{
	
	public event EventHandler OnPlateSpawned;
	public event EventHandler OnPlatePicked;

	[SerializeField] private KitchenObjectsSO plateKitchenObjectSO;
	
    private float plateSpawnTimer;
	private float plateSpawnTimerMax = 4f;
	private int platesSpawnedCount;
	private int platesSpawnedCountMax = 4;

	private void Update(){
		plateSpawnTimer += Time.deltaTime;
		if(plateSpawnTimer > plateSpawnTimerMax){
			plateSpawnTimer = 0f;
			
			if(GameManager.Instance.IsGamePlaying() && platesSpawnedCount < platesSpawnedCountMax){
				platesSpawnedCount++;
				OnPlateSpawned?.Invoke(this, EventArgs.Empty);
			}	
		}
	}
	
	public override void Interact(Player player){
		if(!player.HasKitchenObject()){
			if(platesSpawnedCount > 0){
				KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
				platesSpawnedCount--;
				
				OnPlatePicked?.Invoke(this, EventArgs.Empty);
			}
		}
	}

}
