using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IPointerDownHandler
{
    private TileTouchingObserver tileTouchingObserver;
    private Sequence sequence;
    [HideInInspector]
    public void OnAwake(TileTouchingObserver tileTouchingObserver)
    {
        this.tileTouchingObserver = tileTouchingObserver;
    }

    public void ResetTile()
    {
        sequence.Stop();
        GetComponent<SpriteRenderer>().color = Color.white;
        transform.localScale = new Vector3(0.52f, 0.52f, 1);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        sequence = Sequence.Create().Group(Tween.Alpha(GetComponent<SpriteRenderer>(), 0, 0.2f)).Group(Tween.Scale(transform, 0, 0.2f)).OnComplete(() =>
        {
            tileTouchingObserver.Notify(new TileTouchingData()
            {
                tilePosition = transform.position,
                tile = this
            });
        });
    }
}
