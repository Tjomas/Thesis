using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using De.Wellenblau.Inferfaces;
using UnityEngine;
using Object = UnityEngine.Object;

public class PluginManager
{
    private List<IPlugin> Plugins = new List<IPlugin>();

    private GuiDisplayObject _pluginList;

    private bool showList;

    public PluginManager()
    {
        _pluginList = new GuiGroup(new Rect(Screen.width+10,10,0,0));
		
        GuiManager.AddChild(_pluginList);
		
		
		switch(Application.platform){
		case RuntimePlatform.Android:
			InputManager.I.Subscribe(new GuiKeyEvent.EventHandler(ToggleList), GuiEventType.KEYDOWN, Tools.Hash("key", KeyCode.Menu));
			break;
		default:
			InputManager.I.Subscribe(new GuiKeyEvent.EventHandler(ToggleList), GuiEventType.KEYDOWN, Tools.Hash("key", KeyCode.F3));
			break;
		}
		
				
		
        LoadDlls();
    }

    public void ToggleList(GuiEvent e)
    {
        AppConsole.AddMessage("Toogle PluginList");

		//toggle plugin list
		showList = !showList;	
        if(showList == true) iTween.MoveTo(_pluginList.ToGameObject(),new Vector3(-220,0,0),1);
        else iTween.MoveTo(_pluginList.ToGameObject(), new Vector3(0, 0, 0), 1);    
    }


    private void LoadDlls()
    {
        Object[] byteAsset = Resources.LoadAll("plugins", typeof(TextAsset));
     
        foreach (Object o in byteAsset)
        {

            TextAsset ta = o as TextAsset;
            byte[] bytes = ta.bytes;

            Assembly Asm = Assembly.Load(bytes);
            foreach (Type AsmType in Asm.GetTypes())
            {
                if (AsmType.GetInterface("IPlugin") != null)
                {
                    //AssemblyName[] referencedAssemblies = AsmType.Assembly.GetReferencedAssemblies();
                    //foreach (AssemblyName referencedAssembly in referencedAssemblies)
                    //{
                    //    Debug.Log(referencedAssembly.Name);
                    //}

                    //Debug.Log(AsmType.GetInterface("IPlugin").FullName);
					
                    //Debug.Log(AsmType.FullName);
                    //Debug.Log(AsmType.GUID);
                    //Debug.Log(typeof(IPlugin).FullName);
                    //Debug.Log(typeof(IPlugin).GUID);


                    IPlugin plugin = Activator.CreateInstance(AsmType) as IPlugin;

                    if(plugin != null){
                        Plugins.Add(plugin);

                        _pluginList.AddChild(new Button(new Rect(0, (Plugins.Count-1) * 50, 200, 40), plugin.ToString()));
                        AppConsole.AddMessage("Plugin loaded" + plugin.ToString());
                    }
                }
            }
        }
    }


    public override string ToString()
    {
        string ids = "Plugin-List Start\n";

        foreach (IPlugin plugin in Plugins)
        {
            ids += plugin.GetID() + "\n";

        }

        ids += "Plugin-List End";

        return ids;
    }
}