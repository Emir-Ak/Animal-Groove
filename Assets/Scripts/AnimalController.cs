using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalController : MonoBehaviour {

    Camera cam;

    public LayerMask groundLayer;

    public NavMeshAgent animalAgent;

    private void Awake()
    {
        cam = Camera.main;
    }

    void Update () {
		if (Input.GetMouseButton(1))
        {
            animalAgent.SetDestination(GroundPointFromMouse());
        }
	}

    Vector3 GroundPointFromMouse()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(mouseScreenPosition);

        RaycastHit hit;

        Physics.Raycast(mouseWorldPosition, cam.transform.forward, out hit, 100, groundLayer);
        return hit.point;
    }
}
