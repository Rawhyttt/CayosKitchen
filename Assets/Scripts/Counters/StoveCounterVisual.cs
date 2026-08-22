using UnityEngine;

public class StoveCounterVisual : MonoBehaviour{
	
	[SerializeField] private StoveCounter stoveCounter;
	[SerializeField] private GameObject particlesGameObject;
	[SerializeField] private GameObject stoveOnGameObject;
	
	private void Start(){
		stoveCounter.OnStateChange += StoveCounter_OnStateChange;
	}
    
	private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeEventArgs e){
		bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
		particlesGameObject.SetActive(showVisual);
		stoveOnGameObject.SetActive(showVisual);
	}
}

