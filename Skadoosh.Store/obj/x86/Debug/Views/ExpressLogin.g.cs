﻿

#pragma checksum "C:\Users\lbrown\Documents\GitHub\Skadoosh\Skadoosh.Store\Views\ExpressLogin.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1E818E1B3CBA31B180FEE8CD97C772B2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Skadoosh.Store.Views
{
    partial class ExpressLogin : global::Skadoosh.Store.Common.LayoutAwarePage, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 100 "..\..\..\Views\ExpressLogin.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).KeyDown += this.enterKeyed;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 65 "..\..\..\Views\ExpressLogin.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.GoHome;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 45 "..\..\..\Views\ExpressLogin.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.GoBack;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 179 "..\..\..\Views\ExpressLogin.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Login;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


