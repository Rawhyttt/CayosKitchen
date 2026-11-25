using UnityEngine;

public class ClearCounter : BaseCounter {

    public override void Interact(Player player){
		if(!HasKitchenObject()){
			//There is nothing on the counter.
			if(player.HasKitchenObject()){
				//Player has something.
				player.GetKitchenObject().SetKitchenObjectParent(this);
			}else{
				//Player has nothing.
			}
		}else{
			//There is something on the counetr.
			if(player.HasKitchenObject()){
				//player has somthing.
				if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
				{
					if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectsSO()))
					{
						GetKitchenObject().DestroySelf();
					}
				}
				else 
				{
					//Player has something else than a plate.
					if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) 
					{
						//counter has a plate.
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectsSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else{
				//Player has nothing.
				GetKitchenObject().SetKitchenObjectParent(player);
			}
		}		
    }
}
