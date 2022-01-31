using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ΩÃ±€≈Ê ∞‘¿” ∏≈¥œ¿˙
public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    [Header("Player & Ball")]
    public GameObject player;
    public Transform playerSpawnPoint;
    public GameObject ball;
    public Transform ballSpawnPoint;

    public static GameManager Instance {
        get {
            if (instance == null) {
                return null;
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }
    }

    private void Start() {
        Instantiate(player, playerSpawnPoint);
        Instantiate(ball, ballSpawnPoint);
    }


    void Update()
    {
        
    }
}
