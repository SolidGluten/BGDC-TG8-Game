using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineTesting2 : MonoBehaviour
{
    public static IEnumerator Coroutine2()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Hello2");
    }   
}
