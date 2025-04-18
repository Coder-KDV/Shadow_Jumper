using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Rigidbody2D PlayerRigidbody;

    private Vector2 respawnPoint;
    private Vector2 respawnDirection;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
    }

    private void Start()
    {
        respawnPoint = PlayerRigidbody.transform.position;
        respawnDirection = PlayerRigidbody.transform.localScale;
    }

    public void UpdateRespawn()
    {
        respawnPoint = PlayerRigidbody.transform.position;
    }

    public void RespawnCharacter()
    {
        PlayerRigidbody.transform.position = respawnPoint;
        PlayerRigidbody.transform.localScale = respawnDirection;
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
