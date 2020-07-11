using System;
using TMPro;
using UnityEngine.UI;

[Serializable]
public struct SliderText
{
    public Slider Slider;

    public TMP_Text TmpText;

    public string format;
    
    public void Init()
    {
        var _format = format;
        var text = TmpText;
        
        text.text = string.Format(_format, Slider.value);
        
        Slider.onValueChanged.AddListener(value =>
        {
            text.text = string.Format(_format, value);
        });
    }
}
