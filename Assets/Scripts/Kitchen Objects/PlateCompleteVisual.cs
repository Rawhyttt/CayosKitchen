using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject 
    {
        public KitchenObjectsSO KitchenObjectsSO;
        public GameObject GameObject;
    }


    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectsList;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectsList)
        { 
            kitchenObjectSOGameObject.GameObject.SetActive(false);        
        }

    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectsList)
        {
            if (kitchenObjectSOGameObject.KitchenObjectsSO == e.KitchenObjectsSO)
            {
                kitchenObjectSOGameObject.GameObject.SetActive(true);
            }
        }
    }
}
