using UnityEngine;
using UnityEngine.UI;

public class GoldUI : MonoBehaviour
{
    public EconomySystem EconomySystem;

    public Text GoldText;

    private int lastGold;

    private void Update()
    {
        if (lastGold != EconomySystem.money)
        {
            lastGold = EconomySystem.money;

            GoldText.text = lastGold.ToString();
        }
    }
}
