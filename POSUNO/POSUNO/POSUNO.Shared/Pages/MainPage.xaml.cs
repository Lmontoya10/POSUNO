using POSUNO.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace POSUNO.Pages
{
 
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public User User { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            User = (User)e.Parameter;
            WelcomeTextBlock.Text = $"Bievenido: {User.FullName}";
        }
        
        private async void LogoutImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ContentDialogResult dialog = await ConfirmLeaveAsync();

            if (dialog == ContentDialogResult.Primary)
            {
                Frame.Navigate(typeof(LoginPage));
            }
        }

        private async Task<ContentDialogResult> ConfirmLeaveAsync()
        {
            ContentDialog confirmDialog = new ContentDialog
            {
                Title = "Confirmacion",
                Content = "Estas seguro de salir?",
                PrimaryButtonText = "Si",
                CloseButtonText = "No"
            };

            return await confirmDialog.ShowAsync();
        }
    }
}
