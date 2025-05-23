using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        
    }

    public UIManager uiManager;
    public Player player;

    void Start()
    {
        uiManager ??= UIManager.Instance;
        player ??= FindObjectOfType<Player>();

        player.Init();
        uiManager.Init();

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (player.stat is IDamageable stat)
            {
                stat.TakeDamage(25);
            }
        }
    }
}
