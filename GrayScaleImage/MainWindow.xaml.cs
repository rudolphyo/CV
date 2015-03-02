using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using System.Drawing;

namespace GrayScaleImage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int START_POSITION = 54;
        private const int LAST_POSITION = 3;
        private const int STEP = 4;

        private Image image;
        private BitmapImage originalImage;
        private byte[] originalImageBytes;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void openFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg)|*.jpg; *.jpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true){
                showImage(openFileDialog.FileName);
                redFilterButton.IsEnabled = true;
                invertButton.IsEnabled = true;
                processingButton.IsEnabled = true;
            }
        }

      //  private PixelFormat pxFormat;
        private double width, height;

        private void showImage(string fileName) {

            if (image == null)
            {
                image = new Image();
            }

            originalImage = new BitmapImage();
            originalImage.BeginInit();
            originalImage.UriSource = new Uri(fileName, UriKind.RelativeOrAbsolute);
            originalImage.EndInit();
            image.Source = originalImage;
            image.Stretch = Stretch.Uniform;

            originalImageBytes = ImageToByte(originalImage);
            width = originalImage.Width;
            height = originalImage.Height;

            if (originalPanel.Children.Count == 0)
            {
                originalPanel.Children.Add(image);
            }

            //pxFormat = originalImage.Format;
        }

        private void showImage(BitmapImage byteImage)
        {
            image.Source = byteImage;
            image.Stretch = Stretch.Uniform;
        }

        public byte[] ImageToByte(BitmapImage bitmapImage)
        {
            byte[] data;
            BmpBitmapEncoder encoder = new BmpBitmapEncoder();
            
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                ms.Position = 0;
                data = ms.ToArray();
            }
            return data;
        }

        /**
         * Convert rgb byte array to gray-scale byte array
         * 
         * @param grayImage - current array
         * @return void 
         **/
        private void setGrayscale(byte[] grayImage) {

            for (int i = START_POSITION; i < grayImage.Length - LAST_POSITION; i += STEP)
            {
                byte gray = (byte)(0.299*grayImage[i+2] + 0.587*grayImage[i + 1] + 0.114*grayImage[i]);
                grayImage[i] = gray;     //BLUE
                grayImage[i + 1] = gray; //GREEN
                grayImage[i + 2] = gray; //RED
                grayImage[i + 3] = 0; //OPACITY
            }

        }

        /**
         * 
         * 
         * {
-               arr[i] =(byte)(arr[i] * 0.114);
-               arr[i + 1] = (byte)(arr[i + 1] * 0.587);
-               arr[i + 2] = (byte)(arr[i + 2] * 0.299);
-               res[j] = (byte)( arr[i] + arr[i + 1] + arr[i + 2]);
-               j++;
-            }
         * Invert byte array
         * 
         * @param grayImage - current array
         * @return void 
         **/
        private void setInvert(byte[] grayImage)
        {
            for (int i = START_POSITION; i < grayImage.Length - LAST_POSITION; i += STEP)
            {
                grayImage[i] = (byte)(255 - grayImage[i]);     //BLUE
                grayImage[i + 1] = (byte)(255 - grayImage[i + 1]); //GREEN
                grayImage[i + 2] = (byte)(255 - grayImage[i + 2]); //RED
                grayImage[i + 3] = grayImage[i + 3]; //OPACITY
            }
        }

        /**
         * Return red channel of byte array
         * 
         * @param grayImage - current array
         * @return void 
         **/
        private void setRedFilter(byte[] grayImage)
        {
            for (int i = START_POSITION; i < grayImage.Length - LAST_POSITION; i += STEP)
            {
                grayImage[i] = 0;     //BLUE
                grayImage[i + 1] = 0; //GREEN
                grayImage[i + 2] = grayImage[i + 2]; //RED
                grayImage[i + 3] = 0; //OPACITY
            }
        }

        private BitmapSource ToBitmapImage(byte[] grayImage)
        {
            using (var ms = new System.IO.MemoryStream(grayImage))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        private delegate void makeNewByteArray (byte[] byteArray);

        private void button_Click(makeNewByteArray m)
        {
            m(originalImageBytes);
            originalImage = (BitmapImage)ToBitmapImage(originalImageBytes);
            showImage(originalImage);
        }

        private void redFilterButton_Click(object sender, RoutedEventArgs e)
        {
            button_Click(setRedFilter);    
        }

        private void invertButton_Click(object sender, RoutedEventArgs e)
        {
            button_Click(setInvert); 
        }
        private void processingButton_Click(object sender, RoutedEventArgs e)
        {
            button_Click(setGrayscale);
        }
    }
}
