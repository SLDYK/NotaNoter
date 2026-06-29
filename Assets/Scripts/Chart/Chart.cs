using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ������Ϸͼ�����ݽṹ
/// </summary>
[System.Serializable]
public class Chart
{
    [Header("������Ϣ")]
    public int formatVersion;
    public string name;
    public string composer;
    public string charter;
    public string illustrator;

    [Header("��Ϸ����")]
    public float difficulty;
    public float bpm;
    public int offset;
    public int noteNum;

    [Header("�����б�")]
    public List<BPMSection> bpmList;
    public List<JudgeLine> judgelineList;
    public List<PerformImage> performImgList;

    [Header("��ʾ����")]
    public string _startTipcolor;
}

/// <summary>
/// BPM�仯����
/// </summary>
[System.Serializable]
public class BPMSection
{
    public int time;
    public float bpm;
}

/// <summary>
/// �ж���
/// </summary>
[System.Serializable]
public class JudgeLine
{
    [Header("��������")]
    public int id;
    public float angle;
    public float scale;
    public string _color;
    public string _pos;

    [Header("����")]
    public List<Note> noteList;
    public EventList eventList;
}

/// <summary>
/// ����ͼƬ
/// </summary>
[System.Serializable]
public class PerformImage
{
    [Header("��Դ��Ϣ")]
    public string path;
    public string hash;
    public string name;

    [Header("ʱ������")]
    public int startTime;
    public int endTime;

    [Header("�任����")]
    public float angle;
    public float scale;
    public float scaleX;
    public float scaleY;
    public string _color;
    public string _pos;

    [Header("��Ⱦ����")]
    public int layer;
    public int sortingOrder;

    [Header("�¼�")]
    public EventList eventList;
}

/// <summary>
/// �����¼���
/// </summary>
[System.Serializable]
public class Event
{
    public int startTime;
    public int endTime;
    public int type;
}

/// <summary>
/// �¼��б�����
/// </summary>
[System.Serializable]
public class EventList
{
    public List<MoveEvent> moveEvents;
    public List<RotateEvent> rotateEvents;
    public List<ColorEvent> colorModifyEvents;
    public List<ScaleEvent> scaleXEvents;
    public List<ScaleEvent> scaleYEvents;
}

/// <summary>
/// �ƶ��¼�
/// </summary>
[System.Serializable]
public class MoveEvent : Event
{
    public int pathType;
    public string _pos;
}

/// <summary>
/// ��ת�¼�
/// </summary>
[System.Serializable]
public class RotateEvent : Event
{
    public float startAngle;
    public float endAngle;
}

/// <summary>
/// ��ɫ�¼�
/// </summary>
[System.Serializable]
public class ColorEvent : Event
{
    public string _startcolor;
    public string _endcolor;
}

/// <summary>
/// �����¼�
/// </summary>
[System.Serializable]
public class ScaleEvent : Event
{
    public float startScale;
    public float endScale;
}

/// <summary>
/// ����
/// </summary>
[System.Serializable]
public class Note
{
    [Header("��������")]
    public int id;
    public int type;
    public int time;
    public int duration;

    [Header("��Ϸ����")]
    public int speed;
    public int livingTime;
    public int lineId;
    public int lineSide;
    public bool fake;

    [Header("�Ӿ�Ч��")]
    public string _color;
    public int hitEffectAlpha;

    /// <summary>
    /// �����������
    /// </summary>
    /// <returns>�µ���������</returns>
    public Note DeepCopy()
    {
        return new Note
        {
            type = this.type,
            time = this.time,
            duration = this.duration,
            speed = this.speed,
            livingTime = this.livingTime,
            lineId = this.lineId,
            lineSide = this.lineSide,
            fake = this.fake,
            _color = this._color,
            id = this.id,
            hitEffectAlpha = this.hitEffectAlpha
        };
    }
}