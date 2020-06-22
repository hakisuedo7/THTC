using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    GameController gameController;

    Stage.Stage stage;
    List<List<GameObject>> EnemyList;

    int DefeatEnemyCount;

    Vector3 OneEnemyPos = new Vector3(18f, 9.5f, 0);
    Vector3[] TwoEnemiesPos = { new Vector3(18f, 16f, 0), new Vector3(18f, 3f, 0) };
    Vector3[] ThreeEnemiesPos = { new Vector3(21.5f, 9.5f, 0), new Vector3(16.5f, 16f, 0), new Vector3(16.5f, 3f, 0) };

    // Start is called before the first frame update
    void Start()
    {
        gameController = GetComponent<GameController>();

        stage = Stage.StageManager.GetStage(Setting.Game.CURRENTSTAGE);
        StartStage();
    }

    void StartStage()
    {
        Init();
        StartCoroutine(StartWave(0));
    }
    void Init()
    {
        if (stage != null)
        {
            SpriteRenderer BFsprite = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<SpriteRenderer>();
            BFsprite.sprite = PublicObj.Template.GetSprite(stage.BFindex());
        }

        EnemyList = SetEnemy(stage.WaveL());
    }

    List<List<GameObject>> SetEnemy(List<List<string>> WaveList)
    {
        List<List<GameObject>> EnemyList = new List<List<GameObject>>();

        foreach(var wave in WaveList)
        {
            List<GameObject> tmp = new List<GameObject>();
            foreach(var name in wave)
            {
                tmp.Add(PublicObj.Template.GetEnemy(name));
            }
            EnemyList.Add(tmp);
        }
        return EnemyList;
    }

    IEnumerator StartWave(int wave)
    {
        if (wave >= stage.WaveC())
        {
            Setting.Config.UpdateProgress(stage.CurrentProgress());
            gameController.GameComplete();
            yield return null;
        }
        else
        {
            DefeatEnemyCount = 0;
            List<GameObject> waveList = EnemyList[wave];
            int EnemyCount = waveList.Count;
            if (EnemyCount > 3)
                EnemyCount = 3;

            switch (EnemyCount)
            {
                case 1:
                    Instantiate(waveList[0], OneEnemyPos, Quaternion.identity);
                    break;
                case 2:
                    for (int i = 0; i < 2; i++)
                        Instantiate(waveList[i], TwoEnemiesPos[i], Quaternion.identity);
                    break;
                case 3:
                    for (int i = 0; i < 3; i++)
                        Instantiate(waveList[i], ThreeEnemiesPos[i], Quaternion.identity);
                    break;
                default:
                    break;
            }

            while (true)
            {
                if(DefeatEnemyCount >= EnemyCount)
                {
                    yield return StartCoroutine(StartWave(wave + 1));
                }
                yield return null;
            }
        }
    }

    public void EnemyIsDefeat()
    {
        DefeatEnemyCount++;
    }
}
