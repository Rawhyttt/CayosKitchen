using System;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconSIgnleUI : MonoBehaviour
{

    [SerializeField] private Image image;

    public void SetKitchenObjectSO(KitchenObjectsSO kitchenObjectsSO) 
    {
        image.sprite = kitchenObjectsSO.sprite;
    }
}
