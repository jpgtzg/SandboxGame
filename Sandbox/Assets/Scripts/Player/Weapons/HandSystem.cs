using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSystem : MonoBehaviour
{
    Controls controls;

    public Camera cam;
    public float maxGrabDistance = 4f;
    public Transform objectHolder;

    [HideInInspector] public float pickDistance;
    [HideInInspector] public bool grabbedPressed;

    [HideInInspector] public Rigidbody grabbedObjectRB;

    #region Input System
    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public bool pressedGrabbed()
    {
        return controls.Player.Grab.triggered;
    }

    #endregion

    private void Start()
    {
        pickDistance = maxGrabDistance;
    }

    void Update()
    {
        grabbedPressed = pressedGrabbed();

        objectHolder.transform.localPosition = new Vector3(objectHolder.transform.localPosition.x, objectHolder.transform.localPosition.y, pickDistance);

        if (grabbedPressed)
        {
            if (grabbedObjectRB)
            {
                Release();
            }
            else
            {
                Grab();
            }
        }
        ManageObject();
    }

    void Grab()
    {
        RaycastHit hit;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        if (Physics.Raycast(ray, out hit, maxGrabDistance))
        {
            grabbedObjectRB = hit.collider.GetComponent<Rigidbody>();
            if (grabbedObjectRB)
            {
                grabbedObjectRB.isKinematic = false;
            }
        }
    }

    void Release(bool kinematicValue = false)
    {
        grabbedObjectRB.isKinematic = kinematicValue;
        grabbedObjectRB.freezeRotation = false;
        grabbedObjectRB = null;

    }

    void ManageObject()
    {
        if (grabbedObjectRB)
        {
            grabbedObjectRB.freezeRotation = true;
        }
    }

    private void FixedUpdate()
    {
        if (grabbedObjectRB)
        {
            var targetPosition = objectHolder.position;
            var forceDir = targetPosition - grabbedObjectRB.position;
            var force = forceDir / Time.fixedDeltaTime * 0.3f / grabbedObjectRB.mass;
            grabbedObjectRB.velocity = force;

        }
    }
}
