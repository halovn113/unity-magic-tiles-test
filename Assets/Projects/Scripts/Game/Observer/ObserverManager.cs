public sealed class ObserverManager
{
    public readonly TileTouchingObserver tileTouchingObserver;
    public readonly TileScoreObserver tileScoreObserver;

    public ObserverManager()
    {
        tileTouchingObserver = new TileTouchingObserver();
        tileScoreObserver = new TileScoreObserver();
    }
}