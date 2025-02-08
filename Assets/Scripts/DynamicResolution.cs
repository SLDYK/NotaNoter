using UnityEngine;

public class DynamicResolution : MonoBehaviour
{
    public int fixedHeight = 720; // 固定渲染高度
    private int lastWidth;
    private int lastHeight;

    void Start()
    {
        //AdjustResolution();
    }

    public void AdjustResolution()
    {
        // 计算窗口的宽高比
        float aspectRatio = (float)Screen.width / Screen.height;

        // 计算动态宽度
        int dynamicWidth = Mathf.RoundToInt(fixedHeight * aspectRatio);

        // 设置分辨率
        Screen.SetResolution(dynamicWidth, fixedHeight, false);
        Debug.Log($"Resolution:{dynamicWidth}x{fixedHeight}");
        lastWidth = Screen.width;
        lastHeight = Screen.height;
    }
}
