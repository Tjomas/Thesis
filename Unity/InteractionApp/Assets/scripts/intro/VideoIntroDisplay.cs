using UnityEngine;
using System.Collections;

public class VideoIntroDisplay : MonoBehaviour
{

    public Texture Tex;

	// Use this for initialization
	void Start ()
	{
        GuiVideoTexture vid = new GuiVideoTexture(Tools.List("rect", new Rect(0, 0, 100, 100), "texture", Tex));
        GuiManager.AddChild(vid);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
