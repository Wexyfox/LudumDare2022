using System.Collections;
using UnityEngine;

public class CoreLoopFlow : MonoBehaviour
{
    public WaveSpawner WaveSpawner;

    public DialogueTranscript[] PlayBeforeWave;
    
    private IEnumerator Start()
    {
        WaveSpawner.state = WaveSpawner.SpawnState.Paused;

        for (int i = 0; i < WaveSpawner.waves.Length; i++)
        {
            yield return StartCoroutine(DialogueManager.Instance.DialogueRoutine(PlayBeforeWave[i]));

            WaveSpawner.state = WaveSpawner.SpawnState.Counting;

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
        yield return null;
    }
}
