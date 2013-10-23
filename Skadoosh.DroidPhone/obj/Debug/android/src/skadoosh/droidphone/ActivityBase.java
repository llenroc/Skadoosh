package skadoosh.droidphone;


public class ActivityBase
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Skadoosh.DroidPhone.ActivityBase, Skadoosh.DroidPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ActivityBase.class, __md_methods);
	}


	public ActivityBase () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ActivityBase.class)
			mono.android.TypeManager.Activate ("Skadoosh.DroidPhone.ActivityBase, Skadoosh.DroidPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
