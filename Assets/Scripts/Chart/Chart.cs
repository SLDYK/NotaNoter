using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 音乐游戏图表数据结构
/// </summary>
[System.Serializable]
public class Chart
{
    [Header("基本信息")]
    public int formatVersion;
    public string name;
    public string composer;
    public string charter;
    public string illustrator;

    [Header("游戏设置")]
    public float difficulty;
    public float bpm;
    public int offset;
    public int noteNum;

    [Header("数据列表")]
    public List<BPMSection> bpmList;
    public List<JudgeLine> judgelineList;
    public List<PerformImage> performImgList;

    [Header("显示设置")]
    public string _startTipcolor;
}

/// <summary>
/// BPM变化段落
/// </summary>
[System.Serializable]
public class BPMSection
{
    public int time;
    public float bpm;
}

/// <summary>
/// 判定线
/// </summary>
[System.Serializable]
public class JudgeLine
{
    [Header("基本属性")]
    public int id;
    public float angle;
    public float scale;
    public string _color;
    public string _pos;

    [Header("数据")]
    public List<Note> noteList;
    public EventList eventList;
}

/// <summary>
/// 表演图片
/// </summary>
[System.Serializable]
public class PerformImage
{
    [Header("资源信息")]
    public string path;
    public string hash;
    public string name;

    [Header("时间设置")]
    public int startTime;
    public int endTime;

    [Header("变换属性")]
    public float angle;
    public float scale;
    public float scaleX;
    public float scaleY;
    public string _color;
    public string _pos;

    [Header("渲染设置")]
    public int layer;
    public int sortingOrder;

    [Header("事件")]
    public EventList eventList;
}

/// <summary>
/// 基础事件类
/// </summary>
[System.Serializable]
public class Event
{
    public int startTime;
    public int endTime;
    public int type;
}

/// <summary>
/// 事件列表容器
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
/// 移动事件
/// </summary>
[System.Serializable]
public class MoveEvent : Event
{
    public int pathType;
    public string _pos;
}

/// <summary>
/// 旋转事件
/// </summary>
[System.Serializable]
public class RotateEvent : Event
{
    public float startAngle;
    public float endAngle;
}

/// <summary>
/// 颜色事件
/// </summary>
[System.Serializable]
public class ColorEvent : Event
{
    public string _startcolor;
    public string _endcolor;
}

/// <summary>
/// 缩放事件
/// </summary>
[System.Serializable]
public class ScaleEvent : Event
{
    public float startScale;
    public float endScale;
}

/// <summary>
/// 音符
/// </summary>
[System.Serializable]
public class Note
{
    [Header("基本属性")]
    public int id;
    public int type;
    public int time;
    public int duration;

    [Header("游戏属性")]
    public int speed;
    public int livingTime;
    public int lineId;
    public int LineSide;
    public bool fake;

    [Header("视觉效果")]
    public string _color;
    public int hitEffectAlpha;

    /// <summary>
    /// 深拷贝音符对象
    /// </summary>
    /// <returns>新的音符对象</returns>
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
            LineSide = this.LineSide,
            fake = this.fake,
            _color = this._color,
            id = this.id,
            hitEffectAlpha = this.hitEffectAlpha
        };
    }
}