using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGscript : MonoBehaviour
{
    private float length, startpos;
    public GameObject cam;
    public float parallelfx; void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

    }

    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallelfx));
        float dist = (cam.transform.position.x * parallelfx);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;

    }
}
