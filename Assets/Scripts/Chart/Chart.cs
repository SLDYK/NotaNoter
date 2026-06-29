using System;
using System.Collections.Generic;
using UnityEngine;
// 音乐游戏图表数据结构
[System.Serializable]
public class Chart
{
    [Header("基础信息")]
    public int formatVersion;
    public string name;
    public string composer;
    public string charter;
    public string illustrator;

    [Header("游戏参数")]
    public float difficulty;
    public float bpm;
    public int offset;
    public int noteNum;

    [Header("物件列表")]
    public List<BPMSection> bpmList;
    public List<JudgeLine> judgelineList;
    public List<PerformImage> performImgList;

    [Header("显示参数")]
    public string _startTipcolor;
}

// BPM变化区间
[System.Serializable]
public class BPMSection
{
    public int time;
    public float bpm;
}

// 判定线
[System.Serializable]
public class JudgeLine
{
    [Header("基本信息")]
    public int id;
    public float angle;
    public float scale;
    public string _color;
    public string _pos;

    [Header("音符")]
    public List<Note> noteList;
    public EventList eventList;
}

// 表演图片
[System.Serializable]
public class PerformImage
{
    [Header("资源信息")]
    public string path;
    public string hash;
    public string name;

    [Header("时间参数")]
    public int startTime;
    public int endTime;

    [Header("变换参数")]
    public float angle;
    public float scale;
    public float scaleX;
    public float scaleY;
    public string _color;
    public string _pos;

    [Header("渲染参数")]
    public int layer;
    public int sortingOrder;

    [Header("事件")]
    public EventList eventList;
}

// 基础事件类
[System.Serializable]
public class Event
{
    public int startTime;
    public int endTime;
    public int type;
}

// 事件列表容器
[System.Serializable]
public class EventList
{
    public List<MoveEvent> moveEvents;
    public List<RotateEvent> rotateEvents;
    public List<ColorEvent> colorModifyEvents;
    public List<ScaleEvent> scaleXEvents;
    public List<ScaleEvent> scaleYEvents;
}

// 移动事件
[System.Serializable]
public class MoveEvent : Event
{
    public int pathType;
    public string _pos;
}

// 旋转事件
[System.Serializable]
public class RotateEvent : Event
{
    public float startAngle;
    public float endAngle;
}

// 颜色事件
[System.Serializable]
public class ColorEvent : Event
{
    public string _startcolor;
    public string _endcolor;
}

// 缩放事件
[System.Serializable]
public class ScaleEvent : Event
{
    public float startScale;
    public float endScale;
}

// 音符
[System.Serializable]
public class Note
{
    [Header("基础参数")]
    public int id;
    public int type;
    public int time;
    public int duration;

    [Header("游戏参数")]
    public int speed;
    public int livingTime;
    public int lineId;
    public int lineSide;
    public bool fake;

    [Header("视觉效果")]
    public string _color;
    public int hitEffectAlpha;

    // 深拷贝音符数据
    // 返回新的音符数据
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