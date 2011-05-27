using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using UnityEngine;

public class Logger
{
  #region Singelton

    private static Logger _instance = null;

    public static Logger I
    {
        get
        {
            if (_instance == null) _instance = new Logger();
            return _instance;
        }
    }

    private Logger()
    {
		_queue = new List<byte[]>();
    }

    #endregion
	
	private Socket sock = null;
    private string host = "127.0.0.1";  // Uri
    private int port = 4444;
	
	private List<byte[]> _queue;
	
	
	public static void Trace(string msg){
		I.AddMsg(msg,"trace");
	}
		
	public static void Debug(string msg){
		I.AddMsg(msg,"debug");
	}
	
	public static void Error(string msg){
		I.AddMsg(msg,"error");
	}
	
	private void AddMsg(String msg,string key){
		_queue.Add(Encoding.UTF8.GetBytes("!SOS<showMessage key='"+key+"'>"+msg+"</showMessage>\n" + (char)0));
		SocketSend();
	}
	
	private void SocketSend(){

		if(sock == null) Reconnect();
		
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


