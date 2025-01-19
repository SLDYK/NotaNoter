using UnityEngine;
using UnityEngine.UI;

public class EditorSetting : MonoBehaviour
{
    public Slider AudioSound;
    public Slider HitFXSound;
    public AudioSource AudioSource;
    public AudioManager AudioManager;
    public Timer Timer;
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
    public void EditorScale(float Value)
    {
        foreach (Transform child in Canvas.transform)
        {
            child.localScale = new Vector3(Value, Value, Value);
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
}
