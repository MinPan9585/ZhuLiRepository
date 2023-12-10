using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager1 : MonoBehaviour
{
    public static DrawManager1 dm;
    private void Awake()
    {
        dm = this;
    }

    //public float maxEnergy;
    public float currentEnergy;
    public float smoothing;

    public Camera cam;
    public CinemachineVirtualCameraBase cine;
    [SerializeField] private Line linePrefab;

    public const float RESOLUTION = 0.1f;

    private Line currentLine;

    //public GameObject drawing;

    void Start()
    {
        //cam = Camera.main;
    }

    void Update()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            //Instantiate(drawing, transform.position, Quaternion.identity);
            currentLine = Instantiate(linePrefab, mousePos, Quaternion.identity);
            currentLine.gameObject.layer = LayerMask.NameToLayer("Ground");
        }

        if (Input.GetMouseButton(0))
        {
            currentLine.SetPosition(mousePos);
        }
    }
}