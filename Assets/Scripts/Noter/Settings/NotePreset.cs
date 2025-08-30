using HSVPicker;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PresetNote
{
    public int speed = 10;
    public int livingTime = 2000;
    public int LineSide = 0;
    public bool fake = false;
    public string _color = "000000FF";
    public int HitAlpha = 255;

    public PresetNote DeepCopy()
    {
        PresetNote clone = new PresetNote();
        clone.speed = this.speed;
        clone.livingTime = this.livingTime;
        clone.LineSide = this.LineSide;
        clone.fake = this.fake;
        clone._color = this._color != null ? String.Copy(this._color) : null;
        clone.HitAlpha = this.HitAlpha;
        return clone;
    }
}

public class NotePreset : MonoBehaviour
{   
    public PresetNote Preset = new();

    public TMP_InputField LivingTime;
    public TMP_InputField Speed;
    public Toggle UpSide;
    public Toggle isFake;
    public string _color;
    public TMP_InputField HitAlpha; // 需要添加这个UI元素引用

    public ColorPicker ColorPicker;
    
    public void LoadPreset()
    {
        LivingTime.text = $"{Preset.livingTime}";
        Speed.text = $"{Preset.speed}";
        UpSide.isOn = (Preset.LineSide == 0) ? true : false;
        isFake.isOn = Preset.fake;
        ColorPicker.CurrentColor = HexToColor(Preset._color);
        HitAlpha.text = $"{Preset.HitAlpha}"; // 添加这行
    }
    
    public void SavePreset()
    {
        Preset.livingTime = int.Parse(LivingTime.text);
        Preset.speed = int.Parse(Speed.text);
        Preset.LineSide = UpSide.isOn ? 0 : 1;
        Preset.fake = isFake.isOn;
        Preset._color = ColorToHex(ColorPicker.CurrentColor);
        Preset.HitAlpha = int.Parse(HitAlpha.text); // 添加这行
    }
    
    Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        byte a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, a);
    }
    string ColorToHex(Color32 color)
    {
        string r = color.r.ToString("X2");
        string g = color.g.ToString("X2");
        string b = color.b.ToString("X2");
        string a = color.a.ToString("X2");
        return r + g + b + a;
    }
    public PresetNote GetPresetNote()
    {
        return Preset.DeepCopy();
    }
}
