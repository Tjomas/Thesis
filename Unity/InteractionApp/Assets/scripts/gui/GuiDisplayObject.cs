using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using UnityEngine;

public abstract class GuiDisplayObject
{
    public bool Visible
    {
        get { return _go2D.active; }
        set { _go2D.active = value; }
    }

    protected List<GuiDisplayObject> _childs = null;

    public int ChildCount
    {
        get
        {
            if (_childs != null) return _childs.Count;
            else return 0;
        }
    }

    private List<GuiEvent> _events = null;

    protected GameObject _go2D;

    protected GuiWidget _widget;

    protected int _id;

    protected static int _idCounter = 0;

    public GuiDisplayObject this[int index]
    {
        get
        {
            if (_childs == null || _childs.Count - 1 < index) return null;
            return _childs[index];
        }
        set
        {
            if (_childs == null || _childs.Count - 1 < index) return;
            _childs[index] = value;
        }
    }

    public Vector2 Position
    {
        get { return _widget.Position; }
        set { _widget.LocalPosition = value; }
    }


    public Vector2 Size
    {
        get { return _widget.Size; }
        set { _widget.Size = value; }
    }

    public GameObject Parent
    {
        get { return _go2D.transform.parent.gameObject; }
        set { _go2D.transform.parent = value.transform; }
    }


    public string Name
    {
        get { return _go2D.name; }
        set { _go2D.name = value; }
    }

    public string Text
    {
        get { return _args[_txtIndex].ToString(); }
        set
        {
            for (int i = 0; i < _args.Length; i++)
            {
                object arg = _args[i];
                if(arg.GetType().Equals(typeof(string)))
                {
                    _args[i] = value;
                    _txtIndex = i;
                    return;
                }
            }
        }
    }

    private object[] _args;
    private int _rectIndex = 0;
    private int _txtIndex = 0;

    public GuiDisplayObject(object[] args)
    {
        _args = args;
        _id = _idCounter++;
        _go2D = new GameObject("2D GameObject - " + _id);
        //
        for (int i = 0; i < _args.Length; i++)
        {
            object arg = _args[i];
            if (arg.GetType().Equals(typeof (Rect)))
            {
                Rect r = (Rect) arg;
                _widget = _go2D.AddComponent<GuiWidget>();
                _widget.LocalPosition = new Vector2(r.x, r.y);
                _widget.Size = new Vector2(r.width, r.height);

                _rectIndex = i;
            }
        }
        //
        Visible = true;
    }

    protected object[] GetArgs()
    {
        Rect r = new Rect(Position.x, Position.y, Size.x, Size.y);
        _args[_rectIndex] = r;
        return _args;
    }

    public void Draw()
    {
        if (_go2D.active == false) return; //early out

        DrawImpl();
		
        //if(Input.anyKeyDown) InvokeEvent(GuiEventType.KEY);

        if (_childs != null)
        {
            foreach (GuiDisplayObject child in _childs)
            {
                child.Draw();
            }
        }
    }

    protected MethodInfo FindMethode(string name, object[] args)
    {
        Type[] types = new Type[args.Length];

        for (int i = 0; i < args.Length; i++)
        {
            types[i] = args[i].GetType();
        }

        return typeof (GUI).GetMethod(name, types);
    }

    public void AddChild(GuiDisplayObject displayObject)
    {
        if (_childs == null) _childs = new List<GuiDisplayObject>();

        displayObject.Parent = _go2D;
        _childs.Add(displayObject);
    }

	public void Subscribe(GuiEvent.EventHandler callback,GuiEventType type, Hashtable parameter){
		GuiEvent e = new GuiEvent(this,type, parameter);
		e.Handler += callback;
		//
		if (_events == null) _events = new List<GuiEvent>();
        _events.Add(e);
	}

    protected void InvokeEvent(GuiEventType type)
    {
        if (_events == null) return; //early out

        foreach (GuiEvent guiEvent in _events)
        {
            guiEvent.Invoke(type);
        }
    }

    public GameObject ToGameObject()
    {
        return _go2D;
    }

    protected abstract void DrawImpl();
}