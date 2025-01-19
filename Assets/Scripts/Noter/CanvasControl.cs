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
    //判定线组
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
    //网格速度
    public Slider MeshSpeed;
    public void SetMeshSpeed()
    {
        TimeGrid.ScrollSpeed = MeshSpeed.value;
    }
    //分拍
    public TMP_InputField BeatSplit;
    public void SetBeatSplit()
    {
        TimeGrid.BeatSplit = int.Parse(BeatSplit.text);
    }
    //重载谱面
    public ChartLoader ChartLoader;
    public NoteCreator NoteCreator;
    public void ReLoad()
    {
        ChartLoader.ReLoad();
    }
    //保存谱面
    public void SaveChart()
    {
        ChartLoader.WriteIn(NoteCreator.judgelineList);
    }
    //编辑设置
    public GameObject Setting;
    public void NoteSetting()
    {
        Setting.SetActive(!Setting.activeSelf);
        BPMSetting.SetActive(false);
        Preset.SetActive(false);
        NoteSelect.SetActive(false);
        MultiSelect.SetActive(false);
    }
    //Track设置
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
    //Note模板
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
    //Note单选编辑
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
    //Note多选编辑
    public GameObject MultiSelect;
    public void LoadMulti()
    {
        MultiSelect.SetActive(true);
        NoteSelect.SetActive(false);
        BPMSetting.SetActive(false);
        Setting.SetActive(false);
        Preset.SetActive(false);
    }
    //右键取消选择
    public void Unselect()
    {
        NoteSelect.SetActive(false);
        MultiSelect.SetActive(false);
    }
}