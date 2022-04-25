//General manager for the Arcadia Building System

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : InputManager
{
    //References to other Arcadia Scripts
    [HideInInspector] public DestroyObjects destroyObjects;
    [HideInInspector] public PlaceObjects placeObjects;

    [HideInInspector] public ObjectInventory objectInventory;
    [HideInInspector] public HitPositionManager hitPositionManager;

    public State state;

    bool changeState;

    private void Awake()
    {
        destroyObjects = GetComponent<DestroyObjects>();
        placeObjects = GetComponent<PlaceObjects>();
        objectInventory = GetComponent<ObjectInventory>();
        hitPositionManager = GetComponent<HitPositionManager>();
    }

    public enum State
    {
        PlaceObject,
        DestroyObject
    }

    void Start()
    {
        
    }

    void Update()
    {
        switch (state)
        {
            case State.PlaceObject:
                placeObjects.scriptActive = true;
                destroyObjects.scriptActive = false;
                break;
            case State.DestroyObject:
                placeObjects.scriptActive = false;
                destroyObjects.scriptActive = true;
                break;
        }

        changeState = FAction3();

        if (changeState)
        {
            if(state == State.PlaceObject)
            {
                state = State.DestroyObject;
            }
            else if(state == State.DestroyObject)
            {
                state = State.PlaceObject;
            }
        }
    }
}
