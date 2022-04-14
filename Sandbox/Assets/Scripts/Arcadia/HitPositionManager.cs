// This script manages the calculation of the
// appropiate position in which the player is pointing
// and also calculates the grid position 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPositionManager : MonoBehaviour
{
    [HideInInspector]
    public Vector3 gridPosition;

    [HideInInspector]
    public RaycastHit hit;

    [SerializeField]
    LayerMask groundMask;


    void FixedUpdate()
    {
        hit = SendRay();

        getGridPosition();
    }

    RaycastHit SendRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        
        RaycastHit hit;

        Physics.Raycast(ray, out hit, 6f, groundMask);

        return hit;
    }

    Vector3 CalculateObjectPosition(RaycastHit hit)
    {
        Vector3 calcPos = hit.point + (hit.normal / 2f);

        calcPos = new Vector3(
            Mathf.Round(calcPos.x),
            Mathf.Round(calcPos.y),
            Mathf.Round(calcPos.z));

        return calcPos;
    }

    public Vector3 getGridPosition()
    {
        bool theresHit = hit.collider != null;

        if (theresHit)
        {
            return gridPosition = CalculateObjectPosition(hit);
        }
        else
        {
            return gridPosition = Vector3.zero;
        }
    }
}