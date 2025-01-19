using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ChartLoader : MonoBehaviour
{
    private string ChartPath;

    public TimeGrid TimeGrid;
    public NoteCreator NoteCreator;
    public ComboManager ComboManager;
    public AudioController AudioController;
    
    private Chart Chart;

    private void Start()
    {
        string ManagerSetChartPath = System.Environment.CurrentDirectory + "/LoadPath.txt";
        ChartPath = File.ReadAllText(ManagerSetChartPath).Split('\n')[0].Replace("\r", "").Trim();
        ReLoad();
    }
    public void ReLoad()
    {
        Chart = JsonUtility.FromJson<Chart>(File.ReadAllText(ChartPath));
        NoteCreator.ReLoad();
        ComboManager.FreshTime();
        TimeGrid.SetGrid();
    }
    public Chart GetChart()
    {
        return Chart;
    }
    public void WriteIn(List<judgeline> judgelineList)
    {
        Chart.judgelineList = judgelineList;
        Chart.noteNum = ComboManager.TotalCombo;
        string ChartJson = JsonUtility.ToJson(Chart);
        System.IO.File.WriteAllText(ChartPath, ChartJson);
    }
}
