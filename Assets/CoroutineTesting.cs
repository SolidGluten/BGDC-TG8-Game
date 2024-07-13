using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineTesting : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Coroutine1());
    }

    public IEnumerator Coroutine1()
    {
        yield return StartCoroutine(CoroutineTesting2.Coroutine2());
        yield return new WaitForSeconds(2.0f);
        Debug.Log("Hello 1");
    }
}
