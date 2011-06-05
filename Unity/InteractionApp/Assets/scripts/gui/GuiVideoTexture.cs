using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class GuiVideoTexture : GuiSimple {

    public GuiVideoTexture(Dictionary<string, object> args) : base("DrawTexture", args)
    {

        //_go2D.AddComponent<Renderer>().material.mainTexture = Video;
        Video.Play();
    }

    protected new void DrawImpl()
    {
        base.DrawImpl();


    }

    public MovieTexture Video
    {
        get { return _args["texture"] as MovieTexture; }
    }
    /*
    public MovieTexture Video
    {
        get { return _go2D.GetComponent<Renderer>().material.mainTexture as MovieTexture; }
    }

    public void Play()
    {
        Video.Play();
    }

    public void Stop()
    {
        Video.Stop();
    }
     */
}
