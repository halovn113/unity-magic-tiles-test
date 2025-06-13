using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;

public class EffectManager : MonoBehaviour, IObservable<TileScoreData>
{
    public SpriteRenderer decor;

    public void OnAwake(ObserverManager observerManager)
    {
        observerManager.tileScoreObserver.AddObservable(this);
    }

    public void OnNotify(TileScoreData value)
    {
        if (value.score == 0)
        {
            return;
        }
        Sequence.Create().Chain(Tween.Alpha(decor, 1f, 0.2f)).Chain(Tween.Alpha(decor, 0.5f, 0.5f));
    }
}
