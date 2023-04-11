using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_of_Life
{
    public class LoadImage
    {
        public static Bitmap Image;
        public static void OpenImage()
        {

            var openFileDialog = new OpenFileDialog()
            {
                Filter = "Images | *.bmp; *.png; *.jpg; *.JPEG"
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            Image = new Bitmap(openFileDialog.FileName);
        }
        public static void ResizeImage(PictureBox pictureBox)
        {
            if (Image.Width < pictureBox.Width || Image.Height < pictureBox.Height) return;
            if(Image.Width <= 500 && Image.Height <= 500)
                Image = new Bitmap(Image, 1000, 1000);
            else
                Image = new Bitmap(Image, pictureBox.Width, pictureBox.Height);
        }
       
    }
}
