using System.Windows;
using MessageBox = HandyControl.Controls.MessageBox;
using Window = System.Windows.Window;

namespace Homework_WordEditor
{
    /// <summary>
    /// FindReplace.xaml 的交互逻辑
    /// </summary>
    public partial class FindReplace : Window
    {
        private MainWindow mainWindow;

        public FindReplace(MainWindow window1)
        {
            InitializeComponent();
            mainWindow = window1;
        }

        private void buttonFind_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text.Length != 0)
                mainWindow.FindRichTextBoxString(textBox1.Text);
            else
                MessageBox.Show("查找字符串不能为空", "提示", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
        }

        private void buttonReplace_Click(object sender, RoutedEventArgs e)
        {
            if (textBox2.Text.Length != 0)
                mainWindow.ReplaceRichTextBoxString(textBox2.Text);
            else
                MessageBox.Show("替换字符串不能为空", "提示", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
        }
    }
}