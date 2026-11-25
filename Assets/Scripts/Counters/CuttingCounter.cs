using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress {

	public static event EventHandler OnAnyCut;
	
	public event EventHandler OnCuttingAction;
	
	public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
	
	[SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
	
	private int cuttingProgress;
	
	public override void Interact(Player player){
		if(!HasKitchenObject()){
			//There is nothing on the counter.
			if(player.HasKitchenObject()){
				//Player has something.
				if(IsCuttableKitchenObject(player.GetKitchenObject().GetKitchenObjectsSO())){
					player.GetKitchenObject().SetKitchenObjectParent(this);
					cuttingProgress = 0;
					
					CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectsSO());
					
					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
						progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
					});
				}
			}else{
				//Player has nothing.
			}
		}else{
			//There is something on the counetr.
			if(player.HasKitchenObject()){
                //Player also has something.
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectsSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
            else{
				//Player has nothing.
				GetKitchenObject().SetKitchenObjectParent(player);
				OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
					progressNormalized = 0f
				});
			}
		}
	}
	
	public override void InteractAlternate(Player player){
		if(HasKitchenObject()){
			if(IsCuttableKitchenObject(GetKitchenObject().GetKitchenObjectsSO())){
				OnCuttingAction?.Invoke(this, EventArgs.Empty);
				OnAnyCut?.Invoke(this, EventArgs.Empty);

                cuttingProgress++;
				CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectsSO());
				
				OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
					progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
				});				
				
				if(cuttingProgress >= cuttingRecipeSO.cuttingProgressMax){
					
					KitchenObjectsSO output = GetOutputForInput(GetKitchenObject().GetKitchenObjectsSO());
					GetKitchenObject().DestroySelf();
					KitchenObject.SpawnKitchenObject(output, this);
				}
			}
		}	
	}
	
	private KitchenObjectsSO GetOutputForInput(KitchenObjectsSO inputKitchenObjectsSO){
		CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectsSO);
		if(cuttingRecipeSO != null){
			return cuttingRecipeSO.output;
		}else {
			return null;
		}
	}
	
	private bool IsCuttableKitchenObject(KitchenObjectsSO inputKitchenObjectsSO){
		CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectsSO);
		if(cuttingRecipeSO != null){
			return true;
		}else{
			return false;
		}
	}
	
	private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectsSO){
		foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray){
			if(cuttingRecipeSO.input == inputKitchenObjectsSO){
				return cuttingRecipeSO;
			}
		}
		return null;
	}
}
