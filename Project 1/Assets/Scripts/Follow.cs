using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform transformObject;
    private Vector3 distance;
    private float smooth = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        distance = transform.position - transformObject.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = transformObject.position + distance;
        transform.position = Vector3.Slerp(transform.position, newPosition, smooth);
    }
}