using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HSVPicker;
using UnityEngine.UI;
public class ColorPickerSliderAlpha : MonoBehaviour
{
    public ColorPicker picker;

    private Slider _Slider;

    private void OnEnable()
    {
        _Slider = GetComponent<Slider>();
        picker.onValueChanged.AddListener(ColorChanged);
    }

    private void OnDestroy()
    {
        picker.onValueChanged.RemoveListener(ColorChanged);
    }

    private void ColorChanged(Color newColor)
    {
        _Slider.value = newColor.a;
    }
}
