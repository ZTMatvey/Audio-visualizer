﻿#pragma checksum "..\..\..\..\PagesAndWindows\Windows\PointEditDialogWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B5F97BE185101980AC6A634989084B7109CF10C25D5F19DDDA0DE17481CA84EE"
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
using Visualizer.PagesAndWindows.Windows;


namespace Visualizer.PagesAndWindows {
    
    
    /// <summary>
    /// PointEditDialogWindow
    /// </summary>
    public partial class PointEditDialogWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\..\PagesAndWindows\Windows\PointEditDialogWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border windowFrame;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\PagesAndWindows\Windows\PointEditDialogWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainDialogGrid;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\PagesAndWindows\Windows\PointEditDialogWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox XTextBox;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\PagesAndWindows\Windows\PointEditDialogWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MessageTextBlock;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\PagesAndWindows\Windows\PointEditDialogWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox YTextBox;
        
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
            System.Uri resourceLocater = new System.Uri("/Visualizer;component/pagesandwindows/windows/pointeditdialogwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\PagesAndWindows\Windows\PointEditDialogWindow.xaml"
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
            
            #line 14 "..\..\..\..\PagesAndWindows\Windows\PointEditDialogWindow.xaml"
            ((Visualizer.PagesAndWindows.PointEditDialogWindow)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.windowFrame = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.MainDialogGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.XTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 43 "..\..\..\..\PagesAndWindows\Windows\PointEditDialogWindow.xaml"
            this.XTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.MessageTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.YTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 63 "..\..\..\..\PagesAndWindows\Windows\PointEditDialogWindow.xaml"
            this.YTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 66 "..\..\..\..\PagesAndWindows\Windows\PointEditDialogWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
