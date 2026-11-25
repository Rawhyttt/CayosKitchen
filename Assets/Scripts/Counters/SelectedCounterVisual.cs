using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour{

    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;

    private void Start(){
        Player.Instance.OnSelectedCounterVisualChange += Player_OnSelectedCounterVisualChange;
    }

    private void Player_OnSelectedCounterVisualChange(object sender, Player.OnSelectedCounterVisualChangeEventAgrs e){
        if (e.selectedCounter == baseCounter){
            Show();
        }
        else{
            Hide();
        }
    }

    private void Show(){
		foreach(GameObject visualGameObject in visualGameObjectArray){
			visualGameObject.SetActive(true);
		}
    }

    private void Hide(){
		foreach(GameObject visualGameObject in visualGameObjectArray){
			visualGameObject.SetActive(false);
		}
    }

}
