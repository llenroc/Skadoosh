﻿

#pragma checksum "C:\Users\lbrown\Documents\GitHub\Skadoosh\Skadoosh.Store\Views\Help.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CB5C473E150779564D8BEDA183E42F85"
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
    partial class Help : global::Skadoosh.Store.Common.LayoutAwarePage, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 60 "..\..\..\Views\Help.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.GoHome;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 43 "..\..\..\Views\Help.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.GoBack;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


