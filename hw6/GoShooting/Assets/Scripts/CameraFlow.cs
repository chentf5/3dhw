using UnityEngine;
using System.Collections;

public class CameraFlow : MonoBehaviour
{
    public GameObject bow;               
    public float smothing = 5f;          
    Vector3 offset;                     

    void Start()
    {
        offset = transform.position - bow.transform.position;
    }

    void FixedUpdate()
    {
        Vector3 target = bow.transform.position + offset;

        transform.position = Vector3.Lerp(transform.position, target, smothing * Time.deltaTime);
    }
}