﻿

#pragma checksum "C:\Users\lbrown\Documents\GitHub\Skadoosh\Skadoosh.Store\Views\Presenter\SurveyLibrary.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F691C064E6677E7B81258AD8E0A26AEE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Skadoosh.Store.Views.Presenter
{
    partial class SurveyLibrary : global::Skadoosh.Store.Common.LayoutAwarePage, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 17 "..\..\..\..\Views\Presenter\SurveyLibrary.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).DoubleTapped += this.SurveySelected;
                 #line default
                 #line hidden
                #line 17 "..\..\..\..\Views\Presenter\SurveyLibrary.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).RightTapped += this.ShowAppBar;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 116 "..\..\..\..\Views\Presenter\SurveyLibrary.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.GoToHome;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 96 "..\..\..\..\Views\Presenter\SurveyLibrary.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.GoToHome;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 204 "..\..\..\..\Views\Presenter\SurveyLibrary.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.DeleteSurvey;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 205 "..\..\..\..\Views\Presenter\SurveyLibrary.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.StartSurvey;
                 #line default
                 #line hidden
                break;
            case 6:
                #line 206 "..\..\..\..\Views\Presenter\SurveyLibrary.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.StopSurvey;
                 #line default
                 #line hidden
                break;
            case 7:
                #line 197 "..\..\..\..\Views\Presenter\SurveyLibrary.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.ShowHelp;
                 #line default
                 #line hidden
                break;
            case 8:
                #line 198 "..\..\..\..\Views\Presenter\SurveyLibrary.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Logout;
                 #line default
                 #line hidden
                break;
            case 9:
                #line 188 "..\..\..\..\Views\Presenter\SurveyLibrary.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.CreateSurvey;
                 #line default
                 #line hidden
                break;
            case 10:
                #line 189 "..\..\..\..\Views\Presenter\SurveyLibrary.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.EditSurvey;
                 #line default
                 #line hidden
                break;
            case 11:
                #line 190 "..\..\..\..\Views\Presenter\SurveyLibrary.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.DeleteSurvey;
                 #line default
                 #line hidden
                break;
            case 12:
                #line 191 "..\..\..\..\Views\Presenter\SurveyLibrary.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.StartSurvey;
                 #line default
                 #line hidden
                break;
            case 13:
                #line 192 "..\..\..\..\Views\Presenter\SurveyLibrary.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.StopSurvey;
                 #line default
                 #line hidden
                break;
            case 14:
                #line 193 "..\..\..\..\Views\Presenter\SurveyLibrary.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.ExportData;
                 #line default
                 #line hidden
                break;
            case 15:
                #line 194 "..\..\..\..\Views\Presenter\SurveyLibrary.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.UpdateProfile;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


