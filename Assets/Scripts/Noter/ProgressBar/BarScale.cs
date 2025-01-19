using UnityEngine;
public class BarScale : MonoBehaviour
{
    public SpriteRenderer BarSprite;
    private float BarHeight;
    private float BarWidth;
    void Start()
    {
        BarHeight = BarSprite.bounds.size.y;
    }
    private void Update()
    {
        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight * Screen.width / Screen.height;
        float LeftEdgeX = -worldScreenWidth / 2f;
        float scaleRatioHeight = worldScreenHeight / BarHeight;
        Vector3 Position = new Vector3(LeftEdgeX * scaleRatioHeight, 0, 0);
        transform.position = Position;
        transform.localScale = new Vector3(scaleRatioHeight, scaleRatioHeight, 1);
    }
}
