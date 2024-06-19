using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace SurfaceOverlay
{
    public partial class MainWindow : Window
    {
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
        }

        private void CtrlVButton_Click(object sender, RoutedEventArgs e)
        {
            WindowsAPI.SendCtrlV();
        }

        private void CtrlZButton_Click(object sender, RoutedEventArgs e)
        {
            WindowsAPI.SendCtrlZ();
        }

        private void CtrlFButton_Click(object sender, RoutedEventArgs e)
        {
            WindowsAPI.SendCtrlF();
        }

        private void WinJButton_Click(object sender, RoutedEventArgs e)
        {
            WindowsAPI.SendWinJ();
        }

        private void WinSpaceButton_Click(object sender, RoutedEventArgs e)
        {
            WindowsAPI.SendWinSpace();
        }

        private void KeyboardButton_Click(object sender, RoutedEventArgs e)
        {
            WindowsAPI.ToggleVirtualKeyboard();
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            WindowsAPI.SetWindowNotFocusable(hwnd);
        }
    }
}
