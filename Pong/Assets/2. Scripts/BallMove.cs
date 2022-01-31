using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 공 움직인 구현 스크립트
public class BallMove : MonoBehaviour
{
    private Rigidbody2D ballRigidbody; // 공 리지드 바디 
    private Rigidbody2D playerRigidbody; // 플레이어 리지드 바디
    public float speed = 4f; // 공 속력
    Vector2 minAngle = new Vector2(-4f, 1f); // 공이 플레이어와 충돌할 때 왼쪽으로 가장 많이 꺽을 수 있는 각도
    Vector2 maxAngle = new Vector2(4f, 1f); // 공이 플레이어와 충돌할 때 오른쪽으로 가장 많이 꺽을 수 있는 각도
    GameObject lastCollision; // 마지막으로 충돌한 오브젝트
    Vector2 lastVelocity; // 공 현재 속도
    void Start()
    {
        // 리지드 바디 가져오기
        ballRigidbody = GetComponent<Rigidbody2D>();
        playerRigidbody = FindObjectOfType<PlayerMove>().gameObject.GetComponent<Rigidbody2D>();

        // 게임 시작 시 공 떨어트리기
        ballRigidbody.velocity = Vector2.down * speed;
    }

    private void FixedUpdate() {
        // 공 속도를 일정하게 유지
        if (ballRigidbody.velocity.magnitude < speed - 0.5f) {
            ballRigidbody.velocity = lastVelocity.normalized * speed;
        }
        lastVelocity = ballRigidbody.velocity; // 공 속도 지속적으로 저장하여 충돌 시 사용
    }

    // 충돌 시 공의 방향을 결정
    private void OnCollisionEnter2D(Collision2D collision) {
        ContactPoint2D normalContact;

        // 플레이어와 충돌하면 플레이어의 움직임에 따라 공의 방향이 달라짐
        if (collision.gameObject.tag == "Player" && isCollisionWithPlayer(collision, out normalContact)) {
            float playerSpeed = playerRigidbody.velocity.x; // 플레이어의 X축 속도
            if (playerSpeed > 0) { // 플레이어가 오른쪽으로 이동중이면
                Vector2 reflectVector = getReflectVector(normalContact, lastVelocity); // 플레이어와 충돌할 때 원래 반사 방향
                Vector3 finalVector = reflectVector + maxAngle.normalized; // 오른쪽으로 방향 보정
                ballRigidbody.velocity = finalVector.normalized * speed;
            } else if (playerSpeed < 0) { // 플레이어가 왼쪽으로 이동중이면
                Vector2 reflectVector = getReflectVector(normalContact, lastVelocity); // 플레이어와 충돌할 때 원래 반사 방향
                Vector3 finalVector = reflectVector + minAngle.normalized; // 왼쪽으로 방향 보정
                ballRigidbody.velocity = finalVector.normalized * speed;
            } else { // 플레이어가 정지상태면
                Vector2 reflectVector = getReflectVector(normalContact, lastVelocity); // 플레이어와 충돌할 때 원래 반사 방향
                ballRigidbody.velocity = reflectVector * speed; // 벽에 부딪힌것과 동일하게 반사
            }
        }
        // 그 외의 오브젝트와 충돌 시 그대로 반사
        else {
            Vector2 reflectVector = getReflectVector(collision.contacts[0], lastVelocity); // 플레이어와 충돌할 때 원래 반사 방향
            ballRigidbody.velocity = reflectVector * speed; // 공을 충돌 지점을 기준으로 반사
        }
    }


    // 충돌 시 반사할 벡터 구하는 함수
    private Vector2 getReflectVector(ContactPoint2D normalContact, Vector2 ballVector) { // (반사를 위한 기준 contact, 공 velocity)
        Debug.Log("충돌, " + normalContact.normal + ballVector);
        return Vector2.Reflect(ballVector.normalized, normalContact.normal);
    }

    // 공이 플레이어의 윗면과 충돌했는지 여부 확인
    private bool isCollisionWithPlayer(Collision2D collision, out ContactPoint2D normalContact) { // (충돌 대상, 노말 벡터가 Vector2.up인 contact의 contacts에서의 인덱스)
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
