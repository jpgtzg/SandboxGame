// This script manages the placement on blocks 
// based on the calculated Vector3 position on the
// BuildingSystem script

/*
    Ocupo optimizar la destrucción de los objectos mediante la aplicación del sistema de "object pooling"
    Por el momento el sistema funciona, pero es increiblemente ineficiente. No se puede compartir esta clase de codigo.
    Lo mismo pasa con la construcción de objectos. El sistema de "object pooling" sirve tanto para la creación como para
    la destrucción de objetos. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    HitPositionManager hitPositionManager;
    ObjectInventory objectInventory;
    BuildingSystem buildingSystem;

    public Material previewMaterial;

    public GameObject buildObject;

    public bool scriptActive;

    GameObject previewPrefab;

    Material builtMaterial;

    Vector3 buildPosition;

    bool placeObjectBool;

    void Start()
    {
        buildingSystem = GetComponent<BuildingSystem>();
        hitPositionManager = buildingSystem.hitPositionManager;
        objectInventory = buildingSystem.objectInventory;

        CreateNewPrefab();
    }

    void Update()
    {
        if(scriptActive)
        {
            previewPrefab.SetActive(true);

            placeObjectBool = buildingSystem.LeftMouseAction();
         
            buildPosition = hitPositionManager.getGridPosition();

            if(buildPosition == Vector3.zero)
            {
                previewPrefab.SetActive(false);
            }
            else
            {
                previewPrefab.SetActive(true);

                Debug.Log(buildPosition);

                MoveObject(buildPosition);
                PlaceObject(buildPosition);
            }
        }
        else
        {
            previewPrefab.SetActive(false);
        }
    }

    public void PlaceObject(Vector3 placePosition)
    {
        if(placeObjectBool) 
        {
            GameObject createdObject = Instantiate(previewPrefab, placePosition, Quaternion.identity);
            createdObject.GetComponent<BoxCollider>().enabled = true;
            createdObject.GetComponent<Renderer>().sharedMaterial = builtMaterial;
        }
    }

    void MoveObject(Vector3 buildPosition)
    {
        previewPrefab.transform.position = buildPosition;
    }

    void CreateNewPrefab()
    {
        previewPrefab = Instantiate(buildObject, buildObject.transform.position, Quaternion.identity); //Creates a copy of the building prefab called preview prefab to move
        previewPrefab.GetComponent<BoxCollider>().enabled = false; //Deactivates the collider to avoid physics issues

        builtMaterial = previewPrefab.GetComponent<Renderer>().sharedMaterial; //Stores the built material as the current material in the building prefab object
        previewPrefab.GetComponent<Renderer>().sharedMaterial = previewMaterial; //Changes the material of the previw prefab object to the preview transparent one
    }
}