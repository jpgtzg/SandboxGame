// This script manages the destruction of objects
// based on the hit variable from the BuildingSystem
// script

/*
    Ocupo optimizar la destrucción de los objectos mediante la aplicación del sistema de "object pooling"
    Por el momento el sistema funciona, pero es increiblemente ineficiente. No se puede compartir esta clase de codigo.
    Lo mismo pasa con la construcción de objectos. El sistema de "object pooling" sirve tanto para la creación como para
    la destrucción de objetos. 
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    HitPositionManager hitPositionManager;
    BuildingSystem buildingSystem;

    public Material selectedMaterial;

    public bool scriptActive;

    [SerializeField]
    String pickableTag = "Destroy";

    Material defaultMaterial;

    GameObject currentSelection;

    RaycastHit hit;

    GameObject lastSelection;

    bool destroyObjectBool;

    void Start()
    {
        buildingSystem = GetComponent<BuildingSystem>();
        hitPositionManager = buildingSystem.hitPositionManager;
    }

    void Update()
    {
        if(scriptActive)
        {
            destroyObjectBool = buildingSystem.LeftMouseAction();

            if (lastSelection != currentSelection)
            {
                GameObject selection = hit.collider.gameObject;
                var selectedRenderer = selection.GetComponent<Renderer>();
                selectedRenderer.material = defaultMaterial;
                currentSelection = null;
            }

            lastSelection = currentSelection;

            hit = hitPositionManager.hit;

            bool theresHit = hit.collider != null;
            if (theresHit)
            {
                bool correctObject = hit.collider.CompareTag(pickableTag);
                if (correctObject)
                {
                    ChangeMaterial();
                    DestroyObject();
                }
                else
                    currentSelection = null;
            }
        }
        else
        {
            if(hit.collider != null)
            {
                bool correctObject = hit.collider.CompareTag(pickableTag);
                if (correctObject)
                {
                    GameObject selection = hit.collider.gameObject;
                    var selectedRenderer = selection.GetComponent<Renderer>();
                    selectedRenderer.material = defaultMaterial;
                }
            }
        
        }
    }

    void DestroyObject()
    {
        if (destroyObjectBool) 
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
