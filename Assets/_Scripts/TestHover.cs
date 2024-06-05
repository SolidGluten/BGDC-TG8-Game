using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TestHover : MonoBehaviour
{
    private Vector2 mousePos;
    private float radius = 1f;
    private Camera cam;
    private Collider2D[] prevHits;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        var hits = Physics2D.OverlapCircleAll(mousePos, radius);

        if(prevHits != null)
        {
            foreach(var prevHit in prevHits)
            {
                if (!hits.Contains(prevHit))
                {
                    var cell = prevHit.GetComponent<Cell>();
                    cell.UnHighlight();
                }
            }
        }

        foreach(var hit in hits)
        {
            var cell = hit.GetComponent<Cell>();
            cell.Highlight();
        }

        prevHits = hits;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(mousePos, radius);
    }
}
