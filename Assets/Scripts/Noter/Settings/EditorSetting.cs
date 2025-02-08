using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class EditorSetting : MonoBehaviour
{
    public Slider AudioSound;
    public Slider HitFXSound;
    public AudioSource AudioSource;
    public AudioManager AudioManager;
    public Timer Timer;
    public DynamicResolution DynamicResolution;
    public bool SetScale = false;
    public void Shifting(int Value)
    {
        Timer.PauseTimer();
        switch (Value)
        {
            case 0:
                Timer.TimerShifting = 1f;
                break;
            case 1:
                Timer.TimerShifting = 0.75f;
                break;
            case 2:
                Timer.TimerShifting = 0.5f;
                break;
        }
    }
    public void Sound()
    {
        AudioSource.volume = AudioSound.value;
        AudioManager.SoundLevel = HitFXSound.value;
    }
    public GameObject Canvas;
    public void EditorScale()
    {
        SetScale = !SetScale;
    }
    private void Update()
    {
        if (SetScale)
        {
            float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
            float Value = transform.localScale.x;
            foreach (Transform child in Canvas.transform)
            {
                child.localScale = new Vector3(Value + scrollWheelInput, Value + scrollWheelInput, 1);
            }
        }
    }
    public GameObject IngameConsole;
    public void SetConsole(bool Active)
    {
        IngameConsole.SetActive(Active);
    }
    public Camera Camera;
    public void SetLight(float Value)
    {
        Camera.backgroundColor = new Color(Value, Value, Value);
    }
    public void SetResolution(int Value)
    {
        switch (Value)
        {
            case 0:
                DynamicResolution.fixedHeight = 720;
                break;
            case 1:
                DynamicResolution.fixedHeight = 1080;
                break;
            case 2:
                DynamicResolution.fixedHeight = 480;
                break;
        }
        DynamicResolution.AdjustResolution();
    }
}
