using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Home : BasicScreen
{
    public Button profile;
    public Button storyShop;
    public Button info;
    public Button play;
    public TMP_Text _score;

    private void Start()
    {
        profile.onClick.AddListener(Profile);
        storyShop.onClick.AddListener(StoryShop);
        info.onClick.AddListener(Info);
        play.onClick.AddListener(Game);
    }

    private void OnDestroy()
    {
        profile.onClick.RemoveListener(Profile);
        storyShop.onClick.RemoveListener(StoryShop);
        info.onClick.RemoveListener(Info);
        play.onClick.RemoveListener(Game);
    }

    public override void ResetScreen()
    {
    }

    public override void SetScreen()
    {
        _score.text = PlayerPrefs.GetInt("Score").ToString();
    }

    private void Profile()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Profile);
    }
    private void StoryShop()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Stories);
    }
    private void Info()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Info);
    }
    private void Game()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Game);
    }

}
