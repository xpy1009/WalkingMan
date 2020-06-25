using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLight : MonoBehaviour
{
    public UnityEngine.Experimental.Rendering.Universal.Light2D m_light2D = null;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            m_light2D.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player")
        {
            m_light2D.enabled = false;
        }
    }

}
