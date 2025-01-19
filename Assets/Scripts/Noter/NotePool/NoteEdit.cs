using UnityEngine;
using System.Collections.Generic;

public class NoteEdit : MonoBehaviour
{
    public int Selected = -1;//����ѡ��id��-1��ʾ��ѡ��
    public List<int> MultiSelected = new() { };//����ѡ��id
    private bool Fresh = false;//ˢ��Combo����Note��������ã�һ֡�������һ��

    public NoteCreator NoteCreator;
    public ComboManager ComboManager;
    public PosInGrid PosInGrid;
    public NotePreset NotePreset;
    public CanvasControl CanvasControl;
    public MultiSelector MultiSelector;

    public SelectArea SelectArea;

    private bool Holding = false;
    void Update()
    {
        //ˢ�²���
        if (Fresh)
        {
            ComboManager.FreshTime();
            Fresh = false;
        }
        //�Ҽ��������ѡ��
        if (Input.GetMouseButtonDown(1))
        {
            Selected = -1;
            MultiSelected.Clear();
            CanvasControl.Unselect();
        }
        //���ѡ��
        NoteInfo[] NoteInfos = GetComponentsInChildren<NoteInfo>();
        foreach (NoteInfo NoteInfo in NoteInfos)
        {
            NoteInfo.UnSelect();
            if (NoteInfo.id == Selected)
            {
                NoteInfo.Select();
            }
            if (MultiSelected.Contains(NoteInfo.id))
            {
                NoteInfo.Select();
            }
        }
        //F����
        if (Input.GetKey(KeyCode.F))
        {
            if (Selected != -1)
            {
                foreach (judgeline judgeline in NoteCreator.judgelineList)
                {
                    foreach (note note in judgeline.noteList)
                    {
                        if (note.id == Selected)
                        {
                            Vector2 TargetPos = PosInGrid.TargetPos();
                            note.lineId = (int)TargetPos.x;
                            note.time = (int)TargetPos.y;
                        }
                    }
                }
                Fresh = true;
            }
        }
        //Dɾ��
        if (Input.GetKeyDown(KeyCode.D))
        {
            foreach (judgeline judgeline in NoteCreator.judgelineList)
            {
                judgeline.noteList.RemoveAll(note => note.id == Selected);
                judgeline.noteList.RemoveAll(note => MultiSelected.Contains(note.id));
                foreach (Transform child in transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
            Selected = -1;
            MultiSelected.Clear();
            Fresh = true;
        }
        //Q����Tap
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vector2 TargetPos = PosInGrid.TargetPos();
            note note = new note();
            note.type = 0;
            note.lineId = (int)TargetPos.x;
            note.time = (int)TargetPos.y;
            note.id = NoteCreator.NoteId++;
            note.speed = NotePreset.GetPresetNote().speed;
            note.livingTime = NotePreset.GetPresetNote().livingTime;
            note.LineSide = NotePreset.GetPresetNote().LineSide;
            note.fake = NotePreset.GetPresetNote().fake;
            note._color = NotePreset.GetPresetNote()._color;
            foreach (judgeline judgeline in NoteCreator.judgelineList)
            {
                if(judgeline.id == note.lineId)
                {
                    judgeline.noteList.Add(note);
                }
            }
            Selected = NoteCreator.NoteId - 1;
            MultiSelected.Clear();
            Fresh = true;
        }
        //W����Catch
        if (Input.GetKeyDown(KeyCode.W))
        {
            Vector2 TargetPos = PosInGrid.TargetPos();
            note note = new note();
            note.type = 1;
            note.lineId = (int)TargetPos.x;
            note.time = (int)TargetPos.y;
            note.duration = 0;
            note.id = NoteCreator.NoteId++;
            note.speed = NotePreset.GetPresetNote().speed;
            note.livingTime = NotePreset.GetPresetNote().livingTime;
            note.LineSide = NotePreset.GetPresetNote().LineSide;
            note.fake = NotePreset.GetPresetNote().fake;
            note._color = NotePreset.GetPresetNote()._color;
            foreach (judgeline judgeline in NoteCreator.judgelineList)
            {
                if (judgeline.id == note.lineId)
                {
                    judgeline.noteList.Add(note);
                }
            }
            Selected = NoteCreator.NoteId - 1;
            MultiSelected.Clear();
            Fresh = true;
        }
        //E����Hold
        if (Input.GetKeyDown(KeyCode.E))
        {
            Holding = !Holding;
            if (Holding)//��ʼ����
            {
                Vector2 TargetPos = PosInGrid.TargetPos();
                note note = new note();
                note.type = 2;
                note.lineId = (int)TargetPos.x;
                note.time = (int)TargetPos.y;
                note.duration = 0;
                note.id = NoteCreator.NoteId++;
                note.speed = NotePreset.GetPresetNote().speed;
                note.livingTime = NotePreset.GetPresetNote().livingTime;
                note.LineSide = NotePreset.GetPresetNote().LineSide;
                note.fake = NotePreset.GetPresetNote().fake;
                note._color = NotePreset.GetPresetNote()._color;
                foreach (judgeline judgeline in NoteCreator.judgelineList)
                {
                    if (judgeline.id == note.lineId)
                    {
                        judgeline.noteList.Add(note);
                    }
                }
                Selected = NoteCreator.NoteId - 1;
                MultiSelected.Clear();
            }
            else//��������
            {
                foreach (judgeline judgeline in NoteCreator.judgelineList)
                {
                    foreach (note note in judgeline.noteList)
                    {
                        if (note.id == Selected)
                        {
                            Vector2 TargetPos = PosInGrid.TargetPos();
                            note.duration = (int)TargetPos.y - note.time;
                        }
                    }
                }
                Fresh = true;
            }
        }
        //��ѡ
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetMouseButtonDown(0))
            {
                MultiSelector.SelectStart();
                MultiSelector.Available = true;
                SelectArea.StartPos();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (MultiSelector.Available)
                {
                    MultiSelected = MultiSelector.SelectEnd();
                    if (MultiSelected.Count != 0)
                    {
                        CanvasControl.LoadMulti();
                    }
                    Selected = -1;
                    SelectArea.EndPos();
                }
            }
        }
        else
        {
            MultiSelector.Available = false;
            SelectArea.EndPos();
        }
    }
    //��ֹѡ���ͻ
    public bool SelectActive = false;
    private void CanvasAvailable()
    {
        SelectActive = true;
    }
    public void SetSelect(int id)
    {
        Selected = id;
        MultiSelected.Clear();
        foreach (judgeline judgeline in NoteCreator.judgelineList)
        {
            foreach (note note in judgeline.noteList)
            {
                if (note.id == Selected)
                {
                    SelectActive = false;
                    CanvasControl.LoadNote(note);
                    Invoke("CanvasAvailable", 0.1f);
                }
            }
        }
    }
    public SelectEdit SelectEdit;
    public void CanvasUpdate()
    {
        foreach (judgeline judgeline in NoteCreator.judgelineList)
        {
            foreach (note note in judgeline.noteList)
            {
                if (note.id == Selected && SelectActive)
                {
                    note.livingTime = int.Parse(SelectEdit.LivingTime.text);
                    note.speed = int.Parse(SelectEdit.Speed.text);
                    note.LineSide = SelectEdit.UpSide.isOn ? 0 : 1;
                    note.fake = SelectEdit.isFake.isOn;
                    note._color = SelectEdit.ColorToHex(SelectEdit.ColorPicker.CurrentColor);
                    note.time = int.Parse(SelectEdit.Time.text);
                    note.duration = int.Parse(SelectEdit.Duration.text);
                    note.lineId = int.Parse(SelectEdit.Lineid.text);
                }
            }
        }
    }
    public MultiEdit MultiEdit;
    public void MultiUpdate()
    {
        foreach (judgeline judgeline in NoteCreator.judgelineList)
        {
            foreach (note note in judgeline.noteList)
            {
                if (MultiSelected.Contains(note.id)) 
                {
                    note.livingTime = int.Parse(MultiEdit.LivingTime.text);
                    note.speed = int.Parse(MultiEdit.Speed.text);
                    note.LineSide = MultiEdit.UpSide.isOn ? 0 : 1;
                    note.fake = MultiEdit.isFake.isOn;
                    note._color = MultiEdit.ColorToHex(MultiEdit.ColorPicker.CurrentColor);
                }
            }
        }
    }
}
