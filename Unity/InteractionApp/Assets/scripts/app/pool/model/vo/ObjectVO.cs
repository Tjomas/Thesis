using System;

public class ObjectVO
{
	private string _path;
	
	public ObjectVO (string path)
	{
		_path = path;
	}
	
	public string Path{
		get{return _path;}
	}
}


