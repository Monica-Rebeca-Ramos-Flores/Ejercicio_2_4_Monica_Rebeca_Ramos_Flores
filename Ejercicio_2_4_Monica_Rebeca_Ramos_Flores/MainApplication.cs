using System;
using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.Runtime;

using Plugin.CurrentActivity;

namespace Ejercicio_2_4_Monica_Rebeca_Ramos_Flores
{
#if DEBUG
    [Application(Debuggable = true)]
#else
    [Application(Debuggable = false)]
#endif
    public class MainApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
            : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            CrossCurrentActivity.Current.Init(this);
        }
    }
}
