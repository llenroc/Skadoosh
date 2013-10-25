package skadoosh.droidphone;


public class PushHandlerService
	extends pushsharp.client.PushHandlerServiceBase
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("skadoosh.DroidPhone.PushHandlerService, skadoosh.DroidPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", PushHandlerService.class, __md_methods);
	}


	public PushHandlerService (java.lang.String p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == PushHandlerService.class)
			mono.android.TypeManager.Activate ("skadoosh.DroidPhone.PushHandlerService, skadoosh.DroidPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0 });
	}


	public PushHandlerService () throws java.lang.Throwable
	{
		super ();
		if (getClass () == PushHandlerService.class)
			mono.android.TypeManager.Activate ("skadoosh.DroidPhone.PushHandlerService, skadoosh.DroidPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public PushHandlerService (java.lang.String[] p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == PushHandlerService.class)
			mono.android.TypeManager.Activate ("skadoosh.DroidPhone.PushHandlerService, skadoosh.DroidPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.String[], mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0 });
	}

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
