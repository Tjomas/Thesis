using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class Button : GuiDisplayObject
{ 
    private MethodInfo _methode;

    public Button(params object[] args):base(args)
    {
        Name = "Button " + _id; 
        _methode = FindMethode("Button", args);
    }

    protected override void DrawImpl()
    {
        bool click = (bool) _methode.Invoke(null, GetArgs());
        if(click)
        {
            InvokeEvent(GuiEventType.CLICK);
        }
    }
}