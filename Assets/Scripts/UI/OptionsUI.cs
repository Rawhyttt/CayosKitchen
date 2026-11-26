using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{

    public static OptionsUI Instance { get; private set; }


    [SerializeField] private Button sfxButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI sfxText;
    [SerializeField] private TextMeshProUGUI musicText;

    [SerializeField] private Transform pressToRebindKeyTransform;


    [Header("Keybinding Buttons")]

    [SerializeField] private Button upButton;
    [SerializeField] private Button downButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;

    [Header("Keybinding Text")]

    [SerializeField] private TextMeshProUGUI upText;
    [SerializeField] private TextMeshProUGUI downText;
    [SerializeField] private TextMeshProUGUI rightText;
    [SerializeField] private TextMeshProUGUI leftText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;

    private Action onCloseButtonAction;


    private void Awake()
    {
        Instance = this;
        sfxButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        closeButton.onClick.AddListener(() =>
        {
            Hide();
            onCloseButtonAction();
        });

        upButton.onClick.AddListener(() => { RebindBinding(PlayerInput.Binding.Move_Up); });
        downButton.onClick.AddListener(() => { RebindBinding(PlayerInput.Binding.Move_Down); });
        rightButton.onClick.AddListener(() => { RebindBinding(PlayerInput.Binding.Move_Right); });
        leftButton.onClick.AddListener(() => { RebindBinding(PlayerInput.Binding.Move_Left); });

        interactButton.onClick.AddListener(() => { RebindBinding(PlayerInput.Binding.Interact); });
        interactAltButton.onClick.AddListener(() => { RebindBinding(PlayerInput.Binding.InteractAlternate); });

        pauseButton.onClick.AddListener(() => { RebindBinding(PlayerInput.Binding.Pause); });
    }

    private void Start()
    {

        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;


        UpdateVisual();

        HidePressToRebindKey();
        Hide();
    }

    private void GameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        sfxText.text = "SOUND EFFECTS : " + Mathf.Round( SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "MUSIC : " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        upText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Move_Up);
        downText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Move_Down);
        rightText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Move_Left);
        leftText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Move_Right);
        interactText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Interact);
        interactAltText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.InteractAlternate);
        pauseText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Pause);
    }

    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;

        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }
    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(PlayerInput.Binding binding)
    {
        ShowPressToRebindKey();

        PlayerInput.Instance.RebindBinding(binding, () => 
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }

}
