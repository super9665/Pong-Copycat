using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ������ ���� ��ũ��Ʈ
public class BallMove : MonoBehaviour
{
    private Rigidbody2D ballRigidbody; // �� ������ �ٵ� 
    private Rigidbody2D playerRigidbody; // �÷��̾� ������ �ٵ�
    public float speed = 4f; // �� �ӷ�
    Vector2 minAngle = new Vector2(-4f, 1f); // ���� �÷��̾�� �浹�� �� �������� ���� ���� ���� �� �ִ� ����
    Vector2 maxAngle = new Vector2(4f, 1f); // ���� �÷��̾�� �浹�� �� ���������� ���� ���� ���� �� �ִ� ����
    GameObject lastCollision; // ���������� �浹�� ������Ʈ
    Vector2 lastVelocity; // �� ���� �ӵ�
    void Start()
    {
        // ������ �ٵ� ��������
        ballRigidbody = GetComponent<Rigidbody2D>();
        playerRigidbody = FindObjectOfType<PlayerMove>().gameObject.GetComponent<Rigidbody2D>();

        // ���� ���� �� �� ����Ʈ����
        ballRigidbody.velocity = Vector2.down * speed;
    }

    private void FixedUpdate() {
        // �� �ӵ��� �����ϰ� ����
        if (ballRigidbody.velocity.magnitude < speed - 0.5f) {
            ballRigidbody.velocity = lastVelocity.normalized * speed;
        }
        lastVelocity = ballRigidbody.velocity; // �� �ӵ� ���������� �����Ͽ� �浹 �� ���
    }

    // �浹 �� ���� ������ ����
    private void OnCollisionEnter2D(Collision2D collision) {
        ContactPoint2D normalContact;

        // �÷��̾�� �浹�ϸ� �÷��̾��� �����ӿ� ���� ���� ������ �޶���
        if (collision.gameObject.tag == "Player" && isCollisionWithPlayer(collision, out normalContact)) {
            float playerSpeed = playerRigidbody.velocity.x; // �÷��̾��� X�� �ӵ�
            if (playerSpeed > 0) { // �÷��̾ ���������� �̵����̸�
                Vector2 reflectVector = getReflectVector(normalContact, lastVelocity); // �÷��̾�� �浹�� �� ���� �ݻ� ����
                Vector3 finalVector = reflectVector + maxAngle.normalized; // ���������� ���� ����
                ballRigidbody.velocity = finalVector.normalized * speed;
            } else if (playerSpeed < 0) { // �÷��̾ �������� �̵����̸�
                Vector2 reflectVector = getReflectVector(normalContact, lastVelocity); // �÷��̾�� �浹�� �� ���� �ݻ� ����
                Vector3 finalVector = reflectVector + minAngle.normalized; // �������� ���� ����
                ballRigidbody.velocity = finalVector.normalized * speed;
            } else { // �÷��̾ �������¸�
                Vector2 reflectVector = getReflectVector(normalContact, lastVelocity); // �÷��̾�� �浹�� �� ���� �ݻ� ����
                ballRigidbody.velocity = reflectVector * speed; // ���� �ε����Ͱ� �����ϰ� �ݻ�
            }
        }
        // �� ���� ������Ʈ�� �浹 �� �״�� �ݻ�
        else {
            Vector2 reflectVector = getReflectVector(collision.contacts[0], lastVelocity); // �÷��̾�� �浹�� �� ���� �ݻ� ����
            ballRigidbody.velocity = reflectVector * speed; // ���� �浹 ������ �������� �ݻ�
        }
    }


    // �浹 �� �ݻ��� ���� ���ϴ� �Լ�
    private Vector2 getReflectVector(ContactPoint2D normalContact, Vector2 ballVector) { // (�ݻ縦 ���� ���� contact, �� velocity)
        Debug.Log("�浹, " + normalContact.normal + ballVector);
        return Vector2.Reflect(ballVector.normalized, normalContact.normal);
    }

    // ���� �÷��̾��� ����� �浹�ߴ��� ���� Ȯ��
    private bool isCollisionWithPlayer(Collision2D collision, out ContactPoint2D normalContact) { // (�浹 ���, �븻 ���Ͱ� Vector2.up�� contact�� contacts������ �ε���)
        foreach (ContactPoint2D contact in collision.contacts) {
            if (contact.normal == Vector2.up) {
                normalContact = contact;
                return true;
            }
        }
        normalContact = collision.contacts[0];
        return false;
    }
}
