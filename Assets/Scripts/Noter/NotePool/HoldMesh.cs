using UnityEngine;

public class HoldMesh : MonoBehaviour
{
    public NoteInfo NoteInfo;
    private void OnMouseDown()
    {
        NoteInfo.NoteEdit.SetSelect(NoteInfo.id);
    }
}
