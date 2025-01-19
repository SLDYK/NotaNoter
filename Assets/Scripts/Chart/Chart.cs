using JetBrains.Annotations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Chart
{
    public int formatVersion;
    public string name;
    public string composer;
    public string charter;
    public string illustrator;
    public float difficulty;
    public float bpm;
    public List<BPMSec> bpmList;
    public int offset;
    public int noteNum;
    public List<judgeline> judgelineList;
    public List<performImg> performImgList;
    public string _startTipcolor;
}
[System.Serializable]
public class BPMSec
{
    public int time;
    public float bpm;
}
[System.Serializable]
public class judgeline
{
    public List<note> noteList;
    public eventList eventList;
    public float angle;
    public float scale;
    public string _color;
    public string _pos;
    public int id;
}
[System.Serializable]
public class performImg
{
    public string path;
    public string hash;
    public string name;
    public eventList eventList;
    public float angle;
    public float scale;
    public float scaleX;
    public float scaleY;
    public int startTime;
    public int endTime;
    public int layer;
    public int sortingOrder;
    public string _color;
    public string _pos;
}
[System.Serializable]
public class Event
{
    public int startTime;
    public int endTime;
    public int type;
}
[System.Serializable]
public class eventList
{
    public List<move> moveEvents;
    public List<rotate> rotateEvents;
    public List<color> colorModifyEvents;
    public List<scale> scaleXEvents;
    public List<scale> scaleYEvents;
}
[System.Serializable]
public class move : Event
{
    public int pathType;
    public string _pos;
}
[System.Serializable]
public class rotate : Event
{
    public float startAngle;
    public float endAngle;
}
[System.Serializable]
public class color : Event
{
    public string _startcolor;
    public string _endcolor;
}
[System.Serializable]
public class scale : Event
{
    public float startScale;
    public float endScale;
}
[System.Serializable]
public class note
{
    public int type;
    public int time;
    public int duration;
    public int speed;
    public int livingTime;
    public int lineId;
    public int LineSide;
    public bool fake;
    public string _color;
    public int id;
    public note DeepCopy()
    {
        return new note
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
            id = this.id
        };
    }
}
