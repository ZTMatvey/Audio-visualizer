﻿#pragma checksum "..\..\..\PagesAndWindows\CustomSettingsWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3DB35A64F4C1F93E5BEF8B0AA6464611F5C2FD68058793EED40CB707FB4A926D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Visualizer.PagesAndWindows;


namespace Visualizer.PagesAndWindows {
    
    
    /// <summary>
    /// CustomSettingsWindow
    /// </summary>
    public partial class CustomSettingsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 47 "..\..\..\PagesAndWindows\CustomSettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider DownR;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\PagesAndWindows\CustomSettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider DownG;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\PagesAndWindows\CustomSettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider DownB;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\PagesAndWindows\CustomSettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider DownA;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\..\PagesAndWindows\CustomSettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider UpR;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\PagesAndWindows\CustomSettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider UpG;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\..\PagesAndWindows\CustomSettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider UpB;
        
        #line default
        #line hidden
        
        
        #line 119 "..\..\..\PagesAndWindows\CustomSettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider UpA;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Visualizer;component/pagesandwindows/customsettingswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\PagesAndWindows\CustomSettingsWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.DownR = ((System.Windows.Controls.Slider)(target));
            return;
            case 2:
            this.DownG = ((System.Windows.Controls.Slider)(target));
            return;
            case 3:
            this.DownB = ((System.Windows.Controls.Slider)(target));
            return;
            case 4:
            this.DownA = ((System.Windows.Controls.Slider)(target));
            return;
            case 5:
            this.UpR = ((System.Windows.Controls.Slider)(target));
            return;
            case 6:
            this.UpG = ((System.Windows.Controls.Slider)(target));
            return;
            case 7:
            this.UpB = ((System.Windows.Controls.Slider)(target));
            return;
            case 8:
            this.UpA = ((System.Windows.Controls.Slider)(target));
            return;
            case 9:
            
            #line 130 "..\..\..\PagesAndWindows\CustomSettingsWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
