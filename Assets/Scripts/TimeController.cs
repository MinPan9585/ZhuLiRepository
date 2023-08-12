using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController tc;

    public float defaultFixedDeltaTime=1f;

    public float bulletTimeScale = 1f;

    private void Awake()
    {
        tc = this;
        defaultFixedDeltaTime = Time.fixedDeltaTime;
    }

    private void Update()
    {
        Time.timeScale = bulletTimeScale;
    }


    /*public void BulletTime()
    {
        Time.timeScale = bulletTimeScale;

    }*/
}
