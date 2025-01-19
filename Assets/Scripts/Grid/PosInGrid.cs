using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PosInGrid : MonoBehaviour
{
    public bool VerticalAdsorption = false;
    public TimeGrid TimeGrid;
    public Timer Timer;
    private float worldScreenHeight;
    private float worldScreenWidth;

    private Vector3 MouseVecNode;
    private Vector2 MouseVecDetail;

    private float GridX;
    private float GridTime;

    public Vector2 TargetPos()
    {
        worldScreenHeight = Camera.main.orthographicSize * 2f;
        worldScreenWidth = worldScreenHeight * Screen.width / Screen.height;
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 InGridPos = worldPosition - TimeGrid.NoterPos;

        float RecentPos = 0;
        float RecentDistance = 10000;
        foreach (float VerticalPos in TimeGrid.VerticalLine)
        {
            float VerticalPosFix = VerticalPos / worldScreenWidth * 2f * 3840;
            if (Mathf.Abs(InGridPos.x * 3840 / worldScreenWidth * 2 - VerticalPosFix) <= RecentDistance)
            {
                RecentDistance = Mathf.Abs(InGridPos.x * 3840 / worldScreenWidth * 2 - VerticalPosFix);
                RecentPos = VerticalPos;
            }
        }
        int index = Mathf.Clamp(TimeGrid.VerticalLine.IndexOf(RecentPos) - 2, 0, TimeGrid.ColumnList.Count - 1);
        GridX = TimeGrid.ColumnList[index];

        float RecentTime = 0;
        float RecentTimeDistance = 10000;
        foreach (BeatInfo Timeline in TimeGrid.BeatLinesSplit)
        {
            float LinePosY = (Timeline.BeatTime - Timer.GetElapsedTime()) * TimeGrid.ScrollSpeed + TimeGrid.NoterPos.y;
            if (Mathf.Abs(InGridPos.y + TimeGrid.NoterPos.y - LinePosY) <= RecentTimeDistance)
            {
                RecentTimeDistance = Mathf.Abs(InGridPos.y + TimeGrid.NoterPos.y - LinePosY);
                RecentTime = Timeline.BeatTime;
            }
        }
        GridTime = RecentTime;
        MouseVecNode.x = GridX;
        MouseVecNode.y = GridTime * 1000;
        return MouseVecNode;
    }
    public Vector2 DetailTargetPos()
    {
        worldScreenHeight = Camera.main.orthographicSize * 2f;
        worldScreenWidth = worldScreenHeight * Screen.width / Screen.height;
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 InGridPos = worldPosition - TimeGrid.NoterPos;

        float pos_1 = 0;
        float pos_2 = 0;
        float distance_1 = float.MaxValue;
        float distance_2 = float.MaxValue;
        float scaleFactor = 3840 / worldScreenWidth * 2;
        float targetPosition = InGridPos.x * scaleFactor;

        foreach (float verticalPos in TimeGrid.VerticalLine)
        {
            float verticalPosFix = verticalPos * scaleFactor;
            float distance = Mathf.Abs(targetPosition - verticalPosFix);

            if (distance < distance_1)
            {
                distance_2 = distance_1;
                pos_2 = pos_1;
                distance_1 = distance;
                pos_1 = verticalPos;
            }
            else if (distance < distance_2)
            {
                distance_2 = distance;
                pos_2 = verticalPos;
            }
        }

        int index_1 = TimeGrid.VerticalLine.IndexOf(pos_1);
        int index_2 = TimeGrid.VerticalLine.IndexOf(pos_2);

        GridX = (index_1 + index_2) / 2f - 2;

        List<float> DistanceList = new List<float> { 10000, 10000 };
        List<float> AbsDistanceList = new List<float> { 10000, 10000 };
        List<float> TimeDistance = new List<float> { 0, 0 };
        foreach (BeatInfo Timeline in TimeGrid.BeatLinesSplit)
        {
            float LinePosY = (Timeline.BeatTime - Timer.GetElapsedTime()) * TimeGrid.ScrollSpeed + TimeGrid.NoterPos.y;
            if (Mathf.Abs(InGridPos.y + TimeGrid.NoterPos.y - LinePosY) <= AbsDistanceList[0])
            {
                AbsDistanceList[0] = Mathf.Abs(InGridPos.y + TimeGrid.NoterPos.y - LinePosY);
                DistanceList[0] = InGridPos.y + TimeGrid.NoterPos.y - LinePosY;
                TimeDistance[0] = Timeline.BeatTime;
            }
            if (Mathf.Abs(InGridPos.y + TimeGrid.NoterPos.y - LinePosY) <= AbsDistanceList[1] && Mathf.Abs(InGridPos.y + TimeGrid.NoterPos.y - LinePosY) > AbsDistanceList[0])
            {
                AbsDistanceList[1] = Mathf.Abs(InGridPos.y + TimeGrid.NoterPos.y - LinePosY);
                DistanceList[1] = InGridPos.y + TimeGrid.NoterPos.y - LinePosY;
                TimeDistance[1] = Timeline.BeatTime;
            }
        }
        GridTime = TimeDistance[0] - (TimeDistance[1] - TimeDistance[0]) * DistanceList[0] / (DistanceList[1] - DistanceList[0]);
        MouseVecDetail.x = GridX;
        MouseVecDetail.y = GridTime * 1000;
        return MouseVecDetail;
    }
}