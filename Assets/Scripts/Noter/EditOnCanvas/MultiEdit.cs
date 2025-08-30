using HSVPicker;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultiEdit : MonoBehaviour
{
    public TMP_InputField LivingTime;
    public TMP_InputField Speed;
    public Toggle UpSide;
    public Toggle isFake;
    public string _color;
    public TMP_InputField HitAlpha; // ÃÌº”HitAlpha ‰»Î◊÷∂Œ
    public ColorPicker ColorPicker;

    public NotePreset NotePreset;
    public void LoadPreset()
    {
        LivingTime.text = $"{NotePreset.Preset.livingTime}";
        Speed.text = $"{NotePreset.Preset.speed}";
        UpSide.isOn = (NotePreset.Preset.LineSide == 0) ? true : false;
        isFake.isOn = NotePreset.Preset.fake;
        ColorPicker.CurrentColor = HexToColor(NotePreset.Preset._color);
        HitAlpha.text = $"{NotePreset.Preset.HitAlpha}"; // ÃÌº”HitAlphaº”‘ÿ¬ﬂº≠
    }
    public Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        byte a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, a);
    }
    public string ColorToHex(Color32 color)
    {
        string r = color.r.ToString("X2");
        string g = color.g.ToString("X2");
        string b = color.b.ToString("X2");
        string a = color.a.ToString("X2");
        return r + g + b + a;
    }
}
