using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WpfApp.Pages;

namespace WpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Frame.Navigate(new Auth());
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
        private void Frame_LoadCompleted(object sender, NavigationEventArgs e)
        {
            try
            {
                //ServicesGRUD pg = (ServicesGRUD)e.Content;
                //pg.displayClient();
            }
            catch { };
        }
        private void Frame_ContentRendered(object sender, EventArgs e)
        {
            if (Frame.CanGoBack)
                BtnBack.Visibility = Visibility.Visible;
            else
                BtnBack.Visibility = Visibility.Hidden;
        }
    }
}