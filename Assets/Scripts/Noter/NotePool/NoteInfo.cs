using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class NoteInfo : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public Sprite Tap;
    public Sprite Catch;
    public BoxCollider2D BoxCollider2D;
    public NoteEdit NoteEdit;

    public Color Color;

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

    public Vector3 StartPos;
    public Vector3 EndPos;

    private void Start()
    {
        NoteEdit = transform.parent.GetComponent<NoteEdit>();
    }
    private void OnMouseDown()
    {
        NoteEdit.SetSelect(id);
    }
    public void SetNote()
    {
        if (type != 2)
        {
            SpriteRenderer.sprite = (type == 0) ? Tap : Catch;
            SpriteRenderer.color = HexToColor(_color);
        }
        else
        {
            SetHold();
            BoxCollider2D.enabled = false;
        }
    }
    public MeshFilter Filter;
    public MeshCollider Collider;
    public MeshRenderer Renderer;
    public Vector3[] vertices;
    private void SetHold()
    {
        if (type == 2)
        {
            Physics.queriesHitTriggers = true;
            Mesh mesh = new Mesh();
            Vector3[] vertices = new Vector3[]
            {
                new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 0.25f, 0), new Vector3(1, 0.25f, 0),
                new Vector3(0, 0.75f, 0), new Vector3(1, 0.75f, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0)
            };
            int[] triangles = new int[]
            {
                0, 1, 2, 1, 3, 2,
                2, 3, 4, 3, 5, 4,
                4, 5, 6, 5, 7, 6
            };
            Vector2[] uv = new Vector2[]
            {
                new Vector2(0, 0), new Vector2(1, 0),
                new Vector2(0, 0.25f), new Vector2(1, 0.25f),
                new Vector2(0, 0.75f), new Vector2(1, 0.75f),
                new Vector2(0, 1), new Vector2(1, 1)
            };

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uv;
            mesh.RecalculateNormals();
            Filter.mesh = mesh;
            Renderer.material.color = HexToColor(_color);
        }
    }
    public void SetPos()
    {
        if (type == 2)
        {
            transform.position = new Vector3(0, 0, -0.5f);
        }
        else
        {
            transform.position = StartPos;
        }
    }
    public void MeshSet()
    {
        if (type == 2)
        {
            float radius = 0.3f;
            Vector3 p1 = new Vector3(StartPos.x - radius, StartPos.y - radius, StartPos.z);
            Vector3 p2 = new Vector3(StartPos.x + radius, StartPos.y - radius, StartPos.z);
            Vector3 p3 = new Vector3(EndPos.x - radius, EndPos.y + radius, EndPos.z);
            Vector3 p4 = new Vector3(EndPos.x + radius, EndPos.y + radius, EndPos.z);
            vertices = new Vector3[]
            {
                p1,p2,
                new Vector3(p1.x,StartPos.y,StartPos.z),
                new Vector3(p2.x,StartPos.y,StartPos.z),
                new Vector3(p3.x,EndPos.y,EndPos.z),
                new Vector3(p4.x,EndPos.y,EndPos.z),
                p3,p4
            };
            Filter.mesh.vertices = vertices;
            Collider.sharedMesh = Filter.mesh;
        }
    }
    Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        byte a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, a);
    }
    public void UnSelect()
    {
        if (type != 2)
        {
            SpriteRenderer.color = HexToColor(_color);
        }
        else
        {
            Renderer.material.color = HexToColor(_color);
        }
    }
    public void Select()
    {
        if (type != 2)
        {
            SpriteRenderer.color = Color.green;
        }
        else
        {
            Renderer.material.color = Color.green;
        }
    }
}
