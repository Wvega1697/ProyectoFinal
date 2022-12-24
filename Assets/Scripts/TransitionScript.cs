using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScript : MonoBehaviour
{
    public static TransitionScript instance;
    public GameObject wolfSprite;
    public Animator transition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadSpecificLevel(int levelId)
    {
        StartCoroutine(LoadLevel(levelId));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play Animation
        transition.SetTrigger("Start");
        //Wait
        yield return new WaitForSecondsRealtime(1.5f);
        //Load Scene
        transition.ResetTrigger("Start");
        wolfSprite.SetActive(false);
        SceneManager.LoadScene(levelIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
