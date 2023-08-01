using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    //public DrawManager drawManager;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private EdgeCollider2D edgeCollider;

    private List<Vector2> points = new List<Vector2>();
    void Start()
    {
        //drawManager = FindObjectOfType<DrawManager>();
        edgeCollider.transform.position -= transform.position;
        StartCoroutine(DestroyLine());
    }

    public void SetPosition(Vector2 pos)
    {
        if (!CanAppend(pos)) return;
        if (DrawManager.dm.currentEnergy <= 0) return;

        points.Add(pos);

        lineRenderer.positionCount++;
        DrawManager.dm.currentEnergy --;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos);

        edgeCollider.points = points.ToArray();
    }

    private bool CanAppend(Vector2 pos)
    {
        if (lineRenderer.positionCount == 0) return true;

        return Vector2.Distance(lineRenderer.GetPosition(lineRenderer.positionCount - 1), pos) > DrawManager.RESOLUTION;
    }

    IEnumerator DestroyLine()
    {
        yield return new WaitForSeconds(5);
        DrawManager.dm.currentEnergy += lineRenderer.positionCount;
        Destroy(gameObject);
    }
}
