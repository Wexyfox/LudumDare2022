using System.Collections;
using UnityEngine;

public class CoreLoopFlow : MonoBehaviour
{
    public DialogueTranscript PlayOnStart;

    private IEnumerator Start()
    {
        yield return StartCoroutine(DialogueManager.Instance.DialogueRoutine(PlayOnStart));
    }
}
