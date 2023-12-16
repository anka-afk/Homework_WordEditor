using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Homework_WordEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow
    {
        private string s_fileName; //记录保存文件名
        private TextPointer position = null;  // 用于记录查找的位置

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Drawing.FontFamily[]
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // 在这里添加保存文件的逻辑
            // 你可以使用 SaveFileDialog 来选择保存文件的路径
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            // 在这里添加打开文件的逻辑
            // 你可以使用 OpenFileDialog 来选择要打开的文件
        }

        private void CutButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void PasteButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void RedoButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void UnderlineButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void IncreaseBoldnessButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BulletedListButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void CenterAlignButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void DecreaseBoldnessButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void DecreaseIndentationButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void JustifyButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void LeftAlignButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void NumberedListButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void RightAlignButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void IncreaseIndentationButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void FileOpen_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); ;
            openFileDialog.Filter = "Rtf文件(*.rtf)|*.rtf|所有文件(*.*)|*.*";

            DirectoryInfo directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            openFileDialog.InitialDirectory = directory.Parent.Parent.Parent.FullName;
            if (openFileDialog.ShowDialog().Value == true)
            {
                s_fileName = openFileDialog.FileName;

                string fileExtension = System.IO.Path.GetExtension(s_fileName).ToUpper();

                if (fileExtension == ".RTF")
                {
                    using FileStream fileStream = File.OpenRead(s_fileName);
                    {
                        TextRange textRange = new TextRange(EditorRichTextBox.Document.ContentStart, EditorRichTextBox.Document.ContentEnd);
                        if (textRange.CanLoad(DataFormats.Rtf))
                        {
                            textRange.Load(fileStream, DataFormats.Rtf);
                        }
                    }
                }
            }
        }

        private void FileSave_MenuItem_Click(Object sender, RoutedEventArgs e)
        {
            if (s_fileName.Length != 0)
            {
                using (FileStream fileStream = File.Create(s_fileName))
                {
                    TextRange textRange = new TextRange(EditorRichTextBox.Document.ContentStart, EditorRichTextBox.Document.ContentEnd);
                    textRange.Save(fileStream, DataFormats.Rtf);
                }
            }
            else
            {
                FileSaveAs_MenuItem_Click(sender, e);
            }
        }

        private void FileSaveAs_MenuItem_Click(object obj, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Rtf文件(*.Rtf)|*.rtf|所有文件(*.*)|*.*";
            if (saveFileDialog.ShowDialog().Value == true)
            {
                s_fileName = saveFileDialog.FileName;
                string extention_s = System.IO.Path.GetExtension(s_fileName).ToUpper();
                using (FileStream fileStream = File.Create(s_fileName))
                {
                    TextRange textRange = new TextRange(EditorRichTextBox.Document.ContentStart, EditorRichTextBox.Document.ContentEnd);
                    textRange.Save(fileStream, DataFormats.Rtf);
                }
            }
        }

        private void Exit_MenuItem_Click(object ob, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void HelpAbout_MenuItem_Click(object o, RoutedEventArgs e)
        {
            About about = new About();
            about.Owner = this;
            about.ShowDialog();
        }

        private void New_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            EditorRichTextBox.Document.Blocks.Clear();
            s_fileName = "";
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FontFamily fontFamily = new FontFamily(comboBox1.SelectedItem.ToString());
            EditorRichTextBox.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, fontFamily);
        }

        private void ComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem comboBoxItem = comboBox2.SelectedItem as ComboBoxItem;
            double dontSize = Convert.ToDouble(comboBoxItem.Content.ToString());
            EditorRichTextBox.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, FontSize);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down) > 0)
            {
                if ((Keyboard.GetKeyStates(Key.N
                   ) & KeyStates.Down) > 0)
                {
                    New_MenuItem_Click((object)sender, e);
                }
            }
        }

        private TextPointer FindWordFromPosition(TextPointer position, string word)
        {
            while (position != null)
            {
                if (position.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    string textRun = position.GetTextInRun(LogicalDirection.Forward);
                    int indexInRun = textRun.IndexOf(word);
                    if (indexInRun >= 0)
                    {
                        position = position.GetPositionAtOffset(indexInRun);
                        break;
                    }
                    else
                    {
                        position = position.GetPositionAtOffset(textRun.Length);
                    }
                }
                else
                {
                    position = position.GetNextContextPosition(LogicalDirection.Forward);
                }
            }
            return position;
        }

        public void FindRichTextBoxString(string Findstring)//正向查找
        {
            if (position.CompareTo(EditorRichTextBox.Document.ContentEnd) > 0) //到文档底部
            {
                MessageBox.Show("到达文档底部", "继续", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                position = EditorRichTextBox.Document.ContentStart;
                return; //退出程序，返回调用处
            }
            position = FindWordFromPosition(position, Findstring);//正向查找定位

            if (position == null)
            {
                MessageBox.Show("未找到匹配字符串", "继续", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                position = EditorRichTextBox.Document.ContentStart;
            }
            else  //找到匹配
            {
                EditorRichTextBox.Focus();//焦点切换为主窗体
                TextPointer position1 = position;
                position = position.GetPositionAtOffset(Findstring.Length);//移到下一位置以便继续查找
                EditorRichTextBox.Selection.Select(position1, position);
            }
        }

        public void ReplaceRichTextBoxString(string ReplaceString)
        {
            if (!EditorRichTextBox.Selection.IsEmpty)
                EditorRichTextBox.Selection.Text = ReplaceString;
        }

        private void EditFindReplace_Click(object sender, RoutedEventArgs e)
        {
            position = EditorRichTextBox.Document.ContentStart;
            FindReplace FindReplaceDialog = new FindReplace(this);
            FindReplaceDialog.Owner = this;
            FindReplaceDialog.Show(); //查找替换对话框使用非模式方式
        }
    }
}