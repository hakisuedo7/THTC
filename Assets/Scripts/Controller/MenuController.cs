using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject[] AllMenu;
    [SerializeField] private AudioClip MouseClick;

    AudioSource audioSource;

    public void Awake()
    {
        PublicObj.Template.LoadAll();
        Setting.Config.LoadConfig();
        Playfield.LoadBlock();
        Stage.StageManager.LoadAllStage();

        SetMenuActive("MainMenu");
    }

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetMenuActive(string name)
    {
        int index = 0;
        foreach(GameObject menu in AllMenu)
        {
            if (menu.name == name)
                break;
            index++;
        }

        if(index >= AllMenu.Length)
        {
            Debug.Log("menu" + name + "沒有找到");
            SetMenuActive("MainMenu");
        }
        else
        {
            SetAllMenuInActive();
            AllMenu[index].SetActive(true);
        }
    }

    void SetAllMenuInActive()
    {
        foreach (GameObject menu in AllMenu)
            menu.SetActive(false);
    }

    // 點擊按鈕
    public void NewGame()
    {
        Click();
        Setting.Config.PROGRESS = 0;
        Setting.Config.SaveConfig();
        StartCoroutine(LoadScene("SelectScene"));

    }

    public void ContinueGame()
    {
        Click();
        StartCoroutine(LoadScene("SelectScene"));
    }

    public void GameSetting()
    {
        Click();
        SetMenuActive("OptionMenu");
    }

    public void QuitGame()
    {
        Click();
        Application.Quit();
    }

    public void BackToMenu()
    {
        Click();
        SetMenuActive("MainMenu");
    }

    void Click()
    {
        audioSource.PlayOneShot(MouseClick, Setting.Config.SEVOLUME / 100);
    }

    IEnumerator LoadScene(string name)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(name);
    }
}
