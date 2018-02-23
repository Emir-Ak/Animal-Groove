using System.Collections;
using UnityEngine;

public class RatController : MonoBehaviour
{

    private Vector3 mousePosition;
    public float movementSpeed = 1;
    public float rotationSpeed = 1;
    public bool isFromGroup = true;

    Quaternion toRotation = new Quaternion();

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition) - new Vector3(0, 10, 0);
        }

        Vector3 direction = mousePosition - transform.position;
        toRotation = Quaternion.FromToRotation(transform.forward, new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        if (transform.position != mousePosition)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(mousePosition.x, 0, mousePosition.z), movementSpeed * Time.deltaTime);
        }
    }

}
