using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 움직임 제어 스크립트
public class PlayerMove : MonoBehaviour
{
    private PlayerInput playerInput; // 플레이어 입력 감지 스크립트
    private Rigidbody2D playerRigid; // 플레이어 움직임 구현을 위한 리지드 바디 컴포넌트

    public float playerSpeed = 8f;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        playerRigid.velocity = Vector3.right * playerInput.move * playerSpeed;
        // playerRigid.velocity = Vector3.right * 8 * Time.fixedDeltaTime;
    }
}
