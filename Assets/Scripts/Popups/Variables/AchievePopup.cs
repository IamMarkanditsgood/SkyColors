using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievePopup : BasicPopup
{
    private int _currentAchieve;
    public Image image;
    public Sprite[] achieves;


    public void SetAchieve(int index)
    {
        _currentAchieve = index;
    }
    public override void ResetPopup()
    {
    }

    public override void SetPopup()
    {
        image.sprite = achieves[_currentAchieve];
    }
}

