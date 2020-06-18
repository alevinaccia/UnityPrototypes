using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public Transform target;
    [Range(0.1f, 1.0f)]
    public float speed;
    public Vector3 offset;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 newPosition = Vector3.Lerp(transform.position, desiredPosition, speed * Time.deltaTime * target.position.magnitude);
        transform.position = newPosition;
    }
}
