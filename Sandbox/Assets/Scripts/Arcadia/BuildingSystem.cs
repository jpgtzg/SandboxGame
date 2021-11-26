using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public GameObject buildingPrefab;
    public Material previewMaterial;

    [SerializeField]
    LayerMask groundMask;

    Material builtMaterial;

    GameObject previewPrefab;

    Vector3 gridPosition;

    void Start()
    {
        previewPrefab = Instantiate(buildingPrefab, buildingPrefab.transform.position, Quaternion.identity);
        previewPrefab.GetComponent<BoxCollider>().enabled = false;

        builtMaterial = buildingPrefab.GetComponent<Renderer>().material;
        previewPrefab.GetComponent<Renderer>().material = previewMaterial;

        buildingPrefab.SetActive(false);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));

        RaycastHit hit;

        Physics.Raycast(ray, out hit, 6f, groundMask);

        Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);

        gridPosition = hit.point + (hit.normal / 2f);

        gridPosition = new Vector3(
            Mathf.Round(gridPosition.x),
            Mathf.Round(gridPosition.y),
            Mathf.Round(gridPosition.z));

        previewPrefab.transform.position = gridPosition;

        if (Input.GetMouseButtonDown(0))
        {
            GameObject createdObject = Instantiate(previewPrefab, gridPosition, Quaternion.identity);
            createdObject.GetComponent<BoxCollider>().enabled = true;
            createdObject.GetComponent<Renderer>().material = builtMaterial;
        }
    }
}