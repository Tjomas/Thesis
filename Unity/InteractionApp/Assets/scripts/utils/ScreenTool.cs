using System;
using UnityEngine;

public class ScreenTool
{
	public static Rect FULLSCREEN{
		get{ 
			return new Rect(0,0,Screen.width,Screen.height);
		}
	}
}


