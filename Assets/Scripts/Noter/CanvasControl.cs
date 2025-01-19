using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{
    private void Start()
    {
        SetColumnList();
        SetMeshSpeed();
        SetBeatSplit();
    }
    //�ж�����
    public TMP_InputField ColumnList;
    public TimeGrid TimeGrid;
    private static List<int> ToIntList(string input)
    {
        List<int> result = new List<int>();
        foreach (string num in input.Split(','))
        {
            result.Add(int.Parse(num));
        }
        return result;
    }
    public void SetColumnList()
    {
        TimeGrid.ColumnList = ToIntList(ColumnList.text);
    }
    //�����ٶ�
    public Slider MeshSpeed;
    public void SetMeshSpeed()
    {
        TimeGrid.ScrollSpeed = MeshSpeed.value;
    }
    //����
    public TMP_InputField BeatSplit;
    public void SetBeatSplit()
    {
        TimeGrid.BeatSplit = int.Parse(BeatSplit.text);
    }
    //��������
    public ChartLoader ChartLoader;
    public NoteCreator NoteCreator;
    public void ReLoad()
    {
        ChartLoader.ReLoad();
    }
    //��������
    public void SaveChart()
    {
        ChartLoader.WriteIn(NoteCreator.judgelineList);
    }
    //�༭����
    public GameObject Setting;
    public void NoteSetting()
    {
        Setting.SetActive(!Setting.activeSelf);
        BPMSetting.SetActive(false);
        Preset.SetActive(false);
        NoteSelect.SetActive(false);
        MultiSelect.SetActive(false);
    }
    //Track����
    public GameObject BPMSetting;
    public BPMShow BPMShow;
    public void TrackSetting()
    {
        BPMSetting.SetActive(!BPMSetting.activeSelf);
        Setting.SetActive(false);
        Preset.SetActive(false);
        NoteSelect.SetActive(false);
        MultiSelect.SetActive(false);
        if (BPMSetting.activeSelf)
        {
            BPMShow.LoadBPM();
        }
    }
    //Noteģ��
    public GameObject Preset;
    public NotePreset NotePreset;
    public void PresetSetting()
    {
        Preset.SetActive(!Preset.activeSelf);
        BPMSetting.SetActive(false);
        Setting.SetActive(false);
        NoteSelect.SetActive(false);
        MultiSelect.SetActive(false);
        if (Preset.activeSelf)
        {
            NotePreset.LoadPreset();
        }
    }
    //Note��ѡ�༭
    public GameObject NoteSelect;
    public SelectEdit SelectEdit;
    public void LoadNote(note note)
    {
        NoteSelect.SetActive(true);
        MultiSelect.SetActive(false);
        BPMSetting.SetActive(false);
        Setting.SetActive(false);
        Preset.SetActive(false);
        if (NoteSelect.activeSelf)
        {
            SelectEdit.LoadNote(note);
        }
    }
    //Note��ѡ�༭
    public GameObject MultiSelect;
    public void LoadMulti()
    {
        MultiSelect.SetActive(true);
        NoteSelect.SetActive(false);
        BPMSetting.SetActive(false);
        Setting.SetActive(false);
        Preset.SetActive(false);
    }
    //�Ҽ�ȡ��ѡ��
    public void Unselect()
    {
        NoteSelect.SetActive(false);
        MultiSelect.SetActive(false);
    }
}