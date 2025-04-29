using UnityEngine;
using UnityEngine.UI;

public class StoryShop : BasicScreen
{
    public Button back;

    public Button[] _readButtons;
    public Button[] _buyButtons;

    private void Start()
    {
        back.onClick.AddListener(Close);

        for(int i =0; i< _readButtons.Length; i++)
        {
            int index = i;
            _readButtons[index].onClick.AddListener(()=> Read(index));
        }
        for (int i = 0; i < _buyButtons.Length; i++)
        {
            int index = i;
            _buyButtons[index].onClick.AddListener(() => Buy(index));
        }

        if (!PlayerPrefs.HasKey("Story0"))
            PlayerPrefs.SetInt("Story0", 1);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _readButtons.Length; i++)
        {
            int index = i;
            _readButtons[index].onClick.RemoveListener(() => Read(index));
        }
        for (int i = 0; i < _buyButtons.Length; i++)
        {
            int index = i;
            _buyButtons[index].onClick.RemoveListener(() => Buy(index));
        }
        back.onClick.RemoveListener(Close);
    }

    public override void ResetScreen()
    {
    }

    public override void SetScreen()
    {
        foreach (var button in _readButtons)
        {
            button.gameObject.SetActive(false); 
        }
        for(int i =0; i< _readButtons.Length; i++)
        {
            string saveKey = $"Story{i}";
            if(PlayerPrefs.GetInt(saveKey) == 1)
            {
                _readButtons[i].gameObject.SetActive(true);
            }
        }
    }

    private void Close()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }

    private void Read(int index)
    {
        Story storyScreen = (Story) UIManager.Instance.GetScreen(ScreenTypes.Story);
        storyScreen.SetStoryIndex(index);
        UIManager.Instance.ShowScreen(ScreenTypes.Story);
    }
    private void Buy(int index)
    {
        if(PlayerPrefs.GetInt("Score") >= 2000)
        {
            int newScore = PlayerPrefs.GetInt("Score") - 2000;
            PlayerPrefs.SetInt("Score", newScore);

            string saveKey = $"Story{index}";
            PlayerPrefs.SetInt(saveKey,1);
            SetScreen();
        }
    }
}
