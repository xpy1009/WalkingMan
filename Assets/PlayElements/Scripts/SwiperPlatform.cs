using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwiperPlatform : MonoBehaviour
{
    public float speed;
    public Transform startPoint;
    public Transform endPoint;
    public Transform platform;
    private bool reverse = false;
    private Vector3 direction;
    private Vector3 reverseDirection;

    // Start is called before the first frame update
    void Start()
    {
        reverse = false;
        if (startPoint == null)
        {
            startPoint = platform;
        }
        direction = (endPoint.position - startPoint.position).normalized;
        reverseDirection = -direction;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (reverse)
        {
            platform.position += reverseDirection * speed * Time.fixedDeltaTime;
            if (Vector3.Distance(platform.position, startPoint.position) < 0.1f)
            {
                reverse = false;
            }
        }
        else
        {
            platform.position += direction * speed * Time.fixedDeltaTime;
            if (Vector3.Distance(platform.position, endPoint.position) < 0.1f)
            {
                reverse = true;
            }
        }
    }

}
