using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiArrayUtils : MonoBehaviour
{
    public static void ResizeArray<T>(ref T[,] original, int newWidth, int newHeight, int offsetX = 0, int offsetY = 0)
    {
        T[,] newArray = new T[newWidth, newHeight];
        int width = original.GetLength(0);
        int height = original.GetLength(1);
        for (int x = 0; x < width; x++)
        {
            Array.Copy(original, x * height, newArray, (x + offsetX) * newHeight + offsetY, height);
        }

        original = newArray;
    }
}
