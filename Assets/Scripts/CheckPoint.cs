using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    public GameObject LE;
    /*void ChangeSprite()
    {
        spriteRenderer.sprite = newSprite;
    }*/
    private SaveManager sm;

    private void Start()
    {
        sm = GameObject.FindGameObjectWithTag("SM").GetComponent<SaveManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            sm.lastPosition = transform.position;

            spriteRenderer.sprite = newSprite;
            Instantiate(LE, transform.position, Quaternion.identity);
            SaveManager.Instance.lastPosition=transform.position;
        }
    }
}
