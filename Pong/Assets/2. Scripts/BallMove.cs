using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ������ ���� ��ũ��Ʈ
public class BallMove : MonoBehaviour
{
    public Rigidbody2D ballRigidbody; // �� ������ �ٵ� 
    public float speed = 8f; // �� �ӷ�

    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody2D>(); // ������ �ٵ� ��������

        // ���� ���� �� �� ����Ʈ����
        ballRigidbody.velocity = Vector3.down * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �浹 �� ���� ������ ����
    private void OnCollisionEnter2D(Collision2D collision) {
        // �÷��̾�� �浹�ϸ� �÷��̾��� �����ӿ� ���� ���� ������ �޶���
        if (collision.gameObject.tag == "Player") {
            Rigidbody2D player = collision.gameObject.GetComponent<Rigidbody2D>();
            float playerSpeed = player.velocity.x;
        }
        // �� ���� ������Ʈ�� �浹 �� �״�� �ݻ�
        else {
            ballRigidbody.velocity = getReflectVector(collision, ballRigidbody.velocity); // ���� �浹 ������ �������� �ݻ�
        }
    }


    // �浹 �� �ݻ��� ���� ���ϴ� �Լ�
    private Vector3 getReflectVector(Collision2D collision, Vector3 ballVector) {
        ContactPoint2D[] normalVectors = collision.contacts; // �浹 �������� �������� �迭�� ����
        Vector3 normalVector = Vector3.zero; // ���� �븻 ���� ������ ���� ����

        // �浹 ������ 2���̸� 2���� �߰������� �������� �ݻ� ���͸� ��ȯ
        if (normalVectors.Length == 2) {
            foreach (ContactPoint2D hitInfo in normalVectors) {
                normalVector += (Vector3)hitInfo.point;
            }
            normalVector /= 2; // �� ������ ���� 
            return (Vector3.Reflect(ballVector, normalVector));
        } else {
            return (Vector3.Reflect(ballVector, (Vector3)normalVectors[0].point));
        }
    }
}
