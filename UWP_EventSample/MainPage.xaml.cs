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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP_EventSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const int msDelayTime = 500;

        public MainPage()
        {
            this.InitializeComponent();

            RandonNumberGenerated += MainPage_RandonNumberGenerated;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            IoTListBox.Items.Clear();
        }

        private void IoTTextBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            IoTListBox.Items.Add(e.Key.ToString());
        }
        private void KeyUpEventActiveCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            bool isChecked = IsCheckBoxChecked(checkBox);

            if (isChecked)
            {
                IoTTextBox.KeyUp += IoTTextBox_KeyUp;
            }
            else
            {
                IoTTextBox.KeyUp -= IoTTextBox_KeyUp;
            }
        }
        private bool IsCheckBoxChecked(CheckBox checkBox)
        {
            bool isChecked = false;
            if (checkBox != null)
            {
                if (checkBox.IsChecked.HasValue)
                {
                    isChecked = checkBox.IsChecked.Value;
                }
            }
            return isChecked;
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DisplayEventRoute(sender, e.OriginalSource);
        }
        private void AppBarButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            IoTListBox.Items.Add("AppBarButton tapped event");
        }
        private void DisplayEventRoute(object sender, object originalSource)
        {
            string routeString = string.Empty;
            routeString = " Sender: " + GetControlTypeName(sender);
            routeString += ", original source: " + GetControlTypeName(originalSource);

            IoTListBox.Items.Add(routeString);
        }
        private string GetControlTypeName(object control)
        {
            string typeName = "Unknown";

            if (control != null)
            {
                typeName = control.GetType().Name;
            }

            return typeName;
        }

        public event EventHandler<RandonNumberEventArgs> RandonNumberGenerated = delegate{};

        private async void MainPage_RandonNumberGenerated(object sender, RandonNumberEventArgs e)
        {
            await Dispatcher.TryRunIdleAsync((a) => { IoTListBox.Items.Add(e.Value); });
        }
        private void RaiseCustomEventButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => {
                Task.Delay(msDelayTime).Wait();
                RandonNumberGenerated(this, new RandonNumberEventArgs());
                    });
        }
    }
}
