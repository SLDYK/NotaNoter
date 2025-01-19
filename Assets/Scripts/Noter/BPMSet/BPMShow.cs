using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BPMShow : MonoBehaviour
{
    public TimeGrid TimeGrid;
    public GameObject BPMPrefab;
    public TMP_InputField OFFSET;
    private List<BPMdata> BPMList;
    private int Distance;
    public void LoadBPM()
    {
        BPMList = TimeGrid.GetBPM();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        Distance = 0;
        foreach (BPMdata BPMdata in BPMList) 
        {
            if (BPMdata.beat == 2000)
            {
                break;
            }
            GameObject BPMNode = Instantiate(BPMPrefab, new Vector3(0f, 100f, 0f), Quaternion.identity);
            BPMNode.GetComponent<BPMInfo>().BPM.text = $"{BPMdata.value}";
            BPMNode.GetComponent<BPMInfo>().Beat.text = $"{BPMdata.beat}";
            BPMNode.transform.SetParent(transform);
            BPMNode.transform.localPosition = new Vector3(-50f, -240 - Distance, 0);
            BPMNode.transform.localScale = new Vector3(1, 1, 1);
            Distance += 60;
        }
        OFFSET.text = $"{TimeGrid.GetOffset()}";
    }
    public void AddBPM()
    {
        GameObject BPMNode = Instantiate(BPMPrefab, new Vector3(0f, 100f, 0f), Quaternion.identity);
        BPMNode.GetComponent<BPMInfo>().BPM.text = $"";
        BPMNode.GetComponent<BPMInfo>().Beat.text = $"";
        BPMNode.transform.SetParent(transform);
        BPMNode.transform.localPosition = new Vector3(-50f, -240 - Distance, 0f);
        BPMNode.transform.localScale = new Vector3(1, 1, 1);
        Distance += 60;
    }
    public List<BPMdata> GetBPM()
    {
        BPMInfo[] BPMInfos = GetComponentsInChildren<BPMInfo>();
        List<BPMdata> BPM = new List<BPMdata>();
        foreach (BPMInfo BPMInfo in BPMInfos)
        {
            BPM.Add(new BPMdata { beat = BPMInfo.GetBeat(), value = BPMInfo.GetBPM() });
        }
        BPM.Add(new BPMdata { beat = 2000, value = 2000 });
        return BPM;
    }
    public void BPMConfirm()
    {
        TimeGrid.SetBPM(GetBPM(), Int32.Parse(OFFSET.text));
    }
}
