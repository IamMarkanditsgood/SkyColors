using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lose : BasicPopup
{
    public Button tryAgain;

    public override void Subscribe()
    {
        base.Subscribe();
        tryAgain.onClick.AddListener(TryAgain);
    }
    public override void Unsubscribe()
    {
        base.Unsubscribe();
        tryAgain.onClick.RemoveListener(TryAgain);
    }


    public override void Hide()
    {
        base.Hide();
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
    public override void ResetPopup()
    {
    }

    public override void SetPopup()
    {
    }

    private void TryAgain()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Game);
    }
}
