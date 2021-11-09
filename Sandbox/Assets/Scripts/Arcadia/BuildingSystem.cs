using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public GameObject buildingPrefab;

    public float gridDistance = 2f;

    public LayerMask buildLayer;

    float gridSize;

    GameObject previewPrefab;

    Vector3 gridPosition;
    Vector3 worldPosition;

    void Start()
    {
        worldPosition = transform.position;

        gridSize = 1f / gridDistance;

        previewPrefab = Instantiate(buildingPrefab, buildingPrefab.transform.position, Quaternion.identity);

        previewPrefab.GetComponent<BoxCollider>().enabled = false;
    }

    void Update()
    {
        gridSize = 1f / gridDistance;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));

        RaycastHit hit;

        Physics.Raycast(ray, out hit, 6f, buildLayer);

        worldPosition = hit.point + hit.normal.normalized;

        //Vector3 distanceVec = hit.point - Camera.main.transform.position;
        //worldPosition -= distanceVec.normalized * (distanceVec.magnitude - 1f);

        gridPosition = new Vector3(
        Mathf.Round(worldPosition.x * gridSize) / gridSize,
        Mathf.Round(worldPosition.y * gridSize) / gridSize,
        Mathf.Round(worldPosition.z * gridSize) / gridSize);

        //Vector3 newPos = hit.point + hit.normal.normalized;

        //gridPosition += newPos;

        transform.position = gridPosition;

        previewPrefab.transform.position = gridPosition;

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(buildingPrefab, gridPosition, Quaternion.identity);
        }
    }

    /*
    [SerializeField] GameObject selectedObject;

    [SerializeField] Transform rayCastOrigin;

    [SerializeField]
    LayerMask buildLayer;

    RaycastHit hit;
    Vector3 buildPosition;

    void Update()
    {
        if(Physics.Raycast(rayCastOrigin.position, rayCastOrigin.forward, out hit, 6f, buildLayer))
        {
            if(hit.transform != this.transform)
            {
                previewObject(hit);
            }
        }
    }

    void previewObject(RaycastHit hit2)
    {
        float step = 10f * Time.deltaTime;
        buildPosition = new Vector3(Mathf.RoundToInt(hit.point.x), hit.point.y, Mathf.RoundToInt(hit.point.z));
        selectedObject.transform.position = buildPosition; //Vector3.MoveTowards(selectedObject.transform.position, buildPosition, step);

        if (Vector3.Distance(transform.position, selectedObject.transform.position) < 0.001f)
        {
            selectedObject.transform.position *= -1.0f;
        }
    }
    */
}
