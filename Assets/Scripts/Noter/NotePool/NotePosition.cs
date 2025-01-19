using UnityEngine;

public class NotePosition : MonoBehaviour
{
    public TimeGrid TimeGrid;
    public Timer Timer;
    private void Update()
    {
        NoteInfo[] NoteInfos = GetComponentsInChildren<NoteInfo>();
        foreach (NoteInfo NoteInfo in NoteInfos)
        {
            float ColumnX = TimeGrid.VerticalLine[TimeGrid.ColumnList.IndexOf(NoteInfo.lineId) + 2];
            float NoteYS = (NoteInfo.time / 1000f - Timer.GetElapsedTime()) * TimeGrid.ScrollSpeed;
            float NoteYE = ((NoteInfo.time + NoteInfo.duration) / 1000f - Timer.GetElapsedTime()) * TimeGrid.ScrollSpeed;
            Vector3 StartPos = new Vector3(ColumnX, NoteYS, -1);
            Vector3 EndPos = new Vector3(ColumnX, NoteYE, -1);
            NoteInfo.StartPos = StartPos + TimeGrid.NoterPos;
            NoteInfo.EndPos = EndPos + TimeGrid.NoterPos;
            NoteInfo.SetPos();
            NoteInfo.MeshSet();
        }
    }
}
