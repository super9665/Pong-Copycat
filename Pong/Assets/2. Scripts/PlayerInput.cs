using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // 수평 움직임
    public float move { get; private set; } // 감지된 움직임 값

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal"); // A, D 또는 좌우 화살표 키로 move값을 받아옴( -1, 1)
        if (move != 0) {
            // Debug.Log("플레이어 속도 : " + move);
        }
    }
}
