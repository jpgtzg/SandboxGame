using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    public Material previewMaterial;

    BuildingSystem buildingSystem;
    ObjectInventory objectInventory;

    GameObject buildObject;
    GameObject previewPrefab;
    
    Material builtMaterial;

    void Start()
    {
        buildingSystem = gameObject.GetComponent<BuildingSystem>();
        objectInventory = gameObject.GetComponent<ObjectInventory>();

        previewPrefab = Instantiate(buildObject, buildObject.transform.position, Quaternion.identity); //Creates a copy of the building prefab called preview prefab to move
        previewPrefab.GetComponent<BoxCollider>().enabled = false; //Deactivates the collider to avoid physics issues

        builtMaterial = buildObject.GetComponent<Renderer>().material; //Stores the built material as the current material in the building prefab object
        previewPrefab.GetComponent<Renderer>().material = previewMaterial; //Changes the material of the previw prefab object to the preview transparent one
        
        buildObject.SetActive(false); //Destroys the building prefab for simplicity 
    }

    void Update()
    {
        
    }

    public void placeObject(Vector3 placePosition)
    {
        if(Input.GetMouseButton(0)) //REFACTOR WITH NEW INPUT SYSTEM
        {

        }
    }

    void moveObject(Vector3 placePosition)
    {

    }
}
