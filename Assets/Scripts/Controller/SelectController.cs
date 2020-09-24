using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectController : MonoBehaviour
{
    [SerializeField] private GameObject[] Stages;

    // Start is called before the first frame update
    void Start()
    {
        InActiveAllStage();
        ActiveStage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InActiveAllStage()
    {
        foreach(GameObject stage in Stages)
        {
            stage.SetActive(false);
        }
    }

    void ActiveStage()
    {
        for(int i = 0; i <= Setting.Config.PROGRESS; i++)
        {
            if (Stages[i] != null)
                Stages[i].SetActive(true);
        }
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
