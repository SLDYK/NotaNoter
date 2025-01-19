using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class MultiSelector : MonoBehaviour
{
    public Vector2 Pos_1;
    public Vector2 Pos_2;

    public TimeGrid TimeGrid;
    public PosInGrid PosInGrid;
    public NoteCreator NoteCreator;
    public bool Available = false;

    public void SelectStart()
    {
        Pos_1 = PosInGrid.DetailTargetPos();
    }
    public List<int> SelectEnd()
    {
        Pos_2 = PosInGrid.DetailTargetPos();
        List<float> TimeRange = new() { Math.Min(Pos_1.y, Pos_2.y), Math.Max(Pos_1.y, Pos_2.y) };
        List<int> Columns = FilterList(TimeGrid.ColumnList, GetColumns(Pos_1.x, Pos_2.x));
        List<int> Note = new();
        foreach (judgeline judgeline in NoteCreator.judgelineList)
        {
            if (Columns.Contains(judgeline.id))
            {
                foreach (note note in judgeline.noteList)
                {
                    if(note.time >= TimeRange[0] && note.time <= TimeRange[1])
                    {
                        Note.Add(note.id);
                    }
                }
            }
        }
        return Note;
    }
    public List<int> GetColumns(float num1, float num2) 
    {
        List<int> integers = new List<int>();
        int start = (int)Math.Ceiling(Math.Min(num1, num2));
        int end = (int)Math.Floor(Math.Max(num1, num2));
        for (int i = start; i <= end; i++) 
        { 
            integers.Add(i); 
        } 
        return integers;
    }
    public List<int> FilterList(List<int> ColumnList, List<int> Columns)
    {
        return ColumnList
            .Where((item, index) => Columns
            .Contains(index)).ToList();
    }
}
