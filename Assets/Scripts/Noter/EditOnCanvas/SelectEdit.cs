using HSVPicker;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectEdit : MonoBehaviour
{
    public TMP_InputField LivingTime;
    public TMP_InputField Speed;
    public Toggle UpSide;
    public Toggle isFake;
    public string _color;
    public TMP_InputField Time;
    public TMP_InputField Duration;
    public TMP_InputField Lineid;
    public ColorPicker ColorPicker;
    public void LoadNote(note note)
    {
        LivingTime.text = $"{note.livingTime}";
        Speed.text = $"{note.speed}";
        UpSide.isOn = (note.LineSide == 0) ? true : false;
        isFake.isOn = note.fake;
        ColorPicker.CurrentColor = HexToColor(note._color);
        Time.text = $"{note.time}";
        Duration.text = $"{note.duration}";
        Lineid.text = $"{note.lineId}";
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
