using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QRCoder;

namespace HodHod
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RenderQrCode("Hi this is HodHod", QRCodeGenerator.ECCLevel.H);
        }

        private void RenderQrCode(string Text , QRCodeGenerator.ECCLevel Level)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(Text, Level);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap QrImage = qrCode.GetGraphic(10,System.Drawing.Color.DarkRed, System.Drawing.Color.Pink,null,100,100,true);
            QrImageHolder.Source = (BitmapSource)Bitmap2BitmapImage(QrImage);
            QrImageHolder2.Source = (BitmapSource)Bitmap2BitmapImage(QrImage);

            //this.pictureBoxQRCode.Size = new System.Drawing.Size(pictureBoxQRCode.Width, pictureBoxQRCode.Height);
            ////Set the SizeMode to center the image.
            //this.pictureBoxQRCode.SizeMode = PictureBoxSizeMode.CenterImage;
            //
            //pictureBoxQRCode.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        private object Bitmap2BitmapImage(Bitmap bitmap)
        {
            IntPtr hBitmap = bitmap.GetHbitmap();
            object retval;

            try
            {
                retval = Imaging.CreateBitmapSourceFromHBitmap(
                             hBitmap,
                             IntPtr.Zero,
                             Int32Rect.Empty,
                             BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(hBitmap);
            }

            return retval;
        }

    }
}
