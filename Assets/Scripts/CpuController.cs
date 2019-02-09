using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CpuController : MonoBehaviour {
    public Rigidbody2D rigiCpu;
    public float kecepatan;
    Transform Ball;
    Transform Ai;

    void Start()
    {
        Ball = GameObject.FindGameObjectWithTag("Ball").transform;
        Ai = GameObject.FindGameObjectWithTag("Ai").transform;

    }

    void Update()
    {
        ComMovement();
    }

    public void ComMovement()
    {
        if((Ball != null) && (Ai != null))
        {
            if (Ball.position.y > Ai.position.y)
            {
                MoveUp();
            }
            if (Ball.position.y < Ai.position.y)
            {
                MoveDown();
            }
        }
        
    }

    public void MoveUp()
    {
        rigiCpu.AddForce(Vector2.up * kecepatan);
    }

    public void MoveDown()
    {
        rigiCpu.AddForce(Vector2.down * kecepatan);
        
    }
}
