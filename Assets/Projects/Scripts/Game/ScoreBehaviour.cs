using UnityEngine;

public class ScoreBehaviour : MonoBehaviour, IObservable<TileTouchingData>
{
    private UIManager uiManager;
    public int score;
    [SerializeField]
    private int[] scoreThresholds = new int[] { 100, 50, 0 };
    [SerializeField]
    private float[] timeThresholds = new float[] { 0.16f, 0.2f };

    private ObserverManager observerManager;
    private Vector3 touchPosition;

    public void OnAwake(ObserverManager observerManager, UIManager uiManager, Vector3 touchPosition)
    {
        this.observerManager = observerManager;
        this.uiManager = uiManager;
        this.touchPosition = touchPosition;

        observerManager.tileTouchingObserver.AddObservable(this);
    }

    public void OnNotify(TileTouchingData value)
    {
        float distanceToTouchZone = value.tilePosition.y - touchPosition.y;

        var scoreTextIndex = Text.TimingTexts.Length - 1;
        for (int i = 0; i < timeThresholds.Length; i++)
        {
            if (distanceToTouchZone <= timeThresholds[i])
            {
                scoreTextIndex = i;
                break;
            }
        }
        score += scoreThresholds[scoreTextIndex];
        observerManager.tileScoreObserver.Notify(new TileScoreData()
        {
            scoreIndex = scoreTextIndex,
            score = score
        });
    }
}