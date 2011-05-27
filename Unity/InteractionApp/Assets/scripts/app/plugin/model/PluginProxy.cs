using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using De.Wellenblau.Inferfaces;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using Object = UnityEngine.Object;

public class PluginProxy: Proxy , IProxy
{
    private List<IPlugin> Plugins = new List<IPlugin>();
	
	public new const string NAME = "PluginProxy";

    public PluginProxy():base(NAME)
    {
        LoadDlls();
		Data = Plugins;
    }

    public List<IPlugin> GetPluginList()
    {
        return (List<IPlugin>)Data;
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
                     	
						SendNotification(NoteConsts.CONSOLE_ADDMESSAGE,new MessageVO("Plugin loaded" + plugin.ToString()));
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