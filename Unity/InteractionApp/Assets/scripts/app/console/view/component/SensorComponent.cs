using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SensorComponent
{
    private Texture2D _tex;
    private int _graphWidth;
    private int _graphHeight;
    private int _graphHeightHalf;
    private int _borderBottom;
    private int _height;
    
    private Vector3[] _cache;
    private Vector3 _preVector;
    private int _x = 0;
    private GuiSimple _gui;

    public SensorComponent()
    {
        _graphWidth = 500;
        _graphHeight = 100;
        _graphHeightHalf = _graphHeight >> 1;
        _borderBottom = 100;
        _height = _graphHeight + _borderBottom;

        _cache = new Vector3[_graphWidth];
        _tex = new Texture2D(_graphWidth, _height, TextureFormat.ARGB32, false);
		
		
        _gui = new GuiSimple("DrawTexture", Tools.List("texture",_tex, "rect",new Rect(Screen.width - _graphWidth, 0, _graphWidth, _height)));
  		_gui.Name = "SensorGraph";
		
		GuiManager.I.Subscribe(new GuiEvent.EventHandler(Update), GuiEventType.UPDATE);
    }

    public GuiDisplayObject GetGui()
    {
        return _gui;
    }

    public void Update(GuiEvent e)
    {
        //Faking without sensor
        if(Application.platform != RuntimePlatform.Android){
            _cache[_x] = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)); ;
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

