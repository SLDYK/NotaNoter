using UnityEngine;
using System;

public class SelectArea : MonoBehaviour
{
    public GameObject Area;
    public SpriteRenderer Renderer;

    public Vector2 Point1;
    public Vector2 Point2;
    public void StartPos()
    {
        Area.SetActive(true);
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Point1 = worldPosition;
    }
    public void EndPos()
    {
        Area.SetActive(false);
    }
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Point2 = worldPosition;
        Vector2 size = new Vector2(Mathf.Abs(Point2.x - Point1.x), Mathf.Abs(Point2.y - Point1.y));
        transform.position = new Vector3(Math.Min(Point1.x, Point2.x), Math.Min(Point1.y, Point2.y), -1);
        Renderer.size = size;
    }
}
