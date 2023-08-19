using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length,  startpos;
    private float length2, startpos2;
    public GameObject cam;
    public float parallaxEffect;

    public List<GameObject> CH = new List<GameObject>();//储存物体的列表

    // Start is called before the first frame update

    void FindChild(GameObject child)
    {
        //利用for循环 获取物体下的全部子物体
        for (int c = 0; c < child.transform.childCount; c++)
        {
            //如果子物体下还有子物体 就将子物体传入进行回调查找 直到物体没有子物体为止
            if (child.transform.GetChild(c).childCount > 0)
            {
                FindChild(child.transform.GetChild(c).gameObject);

            }
            CH.Add(child.transform.GetChild(c).gameObject);
        }
    }
void Start()
    {
        FindChild(this.gameObject);
        startpos = transform.position.x;
        startpos2 = transform.position.y;
        length = transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x;
        length2= transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void FixedUpdate()
    {
        //float temp = (cam.transform.position.x * (1-parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);
        float dist2= (cam.transform.position.y * parallaxEffect);

        transform.position = new Vector3(startpos + dist, startpos2 + dist2, transform.position.z);

        //if (temp > startpos + length) startpos += length;
        //else if (temp < startpos - length) startpos -= length;
    }
}
