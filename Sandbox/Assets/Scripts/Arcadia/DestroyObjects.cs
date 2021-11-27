// This script manages the destruction of objects
// based on the hit variable from the BuildingSystem
// script

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    BuildingSystem buildingSystem;

    public Material selectedMaterial;

    [SerializeField]
    String pickableTag = "Destroy";

    Material defaultMaterial;

    GameObject currentSelection;

    RaycastHit hit;

    void Start()
    {
        buildingSystem = gameObject.GetComponent<BuildingSystem>();
    }

    void Update()
    {
        //Sincerely, I dont know how is this supposed to work. Needs refactoring
        if(currentSelection != null)
        {
            GameObject selection = hit.collider.gameObject;
            var selectedRenderer = selection.GetComponent<Renderer>();
            selectedRenderer.material = defaultMaterial;
            currentSelection = null;
        }

        hit = buildingSystem.hit;

        bool theresHit = hit.collider != null;
        if (theresHit)
        {
            bool correctObject = hit.collider.CompareTag(pickableTag);
            if (correctObject)
            {
                ChangeMaterial();
                DestroyObject();
            }
        }
    }

    void DestroyObject()
    {
        if (Input.GetMouseButtonDown(0)) //REFACTOR WITH NEW INPUT SYSTEM
        {
            Destroy(hit.collider.gameObject);
        }
    }

    void ChangeMaterial()
    {
        GameObject selection = hit.collider.gameObject;
        var selectedRenderer = selection.GetComponent<Renderer>();
        defaultMaterial = selectedRenderer.material;
        selectedRenderer.material = selectedMaterial;
        currentSelection = selection;
    }
}
