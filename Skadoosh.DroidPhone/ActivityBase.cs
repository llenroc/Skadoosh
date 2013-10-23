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
using Skadoosh.Common.DomainModels;


namespace Skadoosh.DroidPhone
{
    public class AppModel
    {
        public static ViewModelBase VM { get; set; }
    }
    public class ActivityBase : Activity
    {
      
        public void SetOrientationBackground(int resource)
        {
            var surfaceOrientation = WindowManager.DefaultDisplay.Rotation;
            var lo = FindViewById<LinearLayout>(resource);
            if (surfaceOrientation == SurfaceOrientation.Rotation90 || surfaceOrientation== SurfaceOrientation.Rotation270)
            {
                lo.SetBackgroundResource(Resource.Drawable.menu_dropdown_panel_skadoosh);
            }
            else
            {
                lo.SetBackgroundResource(Resource.Drawable.PandaBack);
            }
        }

        public void ChangeDialogColor(AlertDialog dialog)
        {
            var decorView = (ViewGroup)dialog.Window.DecorView;
            var windowContentView = ViewGroupIndexOf<ViewGroup>(decorView, 0);
            var contentView = ViewGroupIndexOf<ViewGroup>(windowContentView, 0);
            var parentPanel = ViewGroupIndexOf<ViewGroup>(contentView, 0);
            var topPanel = ViewGroupIndexOf<ViewGroup>(parentPanel, 0);
            var titleDivider = ViewGroupIndexOf<View>(topPanel, 2);
            titleDivider.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FF4F00"));
        }
        public T ViewGroupIndexOf<T>(ViewGroup grp, int index) where T : View
        {
            return (T)grp.GetChildAt(index);
        }

    }
}