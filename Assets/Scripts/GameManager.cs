using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LineRenderer lineRenderer;
    private List<Vector2> pointList = new List<Vector2>();
    //是否可以画线
    private bool canDraw;
    public LayerMask layerMask;
    //线的能量值
    public float maxEnergy;
    public float currentEnergy;

    private void Awake()
    {
        lineRenderer.positionCount = 0;
        canDraw = true;
    }
    private void Update()
    {
        
        //能量够不够画线
        if (currentEnergy<=0)
        {
            canDraw = false;
        }

        if(Input.GetMouseButton(0)&&canDraw)
        {
            Vector2 position=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //检测是否为不可画线的地方
            if(pointList.Count<=1&&Physics2D.Raycast(position,Vector3.forward,100,layerMask))
            {
                return;
            }
            if(pointList.Count>1)
            {
                RaycastHit2D raycast = Physics2D.Raycast(position, (pointList[lineRenderer.positionCount - 1] - position).normalized,
                (position - pointList[lineRenderer.positionCount - 1]).magnitude, layerMask);
                if(raycast)
                {
                    return;
                }
            }
            
            //画线
            if(!pointList.Contains(position))
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, position);
                pointList.Add(position);
            }
            //添加物理效果
            if(pointList.Count > 1)
            {
                Vector2 point1 = pointList[lineRenderer.positionCount - 2];
                Vector2 point2 = pointList[lineRenderer.positionCount - 1];

                GameObject go = new GameObject("Collider");
                go.transform.parent = lineRenderer.transform;
                go.transform.localPosition = (point1 + point2) / 2;
                go.AddComponent<BoxCollider2D>();
                go.GetComponent<BoxCollider2D>().size = new Vector2((point2 - point1).magnitude,lineRenderer.endWidth);
                go.transform.right = (point1 - point2).normalized;

                //能量消耗
                if (currentEnergy > 0)
                {
                    currentEnergy -= (point2 - point1).magnitude;
                }
            }

        }
        
        //生成刚体
        if(Input.GetMouseButtonUp(0))
        {
            canDraw = false;
            lineRenderer.gameObject.AddComponent<Rigidbody2D>();
            lineRenderer.gameObject.layer = LayerMask.NameToLayer("layerMask");
        }
    }

}
