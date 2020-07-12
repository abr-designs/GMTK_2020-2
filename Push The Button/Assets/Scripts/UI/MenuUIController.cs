using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour
{
    [SerializeField]
    private SliderText ageSlider;
    [SerializeField]
    private SliderText hueSlider;
    [SerializeField]
    private SliderText saturationSlider;
    [SerializeField]
    private SliderText valueSlider;

    [SerializeField]
    private Image colorImage;

    [SerializeField]
    private TMP_InputField _inputField;

    [SerializeField]
    private TMP_Text nameText;

    [SerializeField]
    private Button readyButton;

    [SerializeField]
    private SimpleGrabber _simpleGrabber;

    [SerializeField]
    private Toggle audioToggle;

    private AudioController _audioController;
    
    // Start is called before the first frame update
    private void Start()
    {
        _audioController = FindObjectOfType<AudioController>();
        
        ageSlider.Init();
        hueSlider.Init();

        nameText.text = string.Empty;
        
        hueSlider.Slider.onValueChanged.AddListener((value) =>
        {
            UpdateColor();
        });
        saturationSlider.Slider.onValueChanged.AddListener((value) =>
        {
            UpdateColor();
        });
        valueSlider.Slider.onValueChanged.AddListener((value) =>
        {
            UpdateColor();
        });
        
        _inputField.onValueChanged.AddListener(value =>
        {
            nameText.text = value;
        });
        
        hueSlider.Slider.value = Random.value;
        saturationSlider.Slider.value = 0.5f;
        valueSlider.Slider.value = 1f;
        
        readyButton.onClick.AddListener(() =>
        {
            Values.name = _inputField.text;
            Values.age = (int)ageSlider.Slider.value;
            Values.color = colorImage.color;
            
            _audioController?.PlaySoundEffect(SOUND.BUTTON, 0.5f);
            
            SceneManager.LoadScene(1);
        });
        
        audioToggle.onValueChanged.AddListener(value =>
        {
            _audioController.SetMasterVolume(!value ? 0f: -80f);
            _audioController?.PlaySoundEffect(SOUND.BUTTON, 0.2f);
        });
        
        _audioController.SetMusic(MUSIC.MENU);
        ageSlider.Slider.value = Random.Range(0, 101);
    }


    private void UpdateColor()
    {
        var color = Color.HSVToRGB(
            hueSlider.Slider.value,
            saturationSlider.Slider.value,
            valueSlider.Slider.value);
        
        colorImage.color = color;

        _simpleGrabber.SetColor(color);
    }
}
