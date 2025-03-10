﻿using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;

namespace SurfaceOverlay
{
    public partial class MainWindow : Window
    {
        private static String RegistryKey = @"SOFTWARE\SurfaceOverlay";
        private static String WidthKey = "Width";
        private static String HeightKey = "Height";
        private static String LeftKey = "Left";
        private static String TopKey = "Top";

        public MainWindow()
        {
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            InitializeComponent();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void CtrlCButton_Click(object sender, RoutedEventArgs e)
        {
            WindowsAPI.SendCtrlC();
            HighlightButtonOnClick(sender as Button);
        }

        private void CtrlVButton_Click(object sender, RoutedEventArgs e)
        {
            WindowsAPI.SendCtrlV();
            HighlightButtonOnClick(sender as Button);
        }

        private void CtrlZButton_Click(object sender, RoutedEventArgs e)
        {
            WindowsAPI.SendCtrlZ();
            HighlightButtonOnClick(sender as Button);
        }

        private void CtrlFButton_Click(object sender, RoutedEventArgs e)
        {
            WindowsAPI.SendCtrlF();
            HighlightButtonOnClick(sender as Button);
        }

        private void WinJButton_Click(object sender, RoutedEventArgs e)
        {
            WindowsAPI.SendWinJ();
            HighlightButtonOnClick(sender as Button);
        }

        private void WinSpaceButton_Click(object sender, RoutedEventArgs e)
        {
            WindowsAPI.SendWinSpace();
            HighlightButtonOnClick(sender as Button);
        }

        private void KeyboardButton_Click(object sender, RoutedEventArgs e)
        {
            WindowsAPI.ToggleVirtualKeyboard();
            HighlightButtonOnClick(sender as Button);
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            WindowsAPI.SetWindowNotFocusable(hwnd);

            RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryKey);
            if (key != null)
            {
                var leftKey = key.GetValue(LeftKey);
                if (leftKey != null)
                    this.Left = Convert.ToInt32(leftKey);

                var topKey = key.GetValue(TopKey);
                if (topKey != null)
                    this.Top = Convert.ToInt32(topKey);

                var widthKey = key.GetValue(WidthKey);
                if (widthKey != null)
                    this.Width = Convert.ToInt32(widthKey);

                var heightKey = key.GetValue(HeightKey);
                if (heightKey != null)
                    this.Height = Convert.ToInt32(heightKey);

                key.Close();
            }
        }
        public void Window_Closing(object sender, CancelEventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryKey);
            key.SetValue(LeftKey, Convert.ToInt32(this.Left));
            key.SetValue(TopKey, Convert.ToInt32(this.Top));
            key.SetValue(WidthKey, Convert.ToInt32(this.Width));
            key.SetValue(HeightKey, Convert.ToInt32(this.Height));
            key.Close();
        }

        private async void HighlightButtonOnClick(Button button)
        {
            // IsPress trigger doesn't work on Surface good enough for some reason
            // You can barely see change in color, so do it manually.
            var originalBackground = button.Background;
            var originalForeground = button.Foreground;
            button.Background = System.Windows.Media.Brushes.SteelBlue;
            button.Foreground = System.Windows.Media.Brushes.White;
            await Task.Delay(180);
            button.Background = originalBackground;
            button.Foreground = originalForeground;
        }
    }
}
