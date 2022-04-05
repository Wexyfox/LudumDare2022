using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    public LivesController LivesController;

    public Text LivesText;

    private int lastLives;

    private void Update()
    {
        if (lastLives != LivesController.numberLives)
        {
            lastLives = LivesController.numberLives;

            LivesText.text = lastLives.ToString();
        }
    }
}
