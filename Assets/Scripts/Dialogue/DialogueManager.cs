using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public DialogueFont DefaultDialogueFont;

    [Header("UI")]
    public Animator TextAnimator;
    public Text TextContent;

    [Space]
    public Text PortraitName;
    public Animator PortraitAnimator;

    [Space]
    public CanvasGroup Darken;
    public RectTransform BottomBar;
    public float TransitionOpenTime = 0.5f;
    public float TransitionCloseTime = 0.5f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        TextContent.text = "";
        UpdateTransitionPhase(0.0f);
    }

    private void UpdateTransitionPhase(float value)
    {
        BottomBar.pivot = new Vector2(BottomBar.pivot.x, 1.0f - value);
        var bottomSize = BottomBar.sizeDelta;
        BottomBar.offsetMin = new Vector2(0, 0);
        BottomBar.offsetMax = new Vector2(0, 0);
        BottomBar.sizeDelta = bottomSize;

        Darken.alpha = value;
    }

    public IEnumerator DialogueRoutine(DialogueTranscript script)
    {
        TextContent.text = "";

        PortraitAnimator.gameObject.SetActive(false);

        foreach (float time in new TimedLoop(TransitionOpenTime))
        {
            UpdateTransitionPhase(time);

            yield return null;
        }
        PortraitAnimator.gameObject.SetActive(true);

        foreach (var message in script.Content)
        {
            PortraitAnimator.runtimeAnimatorController = message.Character.ProfileController;

            yield return StartCoroutine(DisplayMessageRoutine(message));

            yield return new WaitForSeconds(0.05f);

            while (!Input.GetMouseButtonDown(0)
                && !Input.GetKeyDown(KeyCode.Space))
            {
                yield return null;
            }
        }

        foreach (float time in new TimedLoop(TransitionCloseTime))
        {
            UpdateTransitionPhase(1.0f - time);
            yield return null;
        }
    }


    public IEnumerator DisplayMessageRoutine(DialogueMessage message)
    {
        TextContent.text = "";

        var font = message.Font;
        if (font == null)
        {
            font = DefaultDialogueFont;
        }

        TextContent.font = font.Font;
        TextContent.fontStyle = font.FontStyle;
        TextContent.fontSize = font.FontSize;
        TextContent.alignment = font.TextAnchor;
        TextAnimator.runtimeAnimatorController = font.Controller;

        PortraitAnimator.runtimeAnimatorController = message.Character.ProfileController;

        var currentText = new StringBuilder();
        currentText.Append("<color=#00000000>");

        int insertHead = 0;
        int scanHead = currentText.Length;

        currentText.Append(message.Body);
        currentText.Append("</color>");

        while (scanHead < currentText.Length - 8)
        {
            char nextCharacter = currentText[scanHead];

            currentText.Remove(scanHead, 1);
            currentText.Insert(insertHead, nextCharacter);
            TextContent.text = currentText.ToString();

            yield return new WaitForSeconds(1.0f / (font.CharactersPerMinute / 60.0f));

            if (nextCharacter == ' ')
            {
                yield return new WaitForSeconds(font.DelayBetweenWords);
            }
            else if (nextCharacter == '.'
                || nextCharacter == '?'
                || nextCharacter == '!')
            {
                yield return new WaitForSeconds(font.DelayBetweenSentences);
            }

            insertHead++;
            scanHead++;
        }
    }
}
