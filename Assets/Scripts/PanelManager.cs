using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    private static PanelManager pm;
    private void Awake()
    {
        if (pm == null)
        {
            pm = this;
        }
    }
    public static PanelManager GetPanelManager()
    {
        return pm;
    }


    public List<Transform> panels = new List<Transform>();
    public Transform selfPanel;
    public Transform npcPanel;
    private void Start()
    {
        foreach (Transform item in panels)
        {
            item.gameObject.SetActive(false);
        }
    }
    public GameObject OpenPanel(string panelName)
    {
        foreach (Transform item in panels)
        {
            if (item.name == panelName)
            {
                GameObject go = item.gameObject;
                go.SetActive(true);
                return go;
            }
        }
        Debug.Log("未找到指定panel");
        return null;
    }
    public void ClosePanel(string panelName)
    {
        foreach (Transform item in panels)
        {
            if (item.name == panelName)
            {
                GameObject go = item.gameObject;
                go.SetActive(false);
                return;
            }
        }
        Debug.Log("未找到指定panel");

    }

}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    private static PanelManager pm;
    private void Awake()
    {
        if( pm==null)
        {
            pm = this;
        }
    }
    public static PanelManager GetPanelManager()
    {
        return pm;
    }
    public List<Transform> panels = new List<Transform>();
    private void Start()
    {
        foreach(Transform item in panels)
        {
            item.gameObject.SetActive(false);
        }
    }
    public GameObject OpenPanel(string panelName)
    {
        foreach (Transform item in panels)
        {
            if (item.name == panelName)
            {
                GameObject go = item.gameObject;
                go.SetActive(true);
                return go;
            }
        }
        return null;
    }
    public void ClosePanel(string panelName)
    {
        foreach (Transform item in panels)
        {
            if(item.name==panelName)
            {
                GameObject go = item.gameObject;
                go.SetActive(false);
                return;
            }
        }
    }
}*/
