using UnityEngine;

public class ScoreBehaviour : MonoBehaviour, IObservable<TileTouchingData>
{
    private UIManager uiManager;
    public int score;
    [SerializeField]
    private int[] scoreThresholds = new int[] { 100, 50, 0 };
    [SerializeField]
    private int[] comboThresholds = new int[] { 0, 100, 150, 200, 250, 300, 350, 400, 500, 600 };
    [SerializeField]
    private float[] timeThresholds = new float[] { 0.16f, 0.2f };
    [SerializeField]
    private int comboCount = 0;

    private ObserverManager observerManager;
    private Vector3 touchPosition;
    private int currentComboIndex;
    private int[] timingResultInfo;
    public void OnAwake(ObserverManager observerManager, UIManager uiManager, Vector3 touchPosition)
    {
        this.observerManager = observerManager;
        this.uiManager = uiManager;
        this.touchPosition = touchPosition;
        timingResultInfo = new int[Text.TimingTexts.Length];
        for (int i = 0; i < timingResultInfo.Length; i++)
        {
            timingResultInfo[i] = 0;
        }

        observerManager.tileTouchingObserver.AddObservable(this);
    }

    public void OnNotify(TileTouchingData value)
    {
        float distanceToTouchZone = value.tilePosition.y - touchPosition.y;

        var scoreTextIndex = Text.TimingTexts.Length - 1;
        var resultScore = 0;
        for (int i = 0; i < timeThresholds.Length; i++)
        {
            if (distanceToTouchZone <= timeThresholds[i])
            {
                scoreTextIndex = i;
                break;
            }
        }
        timingResultInfo[scoreTextIndex] += 1;

        if (scoreTextIndex != Text.TimingTexts.Length - 1)
        {
            if (currentComboIndex == scoreTextIndex)
            {
                comboCount += 1;
            }
            else
            {
                comboCount = 1;
            }
        }
        else
        {
            comboCount = 0;
        }

        resultScore = scoreTextIndex == Text.TimingTexts.Length - 1 ? 0 :
                    scoreThresholds[scoreTextIndex] + comboThresholds[Mathf.Min(comboCount, comboThresholds.Length - 1)];
        currentComboIndex = scoreTextIndex;
        score += resultScore;
        observerManager.tileScoreObserver.Notify(new TileScoreData()
        {
            scoreIndex = scoreTextIndex,
            score = score,
            comboCount = comboCount,
        });
    }

    public void ResetScore()
    {
        score = 0;
        currentComboIndex = -1;
        comboCount = 0;
        for (int i = 0; i < timingResultInfo.Length; i++)
        {
            timingResultInfo[i] = 0;
        }
    }

    public int[] GetTimingResultInfos()
    {
        return timingResultInfo;
    }
}