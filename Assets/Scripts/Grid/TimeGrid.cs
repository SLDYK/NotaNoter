using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class BeatInfo
{
    public float Beat;
    public float BeatTime;
    public int BeatNum;
    public bool IntBeat;
}
[System.Serializable]
public class BPMdata
{
    public float value;
    public float beat;
}
public class TimeGrid : MonoBehaviour
{
    //用到的组件
    public Timer Timer;
    public LineRenderer lineRenderer;
    public ChartLoader ChartLoader;
    //网格设置
    public int BeatSplit;
    public float ScrollSpeed;
    //坐标计算
    public Vector3 NoterPos;
    private float worldScreenHeight;
    private float worldScreenWidth;
    //列表
    public List<BeatInfo> BeatLine;
    public List<BeatInfo> BeatLinesSplit;
    public List<float> VerticalLine;
    public List<BPMdata> BPMList = new();
    public List<int> ColumnList;
    private Chart Chart;

    //本质是开始函数，但由ChartLoader调用
    public void SetGrid()
    {
        Chart = ChartLoader.GetChart();
        freshBeatline();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material.color = Color.white;
    }
    public void freshBeatline()
    {
        BPMList.Clear();
        BPMList.Add(new BPMdata { beat = 0, value = Chart.bpm });
        BPMList.Add(new BPMdata { beat = 2000, value = Chart.bpm });
        BeatLine = SetBeatLine(BPMList);
    }
    public List<BeatInfo> SetBeatLine(List<BPMdata> BPM)
    {
        List<BeatInfo> BeatLineList = new List<BeatInfo> { };
        for (int beat = 0; beat < 2000; beat++)
        {
            float time = 0;
            for (int i = 0; i < BPM.Count - 1; i++)
            {
                if (beat >= BPM[i].beat && beat >= BPM[i + 1].beat)
                {
                    time += (BPM[i + 1].beat - BPM[i].beat) / BPM[i].value * 60;
                }
                if (beat >= BPM[i].beat && beat < BPM[i + 1].beat)
                {
                    time += (beat - BPM[i].beat) / BPM[i].value * 60;
                }
            }
            BeatInfo Abeat = new BeatInfo();
            Abeat.Beat = beat;
            Abeat.BeatNum = beat;
            Abeat.BeatTime = time;
            Abeat.IntBeat = true;
            BeatLineList.Add(Abeat);
        }
        return BeatLineList;
    }
    private void Update()
    {
        worldScreenHeight = Camera.main.orthographicSize * 2f;
        worldScreenWidth = worldScreenHeight * Screen.width / Screen.height;
        NoterPos = new Vector3(-worldScreenWidth / 5, -worldScreenHeight / 5 * 2, transform.parent.position.z);
        transform.parent.position = NoterPos;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, LinePos(new Vector3(-worldScreenWidth / 4, 0, 0)));
        lineRenderer.SetPosition(1, LinePos(new Vector3(worldScreenWidth / 4, 0, 0)));

        float elapsedTime = Timer.GetElapsedTime();
        BeatLinesSplit = BeatLineSplit(elapsedTime);
        VerticalLine = VerticalSplit();
        DrawLine(BeatLinesSplit, VerticalLine);
    }

    private List<float> VerticalSplit()
    {
        List<float> VerticalSplit = new List<float>();
        int fVerticalNum = ColumnList.Count + 1;
        VerticalSplit.Add(-worldScreenWidth / 4);
        for (int i = 0; i < fVerticalNum; i++)
        {
            VerticalSplit.Add(-worldScreenWidth / 4 + worldScreenWidth / 2 * i / fVerticalNum);
        }
        VerticalSplit.Add(worldScreenWidth / 4);
        return VerticalSplit;
    }

    public Vector3 LinePos(Vector3 Point)
    {
        return NoterPos + Point;
    }
    public List<BeatInfo> BeatLineSplit(float elapsedTime)
    {
        int SplitNum = BeatSplit - 1;
        List<BeatInfo> BeatList = new List<BeatInfo>();
        foreach (BeatInfo beatinfo in BeatLine)
        {
            if (beatinfo.BeatTime > elapsedTime - 0.5 && beatinfo.BeatTime < elapsedTime + 3)
            {
                BeatList.Add(beatinfo);
            }
        }
        int frameBeatNum = BeatList.Count - 1;
        for (int i = 0; i < frameBeatNum; i++)
        {
            for (int j = 1; j <= SplitNum; j++)
            {
                BeatInfo beatInfo = new BeatInfo();
                beatInfo.IntBeat = false;
                beatInfo.BeatTime = BeatList[i].BeatTime + (float)j / (SplitNum + 1) * (BeatList[i + 1].BeatTime - BeatList[i].BeatTime);
                BeatList.Add(beatInfo);
            }
        }
        List<BeatInfo> BeatLineList = new List<BeatInfo>();
        foreach (BeatInfo beatinfo in BeatList)
        {
            BeatInfo beat = new BeatInfo();
            beat.BeatTime = beatinfo.BeatTime + Chart.offset / 1000f;
            beat.BeatNum = beatinfo.BeatNum;
            beat.IntBeat = beatinfo.IntBeat;
            beat.Beat = beatinfo.Beat;
            BeatLineList.Add(beat);
        }
        return BeatLineList;
    }
    public void DrawLine(List<BeatInfo> BeatLinesSplit, List<float> VerticalLine)
    {
        BeatLine[] LinePool = GetComponentsInChildren<BeatLine>();
        foreach (BeatLine Line in LinePool)
        {
            Line.ResetLine();
        }
        int RecentLine = 0;
        if (RecentLine >= 127)
        {
            return;
        }
        foreach (float VerticalPos in VerticalLine)
        {
            Vector3 PointA = LinePos(new Vector3(VerticalPos, 10, 0));
            Vector3 PointB = LinePos(new Vector3(VerticalPos, 0, 0));
            try
            {
                LinePool[RecentLine].Line(PointA, PointB, false, -1, ColumnList[RecentLine - 2]);
            }
            catch
            {
                LinePool[RecentLine].Line(PointA, PointB, false, -1, -1);
            }
            RecentLine++;
        }
        foreach (BeatInfo Beat in BeatLinesSplit)
        {
            float LineTime = Beat.BeatTime;
            if (LineTime >= Timer.GetElapsedTime())
            {
                Vector3 PointA = LinePos(new Vector3(-worldScreenWidth / 4, (LineTime - Timer.GetElapsedTime()) * ScrollSpeed, 0));
                Vector3 PointB = LinePos(new Vector3(worldScreenWidth / 4, (LineTime - Timer.GetElapsedTime()) * ScrollSpeed, 0));
                if (Beat.IntBeat)
                {
                    LinePool[RecentLine].Line(PointA, PointB, true, Beat.BeatNum, 0);
                }
                else
                {
                    LinePool[RecentLine].Line(PointA, PointB, false, Beat.BeatNum, 0);
                }
                RecentLine++;
            }
        }
    }
    public List<BPMdata> GetBPM()
    {
        return BPMList;
    }
    public int GetOffset()
    {
        return Chart.offset;
    }
    public void SetBPM(List<BPMdata> BPM, int Offset)
    {
        BPMList = BPM;
        Chart.offset = Offset;
        BeatLine = SetBeatLine(BPMList);
    }
}