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
        });
    }

    private void Start()
    {

        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;


        UpdateVisual();


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
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
