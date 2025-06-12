using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour, IObservable<TileScoreData>
{
    [Header("Text")]
    public TextMeshProUGUI textResultTiming;
    public TextMeshProUGUI textScore;

    [Header("Progress")]
    public ProgressBar progressBar;

    public void OnAwake(ObserverManager observerManager)
    {
        observerManager.tileScoreObserver.AddObservable(this);
    }

    public void UpdateResult(string resultTiming)
    {
        textResultTiming.text = resultTiming;
    }

    public void UpdateScore(int score)
    {
        textScore.text = score.ToString();
    }

    public void ResetInfo()
    {
        textResultTiming.text = "";
        textScore.text = "0";
    }

    public void OnNotify(TileScoreData value)
    {
        UpdateResult(Text.TimingTexts[value.scoreIndex]);
        UpdateScore(value.score);
    }
}
