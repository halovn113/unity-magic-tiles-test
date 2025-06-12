using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IPointerDownHandler
{
    private TileTouchingObserver tileTouchingObserver;
    public void OnAwake(TileTouchingObserver tileTouchingObserver)
    {
        this.tileTouchingObserver = tileTouchingObserver;
    }

    public void ResetTile()
    {
        if (ColorUtility.TryParseHtmlString(ColorTile.Normal, out Color color))
        {
            GetComponent<Image>().color = color;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        tileTouchingObserver.Notify(new TileTouchingData()
        {
            tilePosition = transform.position,
            tile = this
        });
    }
}
