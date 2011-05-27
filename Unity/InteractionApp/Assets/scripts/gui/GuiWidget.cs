using System;
using System.Collections.Generic;
using UnityEngine;


public class GuiWidget : MonoBehaviour
{
    public Vector2 LocalPosition = Vector2.zero;
    public Vector2 Size = Vector2.zero;
    private Vector2 _gameObjectPosition;

    public Vector2 Position
    {
        get
        {
            GuiWidget parentWidget = transform.parent.gameObject.GetComponent<GuiWidget>();
            if (parentWidget != null)
            {
                return parentWidget.Position + LocalPosition + _gameObjectPosition;
            }
            return LocalPosition + _gameObjectPosition;
        }
    }

    public void Update()
    {
        _gameObjectPosition = gameObject.transform.localPosition;
    }
}