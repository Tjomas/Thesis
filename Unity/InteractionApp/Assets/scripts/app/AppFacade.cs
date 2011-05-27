using System;
using System.Collections.Generic;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;



public class AppFacade : Facade
{
	#region Accessors

	/// <summary>
	/// Facade Singleton Factory method.  This method is thread safe.
	/// </summary>
	public new static IFacade Instance
	{
		get
		{
			if (m_instance == null)
			{
				lock (m_staticSyncRoot)
				{
					if (m_instance == null) m_instance = new AppFacade();
				}
			}

			return m_instance;
		}
	}

	#endregion

	#region Public methods

	/// <summary>
	/// Start the application
	/// </summary>
	/// <param name="app"></param>
	public void Startup(object app)
	{
		SendNotification(NoteConsts.APP_STARTUP, app);
	}

	#endregion

	#region Protected & Internal Methods

	protected AppFacade()
	{
		// Protected constructor.
	}

	/// <summary>
	/// Explicit static constructor to tell C# compiler 
	/// not to mark type as beforefieldinit
	///</summary>
	static AppFacade()
	{
	}

	/// <summary>
	/// Register Commands with the Controller
	/// </summary>
	protected override void InitializeController()
	{
		base.InitializeController();
		RegisterCommand(NoteConsts.APP_STARTUP, typeof(StartupCommand));
	}

	#endregion
}

