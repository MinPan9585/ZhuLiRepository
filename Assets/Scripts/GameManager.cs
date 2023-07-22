using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public LineRenderer lineRenderer;
    public LineRenderer[] lines;
    //private List<Vector2> pointList = new List<Vector2>();
    private Dictionary<int, List<Vector2>> pointLists = new Dictionary<int, List<Vector2>>();
    private int lineIndex;

    private bool isDrawing = false;
    private bool canDraw;
    public LayerMask layerMask;
    public float maxEnergy;
    public float currentEnergy;
    public float[] usedEnergy;

    //bullet time
    /*public class BulletTime : MonoBehaviour
    {
        [SerializeField][Range(0, 1)] float time;

        private void Update()
        {
            Time.timeScale = time;
            Mathf.Lerp()
        }
    }*/

    private void Awake()
    {
        //lineRenderer.positionCount = 0;
        canDraw = true;
        lineIndex = 0;

        List<Vector2> pointListOne = new List<Vector2>();
        List<Vector2> pointListTwo = new List<Vector2>();
        List<Vector2> pointListThree = new List<Vector2>();
        pointLists.Add(0, pointListOne);
        pointLists.Add(1, pointListTwo);
        pointLists.Add(2, pointListThree);
    }

    private void Update()
    {
        if (currentEnergy <= 0)
        {
            canDraw = false;
        }


        if (Input.GetMouseButton(0) && canDraw)
        {
            isDrawing = true;
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (pointLists[lineIndex].Count <= 1 && Physics2D.Raycast(position, Vector3.forward, 100, layerMask))
            {
                return;
            }
            //if (pointLists[lineIndex].Count > 1)
            //{
            //    RaycastHit2D raycast = Physics2D.Raycast(position, (pointLists[lineIndex][lines[lineIndex].positionCount - 1] - position).normalized,
            //    (position - pointLists[lineIndex][lines[lineIndex].positionCount - 1]).magnitude, layerMask);
            //    if (raycast)
            //    {
            //        return;
            //    }
            //}
            if (!pointLists[lineIndex].Contains(position))
            {

                lines[lineIndex].positionCount++;
                lines[lineIndex].SetPosition(lines[lineIndex].positionCount - 1, position);
                pointLists[lineIndex].Add(position);
            }
            print(pointLists[lineIndex].Count);
            if (pointLists[lineIndex].Count > 1)
            {
                Vector2 point1 = pointLists[lineIndex][lines[lineIndex].positionCount - 2];
                Vector2 point2 = pointLists[lineIndex][lines[lineIndex].positionCount - 1];
                GameObject go = new GameObject("Collider");
                go.layer = LayerMask.NameToLayer("Ground");
                go.transform.parent = lines[lineIndex].transform;
                go.transform.localPosition = (point1 + point2) / 2;
                go.AddComponent<BoxCollider2D>();
                go.GetComponent<BoxCollider2D>().size = new Vector2((point2 - point1).magnitude, lines[lineIndex].endWidth);
                go.transform.right = (point1 - point2).normalized;
                if (currentEnergy > 0)
                {
                    currentEnergy -= (point2 - point1).magnitude;
                    usedEnergy[lineIndex] += (point2 - point1).magnitude;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (isDrawing)
            {
                StartCoroutine(DestroyLine(lineIndex));
            }

            for (int i = 0; i < 3; i++)
            {
                if (pointLists[i].Count == 0)
                {
                    lineIndex = i;
                    break;
                }
            }
            if (pointLists[0].Count != 0 && pointLists[1].Count != 0 && pointLists[2].Count != 0)
            {
                canDraw = false;
            }
            isDrawing = false;
            //lineIndex change
            //timer, after relaese 5 seconds, destroy this object, and restore energy


            //canDraw = false;
            //lines[lineIndex].gameObject.AddComponent<Rigidbody2D>();
            //int childrenCount = lines[lineIndex].transform.childCount;
            //for (int i = 0; i < childrenCount; i++)
            //{
            //    lines[lineIndex].transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Ground");
            //}
        }

        IEnumerator DestroyLine(int index)
        {
            yield return new WaitForSeconds(5);
            lines[index].positionCount = 0;
            int childrenCount = lines[index].transform.childCount;
            for (int i = 0; i < childrenCount; i++)
            {
                Destroy(lines[index].transform.GetChild(i).gameObject);
            }
            canDraw = true;
            if (!isDrawing)
            {
                lineIndex = index;
            }

            pointLists[index].Clear();

            currentEnergy += usedEnergy[index];
            usedEnergy[index] = 0;

            //UPDATE LIST AND DICTIONARY!!!!!!!!!!!!!!!!!!!!

            //destroy this object, and restore energy
        }
    }
}




/*using System.Collections;
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
*/