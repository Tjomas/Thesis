using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using UnityEngine;

public class Logger
{
	#region StaticNetwork
	
	static Logger(){
		_queue = new List<byte[]>();
	}
	
	private static Socket sock = null;
    private static string host = NetConsts.SERVER_IP;
    private static int port = 4444;
	private static List<byte[]> _queue;
	
	#endregion
	
	[Flags]
	public enum LEVEL {NONE=0,TRACE=1,DEBUG=2,ERROR=4,ALL=7}
	public static LEVEL Level = LEVEL.ALL;
	
	private string _tag;
	
	public string TAG{
		get{return _tag;}
		set{_tag = value;}
	}
	
	public Logger(string tag)
    {
		_tag = tag;
		_queue = new List<byte[]>();
    }
	
	public void Trace(string msg){
		if((Level & LEVEL.TRACE) == LEVEL.TRACE) 
		AddMsg(msg,"trace");
	}
		
	public void Debug(string msg){
		if((Level & LEVEL.DEBUG) == LEVEL.DEBUG) 
		AddMsg(msg,"debug");
	}
	
	public void Error(string msg){
		if((Level & LEVEL.ERROR) == LEVEL.ERROR) 
		AddMsg(msg,"error");
	}
	
	
	private void AddMsg(String msg,string key){
		_queue.Add(Encoding.UTF8.GetBytes("!SOS<showMessage key='" + key + "'><![CDATA[["+Time.time+"][" + _tag + "] " + msg + "]]></showMessage>\n" + (char)0));
		SocketSend();
	}

	
	private void SocketSend(){
		
		if(sock == null) Reconnect();
		
		lock(sock){
			try {
				if (sock.Connected) {
					
				if(_queue.Count > 0){
					byte[] msg = _queue[0];
					_queue.RemoveAt(0);
					sock.Send(msg, SocketFlags.None);
				}
				
			}
			} catch (Exception e)
			{
	            UnityEngine.Debug.LogError(e.Message);
			}
		}
	}
	

	
	private void Reconnect(){
		try {
			IPAddress ipAddresse = IPAddress.Parse(host);
			
			// Instanziere einen Endpunkt mit der ersten IP-Adresse
			IPEndPoint ipEo = new IPEndPoint(ipAddresse, port);
			
			// IPv4 oder IPv6, Stream Socket, TCP
			sock = new Socket(ipEo.AddressFamily,
			                  SocketType.Stream,
			                  ProtocolType.Tcp);
			
			// Öffne eine Socket Verbindung
			sock.Connect(ipEo);

		} catch (Exception e) {
            UnityEngine.Debug.LogError(e.Message);
		}
	}
}


