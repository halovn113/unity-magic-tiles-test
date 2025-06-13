using PrimeTween;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour, IObservable<TileScoreData>
{
    [Header("Text")]
    public TextMeshProUGUI textResultTiming;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textCombo;

    [Header("Progress")]
    public ProgressBar progressBar;
    private Sequence sequenceTextCombo;

    public void OnAwake(ObserverManager observerManager)
    {
        observerManager.tileScoreObserver.AddObservable(this);
        textCombo.alpha = 0;
    }

    public void UpdateResult(string resultTiming)
    {
        textResultTiming.transform.localScale = Vector3.one;
        if (resultTiming != Text.TimingTexts[Text.TimingTexts.Length - 1])
        {
            float scale = resultTiming == Text.TimingTexts[0] ? 1.2f : 1.1f;
            Sequence.Create().Chain(Tween.Scale(textResultTiming.transform, scale, 0.2f)).Chain(Tween.Scale(textResultTiming.transform, 1f, 0.5f));
        }
        if (resultTiming == Text.TimingTexts[0])
        {
            textResultTiming.color = Color.yellow;
        }
        else if (resultTiming == Text.TimingTexts[1])
        {
            textResultTiming.color = Color.green;
        }
        else
        {
            textResultTiming.color = Color.white;
        }
        textResultTiming.text = resultTiming;
    }

    public void UpdateScore(int score)
    {
        if (score > 0)
        {
            Sequence.Create().Chain(Tween.Scale(textScore.transform, 1.1f, 0.2f)).Chain(Tween.Scale(textScore.transform, 1f, 0.5f));
        }
        textScore.text = score.ToString();
    }

    public void UpdateCombo(int combo)
    {
        if (combo <= 1)
        {
            return;
        }
        sequenceTextCombo.Stop();
        textCombo.alpha = 0;
        textCombo.transform.localScale = Vector3.one;
        sequenceTextCombo = Sequence.Create().Chain(Tween.Alpha(textCombo, 1, 0.5f)).Chain(Tween.Scale(textCombo.transform, 1.1f, 0.2f))
        .Chain(Tween.Scale(textCombo.transform, 1f, 0.5f)).Chain(Tween.Alpha(textCombo, 0, 0.5f));
        textCombo.text = "X" + combo.ToString();
    }

    public void UpdateProgress(float progress)
    {
        progressBar.SetProgress(progress);
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
        UpdateCombo(value.comboCount);
    }
}
