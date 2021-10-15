using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour
{
    public Controls controls;

    [SerializeField] Camera cam;
    [SerializeField] float maxGrabDistance = 10f, minGrabDistance = 1f;
    [SerializeField] Transform objectHolder;

    [SerializeField] float throwForce = 20f;

    [SerializeField] float rotationSpeed;

    float pickDistance;
    bool throwObject;
    bool grabbedPressed;
    bool freezePressed;

    Rigidbody grabbedObjectRB;

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

    public bool throwItem()
    {
        return controls.Player.LeftMouseAction.triggered;
    }

    public bool freezeItem()
    {
        return controls.Player.RightMouseAction.triggered;
    }

    #endregion

    private void Start()
    {
        pickDistance = objectHolder.localPosition.z;
    }

    void Update()
    {
        grabbedPressed = pressedGrabbed();
        throwObject = throwItem();
        freezePressed = freezeItem();

        objectHolder.transform.localPosition = new Vector3(objectHolder.transform.localPosition.x, objectHolder.transform.localPosition.y, pickDistance);

        if (grabbedPressed)
        {
            if(grabbedObjectRB)
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
            pickDistance = Mathf.Clamp(pickDistance + Input.mouseScrollDelta.y, minGrabDistance, maxGrabDistance);

            grabbedObjectRB.freezeRotation = true;

            if (throwObject)
            {
                Throw();
            }

            if (freezePressed)
            {
               Release(true); 
            }
            if(Input.GetKeyDown(KeyCode.R))
            {
                Duplicate();
            }
        }
    }

    void Duplicate()
    {
        GameObject currentObject = grabbedObjectRB.gameObject;
        GameObject newObject = Instantiate(currentObject, objectHolder.position, grabbedObjectRB.rotation);
        Release(true);
        grabbedObjectRB = newObject.GetComponent<Rigidbody>();
    }

    void Throw()
    {
        grabbedObjectRB.isKinematic = false;
        grabbedObjectRB.freezeRotation = false;
        grabbedObjectRB.AddForce(cam.transform.forward * throwForce, ForceMode.VelocityChange);
        grabbedObjectRB = null;
    }

    private void FixedUpdate()
    {
        if (grabbedObjectRB)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                grabbedObjectRB.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.E))
            {
                grabbedObjectRB.transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);
            }

            var targetPosition = objectHolder.position;
            var forceDir = targetPosition - grabbedObjectRB.position;
            var force = forceDir / Time.fixedDeltaTime * 0.3f / grabbedObjectRB.mass;
            grabbedObjectRB.velocity = force;   
            
        }
    }
}
