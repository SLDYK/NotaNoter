using UnityEngine;

public class DynamicResolution : MonoBehaviour
{
    public int fixedHeight = 720; // �̶���Ⱦ�߶�
    private int lastWidth;
    private int lastHeight;

    void Start()
    {
        //AdjustResolution();
    }

    public void AdjustResolution()
    {
        // ���㴰�ڵĿ�߱�
        float aspectRatio = (float)Screen.width / Screen.height;

        // ���㶯̬���
        int dynamicWidth = Mathf.RoundToInt(fixedHeight * aspectRatio);

        // ���÷ֱ���
        Screen.SetResolution(dynamicWidth, fixedHeight, false);
        Debug.Log($"Resolution:{dynamicWidth}x{fixedHeight}");
        lastWidth = Screen.width;
        lastHeight = Screen.height;
    }
}
