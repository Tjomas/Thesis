using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GuiManager : MonoBehaviour
{
    #region Singelton

    private static GuiManager _instance = null;
    private static GameObject _go = null;

    public static GuiManager I
    {
        get
        {
            if (_instance == null)
            {
                _go = new GameObject("GuiManager", new Type[1] {typeof (GuiManager)});
                _instance = _go.GetComponent<GuiManager>();
                //Unter App anzeigen
                _go.transform.parent = App.GetInstance().gameObject.transform;
            }
            //
            return _instance;
        }
    }

    #endregion

    private List<GuiDisplayObject> _elements = null;

    public void OnGUI()
    {
        foreach (GuiDisplayObject element in _elements)
        {
            element.Draw();
        }
    }

    public static void AddChild(GuiDisplayObject displayObject)
    {
        I.AddChildInternal(displayObject);
    }

    protected void AddChildInternal(GuiDisplayObject displayObject)
    {
        if (_elements == null) _elements = new List<GuiDisplayObject>();

        displayObject.Parent = this.gameObject;
        _elements.Add(displayObject);
    }
}