using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Game : BasicScreen
{
    public Button close;
    public TMP_Text score;
    public TMP_Text timer;

    private int _compeatedRows;

    private void Start()
    {
        close.onClick.AddListener(Close);
    }

    private void OnDestroy()
    {
        close.onClick.RemoveListener(Close);
    }
    public override void ResetScreen()
    {
        FinishGame();
    }

    public override void SetScreen()
    {
        score.text = PlayerPrefs.GetInt("Score").ToString();
        StartGame();
    }

    private void StartGame()
    {
        StartCoroutine(Timer());
    }
    private void FinishGame()
    {
        StopCoroutine(Timer());
    }

    private void Lose()
    {

    }

    private void Win()
    {

    }

    private IEnumerator Timer()
    {
        float time = 90;
        while (time != 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);

            timer.text = $"{minutes}:{seconds:D2}";
        }
        if(_compeatedRows > 0)
        {
            Win();
        }
        else
        {
            Lose();
        }
    }
    private void Close()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
}
