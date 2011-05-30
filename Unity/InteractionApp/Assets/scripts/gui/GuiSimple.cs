using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class GuiSimple : GuiDisplayObject
{ 
    protected MethodInfo _methode;

    public GuiSimple(string guiType, Dictionary<string,object> args)
        : base(args)
    {
        Name = guiType + " " + _id;
        _methode = FindMethode(guiType, args);
    }

    protected override void DrawImpl()
    {
        _methode.Invoke(null, GetArgs());
    }
}