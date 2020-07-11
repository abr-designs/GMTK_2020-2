using System.Collections;
using System.Collections.Generic;
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
    private Color color;
    [SerializeField]
    private Image colorImage;

    [SerializeField]
    private TMP_InputField _inputField;

    [SerializeField]
    private TMP_Text nameText;

    [SerializeField]
    private Button readyButton;
    
    // Start is called before the first frame update
    private void Start()
    {
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
            
            SceneManager.LoadScene(1);
        });
    }


    private void UpdateColor()
    {
        colorImage.color = Color.HSVToRGB(
            hueSlider.Slider.value,
            saturationSlider.Slider.value,
            valueSlider.Slider.value);
    }
}
