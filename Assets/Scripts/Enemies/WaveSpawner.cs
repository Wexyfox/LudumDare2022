using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform spawnLocation;
    private GameOver finishScript;
    public AudioController audioScript;

    public enum SpawnState
    {
        Spawning,
        Waiting,
        Counting,
        Paused
    };

    [System.Serializable]
    public class Wave
    {
        [Header("Wave attributes")]
        public string name;
        public GameObject slime;
        public GameObject rat;
        public GameObject mushroom;
        public int slimeCount;
        public int ratCount;
        public int mushroomCount;
        public float spawnRate;

        public DialogueTranscript playBeforeWave;

        [HideInInspector]
        public int totalCount;
    }

    public Wave[] waves;
    private int nextWaveIndex = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float checkAliveCountdown = 1f;

    public SpawnState state = SpawnState.Counting;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
        finishScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameOver>();
    }

    private void Update()
    {
        if (state == SpawnState.Paused)
        {
            return;
        }

        if (state == SpawnState.Waiting)
        {
            if (!CheckEnemiesAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWaveIndex]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave(Wave waveName)
    {
        state = SpawnState.Spawning;
        waveName.totalCount = waveName.slimeCount + waveName.ratCount + waveName.mushroomCount;

        for (int i = 0; i < waveName.totalCount; i++)
        {
            int temp = 3;
            while (true)
            {
                temp = RandomiserSpawner(waveName.slimeCount, waveName.ratCount, waveName.mushroomCount);

                if (temp == 0)
                {
                    waveName.slimeCount -= 1;
                    SpawnEnemy(waveName.slime);
                }
                else if (temp == 1)
                {
                    waveName.ratCount -= 1;
                    SpawnEnemy(waveName.rat);
                }
                else if (temp == 2)
                {
                    waveName.mushroomCount -= 1;
                    SpawnEnemy(waveName.mushroom);
                }


                if (temp != 3)
                {
                    break;
                }
            }

            yield return new WaitForSeconds(1f / waveName.spawnRate);

        }

        state = SpawnState.Waiting;

        yield break;
    }

    void SpawnEnemy(GameObject enemyObject)
    {
        Instantiate(enemyObject, spawnLocation.position, spawnLocation.rotation);
    }

    private int RandomiserSpawner(int slimes, int rats, int mushrooms)
    {
        int randomisedNumber = Random.Range(0, 3);
        if (randomisedNumber == 0)
        {
            if (slimes == 0)
            {
                return 3;
            }
            else
            {
                return 0;
            }
        }
        else if (randomisedNumber == 1)
        {
            if (rats == 0)
            {
                return 3;
            }
            else
            {
                return 1;
            }
        }
        else if (randomisedNumber == 2)
        {
            if (mushrooms == 0)
            {
                return 3;
            }
            else
            {
                return 2;
            }
        }
        return 3;
    }

    private bool CheckEnemiesAlive()
    {
        checkAliveCountdown -= Time.deltaTime;
        if (checkAliveCountdown <= 0f)
        {
            checkAliveCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    private void WaveCompleted()
    {
        state = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;

        if (nextWaveIndex + 1 > waves.Length - 1)
        {
            gameObject.GetComponent<WaveSpawner>().enabled = false;
            Debug.Log(nextWaveIndex);
            finishScript.GameWin();
        }
        else
        {
            nextWaveIndex++;
        }

        if (nextWaveIndex == 9)
        {
            audioScript.HardMusicPlay();
        }
        else if (nextWaveIndex == 14)
        {
            audioScript.FinalMusicPlay();
        }
    }
}
