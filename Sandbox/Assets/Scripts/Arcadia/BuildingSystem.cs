using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField] GameObject selectedObject;

    [SerializeField] Transform rayCastOrigin;

    [SerializeField]
    LayerMask buildLayer;

    RaycastHit hit;
    Vector3 buildPosition;

    void Update()
    {
        if(Physics.Raycast(rayCastOrigin.position, rayCastOrigin.forward, out hit, 6f, buildLayer))
        {
            if(hit.transform != this.transform)
            {
                previewObject(hit);
            }
        }
    }

    void previewObject(RaycastHit hit2)
    {
        float step = 5f * Time.deltaTime;
        buildPosition = new Vector3(Mathf.RoundToInt(hit.point.x), hit.point.y, Mathf.RoundToInt(hit.point.z));
        selectedObject.transform.position = Vector3.MoveTowards(selectedObject.transform.position, buildPosition, step);

        if (Vector3.Distance(transform.position, selectedObject.transform.position) < 0.001f)
        {
            selectedObject.transform.position *= -1.0f;
        }
    }
}
