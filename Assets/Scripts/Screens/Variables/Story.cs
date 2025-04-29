using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Story : BasicScreen
{
    public Button back;
    public Image image;
    public TMP_Text _title;
    public TMP_Text _description;
    public StoryConfig[] _stories;

    private int _currentStory;

    private void Start()
    {
        back.onClick.AddListener(Close);
    }

    private void OnDestroy()
    {
        back.onClick.RemoveListener(Close);
    }
    public void SetStoryIndex(int index)
    {
        _currentStory = index;
    }
    public override void ResetScreen()
    {
    }

    public override void SetScreen()
    {
        image.sprite = _stories[_currentStory]._image;
        _title.text = _stories[_currentStory]._title;
        _description.text = _stories[_currentStory]._description;
    }

    private void Close()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Stories);
    }
}
