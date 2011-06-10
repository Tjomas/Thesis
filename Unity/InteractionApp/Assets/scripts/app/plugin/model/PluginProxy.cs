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
	
	private static Logger _logger = new Logger(NAME); 
	
    public PluginProxy():base(NAME)
    {
        LoadDlls();
		Data = Plugins;
    }

    public List<IPlugin> GetPluginList()
    {
        return (List<IPlugin>)Data;
    }
	
	/*IEnumerator Start() {
        WWW www = new WWW(url);
        yield return www;
        _logger.Error("YIIIIIEEEEEELLLLLDDDDDDD");
    }*/

    private void LoadDlls()
    {
		_logger.Trace("sdsdsdsdsd 1");
		
     	Object[] byteAsset = Resources.LoadAll("plugins", typeof(TextAsset));
		
		_logger.Trace("sdsdsdsdsd 2");
		
		
		Start();
		
		int i = 0;
		
		int j = 0;
		
        foreach (Object o in byteAsset)
        {
			i++;
			
			_logger.Trace("sdsdsdsdsd 3."+i);
			
            TextAsset ta = o as TextAsset;
            
			_logger.Trace("sdsdsdsdsd 4."+i);
			
			byte[] bytes = ta.bytes;
			
			_logger.Trace("sdsdsdsdsd 5."+i);
			
			try{
				_logger.Trace("sdsdsdsdsd 6a."+i + " " + bytes.ToString()+ " " + bytes.Length);
				
				//WWW www = new WWW("http://www.thomas-reufer.de/InteractionOne.bytes");
		        //yield return www;
		        //bytes = www.bytes;
				
				bytes = StringToByteArray("###################### DebugString");
				
            	Assembly Asm = Assembly.Load(bytes);
				
				_logger.Trace("sdsdsdsdsd 6b."+i);
			
				
				foreach (Type AsmType in Asm.GetTypes())
	            {
					j++;
					
					_logger.Trace("sdsdsdsdsd 7."+i+"."+j);
					
	                if (AsmType.GetInterface("IPlugin") != null)
	                {
						_logger.Trace("sdsdsdsdsd 8."+i+"."+j);
						
	                    IPlugin plugin = Activator.CreateInstance(AsmType) as IPlugin;
						
						_logger.Trace("sdsdsdsdsd 9."+i+"."+j);
						
	                    if(plugin != null){
	                        Plugins.Add(plugin);
	                     	
							_logger.Trace("sdsdsdsdsd 10."+i+"."+j);
							
							SendNotification(NoteConsts.CONSOLE_ADDMESSAGE,new MessageVO("Plugin loaded" + plugin.ToString()));
	                    }
	                }
	            }
			}
			catch(Exception e){
				
				_logger.Trace(e.Message);	
				
			}
        }
    }
	
	
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

	
	private byte[] StringToByteArray(string str)
	{
	    System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
	    return enc.GetBytes(str);
	}
	
	private string ByteArrayToString(byte[] arr)
	{
	    System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
	    return enc.GetString(arr);
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