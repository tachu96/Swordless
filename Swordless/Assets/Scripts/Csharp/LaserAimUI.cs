using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class LaserAimUI : MonoBehaviour
{
    private int maxBounces;
    private LineRenderer lineRenderer;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform playerCam;
    [SerializeField] private LayerMask layerMask;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        maxBounces = lineRenderer.positionCount;
        lineRenderer.SetPosition(0,startPoint.position);
        for (int i = 0; i < maxBounces - 1; i++) {
            lineRenderer.SetPosition(i, startPoint.position);
        }
    }

    void Update()
    {
        lineRenderer.SetPosition(0, startPoint.position);
        CastLaser(playerCam.position, playerCam.forward);
    }

    void CastLaser(Vector3 position, Vector3 direction) {
       
        Vector3 lasthit=playerCam.forward*500f;
        for (int i = 0; i < maxBounces; i++)
        {
            Ray ray = new Ray(position, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 500f, layerMask.value))
            {
                lasthit = hit.point;
                position = lasthit;
                direction = Vector3.Reflect(direction, hit.normal);
                if (i + 1 < maxBounces)
                {
                    lineRenderer.SetPosition(i + 1, hit.point);
                }

            }
            else {

                if (i + 1 < maxBounces)
                {
                    lineRenderer.SetPosition(i + 1, lasthit);
                }
                break;
            }

        }
    }
}
