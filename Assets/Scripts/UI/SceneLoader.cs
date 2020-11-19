using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public Animator anim;

    public void BackToMain() 
    {
        StartCoroutine(LoadLevel("MainMenu"));
    }

    public void LoadNextscene()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void RestartScene()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        anim.SetTrigger("Fade");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator LoadLevel(string levelName)
    {
        anim.SetTrigger("Fade");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelName);
    }
}
