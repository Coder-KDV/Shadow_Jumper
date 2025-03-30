using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private BoxCollider2D bcol;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bcol = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bcol.enabled = false;
            GameManager.Instance.UpdateRespawn();
            GameManager.Instance.RespawnCharacter();
        }
    }
}
