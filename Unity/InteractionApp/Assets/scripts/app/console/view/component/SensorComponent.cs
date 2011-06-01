using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SensorComponent: GuiSimple
{
    private Texture2D _tex;
    private int _graphWidth;
    private int _graphHeight;
    private int _graphHeightHalf;
    private int _borderBottom;
    private int _height;
    
    private Vector3[] _cache;
    private Vector3 _preVector;
	private Vector3 _debugVec; //slidervalues in editormode
    private int _x = 0;
    //private GuiSimple _gui;

    public SensorComponent():base("DrawTexture", Tools.List("rect",new Rect(0,0,0,0),"texture",new Texture2D(1,1)))
    {
        _graphWidth = 500;
        _graphHeight = 100;
        _graphHeightHalf = _graphHeight >> 1;
        _borderBottom = 100;
        _height = _graphHeight + _borderBottom;

        _cache = new Vector3[_graphWidth];
        _tex = new Texture2D(_graphWidth, _height, TextureFormat.ARGB32, false);
		
		//default debug vec
		_debugVec = new Vector3(0f,0.1f,0.2f);
		
		Position = new Vector2(Screen.width - _graphWidth, 0);
		Dimension = new Vector2(_graphWidth, _height);
		
		_args["texture"] = _tex;
		Name = "SensorGraph";
		MarkDirty();
		
        if(!AppConsts.MOBILE){
			GuiSlider slider;
			
			//X
			slider = new GuiSlider(Tools.List("rect",new Rect(0,_height,_graphWidth,20),"value",0f,"leftvalue",-1.0f,"rightvalue",1f));
			slider.Subscribe (new GuiEvent.EventHandler (EditorVecUpdate), GuiEventType.VALUE_CHANGED, Tools.List("axis",0));
			AddChild(slider);
			
			//Y
			slider = new GuiSlider(Tools.List("rect",new Rect(0,_height + 20,_graphWidth,20),"value",0f,"leftvalue",-1.0f,"rightvalue",1f));
			slider.Subscribe (new GuiEvent.EventHandler (EditorVecUpdate), GuiEventType.VALUE_CHANGED, Tools.List("axis",1));
			AddChild(slider);
			
			//Z
			slider = new GuiSlider(Tools.List("rect",new Rect(0,_height + 40,_graphWidth,20),"value",0f,"leftvalue",-1.0f,"rightvalue",1f));
			slider.Subscribe (new GuiEvent.EventHandler (EditorVecUpdate), GuiEventType.VALUE_CHANGED, Tools.List("axis",2));
			AddChild(slider);
		}
		
		GuiManager.I.Subscribe(new GuiEvent.EventHandler(Update), GuiEventType.UPDATE);
    }

    /*public GuiDisplayObject GetGui()
    {
        return _gui;
    }*/
	
	public void EditorVecUpdate(GuiEvent e){
		switch((int)e["axis"]){
			case 0: _debugVec = new Vector3(((GuiSlider)e.Sender).VALUE,_debugVec.y,_debugVec.z);break;
			case 1: _debugVec = new Vector3(_debugVec.x,((GuiSlider)e.Sender).VALUE,_debugVec.z);break;
			case 2: _debugVec = new Vector3(_debugVec.x,_debugVec.y,((GuiSlider)e.Sender).VALUE);break;	
		}
	}

    public void Update(GuiEvent e)
    {
        //Faking without sensor
        if(!AppConsts.MOBILE){
			_cache[_x] = _debugVec;
        }
        else
        {
            _cache[_x] = Input.acceleration;
        }

        //clear col
        for (int y = 0; y < _height; y++)
        {
            _tex.SetPixel(_x, y, Color.clear);
        }

        //Calc Position
        int currX = (int)(_cache[_x].x * _graphHeightHalf) + _graphHeightHalf - _borderBottom;
        int currY = (int)(_cache[_x].y * _graphHeightHalf) + _graphHeightHalf - _borderBottom;
        int currZ = (int)(_cache[_x].z * _graphHeightHalf) + _graphHeightHalf - _borderBottom;
        Vector3 currentVec = new Vector3(currX, currY, currZ);

        //draw sensor values
        drawLine(Math.Max(_x - 1, 0), (int)_preVector.x, _x, (int)currentVec.x, Color.green);
        drawLine(Math.Max(_x - 1, 0), (int)_preVector.y, _x, (int)currentVec.y, Color.red);
        drawLine(Math.Max(_x - 1, 0), (int)_preVector.z, _x, (int)currentVec.z, Color.blue);

        //draw jitter
        float jitterZ = Mathf.Abs(_cache[Math.Max(_x - 1, 0)].z - _cache[_x].z) / 2f;
        drawLine(_x, _height - _borderBottom, _x, (int)(_height - _borderBottom - (jitterZ * _borderBottom)), Color.blue);
       
		//draw line
        drawLine(0, _height - _borderBottom, _graphWidth, _height - _borderBottom, Color.white); 

        _tex.Apply();

        _preVector = currentVec;

        _x = (_x + 1) % _graphWidth;
    }

    //Bresenham
    //http://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm
    private void drawLine(int x0,int y0 ,int x1,int y1, Color color)
    {

        int dx = Math.Abs(x1 - x0);
        int dy = Math.Abs(y1 - y0);

        int sx, sy;

        if( x0 < x1){ sx = 1;}
        else{ sx = -1;}
        if( y0 < y1){ sy = 1;}
        else{ sy = -1;}
        int err = dx - dy;

        while (x0 != x1 || y0 != y1)
        {
            _tex.SetPixel(x0, y0, color);

            int e2 = 2*err;
            if(e2 > -dy)
            {
                err = err - dy;
                x0 = x0 + sx;
            }
             if( e2 <  dx)
             {
                 err = err + dx;
                 y0 = y0 + sy;
             }
        }
    }
}

