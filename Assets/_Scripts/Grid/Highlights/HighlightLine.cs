using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightLine : Highlighter
{
    public override List<Cell> Highlight(Cell targetCell, int width, int length, Direction dir = Direction.Right)
    {
        List<Cell> res = new List<Cell>();
        if (dir == Direction.Left || dir == Direction.Right)
        {
            HighlightVertical(targetCell, width + 1, ref res);
            List<Cell> tempRes = new List<Cell>();
            if (dir == Direction.Left)
            {
                foreach (var cell in res)
                {
                    var temp = cell.left;
                    for (int i = 0; i < length && temp != null; i++)
                    {
                        tempRes.Add(temp);
                        temp = temp.left;
                    }
                }
            }
            else
            {
                foreach (var cell in res)
                {
                    var temp = cell.right;
                    for (int i = 0; i < length && temp != null; i++)
                    {
                        tempRes.Add(temp);
                        temp = temp.right;
                    }
                }
            }
            res.AddRange(tempRes);
        }
        else
        {
            List<Cell> tempRes = new List<Cell>();
            HighlightHorizontal(targetCell, width + 1, ref res);
            if (dir == Direction.Up)
            {
                foreach (var cell in res)
                {
                    var temp = cell.up;
                    for (int i = 0; i < length && temp != null; i++)
                    {
                        tempRes.Add(temp);
                        temp = temp.up;
                    }
                }
            }
            else
            {
                foreach (var cell in res)
                {
                    var temp = cell.down;
                    for (int i = 0; i < length && temp != null; i++)
                    {
                        tempRes.Add(temp);
                        temp = temp.down;
                    }
                }
            }
            res.AddRange(tempRes);
        }

        return res;
    }
}
