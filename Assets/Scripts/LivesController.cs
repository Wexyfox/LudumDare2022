using UnityEngine;

public class LivesController : MonoBehaviour
{
    public int numberLives;
    private WaveSpawner spawningScript;
    private GameOver finishScript;

    private void Start()
    {
        spawningScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<WaveSpawner>();
        finishScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameOver>();
    }

    public void LoseLives(int damageDone)
    {
        numberLives -= damageDone;

        if (numberLives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        spawningScript.enabled = false;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
        finishScript.GameLoss();
    }
}
