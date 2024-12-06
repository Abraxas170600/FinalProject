using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public static LoadSceneManager Instance { get; private set; }
    [SerializeField] private Animator fadeAnim;
    [SerializeField] private float fadeDelay;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(Fading(fadeDelay, sceneName));
    }

    IEnumerator Fading(float delay, string sceneName)
    {
        Time.timeScale = 1;
        fadeAnim.SetBool("Fade", true);
        yield return new WaitForSeconds(delay);
        SceneManager.LoadSceneAsync(sceneName);
        fadeAnim.SetBool("Fade", false);
    }
}
