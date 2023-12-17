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
    public partial class MainWindow
    {
        public static string LastText = "";
        public static bool caseSensitive = true;

        public delegate void ColorChangedEventHandler(System.Windows.Media.Color selectedColor);

        public event ColorChangedEventHandler ColorChanged;

        private string s_fileName;
        private TextPointer position = null;

        public MainWindow()
        {
            InitializeComponent();
            ColorChanged += OnColorChanged;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            System.Drawing.FontFamily[] fontFamilies = System.Drawing.FontFamily.Families;
            foreach (System.Drawing.FontFamily family in fontFamilies)
            {
                comboBox1.Items.Add(family.Name);
            }

            double[] fontsizegroup = new double[120];
            for (int i = 0; i < 120; i++)
            {
                fontsizegroup[i] = i + 1;
            }
            foreach (double n in fontsizegroup)
            {
                comboBox2.Items.Add(n);
            }
        }

        public void TriggerColorChanged(System.Windows.Media.Color selectedColor)
        {
            ColorChanged?.Invoke(selectedColor);
        }

        public void OnColorChanged(System.Windows.Media.Color selectedColor)
        {
            colorToggleButton.IsChecked = false;
            ((System.Windows.Shapes.Rectangle)colorToggleButton.Content).Fill = new SolidColorBrush(selectedColor);
            EditorRichTextBox.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(selectedColor));
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

                            LastText = new TextRange(EditorRichTextBox.Document.ContentStart, EditorRichTextBox.Document.ContentEnd).Text;
                        }
                    }
                }
            }
        }

        private void FileSave_Addition()
        {
            if (s_fileName != null)
            {
                if (s_fileName.Length != 0)
                {
                    using (FileStream fileStream = File.Create(s_fileName))
                    {
                        TextRange textRange = new TextRange(EditorRichTextBox.Document.ContentStart, EditorRichTextBox.Document.ContentEnd);
                        textRange.Save(fileStream, DataFormats.Rtf);
                    }
                }
            }
            else
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
        }

        private void FileSave_MenuItem_Click(Object sender, RoutedEventArgs e)
        {
            if (s_fileName != null)
            {
                if (s_fileName.Length != 0)
                {
                    using (FileStream fileStream = File.Create(s_fileName))
                    {
                        TextRange textRange = new TextRange(EditorRichTextBox.Document.ContentStart, EditorRichTextBox.Document.ContentEnd);
                        textRange.Save(fileStream, DataFormats.Rtf);
                    }
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
            System.Windows.Media.FontFamily fontFamily = new System.Windows.Media.FontFamily(comboBox1.SelectedItem.ToString());
            EditorRichTextBox.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, fontFamily);
        }

        private void ComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditorRichTextBox.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, comboBox2.SelectedItem);
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
            StringComparison comparison = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            while (position != null)
            {
                if (position.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    string textRun = position.GetTextInRun(LogicalDirection.Forward);
                    int indexInRun = textRun.IndexOf(word, comparison);
                    if (indexInRun >= 0)
                    {
                        FindReplace.isfind = true;
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

        public void FindRichTextBoxString(string Findstring)
        {
            TextPointer initialPosition = position;

            position = FindWordFromPosition(position, Findstring);

            if (position == null && initialPosition.CompareTo(EditorRichTextBox.Document.ContentEnd) < 0 && FindReplace.isfind == true)
            {
                position = EditorRichTextBox.Document.ContentStart;
            }

            if (position == null && initialPosition.CompareTo(EditorRichTextBox.Document.ContentEnd) < 0 && FindReplace.isfind == false)
            {
                FindReplace.isfind = false;
                MessageBox.Show("未找到匹配字符串", "继续", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                position = EditorRichTextBox.Document.ContentStart;
            }
            else if (position != null)
            {
                EditorRichTextBox.Focus();
                TextPointer position1 = position;
                position = position.GetPositionAtOffset(Findstring.Length);
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
            FindReplaceDialog.Show();
        }

        private void ColorToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ColorSelect colorselect = new ColorSelect();
            colorselect.Owner = this;
            colorselect.ShowDialog();
        }

        private TextPointer FindWordFromPositionBackward(TextPointer position, string word)
        {
            StringComparison comparison = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            while (position != null)
            {
                if (position.GetPointerContext(LogicalDirection.Backward) == TextPointerContext.Text)
                {
                    string textRun = position.GetTextInRun(LogicalDirection.Backward);
                    int indexInRun = textRun.LastIndexOf(word, comparison);
                    if (indexInRun >= 0)
                    {
                        FindReplace.isfind = true;
                        position = position.GetPositionAtOffset(-textRun.Length + indexInRun + 1);
                        break;
                    }
                    else
                    {
                        position = position.GetPositionAtOffset(-textRun.Length);
                    }
                }
                else
                {
                    position = position.GetNextContextPosition(LogicalDirection.Backward);
                }
            }
            return position;
        }

        public void FindRichTextBoxStringBackward(string Findstring)
        {
            TextPointer initialPosition = position;

            position = FindWordFromPositionBackward(position, Findstring);

            if (position == null && initialPosition.CompareTo(EditorRichTextBox.Document.ContentStart) > 0 && FindReplace.isfind == true)
            {
                position = EditorRichTextBox.Document.ContentEnd;
            }

            if (position == null && initialPosition.CompareTo(EditorRichTextBox.Document.ContentStart) > 0 && FindReplace.isfind == false)
            {
                FindReplace.isfind = false;
                MessageBox.Show("未找到匹配字符串", "继续", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                position = EditorRichTextBox.Document.ContentEnd;
            }
            else if (position != null)
            {
                EditorRichTextBox.Focus();
                TextPointer position1 = position;
                position = position.GetPositionAtOffset(-Findstring.Length);
                EditorRichTextBox.Selection.Select(position, position1);
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (LastText != "")
            {
                if (IsDocumentModified())
                {
                    MessageBoxResult result = MessageBox.Show("文档已更改，是否保存？", "提示", MessageBoxButton.YesNoCancel);

                    if (result == MessageBoxResult.Yes)
                    {
                        FileSave_Addition();
                    }
                    else if (result == MessageBoxResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private bool IsDocumentModified()
        {
            string currentText = new TextRange(EditorRichTextBox.Document.ContentStart, EditorRichTextBox.Document.ContentEnd).Text;

            return !string.Equals(LastText, currentText);
        }
    }
}