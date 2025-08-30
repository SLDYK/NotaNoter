using System.Collections.Generic;
using UnityEngine;

public class NoteCreator : MonoBehaviour
{
    public ChartLoader ChartLoader;
    public List<JudgeLine> judgelineList;
    public Timer Timer;
    public TimeGrid TimeGrid;

    public GameObject NotePrefab;

    public int NoteId;
    public int LineId;
    public void ReLoad()
    {
        judgelineList = ChartLoader.GetChart().judgelineList;
        LineId = 0;
        NoteId = 0;
        foreach (JudgeLine judgeline in judgelineList)
        {
            judgeline.id = LineId++;
            foreach (Note note in judgeline.noteList)
            {
                note.id = NoteId++;
            }
        }
        // 修复遍历 transform 的子对象并销毁
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
    private List<int> NotePool = new List<int>();
    private void Update()
    {
        NotePool.Clear();
        NoteInfo[] NoteInfos = GetComponentsInChildren<NoteInfo>();
        foreach (NoteInfo NoteInfo in NoteInfos)
        {
            NotePool.Add(NoteInfo.id);
        }
        foreach (NoteInfo NoteInfo in NoteInfos)
        {
            if (!InTime(NoteInfo) || !InColumn(NoteInfo)) 
            {
                Destroy(NoteInfo.gameObject);
            }
        }
        foreach (JudgeLine judgeline in judgelineList)
        {
            foreach(Note note in judgeline.noteList)
            {
                if (InTime(note) && InColumn(note))
                {
                    if (NotePool.Contains(note.id))
                    {
                        foreach(NoteInfo NoteInfo in NoteInfos)
                        {
                            if (NoteInfo.id == note.id) 
                            {
                                NoteInfo.type = note.type;
                                NoteInfo.time = note.time;
                                NoteInfo.duration = note.duration;
                                NoteInfo.speed = note.speed;
                                NoteInfo.livingTime = note.livingTime;
                                NoteInfo.lineId = note.lineId;
                                NoteInfo.LineSide = note.LineSide;
                                NoteInfo.fake = note.fake;
                                NoteInfo._color = note._color;
                                NoteInfo.hitEffectAlpha = note.hitEffectAlpha; // 添加这行
                            }
                        }
                    }
                    else
                    {
                        GameObject CreateNote = Instantiate(NotePrefab, new Vector3(0f, 100f, 0f), Quaternion.identity);
                        CreateNote.GetComponent<NoteInfo>().type = note.type;
                        CreateNote.GetComponent<NoteInfo>().time = note.time;
                        CreateNote.GetComponent<NoteInfo>().duration = note.duration;
                        CreateNote.GetComponent<NoteInfo>().speed = note.speed;
                        CreateNote.GetComponent<NoteInfo>().livingTime = note.livingTime;
                        CreateNote.GetComponent<NoteInfo>().lineId = note.lineId;
                        CreateNote.GetComponent<NoteInfo>().LineSide = note.LineSide;
                        CreateNote.GetComponent<NoteInfo>().fake = note.fake;
                        CreateNote.GetComponent<NoteInfo>()._color = note._color;
                        CreateNote.GetComponent<NoteInfo>().hitEffectAlpha = note.hitEffectAlpha; // 添加这行
                        CreateNote.GetComponent<NoteInfo>().id = note.id;
                        CreateNote.GetComponent<NoteInfo>().SetNote();
                        CreateNote.transform.SetParent(transform);
                    }
                }
            }
        }
    }
    private bool InTime(NoteInfo NoteInfo)
    {
        return NoteInfo.time + NoteInfo.duration > (Timer.GetElapsedTime() - 1) * 1000 && NoteInfo.time <= (Timer.GetElapsedTime() * 1000 + NoteInfo.livingTime);
    }
    private bool InColumn(NoteInfo NoteInfo)
    {
        return TimeGrid.ColumnList.Contains(NoteInfo.lineId);
    }
    private bool InTime(Note NoteInfo)
    {
        return NoteInfo.time + NoteInfo.duration > (Timer.GetElapsedTime() - 1) * 1000 && NoteInfo.time <= (Timer.GetElapsedTime() * 1000 + NoteInfo.livingTime);
    }
    private bool InColumn(Note NoteInfo)
    {
        return TimeGrid.ColumnList.Contains(NoteInfo.lineId);
    }
}
