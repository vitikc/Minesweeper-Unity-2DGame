using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static int WIDTH = 10;
    public static int HEIGTH = 16;

    public static Tile[,] field = new Tile[WIDTH,HEIGTH];

    public GameObject prefab;

    private void Start()
    {
        int count = 0;//DEBUG V
        for(int x = 0; x < WIDTH; x++)
        {
            for(int y = 0; y < HEIGTH; y++)
            {
                bool isMine = Random.Range(0, WIDTH * HEIGTH) < 16;
                if (isMine) count++;
                GameObject go = Instantiate(prefab, new Vector2(x, -y), Quaternion.identity, transform);
                Tile t = go.GetComponent<Tile>();
                t.x = x;
                t.y = y;
                t.isMine = isMine;
                field[x, y] = t;
            }
        }
        Debug.Log(count);
    }
    public static void UncoverMines()
    {
        foreach (Tile t in field)
        {
            if (t.isMine)
            {
                if(!t.isMarked)
                t.DrawSprite(10);
            }
        }
    }

    public static bool isMineAt(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < WIDTH && y < HEIGTH)
        {
            return field[x, y].isMine;
        }
        return false;
    }
    public static int CalcMines(int x, int y)
    {
        int count = 0;
        if (isMineAt(x + 1, y)) count++; // RIGHT 
        if (isMineAt(x, y - 1)) count++; // BOTTOM
        if (isMineAt(x - 1, y)) count++; // LEFT
        if (isMineAt(x, y + 1)) count++; // TOP

        if (isMineAt(x + 1, y + 1)) count++; // TOP-RIGHT
        if (isMineAt(x - 1, y + 1)) count++; // TOP-LEFT
        if (isMineAt(x + 1, y - 1)) count++; // BOTTOM-RIGHT
        if (isMineAt(x - 1, y - 1)) count++; // BOTTOM-LEFT
        return count;
    }
    public static void FastUncover(int x, int y, bool [,] visited) 
    {
        if (x >= 0 && y >= 0 && x < WIDTH && y < HEIGTH)
        {
            if (visited[x, y]) return;

            if (!field[x, y].isMarked)
            {
                field[x, y].DrawSprite(CalcMines(x, y));
                field[x, y].isOpened = true;
            }
            if (CalcMines(x, y) > 0) return;
            visited[x, y] = true;
            
            FastUncover(x + 1, y, visited);
            FastUncover(x, y - 1, visited);
            FastUncover(x - 1, y, visited);
            FastUncover(x, y - 1, visited);

            FastUncover(x + 1, y + 1, visited);
            FastUncover(x + 1, y - 1, visited);
            FastUncover(x - 1, y + 1, visited);
            FastUncover(x - 1, y - 1, visited);

        }
    }
}
