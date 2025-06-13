using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textResult;
    public TextMeshProUGUI textResultTimingInfo;

    public Button buttonReplay;

    public void OnAwake(ObserverManager observerManager, GameManager gameManager)
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        buttonReplay.onClick.AddListener(() =>
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            gameManager.ChangeState(GameState.Game);
        });
    }

    public void ShowGameOver(int score, bool win, int[] timingResultInfos)
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        textResult.text = win ? Text.Win : Text.Lose;
        textScore.text = score.ToString();
        textResultTimingInfo.text = "";
        for (int i = 0; i < timingResultInfos.Length; i++)
        {
            textResultTimingInfo.text += Text.TimingTexts[i] + " : " + timingResultInfos[i].ToString() + "\n";
        }
        Tween.Alpha(canvasGroup, 1f, 0.5f);
    }
}
