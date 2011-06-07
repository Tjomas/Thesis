using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GuiManager : MonoBehaviour
{
    #region Singelton

    private static GuiManager _instance = null;
    private static GameObject _go = null;
	private List<GuiEvent> _events = null;

    public static GuiManager I
    {
        get
        {
            if (_instance == null)
            {
                _go = new GameObject("Graph2D", new Type[1] {typeof (GuiManager)});
                _instance = _go.GetComponent<GuiManager>();
                _go.transform.parent = GameObject.Find("Graph").transform;
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
	
	public void Subscribe(GuiEvent.EventHandler callback,GuiEventType type){
		Subscribe(callback,type,null);
	}
	
	public void Subscribe(GuiEvent.EventHandler callback,GuiEventType type, Dictionary<string,object> parameter){
		GuiEvent e = new GuiEvent(null ,type, parameter);
		e.Handler += callback;
		//
		if (_events == null) _events = new List<GuiEvent>();
        _events.Add(e);
	}
	
	public void Update(){
        if(_events == null) return;

		foreach(GuiEvent e in _events){
			e.Invoke(GuiEventType.UPDATE);
		}
	}
}