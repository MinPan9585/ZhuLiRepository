using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public string[] sentenceTalk;
    public TMP_Text sentenceText;
    public int index;
    void Start()
    {
        index = 0;
        sentenceText.text = sentenceTalk[index];
    }

    // Update is called once per frame
    public void MoveNext()
    {
        index++;
        sentenceText.text = sentenceTalk[index];
    }
}
