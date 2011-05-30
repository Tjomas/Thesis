using System;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;

public class ObjectProxy: Proxy, IProxy
{
	public new const string NAME = "ObjectProxy";
	
	private Logger _logger = new Logger(NAME);
	
	public ObjectProxy ():base(NAME)
	{
		Data = new List<ObjectVO>();	
	}

	public void LoadScene(ObjectVO vo){
		_logger.Debug("Load new object " + vo.Path);
		Data.Add(vo);
	}
	
	public List<ObjectVO> Data {
		get {
			return (List<ObjectVO>)base.Data;
		}
	}
	
}


