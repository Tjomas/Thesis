using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GuiScaleTexture:GuiDisplayObject
{
	protected MethodInfo _methode;
	
	private float _scaleY;
	
	public GuiScaleTexture(Dictionary<string,object> args)
	    : base(args)
	{
		if(!_args.ContainsKey("texture")) throw new Exception("texture is missing");
			
	    Name = "GuiScaleTexture " + _id;
	    _methode = FindMethode("DrawTexture", args);
		
		
		_scaleY = (float)Screen.height / (float)Texture.height;
		
		Debug.Log("EEEEEEEEEEEEEEE" + Texture.height +" "+ Texture.width );
		Debug.Log("EEEEEEEEEEEEEEE" + Texture.width * _scaleY);
		
		Dimension = new Vector2(Texture.width * _scaleY, Screen.height);
		MarkDirty();	
	}
	
	public float ScaleValue(float value){
		return value * _scaleY;	
	}
	
	public Texture2D Texture{
		get{return _args["texture"] as Texture2D;}
		set{_args["texture"] = value;}
	}
	
	protected override void DrawImpl()
	{
		if(_methode != null) _methode.Invoke(null, GetArgs());
	}
}


