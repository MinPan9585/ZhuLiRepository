using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    [Header("UI组件")]
    public TMP_Text textLabel;
    //public TMP_FontAsset font;
    public Image leftImage;
    public Image rightImage;
    public Image tips;

    [Header("文本文件")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    [Header("头像")]
    public Sprite tips0, tips1, tips2, faceL0, faceL1, faceR0, faceR1, faceR2, faceR3;


    bool textFinished;

    //private Vector3 targetPosition;

    List<string> textList = new List<string>();
    void Awake()
    {
        GetTextFormFile(textFile);
        //targetPosition = new Vector3(1, 1, 0);
    }

    private void OnEnable()
    {
        /*textLabel.text = textList[index];
        index++;*/
        textFinished = true;
        StartCoroutine(SetTextUI());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&&index==textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }

        if (Input.GetKeyDown(KeyCode.E)&&textFinished)
        {
            /*textLabel.text = textList[index];
            index++;*/
            StartCoroutine(SetTextUI());
            //leftImage.transform.position = Vector3.Lerp(leftImage.transform.position, targetPosition, 0.001f);
        }
    }





    void GetTextFormFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var lineData=file.text.Split('\n');
        foreach(var line in lineData)
        {
            textList.Add(line);
        }
    }

    IEnumerator SetTextUI()
    {
        textFinished = false;
        textLabel.text = "";

        switch(textList[index])
        {
            case"A":
                leftImage.sprite = faceL1;
                rightImage.sprite = faceR0;
                tips.sprite = tips0;
                index++;
                break;
            case "B":
                leftImage.sprite = faceL0;
                rightImage.sprite = faceR1;
                tips.sprite = tips0;
                index++;
                break;
            case "C":
                leftImage.sprite = faceL0;
                rightImage.sprite = faceR1;
                tips.sprite = tips1;
                index++;
                break;
            case "D":
                leftImage.sprite = faceL0;
                rightImage.sprite = faceR2;
                tips.sprite = tips0;
                index++;
                break;
            case "E":
                leftImage.sprite = faceL0;
                rightImage.sprite = faceR3;
                tips.sprite = tips0;
                index++;
                break;

        }

        for(int i = 0; i < textList[index].Length;i++)
        {
            textLabel.text += textList[index][i];
            yield return new WaitForSeconds(textSpeed);
        }
        textFinished = true;
        index++;
    }
}
