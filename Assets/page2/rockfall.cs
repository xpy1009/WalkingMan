using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockfall : MonoBehaviour
{
    public GameObject rock;
    public float falltime = 2.0f;
    float falltimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(falltimer>=0)
            falltimer -= Time.deltaTime;
        if (falltimer < 0){
            falltimer=falltime;
            GameObject projectileObject = Instantiate(rock, transform.position-Vector3.up , Quaternion.identity);
            rock projectile = projectileObject.GetComponent<rock>();
            projectile.Launch(-1*Vector3.up, 2000);
        }
        
    }
}
