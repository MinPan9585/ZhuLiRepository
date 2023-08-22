using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class BaseNpc : MonoBehaviour
{

    public GameObject tipIcon;
    [Header("npc 相关")]
    public string npcName;
    public Sprite npcIcon;
    [Header("对话相关")]
    public string currentDialogNeme;
    private string[] dialogContent;
    private TMP_Text nameTmp;
    private TMP_Text contentTmp;
    private Image iconImg;
    private bool canTalk = false;
    private void Start()
    {
        //dialogContent = MyUtilities.GetDialogsFromJson("Dialogs", currentDialogNeme);
        tipIcon.SetActive(false);
    }

    public GameObject talkUI;
    private void Update()
    {
        if (canTalk)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                talkUI.SetActive(true);
            }
        }
    }
    int index = 0;
    bool isFirstOpPanel = true;
    private void Talk()
    {
        if (isFirstOpPanel)
        {
            //判断第一句话是player还是npc
            if (dialogContent[index].Length == 1 && dialogContent[index] == "P")
            {
                currentOpenedPanelName = "selfPanel";
                index++;
            }
            if (dialogContent[index].Length == 1 && dialogContent[index] == "N")
            {
                currentOpenedPanelName = "npcPanel";
                index++;
            }
            SwitchPanel(currentOpenedPanelName);
            //设置内容         
            contentTmp.text = dialogContent[index];
            index++;
            isFirstOpPanel = false;
        }
        else
        {
            //超出索引，对话结束
            if (index >= dialogContent.Length)
            {
                PanelManager.GetPanelManager().ClosePanel(currentOpenedPanelName);
                isFirstOpPanel = true;
                index = 0;
                return;
            }
            if (dialogContent[index].Length == 1 && dialogContent[index] == "P")
            {
                SwitchPanel("selfPanel");
                index++;
            }
            else
            if (dialogContent[index].Length == 1 && dialogContent[index] == "N")
            {
                SwitchPanel("npcPanel");
                index++;
            }

            contentTmp.text = dialogContent[index];
            index++;
        }

    }

    string currentOpenedPanelName;
    public void SwitchPanel(string panelName)
    {
        PanelManager.GetPanelManager().ClosePanel(currentOpenedPanelName);
        GameObject go = PanelManager.GetPanelManager().OpenPanel(panelName);
        foreach (Transform item in go.transform)
        {
            if (item.name == "nameTmp")
            {
                this.nameTmp = item.GetComponent<TMP_Text>();

            }
            else if (item.name == "iconImg")
            {
                this.iconImg = item.GetComponent<Image>();
            }
            else if (item.name == "contentTmp")
            {
                this.contentTmp = item.GetComponent<TMP_Text>();
            }
        }
        if (panelName == "npcPanel")
        {
            SetDialogPanel(this.nameTmp, this.iconImg);
        }
        currentOpenedPanelName = panelName;
    }
    private void SetDialogPanel(TMP_Text _name, Image _icon)
    {
        _name.text = this.npcName;
        _icon.sprite = this.npcIcon;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canTalk = true;
            tipIcon.SetActive(true);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tipIcon.SetActive(false);
            canTalk = false;
        }
    }
}























/*using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class BaseNpc : MonoBehaviour
{
    public GameObject tipIcon;
    [Header("dialog")]
    public string[] dialogContent;
    private bool canTalk = false;

    public void Start()
    {
        tipIcon.SetActive(false);
    }

    private void Update()
    {
        if (canTalk)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Talk();
            }
        }
    }
    int index = 0;
    bool isFirstOpPanel = true;
    
    private void Talk()
    {
*//*        if(isFirstOpPanel)
        {
            //判断第一句话是player or NPC
            if (dialogContent[index].Length == 1 && dialogContent[index]=="P")
            {
                //打开selfPanel
                currentOpenedPanelName = "selfPanel";
                index++;
            }
            if (dialogContent[index].Length == 1 && dialogContent[index] == "N")
            {
                //打开NPCPanel
                currentOpenedPanelName = "NPCPanel";
                index++;
            }
            SwitchPanel(currentOpenedPanelName);
            //设置内容
            contentTmp.text = dialogContent[index];
            index++;
            isFirstOpPanel = false;
        }
        else
        {

        }*//*
    }

    string currentOpenedPanelName;

    *//*public void SwitchPanel(string panelName)
    {
        PanelManager.GetPanelManager().ClosePanel(currentOpenedPanelName);
        GameObject go = PanelManager.GetPanelManager().OpenPanel(panelName);
        foreach(Transform item in go.transform)
        {
            if(item.name=="nameTmp")
            {
                this.nameTmp = item.GetComponent<TMP_Text>();
            }
            else if(item.name=="iconImg")
            {
                this.iconTmp = item.GetComponent<Image>();
            }
            else if (item.name == "ContentImg")
            {
                this.contentTmp = item.GetComponent<TMP_Text>();
            }
        }
    }*//*

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            canTalk = true;
            tipIcon.SetActive(true);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tipIcon.SetActive(false);
            canTalk = false;
        }
    }
}
*/