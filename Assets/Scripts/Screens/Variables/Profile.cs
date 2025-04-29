using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Profile : BasicScreen
{
    public Button b;

    public Button avatar;
    public AvatarManager avatarManager;
    public TMP_InputField name;

    public Image[] achievementsImage;
    public Sprite[] openedAchievements;
    public Button[] _achievementButtons;


    void Start()
    {
        b.onClick.AddListener(Home);
        avatar.onClick.AddListener(Avatar);

        for(int i = 0;i < _achievementButtons.Length;i++)
        {
            int index = i;
            _achievementButtons[index].onClick.AddListener(() => AchievePopupOpen(index));
        }

    }

    // Update is called once per frame
    void OnDestroy()
    {
        b.onClick.RemoveListener(Home);
        avatar.onClick.RemoveListener(Avatar);
        for (int i = 0; i < _achievementButtons.Length; i++)
        {
            int index = i;
            _achievementButtons[index].onClick.RemoveListener(() => AchievePopupOpen(index));
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("Name", name.text);
    }

    public override void SetScreen()
    {

        avatarManager.SetSavedPicture();
        name.text = PlayerPrefs.GetString("Name", "USER_NAME");
        SetAchievements();
    }
    public override void ResetScreen()
    { 
    }

    private void SetAchievements()
    {
        for(int i = 0; i < achievementsImage.Length; i++)
        {
            string key = "Achieve" + i;
            if (PlayerPrefs.GetInt(key) == 1)
            {
                achievementsImage[i].sprite = openedAchievements[i];
            }
        }
    }

    private void Home()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
        PlayerPrefs.SetString("Name", name.text);
    }

    private void Avatar()
    {
        avatarManager.PickFromGallery();
    }

    private void AchievePopupOpen(int index)
    {
        AchievePopup achievePopup = (AchievePopup)UIManager.Instance.GetPopup(PopupTypes.Achieve);
        achievePopup.SetAchieve(index);
        UIManager.Instance.ShowPopup(PopupTypes.Achieve);
    }
}
