﻿#pragma checksum "..\..\..\..\Views\RailwaysDBAppMainMenu.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A343DAFE0EF926236B8B2237E944C079"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.18444
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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
using WpfLocalization;


namespace RailwaysDBApp.Views {
    
    
    /// <summary>
    /// RailwaysDBAppMainMenu
    /// </summary>
    public partial class RailwaysDBAppMainMenu : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\..\Views\RailwaysDBAppMainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image MainMenuBackground;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\Views\RailwaysDBAppMainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Tickets;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\Views\RailwaysDBAppMainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Routes;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\Views\RailwaysDBAppMainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Races;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\Views\RailwaysDBAppMainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EditTables;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\Views\RailwaysDBAppMainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Settings;
        
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
            System.Uri resourceLocater = new System.Uri("/RailwaysDBApp;component/views/railwaysdbappmainmenu.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\RailwaysDBAppMainMenu.xaml"
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
            
            #line 10 "..\..\..\..\Views\RailwaysDBAppMainMenu.xaml"
            ((RailwaysDBApp.Views.RailwaysDBAppMainMenu)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.MainMenuBackground = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.Tickets = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\..\Views\RailwaysDBAppMainMenu.xaml"
            this.Tickets.Click += new System.Windows.RoutedEventHandler(this.Tickets_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Routes = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\..\Views\RailwaysDBAppMainMenu.xaml"
            this.Routes.Click += new System.Windows.RoutedEventHandler(this.Routes_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Races = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\..\Views\RailwaysDBAppMainMenu.xaml"
            this.Races.Click += new System.Windows.RoutedEventHandler(this.Races_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.EditTables = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\..\Views\RailwaysDBAppMainMenu.xaml"
            this.EditTables.Click += new System.Windows.RoutedEventHandler(this.EditTables_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Settings = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\..\Views\RailwaysDBAppMainMenu.xaml"
            this.Settings.Click += new System.Windows.RoutedEventHandler(this.Settings_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

