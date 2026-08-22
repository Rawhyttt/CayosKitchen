using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour {
	
	private const string CUT = "Cut";
    
	[SerializeField] private CuttingCounter cuttingCounter;
	
	private Animator animator;
	
	private void Awake(){
		animator = GetComponent<Animator>();
	}
	
	private void Start(){
		cuttingCounter.OnCuttingAction += CuttingCounter_OnCuttingAction; 
	}
	
	private void CuttingCounter_OnCuttingAction(object sender, System.EventArgs e){
		animator.SetTrigger(CUT);
	}
}
