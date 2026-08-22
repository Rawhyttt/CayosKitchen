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
                if (player.GetKitchenObject() is PlateKitchenObject)
                {
					PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
					if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectsSO()))
					{
                        GetKitchenObject().DestroySelf();
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
