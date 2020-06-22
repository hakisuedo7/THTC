using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    GameController gameController;

    float lastFall = 0;
    float lastMove = 0;
    float lastFallInput = 0;

    GameObject TipsBlock;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        Init();
    }

    public void Init()
    {
        lastFall = Time.time;
        if (!isValidGridPos())
        {
            gameOver();
        }
        else
        {
            TipsBlock = Instantiate(this.gameObject, getTipsPos(), Quaternion.identity);
            SpriteRenderer[] sprites = TipsBlock.GetComponentsInChildren<SpriteRenderer>();
            Destroy(TipsBlock.GetComponent<Group>());

            foreach (SpriteRenderer sp in sprites)
            {
                sp.color = new Color(1, 1, 1, 0.4f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 遊戲結束判斷
        if (!isValidGridPos())
        {
            gameOver();
        }
        
        if(!gameController.isPause() && gameController.inWave())
        {
            // 左右移動
            if (Input.GetKey(KeyCode.LeftArrow) && Time.time - lastMove >= Setting.Game.MOVETICK)
            {
//                gameController.MoveSoundEffect();
                transform.position += new Vector3(-1, 0, 0);

                if (isValidGridPos())
                    updateGrid();
                else
                    transform.position += new Vector3(1, 0, 0);
                lastMove = Time.time;
            }
            else if (Input.GetKey(KeyCode.RightArrow) && Time.time - lastMove >= Setting.Game.MOVETICK)
            {
//                gameController.MoveSoundEffect();
                transform.position += new Vector3(1, 0, 0);

                if (isValidGridPos())
                    updateGrid();
                else
                    transform.position += new Vector3(-1, 0, 0);
                lastMove = Time.time;
            }

            // 下降
            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (Time.time - lastFallInput >= Setting.Game.INPUTFALLTICK)
                {
                    fall();
                    lastFallInput = Time.time;
                }
            }
            else if (Time.time - lastFall >= Setting.Game.FALLTICK)
            {
                fall();
                lastFall = Time.time;
            }

            // 直接放到底
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameController.FallSE();

                while (isValidGridPos())
                {
                    transform.position += new Vector3(0, -1, 0);
                }
                transform.position += new Vector3(0, 1, 0);
                updateGrid();

                Playfield.deleteFullRows();
                FindObjectOfType<Spawner>().EnableNext();
                FindObjectOfType<Spawner>().EnableSwitch();
                enabled = false;
                Destroy(TipsBlock);
            }

            // 旋轉
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                gameController.MoveSoundEffect();
                transform.Rotate(0, 0, -90);
                TipsBlock.transform.Rotate(0, 0, -90);

                foreach (Transform child in transform)
                    child.Rotate(0, 0, 90);

                foreach (Transform child in TipsBlock.transform)
                    child.Rotate(0, 0, 90);

                if (isValidGridPos())
                    updateGrid();
                else
                {
                    if (transform.position.x <= Playfield.w / 2)
                        while (!isValidGridPos())
                            transform.position += new Vector3(1, 0, 0);
                    else
                        while (!isValidGridPos())
                            transform.position += new Vector3(-1, 0, 0);
                    updateGrid();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                gameController.MoveSoundEffect();
                transform.Rotate(0, 0, 90);
                TipsBlock.transform.Rotate(0, 0, 90);

                foreach (Transform child in transform)
                    child.Rotate(0, 0, -90);

                foreach (Transform child in TipsBlock.transform)
                    child.Rotate(0, 0, -90);

                if (isValidGridPos())
                    updateGrid();
                else
                {
                    if (transform.position.x <= Playfield.w / 2)
                        while (!isValidGridPos())
                            transform.position += new Vector3(1, 0, 0);
                    else
                        while (!isValidGridPos())
                            transform.position += new Vector3(-1, 0, 0);
                    updateGrid();
                }
            }

            // 切換
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (FindObjectOfType<Spawner>().Switch())
                {
                    gameController.SwitchSoundEffect();
                    Destroy(TipsBlock);
                    switchGrid();
                    transform.rotation = Quaternion.identity;
                    foreach (Transform child in transform)
                        child.rotation = Quaternion.identity;
                    FindObjectOfType<Spawner>().SwitchBlocK(this.gameObject);
                }
            }

            // 更新提示
            TipsBlock.transform.position = getTipsPos();
        }
    }

    void fall()
    {
        transform.position += new Vector3(0, -1, 0);

        if (isValidGridPos())
            updateGrid();
        else
        {
            transform.position += new Vector3(0, 1, 0);

            Playfield.deleteFullRows();

            FindObjectOfType<Spawner>().EnableNext();
            FindObjectOfType<Spawner>().EnableSwitch();
            
            enabled = false;
            Destroy(TipsBlock);
        }
    }

    bool isValidGridPos() {
        foreach(Transform child in transform)
        {
            Vector2 v = Playfield.roundVec2(child.position);

            if (!Playfield.insideBorder(v))
                return false;
            if (Playfield.grid[(int)v.x, (int)v.y] != null && Playfield.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }

    Vector3 getTipsPos()
    {
        Vector3 originPos = transform.position;
        while (isValidGridPos())
        {
            transform.position += new Vector3(0, -1, 0);
        }
        transform.position += new Vector3(0, 1, 0);
        Vector3 v = transform.position;

        transform.position = originPos;
        return v;
    }

    void updateGrid() {
        for (int y = 0; y < Playfield.h; ++y)
            for (int x = 0; x < Playfield.w; ++x)
                if (Playfield.grid[x, y] != null)
                    if (Playfield.grid[x, y].parent == transform)
                        Playfield.grid[x, y] = null;

        foreach(Transform child in transform)
        {
            Vector2 v = Playfield.roundVec2(child.position);
            Playfield.grid[(int)v.x, (int)v.y] = child;
        }
    }

    void switchGrid()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.roundVec2(child.position);
            Playfield.grid[(int)v.x, (int)v.y] = null;
        }
    }

    void gameOver()
    {
        Destroy(gameObject);
        gameController.GameOver();
    }
}
