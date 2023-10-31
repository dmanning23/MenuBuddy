using Android.App;
using Android.OS;
using Android.Runtime;
using Square.LeakCanary;
using System;

namespace MenuBuddySample.Android
{
#if DEBUG
	[Application(Debuggable = true)]
#else
	[Application(Debuggable = false)]
#endif
	public class MainApplication : Application
	{
		private RefWatcher _refWatcher;

		public MainApplication(IntPtr handle, JniHandleOwnership transer)
			: base(handle, transer)
		{
		}

		public override void OnCreate()
		{
			StrictMode.VmPolicy.Builder builder = new StrictMode.VmPolicy.Builder();
			StrictMode.SetVmPolicy(builder.Build());
			base.OnCreate();
			SetupLeakCanary();
		}

		protected void SetupLeakCanary()
		{
			if (LeakCanaryXamarin.IsInAnalyzerProcess(this))
			{
				// This process is dedicated to LeakCanary for heap analysis.
				// You should not init your app in this process.
				return;
			}
			_refWatcher = LeakCanaryXamarin.Install(this);
		}
	}
}