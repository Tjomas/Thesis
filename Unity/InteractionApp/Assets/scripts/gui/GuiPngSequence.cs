using System;
using System.Xml;
using UnityEngine;
using System.Collections;

public class GuiPngSequence: GuiSimple
{
    private int _fps = 25;
    private int _length = 100;
    private int _currentframe = 0;
    private bool _enabled = false;
    private Texture2D _lastTex;
    private float _dt = 0f;
    private float _timePerFrame;
    private string _sequence;
    private bool _toggleLoop;

    public GuiPngSequence(string sequence, int length, int fps):base("DrawTexture",Tools.List("rect",new Rect(0,0,Screen.width,Screen.height),"texture",new Texture2D(1,1),"scaleMode",ScaleMode.ScaleAndCrop))
    {
        _sequence = sequence;
        _length = length;
        _fps = fps;
        _timePerFrame = 1000f/(float)_fps;
        //
        _enabled = true;
        //
		Texture2D newTex = Resources.Load("textures/menu/"+BuildPath(0)) as Texture2D;
		if (newTex){
			_args["texture"] = newTex;
			Debug.Log("Yeah");
		}
		else{
			Debug.Log("Kackamist textures/menu/"+BuildPath(0));	
		}
        //Texture2D newTex = _prelaoder.GetPool().GetTexture(BuildPath(0));
        //if (newTex) _lastTex = newTex;
    }

    public void Play()
    {
        _enabled = true;
    }

    public void Stop()
    {
        _enabled = false;
    }

    public void Toggle()
    {
        _enabled = !_enabled;
    }
	
	protected override void DrawImpl()
    {
		base.DrawImpl();
		
		Debug.Log("Pups " + UnityEngine.Random.value);
		
		if(_enabled)
        {
            _dt += Time.deltaTime * 1000f;
            if (_dt > _timePerFrame)
            {
                _dt -= _timePerFrame;
                int newFrame = (_currentframe + 1)%(_length);
                if(newFrame < _currentframe && _toggleLoop)
                {
                    Stop();
                }

                _currentframe = newFrame;
            }
            
            //Texture2D newTex = _prelaoder.GetPool().GetTexture(buildPath(_currentframe));
			Texture2D newTex = Resources.Load("textures/menu/"+BuildPath(_currentframe)) as Texture2D;
            if(newTex){ 
				_args["texture"]  = newTex;
				MarkDirty();
			}
        }
    }


    public Texture2D getTexture()
    {
        if(_enabled)
        {
            _dt += Time.deltaTime * 1000f;
            if (_dt > _timePerFrame)
            {
                _dt -= _timePerFrame;
                int newFrame = (_currentframe + 1)%(_length);
                if(newFrame < _currentframe && _toggleLoop)
                {
                    Stop();
                }

                _currentframe = newFrame;
            }
            
            //Texture2D newTex = _prelaoder.GetPool().GetTexture(buildPath(_currentframe));
			Texture2D newTex = Resources.Load("textures/menu/"+BuildPath(_currentframe)) as Texture2D;
            if(newTex) _args["texture"]  = newTex;
        }
        return _lastTex;
    }

    private string BuildPath(int id)
    {
        string picpath = (id + 1).ToString();
        if (id < 10) picpath = "000" + picpath;
        else if (id < 100) picpath = "00" + picpath;
        return _sequence + picpath;
    }

    public void ToggleLoop()
    {
        _toggleLoop = !_toggleLoop;
    }
}
