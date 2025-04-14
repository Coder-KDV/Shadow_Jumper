using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCompleted : MonoBehaviour
{
    [SerializeField]
    private AudioClip playSound;

    [SerializeField]
    private AudioClip quitSound;

    [SerializeField]
    private AudioSource audioSource;

    public void PlayBtnClicked()
    {
        StartCoroutine(PlaySoundAndLoadScene(playSound));
    }

    public void QuitBtnClicked()
    {
        StartCoroutine(PlaySoundAndQuit(quitSound));
    }

    private System.Collections.IEnumerator PlaySoundAndLoadScene(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
        yield return new WaitForSeconds(clip.length);
        SceneManager.LoadScene(1);
    }

    private System.Collections.IEnumerator PlaySoundAndQuit(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
        yield return new WaitForSeconds(clip.length);
        Application.Quit();
    }
}