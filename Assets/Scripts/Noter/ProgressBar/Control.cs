using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    private bool onClick = false;
    public Timer Timer;
    public GameObject Point;

    public float ScrollRate;
    void OnMouseDown()
    {
        onClick = true;
        if (!Timer.Paused)
        {
            Timer.PauseTimer();
        }
    }
    private void Update()
    {
        if (onClick)
        {
            if (Input.GetMouseButtonUp(0))
            {
                onClick = false;
            }
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            //Point.transform.position = new Vector3(transform.position.x, worldPos.y, transform.position.z);
            Timer.SetTimer((worldPos.y + 5f) / 10f * Timer.Length);
        }
        if (Timer.Length != 0)
        {
            Point.transform.position = new Vector3(transform.position.x, -5 + Timer.GetElapsedTime() / Timer.Length * 10, transform.position.z);
        }
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelInput != 0f)
        {
            if (!Timer.Paused)
            {
                Timer.PauseTimer();
            }
            float elapsedY = Point.transform.position.y + scrollWheelInput * ScrollRate / 20;
            //Point.transform.position = new Vector3(transform.position.x, elapsedY, transform.position.z);
            Timer.SetTimer((elapsedY + 5f) / 10f * Timer.Length);
        }
    }
    public void SetRate(float rate)
    {
        ScrollRate = rate;
    }
}
