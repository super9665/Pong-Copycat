using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ������ ���� ��ũ��Ʈ
public class BallMove : MonoBehaviour
{
    public Rigidbody2D ballRigidboty; // �� ������ �ٵ� 
    public float speed = 8f; // �� �ӷ�

    void Start()
    {
        ballRigidboty = GetComponent<Rigidbody2D>(); // ������ �ٵ� ��������
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        
    }
}
