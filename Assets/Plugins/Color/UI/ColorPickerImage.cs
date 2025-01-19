using System.Collections.Generic;
using UnityEngine;
using HSVPicker;
using UnityEngine.UI;
public class ColorPickerImage : MonoBehaviour
{
    public Image _iamge;
    public ColorPicker picker;

    public Color Color = Color.red;
    public bool SetColorOnStart = false;

    // Use this for initialization
    void Start()
    {
        picker.onValueChanged.AddListener(color =>
        {
            _iamge.color = color;
            Color = color;
        });

        _iamge.color = picker.CurrentColor;
        if (SetColorOnStart)
        {
            picker.CurrentColor = Color;
        }
    }
}

