using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI upKeyText;
    [SerializeField] private TextMeshProUGUI downKeyText;
    [SerializeField] private TextMeshProUGUI leftKeyText;
    [SerializeField] private TextMeshProUGUI rightKeyText;
    [SerializeField] private TextMeshProUGUI interactKeyText;
    [SerializeField] private TextMeshProUGUI interactAltKeyText;
    [SerializeField] private TextMeshProUGUI pauseKeyText;

    private void Start()
    {
        PlayerInput.Instance.OnBindingRebind += PlayerInput_OnBindingRebind;
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;

        UpdateVisual();

        Show();
    }

    private void GameManager_OnStateChange(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
        
    }

    private void PlayerInput_OnBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        upKeyText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Move_Up);
        downKeyText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Move_Down);
        leftKeyText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Move_Left);
        rightKeyText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Move_Right);
        interactKeyText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Interact);
        interactAltKeyText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.InteractAlternate);
        pauseKeyText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Pause);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
