using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{

    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs 
    {
        public KitchenObjectsSO KitchenObjectsSO;
    }

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

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                KitchenObjectsSO = kitchenObjectsSO
            });
            return true;
        }
    }

    public List<KitchenObjectsSO> GetKitchenObjectsSOList() 
    {
        return kitchenObjectsSOList;
    }

}
