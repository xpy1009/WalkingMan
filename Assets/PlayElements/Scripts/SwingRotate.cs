using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingRotate : MonoBehaviour
{
    public Transform ball;
    public Transform chain;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BallRotate();
    }

    private void BallRotate()
    {
        ball.up = (chain.position - ball.position).normalized;
    }

}
