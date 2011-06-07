#define MOBILE_PLATFORM

using UnityEngine;
using System.Collections;

public class VideoIntroDisplay : MonoBehaviour
{
	
	public Texture Tex;
	
#if !MOBILE_PLATFORM
	public MovieTexture MovieTex{
		get{return Tex as MovieTexture;}
	}
#else	
	//Mobile - Android Check
	private float _startTime = 0f;
	private const float _duration = 4f;
#endif
	
	private bool _skipIntro;
	
	// Use this for initialization
	void Start ()
	{
#if !MOBILE_PLATFORM
		GuiSimple vid = new GuiSimple("DrawTexture",Tools.List("rect", new Rect(0, 0, Screen.width, Screen.height), "texture", MovieTex, "scaleMode",ScaleMode.ScaleAndCrop));
        GuiManager.AddChild(vid);
		
		audio.clip = MovieTex.audioClip;
		MovieTex.Play();
		audio.Play();
#else
		iPhoneSettings.screenCanDarken = false;
		iPhoneUtils.PlayMovie("wbi_intro.mp4",Color.white,iPhoneMovieControlMode.CancelOnTouch,iPhoneMovieScalingMode.Fill);	
        _startTime = Time.time;
#endif
	}
	
	// Update is called once per frame
	void Update () {
		
		if(_skipIntro == true) return;
		
#if !MOBILE_PLATFORM		
		if(Input.anyKeyDown || !MovieTex.isPlaying){
			_skipIntro = true;
			Application.LoadLevel("main");
		}
#else		
		//Mobile
		Application.LoadLevel("main");
#endif		
	}
}

#if MOBILE_PLATFORM
public class MovieTexture: Texture
{}
#endif
