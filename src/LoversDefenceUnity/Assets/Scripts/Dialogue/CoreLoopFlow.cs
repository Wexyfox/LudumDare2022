using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreLoopFlow : MonoBehaviour
{
    public int FirstLevel = 0;
    public WaveSpawner WaveSpawner;

    [Header("MainMenu")]
    public CanvasGroup MainMenuFader;
    public float MainMenuWaitTime = 1.0f;
    public float MainMenuFadeOutTime = 0.2f;

    [Space]
    public CanvasGroup MainMenuContinueButtonFader;
    public float MainMenuContinueButtonFadeInTime = 0.2f;

    [Header("Game")]
    public float EmptyWorldWaitTime = 1.0f;

    [Space]
    public CanvasGroup HUDFader;
    public float HUDFadeTime = 0.5f;

    [Header("GameOver")]
    public DialogueTranscript LastDialogue;
    public CanvasGroup GameOverFader;
    public float GameOverFadeInTime = 0.2f;

    private IEnumerator Start()
    {
        WaveSpawner.state = WaveSpawner.SpawnState.Paused;
        GameOverFader.gameObject.SetActive(false);
        HUDFader.alpha = 0.0f;

        MainMenuFader.gameObject.SetActive(false);

        // MainMenuFader.gameObject.SetActive(true);
        // MainMenuFader.alpha = 1.0f;
        // yield return new WaitForSeconds(MainMenuWaitTime);
        // 
        // foreach (var time in new TimedLoop(MainMenuContinueButtonFadeInTime))
        // {
        //     MainMenuContinueButtonFader.alpha = time;
        //     yield return null;
        // }
        // 
        // while (!Input.GetMouseButtonDown(0)
        //     && !Input.GetKeyDown(KeyCode.Space))
        // {
        //     yield return null;
        // }
        // 
        // foreach (var time in new TimedLoop(MainMenuFadeOutTime))
        // {
        //     MainMenuFader.alpha = 1.0f - time;
        //     yield return null;
        // }

        yield return new WaitForSeconds(EmptyWorldWaitTime);

        for (int i = 0; i < WaveSpawner.waves.Length; i++)
        {
            var wave = WaveSpawner.waves[i];
            if (wave.playBeforeWave != null)
            {
                if (i != 0)
                {
                    foreach (var time in new TimedLoop(HUDFadeTime))
                    {
                        HUDFader.alpha = 1.0f - time;
                    }
                }

                WaveSpawner.state = WaveSpawner.SpawnState.Paused;
                yield return StartCoroutine(DialogueManager.Instance.DialogueRoutine(wave.playBeforeWave));
                WaveSpawner.state = WaveSpawner.SpawnState.Counting;

                foreach (var time in new TimedLoop(HUDFadeTime))
                {
                    HUDFader.alpha = time;
                }
            }

            while (WaveSpawner.state == WaveSpawner.SpawnState.Counting)
            {
                yield return null;
            }

            while (WaveSpawner.state == WaveSpawner.SpawnState.Spawning)
            {
                yield return null;
            }

            while (WaveSpawner.state == WaveSpawner.SpawnState.Waiting)
            {
                yield return null;
            }
        }

        GameOverFader.gameObject.SetActive(true);
        foreach (var time in new TimedLoop(GameOverFadeInTime))
        {
            GameOverFader.alpha = time;
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        while (!Input.GetMouseButtonDown(0)
            && !Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }

        SceneManager.LoadScene(FirstLevel);
    }
}
