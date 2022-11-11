using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public GameObject crossfade;
    public float transitionTime = 1f;
    private static readonly int StartTransition = Animator.StringToHash("StartTransition");

    private void Start()
    {
        // Enable the crossfade gameobject
        crossfade.gameObject.SetActive(true);
    }

    private void Update()
    {
        // On R key press, reload the current scene
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    public void ReloadLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().name));
    }
    public void LoadLevelByName(string levelToLoad)
    {
        StartCoroutine(LoadLevel(levelToLoad));
    }

    private IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger(StartTransition);
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
