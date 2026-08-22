using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectsSO> validKitchenObjectsSO;
    private List<KitchenObjectsSO> kitchenObjectsSOList;

    private void Awake()
    {
        kitchenObjectsSOList = new List<KitchenObjectsSO>();
    }
    public bool TryAddIngredient(KitchenObjectsSO kitchenObjectsSO) 
    {
        if (!validKitchenObjectsSO.Contains(kitchenObjectsSO))
        {
            return false;
        }
        if (kitchenObjectsSOList.Contains(kitchenObjectsSO))
        {
            return false;
        }
        else 
        {
            kitchenObjectsSOList.Add(kitchenObjectsSO);
            return true;
        }
    }

}
