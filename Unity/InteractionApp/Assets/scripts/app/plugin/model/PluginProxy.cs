using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
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
        //LoadDlls();
		ReadPlugins();
		Data = Plugins;
    }

    public List<IPlugin> GetPluginList()
    {
        return (List<IPlugin>)Data;
    }
	

    private void LoadDlls()
    {
		_logger.Trace("sdsdsdsdsd 1");
		
     	Object[] byteAsset = Resources.LoadAll("plugins", typeof(TextAsset));
		
		_logger.Trace("sdsdsdsdsd 2");

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
			
			AddPlugin(bytes);
        }
    }
	
	private void AddPlugin(byte[] bytes){
		try{
			//_logger.Trace("sdsdsdsdsd 6a."+i + " " + bytes.ToString()+ " " + bytes.Length);
			
			//WWW www = new WWW("http://www.thomas-reufer.de/InteractionOne.bytes");
	        //yield return www;
	        //bytes = www.bytes;
			
			//bytes = StringToByteArray("###################### DebugString");
			
        	Assembly Asm = Assembly.Load(bytes);
			
			//_logger.Trace("sdsdsdsdsd 6b."+i);
		
			
			foreach (Type AsmType in Asm.GetTypes())
            {
				//j++;
				
				//_logger.Trace("sdsdsdsdsd 7."+i+"."+j);
				
                if (AsmType.GetInterface("IPlugin") != null)
                {
					//_logger.Trace("sdsdsdsdsd 8."+i+"."+j);
					
                    IPlugin plugin = Activator.CreateInstance(AsmType) as IPlugin;
					
					//_logger.Trace("sdsdsdsdsd 9."+i+"."+j);
					
                    if(plugin != null){
                        Plugins.Add(plugin);
                     	
						//_logger.Trace("sdsdsdsdsd 10."+i+"."+j);
						
						SendNotification(NoteConsts.CONSOLE_ADDMESSAGE,new MessageVO("Plugin loaded" + plugin.ToString()));
                    }
                }
            }
		}
		catch(Exception e){
			
			_logger.Trace(e.Message);	
			
		}
		
	}
	
	
	private void ReadPlugins(){
		/*IPAddress ipAddresse = IPAddress.Parse(NetConsts.SERVER_IP);
			
		// Instanziere einen Endpunkt mit der ersten IP-Adresse
		IPEndPoint ipEo = new IPEndPoint(ipAddresse, 8000);
		
		// IPv4 oder IPv6, Stream Socket, TCP
		Socket sock = new Socket(ipEo.AddressFamily,
		                  SocketType.Stream,
		                  ProtocolType.Tcp);
		
		// Öffne eine Socket Verbindung
		sock.Connect(ipEo);
	
		try
		{
			String szData = "requestPluginList";
			byte[] byData = System.Text.Encoding.ASCII.GetBytes(szData);
			sock.Send(byData);
		}
		catch (SocketException se)
		{
		
		}
		
		byte [] buffer = new byte[1024];
		int iRx = sock.Receive (buffer);
		
		AddPlugin(buffer);
		*/
		
		TcpClient MyClient = new TcpClient();
		MyClient.Connect(NetConsts.SERVER_IP, 8000);
		NetworkStream MyNetStream = MyClient.GetStream();
		
		if(MyNetStream.CanWrite && MyNetStream.CanRead)
		{
			// Does a simple write.
			Byte[] sendBytes = Encoding.ASCII.GetBytes("requestPluginList");
			MyNetStream.Write(sendBytes, 0, sendBytes.Length);
			
			// Reads the NetworkStream into a byte buffer.
			byte[] bytes = new byte[MyClient.ReceiveBufferSize];
			MyNetStream.Read(bytes, 0, (int) MyClient.ReceiveBufferSize);
			
			_logger.Trace("bytes recived");
			
			AddPlugin(bytes);
			
			//Returns the data received from the host to the console.
			//string returndata = Encoding.ASCII.GetString(bytes);
			//_logger.Trace("This is what the host returned to you: " + returndata);
		
		}
		else if (!MyNetStream.CanRead)
		{
			_logger.Trace("You can not write data to this stream");
			MyClient.Close();
		}
		else if (!MyNetStream.CanWrite)
		{             
			_logger.Trace("You can not read data from this stream");
			MyClient.Close();
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