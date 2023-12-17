using System.Windows;
using System.Windows.Media;

namespace Homework_WordEditor
{
    public partial class ColorSelect : Window
    {
        public ColorSelect()
        {
            InitializeComponent();
        }

        private void ColorPicker_Canceled(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ColorPicker_Confirmed(object sender, HandyControl.Data.FunctionEventArgs<Color> e)
        {
            System.Windows.Media.Color selectedColor = ColorPicker1.SelectedBrush.Color;

            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            if (mainWindow != null)
            {
                mainWindow?.TriggerColorChanged(selectedColor);
            }

            this.Close();
        }
    }
}