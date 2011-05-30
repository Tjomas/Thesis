using System;
using System.Collections;

public class GuiGroup : GuiDisplayObject
{
    public GuiGroup(SortedList args)
        : base(args)
    {
        Name = "Group";
    }

    protected override void DrawImpl()
    {
        //Nothing to do
    }
}