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



namespace skadoosh.DroidPhone
{
    public class AppModel
    {
        public static ViewModelBase VM { get; set; }
        public static GCMNotifier GCMNote { get; set; }

    }
    public delegate void GCMRegistrationHander(string registrationId);
    public class GCMNotifier
    {
        public event GCMRegistrationHander RegistrationUpdated;
        public void SendGCMNotification(string registrationId)
        {
            if (RegistrationUpdated != null)
            {
                RegistrationUpdated(registrationId);
            }
        }

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


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            menu.Add(0, 0, 0, "Help");
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == 0)
            {
                StartActivity(typeof(Help));
                return true;
            }
            else
            {
                return base.OnOptionsItemSelected(item);
            }
        }

    }
}