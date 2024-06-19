using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static int currSceneIdx;
    [SerializeField] private static float loadDelay = 1.2f;

    public static SceneLoader Instance;

    public event Action OnSceneChange;
    public event Action OnSceneLoad;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        } else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("test");
        currSceneIdx = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReloadSceneAsync();
        } else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            LoadSceneAsync(1);
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currSceneIdx = scene.buildIndex;
        Debug.Log(currSceneIdx);
        OnSceneLoad?.Invoke();
    }

    public void ReloadSceneAsync()
    {
        StopAllCoroutines();
        StartCoroutine(IEReloadSceneAsync());
    }

    public void LoadSceneAsync(int buildIdx)
    {
        StopAllCoroutines();
        StartCoroutine(IELoadSceneAsync(buildIdx));
    }

    private IEnumerator IEReloadSceneAsync()
    {
        OnSceneChange?.Invoke();
        yield return new WaitForSeconds(loadDelay);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currSceneIdx);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private IEnumerator IELoadSceneAsync(int buildIdx)
    {
        OnSceneChange?.Invoke();
        yield return new WaitForSeconds(loadDelay);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(buildIdx);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(currSceneIdx);
    }

    public void ChangeScene(int buildIdx)
    {
        SceneManager.LoadScene(buildIdx);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
