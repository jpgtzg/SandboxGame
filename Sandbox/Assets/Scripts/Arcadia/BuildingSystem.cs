// This script manages the calculation of the
// appropiate position in which the player is pointing

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

    RaycastHit hit;

    void Start()
    {
        previewPrefab = Instantiate(buildingPrefab, buildingPrefab.transform.position, Quaternion.identity); //Creates a copy of the building prefab called preview prefab to move
        previewPrefab.GetComponent<BoxCollider>().enabled = false; //Deactivates the collider to avoid physics issues

        builtMaterial = buildingPrefab.GetComponent<Renderer>().material; //Stores the built material as the current material in the building prefab object
        previewPrefab.GetComponent<Renderer>().material = previewMaterial; //Changes the material of the previw prefab object to the preview transparent one

        buildingPrefab.SetActive(false); //Destroys the building prefab for simplicity 
    }

    void Update()
    {
        hit = sendRay();

        bool theresHit = hit.collider != null;

        if(theresHit)
        {
            gridPosition = calculateOjectPosition(hit);
            moveObject(gridPosition);
            placeObject(gridPosition);
        }
    }

    RaycastHit sendRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        
        RaycastHit hit;

        Physics.Raycast(ray, out hit, 6f, groundMask);

        return hit;
    }

    Vector3 calculateOjectPosition(RaycastHit hit)
    {
        Vector3 calcPos = hit.point + (hit.normal / 2f);

        calcPos = new Vector3(
            Mathf.Round(calcPos.x),
            Mathf.Round(calcPos.y),
            Mathf.Round(calcPos.z));

        return calcPos;
    }

    void placeObject(Vector3 placePosition)
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject createdObject = Instantiate(previewPrefab, placePosition, Quaternion.identity);
            createdObject.GetComponent<BoxCollider>().enabled = true;
            createdObject.GetComponent<Renderer>().material = builtMaterial;
        }
    }

    void moveObject(Vector3 placePosition)
    {
        previewPrefab.transform.position = placePosition;
    }
}