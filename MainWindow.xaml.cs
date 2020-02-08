using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using iText;


namespace mint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            filedrop.Items.Clear();
            clear.Visibility = Visibility.Hidden;
            
        }

        private void Filetype1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Filedrop_Drop(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //get filedrop data
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                //int filedrop class
                fileHandling fh = new fileHandling();
                
                //add files to filedrop collection
                foreach(object item in files)
                {
                    fh.myfiles.Add(new fileHandling() { filePath = item.ToString() }) ;
                }
                filedrop.ItemsSource = fh.myfiles;

                //disable clear button
                clear.Visibility = Visibility.Visible;
            }
          
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            if (filedrop.HasItems)
            {
                filedrop.ItemsSource = null;
                clear.Visibility = Visibility.Hidden;
            }
        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            if (filetype1.Text == "img" && filetype2.Text == "pdf")
            {
                imgTopdf();
            }
            else if(filetype1.Text == "pdf" && filetype2.Text == "img")
            {
                pdfToimg();
            }
        }

        private void pdfToimg()
        {
            
        }

        private void imgTopdf()
        {
            string dest = "C:/Users/Brynndolin/Downloads";
            //get filedrop data
            //string[] files = (string[](DataFormats.FileDrop);
            
        }

    }
}

