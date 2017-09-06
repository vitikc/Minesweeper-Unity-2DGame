using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public Sprite[] sprites;
    public bool isMine = false;
    public bool isMarked = false;
    public bool isOpened = false;
    public int x;
    public int y;

    private SpriteRenderer ownRenderer;
    private void Awake()
    {
        ownRenderer = GetComponent<SpriteRenderer>();
        x = (int)transform.position.x;
        y = (int)transform.position.y;

    }
    public void DrawSprite(Sprite s)
    {  
        ownRenderer.sprite = s;
    }
    public void DrawSprite(int s)
    {
        ownRenderer.sprite = sprites[s];
    }
    public void OnMouseUpAsButton()
    {
        if (!isMarked)
        {
            if (isMine)
            {
                GameManager.UncoverMines();
            }
            else
            {
                DrawSprite(GameManager.CalcMines(x, y));
                isOpened = true;
                GameManager.FastUncover(x, y, new bool[GameManager.WIDTH, GameManager.HEIGTH]);
            }
        }
    }
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(1)) {
            if (!isOpened)
            {
                if (!isMarked) DrawSprite(9);
                else DrawSprite(11);

                isMarked = !isMarked;
            }
        }
    }
}
