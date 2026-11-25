using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress {
	
	public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
	
	public event EventHandler<OnStateChangeEventArgs> OnStateChange;
	public class OnStateChangeEventArgs : EventArgs {
		public State state;
	}
	
	[SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
	[SerializeField] private BurnedRecipeSO[] burnedRecipeSOArray;
	
	public enum State{
		Idle,
		Frying,
		Fried,
		Burned,
	}
	
	private State state;
	private float fryingTimer;
	private FryingRecipeSO fryingRecipeSO;
	private float burningTimer;
	private BurnedRecipeSO burnedRecipeSO;
	
	private void Start(){
		state = State.Idle;
	}
	
	private void Update(){
		if(HasKitchenObject()){
			switch(state){
				case State.Idle:
					break;
				case State.Frying:
					fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectsSO());
					
					fryingTimer += Time.deltaTime;
					
					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
						progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
					});
					
					if(fryingTimer > fryingRecipeSO.fryingTimerMax){
						GetKitchenObject().DestroySelf();
						
						KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
						
						state = State.Fried;
						
						burnedRecipeSO = GetBurnedRecipeSOWithInput(GetKitchenObject().GetKitchenObjectsSO());
						burningTimer = 0f;
						
						OnStateChange?.Invoke(this, new OnStateChangeEventArgs{
							state = state
						});
					}
					break;
				case State.Fried:
					burningTimer += Time.deltaTime;
					
					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
						progressNormalized = burningTimer / burnedRecipeSO.burningTimerMax
					});
					
					if(burningTimer > burnedRecipeSO.burningTimerMax){
						GetKitchenObject().DestroySelf();
						
						KitchenObject.SpawnKitchenObject(burnedRecipeSO.output, this);
						state = State.Burned;
						
						OnStateChange?.Invoke(this, new OnStateChangeEventArgs{
							state = state
						});
						
						OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
							progressNormalized = 0f
						});
					}
					break;
				case State.Burned:
					break;
			}
		}
		
	}
	
    public override void Interact(Player player){
		if(!HasKitchenObject()){
			//There is nothing on the counter.
			if(player.HasKitchenObject()){
                //Player has something.        
                if (IsFryableKitchenObject(player.GetKitchenObject().GetKitchenObjectsSO())){
					player.GetKitchenObject().SetKitchenObjectParent(this);
					fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectsSO());
					
					fryingTimer = 0f;
					state = State.Frying;
					
					OnStateChange?.Invoke(this, new OnStateChangeEventArgs{
						state = state
					});
					
					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
						progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
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

                        state = State.Idle;

                        OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                        {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                }
            }
            else{
				//Player has nothing.
				GetKitchenObject().SetKitchenObjectParent(player);
				state = State.Idle;
				
				OnStateChange?.Invoke(this, new OnStateChangeEventArgs{
					state = state
				});
				
				OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
					progressNormalized = 0f
				});
			}
		}		
    }
	
	private KitchenObjectsSO GetOutputForInput(KitchenObjectsSO inputKitchenObjectsSO){
		FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectsSO);
		if(fryingRecipeSO != null){
			return fryingRecipeSO.output;
		}else {
			return null;
		}
	}
	
	private bool IsFryableKitchenObject(KitchenObjectsSO inputKitchenObjectsSO){
		FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectsSO);
		if(fryingRecipeSO != null){
			return true;
		}else{
			return false;
		}
	}
	
	private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectsSO){
		foreach(FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray){
			if(fryingRecipeSO.input == inputKitchenObjectsSO){
				return fryingRecipeSO;
			}
		}
		return null;
	}
	
	private BurnedRecipeSO GetBurnedRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectsSO){
		foreach(BurnedRecipeSO burnedRecipeSO in burnedRecipeSOArray){
			if(burnedRecipeSO.input == inputKitchenObjectsSO){
				return burnedRecipeSO;
			}
		}
		return null;
	}
}
