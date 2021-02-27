using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MovementTest : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                agent.destination = hit.point;
                Debug.Log(hit.point);
            }
        }
    }
}
