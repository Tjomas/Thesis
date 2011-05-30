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
	
	private Rect _rect;
    public Vector2 Position
    {
        get { return _widget.Position; }
        set { _widget.LocalPosition = value; }
    }


    public Vector2 Dimension
    {
        get { return _widget.Dimension; }
        set { _widget.Dimension = value; }
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
	
	private string _text;
    public string Text
    {
        get { return _text; }
        set
        {
            _text = value;
			if(_args.ContainsKey("text")) _args["text"] = value;
			else _args.Add("text",value);
        }
    }
	
	private bool _dirty = false;
	
	private static Logger _logger = new Logger("GuiDisplayObject");
	
    private SortedList _args;
	private object[] _argsObject;
	private System.Type[] _argsType;

    public GuiDisplayObject(SortedList args)
    {
		
		//Pflichtfelde
		if(args.ContainsKey("rect") == false){
			throw new Exception("No Rect defined");
		}
		
		//Text
		_text = "";
		if(args.ContainsKey("text")){
			_text = (string) args["text"];	
		}
		
        _args = args;
        _id = _idCounter++;
        _go2D = new GameObject("2D GameObject - " + _id);

		
		//Position
        _rect = (Rect) args["rect"];
        _widget = _go2D.AddComponent<GuiWidget>();
        _widget.LocalPosition = new Vector2(_rect.x, _rect.y);
        _widget.Dimension = new Vector2(_rect.width, _rect.height);

        //make active
        Visible = true;
    }

    protected object[] GetArgs()
    {
		
		
		if(_argsObject == null || _dirty){
			_rect.x = Position.x;
			_rect.y = Position.y;
			_rect.width = Dimension.x;
			_rect.height = Dimension.y;
			_args["rect"] = _rect;
			_argsObject = HashToObjectArray(_args);
			_dirty = false;
			//
			_logger.Debug("Dirty");
        }
			
		return _argsObject;
    }
	
    public void Draw()
    {
        if (_go2D.active == false) return; //early out
		
		//dirtycheck
		_dirty = _dirty || !Position.Equals(new Vector2(_rect.x,_rect.y));
		_dirty = _dirty || !Dimension.Equals(new Vector2(_rect.width,_rect.height));
		
		//draw
        DrawImpl();

        if (_childs != null)
        {
            foreach (GuiDisplayObject child in _childs)
            {
                child.Draw();
            }
        }
    }
	
	protected object[] HashToObjectArray(SortedList args){
		object[] tmp = new object[args.Count]; 
		int i = 0;
		
		foreach(DictionaryEntry arg in args)
            {
               tmp[i++] = arg.Value;
            }
		
		return tmp;
	}
	
	protected Type[] HashToTypeArray(SortedList args){
		Type[] tmp = new Type[args.Count]; 
		int i = 0;
		
		foreach(DictionaryEntry a in args)
        {
           tmp[i++] = a.Value.GetType();
        }
		
		return tmp;
	}
	
    protected MethodInfo FindMethode(string name, SortedList args)
    {
        Type[] types = HashToTypeArray(args);
		MethodInfo methode = typeof (GUI).GetMethod(name, types);
        return methode;
    }

    public void AddChild(GuiDisplayObject displayObject)
    {
        if (_childs == null) _childs = new List<GuiDisplayObject>();

        displayObject.Parent = _go2D;
        _childs.Add(displayObject);
    }

	public void Subscribe(GuiEvent.EventHandler callback,GuiEventType type, SortedList parameter){
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