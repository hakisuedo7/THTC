using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private AudioClip[] SoundEffects;
    [SerializeField] private AudioClip FallEffect;
    [SerializeField] private AudioClip MVEffect;
    [SerializeField] private AudioClip SWEffect;

    AudioSource audioSource;

    private bool GameIsPause;
    private bool IsInWave;

    private void Awake()
    {
        Setting.Game.Default();

        GameIsPause = false;
        IsInWave = true;
}
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause())
                Resume();
            else
                Pause();
        }
    }

    public bool isPause()
    {
        return GameIsPause;
    }

    public bool inWave()
    {
        return IsInWave;
    }

    public void Pause()
    {
        GameIsPause = true;
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        GameIsPause = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER");
        Ally.Ally[] allies = FindObjectsOfType<Ally.Ally>();
        foreach (Ally.Ally ally in allies)
            ally.Defeat();

        SceneManager.LoadScene("SelectScene");
    }

    public void GameComplete()
    {
        SceneManager.LoadScene("SelectScene");
    }

    public void PauseButton()
    {
        if (isPause())
            Resume();
        else
            Pause();
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void BackToSelect()
    {
        SceneManager.LoadScene("SelectScene");
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void ComboEffect(int index)
    {
        if(index >= SoundEffects.Length)
            index = SoundEffects.Length;
        audioSource.PlayOneShot(SoundEffects[index - 1], Setting.Config.SEVOLUME / 100f);
    }

    public void FallSE()
    {
        audioSource.PlayOneShot(FallEffect, Setting.Config.SEVOLUME / 100f);
    }

    public void MoveSoundEffect()
    {
        audioSource.PlayOneShot(MVEffect, Setting.Config.SEVOLUME / 100f);
    }

    public void SwitchSoundEffect()
    {
        audioSource.PlayOneShot(SWEffect, Setting.Config.SEVOLUME / 100f);
    }
}
