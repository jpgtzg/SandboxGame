// This script manages the placement on blocks 
// based on the calculated Vector3 position on the
// BuildingSystem script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    public Material previewMaterial;

    BuildingSystem buildingSystem;
    ObjectInventory objectInventory;

    public GameObject buildObject;
    GameObject previewPrefab;
    
    Material builtMaterial;

    Vector3 buildPosition;

    void Start()
    {
        buildingSystem = gameObject.GetComponent<BuildingSystem>();
        objectInventory = gameObject.GetComponent<ObjectInventory>();

        CreateNewPrefab();
    }

    void Update()
    {
        //For the inventory
        /*
        GameObject lastBuildObject = buildObject;
        if(buildObject != lastBuildObject)
        {
            createNewPrefab();
        }
        */
        buildPosition = buildingSystem.gridPosition;

        MoveObject(buildPosition);
        PlaceObject(buildPosition);
    }

    public void PlaceObject(Vector3 placePosition)
    {
        if(Input.GetMouseButtonDown(0)) //REFACTOR WITH NEW INPUT SYSTEM
        {
            GameObject createdObject = Instantiate(previewPrefab, placePosition, Quaternion.identity);
            createdObject.GetComponent<BoxCollider>().enabled = true;
            createdObject.GetComponent<Renderer>().material = builtMaterial;
        }
    }

    void MoveObject(Vector3 placePosition)
    {
        previewPrefab.transform.position = placePosition;
    }

    void CreateNewPrefab()
    {
        previewPrefab = Instantiate(buildObject, buildObject.transform.position, Quaternion.identity); //Creates a copy of the building prefab called preview prefab to move
        previewPrefab.GetComponent<BoxCollider>().enabled = false; //Deactivates the collider to avoid physics issues

        builtMaterial = buildObject.GetComponent<Renderer>().material; //Stores the built material as the current material in the building prefab object
        previewPrefab.GetComponent<Renderer>().material = previewMaterial; //Changes the material of the previw prefab object to the preview transparent one
    }
}