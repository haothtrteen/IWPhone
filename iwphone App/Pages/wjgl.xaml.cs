using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace iwphone.Pages
{
    /// <summary>
    /// wjgl.xaml 的交互逻辑
    /// </summary>
    public partial class wjgl : Window
    {
        public wjgl()
        {
            InitializeComponent();
            LoadFile();
        }

        private void LoadFile()
        {
           
        }
        private void LoadFileList(string output)
        {
            // Split the output into lines
            string[] lines = output.Split('\n');

            // Create a list to store file names
            List<string> fileList = new List<string>();

            // Add each line (file name) to the list
            foreach (string line in lines)
            {
                string fileName = line.Trim();
                if (!string.IsNullOrEmpty(fileName))
                {
                    fileList.Add(fileName);
                }
            }

            // Set the file list as the data source for the ListBox
            fileListBox.ItemsSource = fileList;
        }

        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string folderName = button.Content.ToString();

            if (folderName == "..")
            {
             
             
              
               
            }
            else
            {
               
                //MessageBox.Show(folderName);
            }
        }
    }
}
