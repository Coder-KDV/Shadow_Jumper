using UnityEngine;
using UnityEngine.SceneManagement;

public class GameIntro : MonoBehaviour
{
    [SerializeField]
    private AudioClip confirmSound;

    [SerializeField]
    private AudioSource audioSource;

    private bool hasClicked = false;

    private void Update()
    {
        // Prevent double click (optional)
        if (hasClicked && !audioSource.isPlaying)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void PlayBtnClicked()
    {
        if (hasClicked) return;

        hasClicked = true;
        PlaySound(confirmSound);
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
