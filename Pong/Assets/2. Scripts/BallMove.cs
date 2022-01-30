using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 공 움직인 구현 스크립트
public class BallMove : MonoBehaviour
{
    public Rigidbody2D ballRigidbody; // 공 리지드 바디 
    public float speed = 8f; // 공 속력

    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody2D>(); // 리지드 바디 가져오기

        // 게임 시작 시 공 떨어트리기
        ballRigidbody.velocity = Vector3.down * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 충돌 시 공의 방향을 결정
    private void OnCollisionEnter2D(Collision2D collision) {
        // 플레이어와 충돌하면 플레이어의 움직임에 따라 공의 방향이 달라짐
        if (collision.gameObject.tag == "Player") {
            Rigidbody2D player = collision.gameObject.GetComponent<Rigidbody2D>();
            float playerSpeed = player.velocity.x;
        }
        // 그 외의 오브젝트와 충돌 시 그대로 반사
        else {
            ballRigidbody.velocity = getReflectVector(collision, ballRigidbody.velocity); // 공을 충돌 지점을 기준으로 반사
        }
    }


    // 충돌 시 반사할 벡터 구하는 함수
    private Vector3 getReflectVector(Collision2D collision, Vector3 ballVector) {
        ContactPoint2D[] normalVectors = collision.contacts; // 충돌 지점들의 정보들을 배열로 저장
        Vector3 normalVector = Vector3.zero; // 최종 노말 벡터 저장할 변수 선언

        // 충돌 지점이 2곳이면 2곳의 중간지점을 기준으로 반사 벡터를 반환
        if (normalVectors.Length == 2) {
            foreach (ContactPoint2D hitInfo in normalVectors) {
                normalVector += (Vector3)hitInfo.point;
            }
            normalVector /= 2; // 두 지점의 중점 
            return (Vector3.Reflect(ballVector, normalVector));
        } else {
            return (Vector3.Reflect(ballVector, (Vector3)normalVectors[0].point));
        }
    }
}
