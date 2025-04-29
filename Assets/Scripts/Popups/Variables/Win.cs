using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : BasicPopup
{
    public override void ResetPopup()
    {
    }

    public override void SetPopup()
    {
    }
    public override void Hide()
    {
        base.Hide();
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
}
