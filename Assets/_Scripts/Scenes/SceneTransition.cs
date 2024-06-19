using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private float transitionDuration = 1f;

    private void Start()
    {
        if (SceneLoader.Instance)
        {
            SceneLoader.Instance.OnSceneChange += StartTransition;
            SceneLoader.Instance.OnSceneLoad += EndTransition;
        }
    }

    public void StartTransition()
    {
        Debug.Log("Transition Starto!");
    }

    public void EndTransition()
    {
        Debug.Log("Transition Endo!");
    }

    private void OnDisable()
    {
        if (SceneLoader.Instance)
        {
            SceneLoader.Instance.OnSceneChange -= StartTransition;
            SceneLoader.Instance.OnSceneLoad -= EndTransition;
        }
    }
}
