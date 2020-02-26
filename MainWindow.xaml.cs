using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Drawing;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.Windows.Media.Animation;
using System.ComponentModel;
using System.Windows.Threading;

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
            
            //min max window size
            
        }

        fileHandling fh = new fileHandling();
        public void Filedrop_Drop(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //get filedrop data
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                //int filedrop class
                
                
                //add files to filedrop collection
                foreach(object item in files)
                {
                    fh.myfiles.Add(new fileHandling() { filePath = item.ToString() }) ;
                }
                filedrop.ItemsSource = fh.myfiles;

                //show clear button
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

        private async void Convert_Click(object sender, RoutedEventArgs e)
        {
            //if filedrop has items
            if(filedrop.Items.Count != 0)
            {
                if (dropdown1_label.Text == "IMG" && dropdown2_label.Text == "PDF")
                {
                    imgTopdfAsync();
                }
                else if (dropdown1_label.Text == "pdf" && dropdown2_label.Text == "img")
                {
                    pdfToimg();
                }
            }
            else
            {
                output_log.Text = "no items in filedrop";
                output_log.Opacity = 0;
                outputlog_Opacity();
            }
        }

        private void pdfToimg()
        {
            
        }

        public async Task imgTopdfAsync()
        {
            string dest = "C:\\Users\\Brynndolin\\Downloads\\";
            

            //get name of parent dir. this will be the new filename of our pdf.
            string file1 = System.IO.Directory.GetParent(fh.myfiles[0].filePath).FullName;
            string parent = file1.Substring(file1.LastIndexOf("\\") + 1);

            //progress bar setup
            var progressBar = new Progress<int>(value => progress.Value = value);
            progress.Value = 0;

            //animate opacity to fade in and out
            //output_log.BeginAnimation(UIElement.OpacityProperty, new DoubleAnimation(1d, TimeSpan.FromSeconds(1.0)));
            //progress.BeginAnimation(UIElement.OpacityProperty, new DoubleAnimation(1d, TimeSpan.FromSeconds(1.0)));
            //await Task.Delay(TimeSpan.FromSeconds(0.5));
            output_log.Opacity = 100;
            progress.Opacity = 100;

            PdfDocument doc = new PdfDocument();
            //create a pdf document and add images from filedrop
            for (int i = 0; i < fh.myfiles.Count; i++)
            {
                //get image
                string imgpath = fh.myfiles[i].filePath;
                PdfImage image = PdfImage.FromFile(imgpath);
                SizeF size = new SizeF(image.Width, image.Height);
                PdfMargins margins = new PdfMargins(0, 0);
                float width = image.Width;
                float height = image.Height;

                //sizing
                PdfPageBase page = doc.Pages.Add(size, margins);

                //add to new pdf
                page.Canvas.DrawImage(image, 0, 0, width, height);

                //update progress bar
                
                progress.Value = i;
                output_log.Text = "Exporting: " + progress.Value.ToString() + "%";
                progress.Dispatcher.Invoke(() => progress.Value = i, DispatcherPriority.Background);

            }

            //save pdf with name as parent folder
            doc.SaveToFile(dest + parent + ".pdf");
            doc.Close();


            //update output log
            output_log.Text = "Exported '" + parent + ".pdf' to: " + dest;
            await Task.Delay(TimeSpan.FromSeconds(1));

            //output_log.Opacity = 0;
            //progress.Opacity = 0;
            progress.BeginAnimation(UIElement.OpacityProperty, new DoubleAnimation(0d, TimeSpan.FromSeconds(1.0)));
            output_log.BeginAnimation(UIElement.OpacityProperty, new DoubleAnimation(0d, TimeSpan.FromSeconds(1.0)));

        }

        private void dropdown1_Click(object sender, RoutedEventArgs e)
        {
            if(filetype1.Visibility == Visibility.Hidden)
            {
                filetype1.Visibility = Visibility.Visible;
            }
            else if(dropdown1.Visibility == Visibility.Visible)
            {
                filetype1.Visibility = Visibility.Hidden;
            }
        }

        private void dropdown2_Click(object sender, RoutedEventArgs e)
        {
            if (filetype2.Visibility == Visibility.Hidden)
            {
                filetype2.Visibility = Visibility.Visible;
            }
            else if (dropdown2.Visibility == Visibility.Visible)
            {
                filetype2.Visibility = Visibility.Hidden;
            }
        }

        private void filetype1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dropdown1_label.Text = ((ListBoxItem)filetype1.SelectedItem).Content.ToString();
            filetype1.Visibility = Visibility.Hidden;
        }

        private void filetype2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dropdown2_label.Text = ((ListBoxItem)filetype2.SelectedItem).Content.ToString();
            filetype2.Visibility = Visibility.Hidden;
        }

        private void closeWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void minmax_Click(object sender, RoutedEventArgs e)
        {

        }

        private void minimize_Click(object sender, RoutedEventArgs e)
        {

        }

        private void titlebar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private  async void outputlog_Opacity()
        {
            //animate opacity to fade in and out
            output_log.BeginAnimation(UIElement.OpacityProperty, new DoubleAnimation(1d, TimeSpan.FromSeconds(1.0)));
            await Task.Delay(TimeSpan.FromSeconds(3));
            output_log.BeginAnimation(UIElement.OpacityProperty, new DoubleAnimation(0d, TimeSpan.FromSeconds(1.0)));
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
        
    }
}

