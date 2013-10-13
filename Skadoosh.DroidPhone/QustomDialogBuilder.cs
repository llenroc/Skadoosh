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
using Android.Graphics.Drawables;
using Android.Graphics;

namespace Skadoosh.DroidPhone
{
    public class QustomDialogBuilder : AlertDialog.Builder
    {
        /** The custom_body layout */
        private View mDialogView;

        /** optional dialog title layout */
        private TextView mTitle;
        /** optional alert dialog image */
        private ImageView mIcon;
        /** optional message displayed below title if title exists*/
        private TextView mMessage;
        /** The colored holo divider. You can set its color with the setDividerColor method */
        private View mDivider;

        public QustomDialogBuilder(Context context)
            : base(context)
        {
       
            mDialogView = View.Inflate(context, Resource.Layout.Alert, null);
            SetView(mDialogView);

            mTitle = (TextView)mDialogView.FindViewById(Resource.Id.alertTitle);
            mMessage = (TextView)mDialogView.FindViewById(Resource.Id.message);
            mIcon = (ImageView)mDialogView.FindViewById(Resource.Id.icon);
            mDivider = mDialogView.FindViewById(Resource.Id.titleDivider);
        }
        public QustomDialogBuilder setDividerColor(string colorString)
        {
            
            mDivider.SetBackgroundColor(Color.ParseColor(colorString));
            return this;
        }


        public QustomDialogBuilder setTitle(string text)
        {
            mTitle.SetText(text, Android.Widget.TextView.BufferType.Normal);
            return this;
        }

        public QustomDialogBuilder setTitleColor(string colorString)
        {
            mTitle.SetTextColor(Color.ParseColor(colorString));
            return this;
        }


        public QustomDialogBuilder setMessage(int textResId)
        {
            mMessage.SetText(textResId);
            return this;
        }


        public QustomDialogBuilder setMessage(string text)
        {
            mMessage.SetText(text, Android.Widget.TextView.BufferType.Normal);
            return this;
        }


        public QustomDialogBuilder setIcon(int drawableResId)
        {
            mIcon.SetImageResource(drawableResId);
            return this;
        }


        public QustomDialogBuilder setIcon(Drawable icon)
        {
            mIcon.SetImageDrawable(icon);
            return this;
        }

        public QustomDialogBuilder setCustomView(int resId, Context context)
        {
            View customView = View.Inflate(context, resId, null);
            ((FrameLayout)mDialogView.FindViewById(Resource.Id.customPanel)).AddView(customView);
            return this;
        }


        public AlertDialog show()
        {
            if (mTitle.Text.Equals("")) mDialogView.FindViewById(Resource.Id.topPanel).Visibility = ViewStates.Gone;
            return base.Show();
        }

    }
}