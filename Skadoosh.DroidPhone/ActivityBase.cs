using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;


namespace Skadoosh.DroidPhone
{

    public class ActivityBase : Activity
    {
      
        public void SetOrientationBackground(int resource)
        {
            var surfaceOrientation = WindowManager.DefaultDisplay.Rotation;
            var lo = FindViewById<LinearLayout>(resource);
            if (surfaceOrientation == SurfaceOrientation.Rotation90)
            {
                lo.SetBackgroundResource(Resource.Drawable.menu_dropdown_panel_skadoosh);
            }
            else
            {
                lo.SetBackgroundResource(Resource.Drawable.PandaBack);
            }
        }

    }
}