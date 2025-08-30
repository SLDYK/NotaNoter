using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    public Timer Timer;
    public Text ComboText;
    public NoteCreator NoteCreator;
    public AudioManager AudioManager;

    private List<float> TapHitTime = new();
    private List<float> CatchHitTime = new();

    private int TapCount = 0;
    private int TapCountTemp = 0;
    private int CatchCount = 0;
    private int CatchCountTemp = 0;
    public int TotalCombo;
    public void FreshTime()
    {
        TapHitTime.Clear();
        CatchHitTime.Clear();
        foreach (JudgeLine judgeline in NoteCreator.judgelineList)
        {
            foreach (Note note in judgeline.noteList)
            {
                if (note.type != 1 && !note.fake)
                {
                    TapHitTime.Add(note.time / 1000f);
                }
                else if (!note.fake) 
                {
                    CatchHitTime.Add(note.time / 1000f);
                }
            }
        }
        TotalCombo = TapHitTime.Count + CatchHitTime.Count;
    }
    void Update()
    {
        int Combo = TapHitTime.Count(x => x < Timer.GetElapsedTime()) + CatchHitTime.Count(x => x < Timer.GetElapsedTime());
        ComboText.text = $"{Combo}\nCombo";
        TapCount = TapHitTime.Count(x => x < Timer.GetElapsedTime());
        CatchCount = CatchHitTime.Count(x => x < Timer.GetElapsedTime());
        if (TapCount > TapCountTemp && !Timer.Paused)
        {
            for (int i = TapCountTemp; i < TapCount; i++)
            {
                AudioManager.PlayClip1(1);
            }
        }
        if (CatchCount > CatchCountTemp && !Timer.Paused)
        {
            for (int i = CatchCountTemp; i < CatchCount; i++)
            {
                AudioManager.PlayClip2(1);
            }
        }
        TapCountTemp = TapCount;
        CatchCountTemp = CatchCount;
    }
}
