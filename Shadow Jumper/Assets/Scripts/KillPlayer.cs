using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip teleportSound;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip deathSound;

    [SerializeField]
    private AudioSource audioSource2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.RespawnCharacter();
            PlayDeathSound(deathSound);
            PlaySound(teleportSound);
        }
    }

    private void PlayDeathSound(AudioClip clip)
    {
        audioSource2.clip = clip;
        audioSource2.Play();
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.pitch = 3;
        audioSource.clip = clip;
        audioSource.Play();
        audioSource.pitch = 1;
    }
}
