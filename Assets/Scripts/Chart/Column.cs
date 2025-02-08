using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;
using TMPro;
[System.Serializable]
public class ColumnPoint
{
    public int Beat;
    public string Columns;
    public string Previous;
}
public class Column : MonoBehaviour
{
    public List<ColumnPoint> ColumnEvents;
    public TMP_InputField InputField;
    public CanvasControl CanvasControl;
    private void Start()
    {
        LoadColumnEvent();
        if (ColumnEvents.Count != 0)
        {
            InputField.text = ColumnEvents[0].Columns;
            CanvasControl.SetColumnList();
        }
    }
    private float Temp = 0;
    public Timer Timer;
    private void Update()
    {
        if (ColumnEvents.Count != 0)
        {
            float Elapsed = GetBeat(Timer.GetElapsedTime());
            foreach (ColumnPoint p in ColumnEvents)
            {
                if (Temp < p.Beat && Elapsed > p.Beat)
                {
                    InputField.text = p.Columns;
                    CanvasControl.SetColumnList();
                }
                else if (Temp > p.Beat && Elapsed < p.Beat) 
                {
                    InputField.text = p.Previous;
                    CanvasControl.SetColumnList();
                }
            }
            Temp = Elapsed;
        }
    }
    private void LoadColumnEvent()
    {
        string ManagerSetChartPath = System.Environment.CurrentDirectory + "/LoadPath.txt";
        List<string>  ColumnData = File.ReadAllLines(ManagerSetChartPath).ToList();
        ColumnData.RemoveRange(0, 2);
        ColumnEvents = ParseColumnEvents(ColumnData);
    }
    public static List<ColumnPoint> ParseColumnEvents(List<string> ColumnData)
    {
        List<ColumnPoint> columnPoints = new List<ColumnPoint>();

        foreach (string eventString in ColumnData)
        {
            ColumnPoint columnPoint = ParseColumnPoint(eventString);
            if (columnPoints.Count > 0)
            {
                columnPoint.Previous = columnPoints[columnPoints.Count - 1].Columns;
            }
            else
            {
                columnPoint.Previous = columnPoint.Columns;
            }
            columnPoints.Add(columnPoint);
        }

        return columnPoints;
    }
    public static ColumnPoint ParseColumnPoint(string input)
    {
        input = input.Replace(" ", "").Substring(1);
        string[] parts = input.Split(new char[] { 's' }, 2);
        int beat = int.Parse(parts[0]);
        string columns = parts[1];
        return new ColumnPoint
        {
            Beat = beat,
            Columns = columns
        };
    }
    public TimeGrid TimeGrid;
    public float GetBeat(float time)
    {
        List<BeatInfo> beatInfos = TimeGrid.BeatLine;
        for (int i = 0; i < beatInfos.Count - 1; i++)
        {
            if (time >= beatInfos[i].BeatTime && time <= beatInfos[i + 1].BeatTime)
            {
                float t = (time - beatInfos[i].BeatTime) / (beatInfos[i + 1].BeatTime - beatInfos[i].BeatTime);
                return Mathf.Lerp(beatInfos[i].BeatNum, beatInfos[i + 1].BeatNum, t);
            }
        }
        return -1;
    }
}