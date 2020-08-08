using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_camera : MonoBehaviour
{

    public Transform alvo;
    public float suavizar = 5f;

    Vector3 offset;

    void Start()
    {

        offset = transform.position - alvo.position;

    }

    void Update()
    {

        Vector3 alvoCamPos = alvo.position + offset;

        transform.position = Vector3.Lerp(transform.position, alvoCamPos, suavizar * Time.deltaTime);

    }
}
