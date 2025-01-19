using TMPro;
using UnityEngine;

public class BPMInfo : MonoBehaviour
{
    public TMP_InputField BPM;
    public TMP_InputField Beat;
    public void Delete()
    {
        Destroy(gameObject);
    }
    public float GetBPM()
    {
        return float.Parse(BPM.text);
    }
    public int GetBeat()
    {
        return int.Parse(Beat.text);
    }
}
