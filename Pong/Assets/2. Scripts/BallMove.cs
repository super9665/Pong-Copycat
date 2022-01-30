using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 공 움직인 구현 스크립트
public class BallMove : MonoBehaviour
{
    public Rigidbody2D ballRigidboty; // 공 리지드 바디 
    public float speed = 8f; // 공 속력

    void Start()
    {
        ballRigidboty = GetComponent<Rigidbody2D>(); // 리지드 바디 가져오기
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        
    }
}
