using System;
using UnityEngine;

public class AppConsts
{
	
	public const string ROOTNODE = "App";
	
	public static readonly bool MOBILE;
	public static readonly bool EDITOR;
	public static readonly bool DESKTOP;
	public static readonly bool WEB;
	
	static AppConsts(){
		MOBILE = Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
		EDITOR = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor;
		DESKTOP = Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.WindowsPlayer;
		WEB = Application.platform == RuntimePlatform.OSXWebPlayer || Application.platform == RuntimePlatform.WindowsWebPlayer;
	}
}


