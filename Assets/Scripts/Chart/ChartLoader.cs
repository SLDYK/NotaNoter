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
        ChartPath = File.ReadAllLines(ManagerSetChartPath)[0].Trim();
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
    public void WriteIn(List<JudgeLine> judgelineList)
    {
        judgelineList = NoteReplace(judgelineList);
        Chart.judgelineList = judgelineList;
        Chart.noteNum = ComboManager.TotalCombo;
        string ChartJson = JsonUtility.ToJson(Chart);
        System.IO.File.WriteAllText(ChartPath, ChartJson);
    }
    //Note重分配
    public List<JudgeLine> NoteReplace(List<JudgeLine> judgelineList)
    {
        List<Note> Replace = new List<Note>();

        foreach (var judgeline in judgelineList)
        {
            Replace.AddRange(judgeline.noteList);
            judgeline.noteList.Clear();
        }

        foreach (Note note in Replace)
        {
            judgelineList[note.lineId].noteList.Add(note);
        }

        // 对每个judgeline的noteList进行排序
        foreach (var judgeline in judgelineList)
        {
            judgeline.noteList.Sort((note1, note2) => note1.time.CompareTo(note2.time));
        }

        return judgelineList;
    }

}
