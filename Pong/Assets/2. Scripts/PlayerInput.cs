using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // ���� ������
    public float move { get; private set; } // ������ ������ ��

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal"); // A, D �Ǵ� �¿� ȭ��ǥ Ű�� move���� �޾ƿ�( -1, 1)
        if (move != 0) {
            // Debug.Log("�÷��̾� �ӵ� : " + move);
        }
    }
}
