using System.Windows;
using System.Windows.Documents;

namespace Homework_WordEditor
{
    /// <summary>
    /// About.xaml 的交互逻辑
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            string url = ((Hyperlink)sender).NavigateUri.AbsoluteUri;

            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };

            System.Diagnostics.Process.Start(psi);

            e.Handled = true;
        }
    }
}