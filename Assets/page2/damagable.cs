using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damagable : MonoBehaviour
{
    public GameObject hook;
    void OnCollisionEnter2D(Collision2D other){
        if(other.collider.GetComponent<rock>())
            Destroy(hook);
    } 
        
    
}
