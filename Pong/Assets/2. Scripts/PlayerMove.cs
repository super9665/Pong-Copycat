using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾� ������ ���� ��ũ��Ʈ
public class PlayerMove : MonoBehaviour
{
    private PlayerInput playerInput; // �÷��̾� �Է� ���� ��ũ��Ʈ
    private Rigidbody2D playerRigid; // �÷��̾� ������ ������ ���� ������ �ٵ� ������Ʈ

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
