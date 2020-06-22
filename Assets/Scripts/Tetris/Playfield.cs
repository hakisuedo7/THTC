using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playfield : MonoBehaviour
{
    public static int Combo = 0;
    public static int atkBlock = 0;

    public static int w = 10;
    public static int h = 25;
    public static Transform[,] grid = new Transform[w, h];

    static GameObject block;

    public static void LoadBlock()
    {
        block = Resources.Load<GameObject>("Prefab/Block/BlockB");
    }
    public static Vector2 roundVec2(Vector2 v) {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    public static bool insideBorder(Vector2 pos) {
        return ((int)pos.x >= 0 && (int)pos.x < w && (int)pos.y >= 0);
    }

    public static void deleteRow(int y) { 
        for(int x = 0; x < w; ++x)
        {
            grid[x, y].gameObject.GetComponent<Block>().DestroyBlock();
            grid[x, y] = null;
        }
    }

    public static void decreaseRow(int y)
    {
        for(int x = 0; x < w; ++x)
        {
            if(grid[x,y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public static void increaseRow(int y)
    {
        for(int x = 0; x < w; ++x)
        {
            if(grid[x, y] != null)
            {
                bool increase = false;

                if(grid[x,y].gameObject.transform.parent == null)
                {
                    increase = true;
                }
                else
                {
                    if (!grid[x, y].gameObject.transform.parent.GetComponent<Group>().isActiveAndEnabled)
                        increase = true;
                }

                if (increase)
                {
                    grid[x, y + 1] = grid[x, y];
                    grid[x, y] = null;

                    grid[x, y + 1].position += new Vector3(0, 1, 0);
                }
            }
        }
    }

    public static void decreseRowsAbove(int y)
    {
        for (int i = y; i < h; ++i)
            decreaseRow(i);
    }

    public static bool isRowFull(int y)
    {
        for(int x = 0; x < w; ++x)
        {
            if (grid[x, y] == null)
                return false;
        }
        return true;
    }

    public static bool isRowEmpty(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] != null)
                return false;
        }
        return true;
    }

    public static bool isRowContainBlock(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] != null)
                return true;
        }
        return false;
    }

    public static void deleteFullRows()
    {
        int fullRowCount = 0;
        for(int y = 0; y < h; ++y)
        {
            if (isRowFull(y))
            {
                fullRowCount++;
                deleteRow(y);
                decreaseRow(y);
                decreseRowsAbove(y + 1);
                --y;
            }
        }
        if (fullRowCount > 0)
        {
            Combo++;
            Ally.Ally[] allies = FindObjectsOfType<Ally.Ally>();
            if (isAmptyField())
            {
                foreach (Ally.Ally ally in allies)
                    ally.Attack(10, Combo, atkBlock);
            }
            else
            {
                foreach (Ally.Ally ally in allies)
                    ally.Attack(fullRowCount, Combo, atkBlock);
            }

            GameObject.FindObjectOfType<GameController>().ComboEffect(Combo);
        }
        else
        {
            Combo = 0;
            atkBlock = 0;
        }
    }

    public static void increaseFullRow()
    {
        increaseFullRow(Random.Range(0, w));
    }

    public static void increaseFullRow(int i)
    {
        for (int y = h - 2; y >= 0; --y)
        {
            if (isRowContainBlock(y))
            {
                increaseRow(y);
            }
        }
        for(int x = 0; x < w; x++)
        {
            if (x != i && grid[x,0] == null)
            {
                Vector3 v = roundVec2(new Vector3(x, 0, 0));
                grid[x, 0] = Instantiate(block, v, Quaternion.identity).transform;
            }
        }
    }

    public static bool isAmptyField()
    {
        for (int y = 0; y < h; ++y)
        {
            if (!isRowEmpty(y))
                return false;
        }
        return true;
    }

    public static void DestroyBlock(Vector2 v2)
    {
        if(grid[(int)v2.x,(int)v2.y] != null)
        {
            bool destroy = false;

            if (grid[(int)v2.x, (int)v2.y].gameObject.transform.parent == null)
            {
                destroy = true;
            }
            else
            {
                if (!grid[(int)v2.x, (int)v2.y].gameObject.transform.parent.GetComponent<Group>().isActiveAndEnabled)
                    destroy = true;
            }

            if (destroy)
            {
                grid[(int)v2.x, (int)v2.y].gameObject.GetComponent<Block>().DestroyBlock();
                grid[(int)v2.x, (int)v2.y] = null;
                atkBlock--;
            }
        }
    }
}
