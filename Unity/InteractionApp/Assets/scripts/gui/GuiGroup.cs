using System;
using System.Collections.Generic;

public class GuiGroup : GuiDisplayObject
{
    public GuiGroup(params object[] args)
        : base(args)
    {
        Name = "Group";
    }

    protected override void DrawImpl()
    {
        //Nothing to do
    }
}