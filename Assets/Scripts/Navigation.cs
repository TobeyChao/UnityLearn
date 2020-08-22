using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    private NavMeshPath navMeshPath;
    private void Start()
    {
        navMeshPath = new NavMeshPath();
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Debug.LogFormat("Mouse Pos:{0}", Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);
            Vector3 dstPos = Vector3.zero;
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.name.Equals("Plane"))
                {
                    navMeshAgent.SetDestination(hit.point);
                    dstPos = hit.point;
                    Debug.LogFormat("Dst Pos:{0}", hit.point);
                }
            }

            NavMesh.CalculatePath(navMeshAgent.gameObject.transform.position, dstPos, NavMesh.AllAreas, navMeshPath);
        }

        for (int i = 0; i < navMeshPath.corners.Length - 1; ++i)
        {
            Debug.DrawLine(navMeshPath.corners[i], navMeshPath.corners[i + 1], Color.red);
        }
    }
}
