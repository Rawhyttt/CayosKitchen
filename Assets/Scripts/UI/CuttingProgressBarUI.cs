using UnityEngine;
using UnityEngine.UI;

public class CuttingProgressBarUI : MonoBehaviour{
    
	[SerializeField] private GameObject hasProgressGameObject;
	[SerializeField] private Image progressBarVisual;
	
	private IHasProgress hasProgress;
	
	private void Start(){
		hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
		if(hasProgress == null){
			Debug.LogError("The gameObject " + hasProgressGameObject + "dosen't have the component IHasProgress !");
		}
		hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
		
		progressBarVisual.fillAmount = 0f;
		
		Hide();
	}
	
	private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e){
		progressBarVisual.fillAmount = e.progressNormalized;	
		if(e.progressNormalized == 0f || e.progressNormalized == 1f){
			Hide();
		}else{
			Show();
		}
	}
	
	private void Show(){
		gameObject.SetActive(true);
	}
	
	private void Hide(){
		gameObject.SetActive(false);
	}
}
