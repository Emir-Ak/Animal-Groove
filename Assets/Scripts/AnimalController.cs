using UnityEngine;
using System.Collections;
public class AnimalController : MonoBehaviour {

    public float movSpeed = 5f;
    public float rotSpeed = 5f;

    public LayerMask groundLayer;

    float rndInt;

    Quaternion oldRotation;
    Vector3 newPosition;
    Quaternion targetRotation;
    Camera cam;
    Ray mouseRay;

    private void Awake()
    {
        cam = Camera.main;
    }
    private void Start()
    {
        newPosition = transform.position;
        targetRotation = transform.rotation;
        oldRotation = transform.rotation;
    }

    void Update () {
        

		if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out hit, groundLayer))
            {
                newPosition = hit.point;
            }

            rndInt = Random.Range(10, 20);
        }
        if (transform.position != newPosition){
            Plane playerPlane = new Plane(Vector3.up, transform.position);

            float hitdist = 0.0f;
            if (playerPlane.Raycast(mouseRay, out hitdist))
            {
                Vector3 targetPoint = mouseRay.GetPoint(hitdist);

                targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
            }
        }

        if (oldRotation == transform.rotation)
        {

            if (transform.position != newPosition)
            {
                

                float distanceBetween = Vector3.Distance(transform.position, newPosition);
                if (distanceBetween > rndInt)
                {
                    float distanceTime = movSpeed;
                    float relativeTime = distanceTime / distanceBetween;
                    transform.position = Vector3.Lerp(transform.position, newPosition, relativeTime * Time.deltaTime);
                }
            }
        }

        else
        {
            oldRotation = transform.rotation;
        }
    }
}
