using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkinButtons
{
    public class SkinButton : Button
    {
        Bitmap[] bmp = new Bitmap[4];
        public void SetSkin(string dir, string name)
        {
            if (File.Exists(dir + name + "_normal.png"))
            {
                bmp[0] = new Bitmap(dir + name + "_normal.png");
            }
            else
            {
                throw (new SystemException("File: " + dir + name + "_normal.png" + " does not exists"));
            }

            if (File.Exists(dir + name + "_enter.png"))
            {
                bmp[1] = new Bitmap(dir + name + "_enter.png");
            }
            else
            {                
               throw (new SystemException("File: " + dir + name + "_enter.png" + " does not exists"));             
            }

            if (File.Exists(dir + name + "_press.png"))
            {
                bmp[2] = new Bitmap(dir + name + "_press.png");
            }
            else
            {
                throw (new SystemException("File: " + dir + name + "_press.png" + " does not exists"));
            }

            if (File.Exists(dir + name + "_disable.png"))
            {
                bmp[3] = new Bitmap(dir + name + "_disable.png");
            }
            else
            {
                throw (new SystemException("File: " + dir + name + "_disable.png" + " does not exists"));
            }


            CommonApi.BitmapRegion.CreateControlRegion(this, bmp[0]);
            this.MouseDown += SkinButton_MouseDown;
            this.MouseEnter += SkinButton_MouseEnter;
            this.MouseLeave += SkinButton_MouseLeave;
            this.MouseUp += SkinButton_MouseUp;

        }

        private void SkinButton_MouseUp(object sender, MouseEventArgs e)
        {
            CommonApi.BitmapRegion.CreateControlRegion(this, bmp[CommonApi.Common.UP]);
        }

        private void SkinButton_MouseLeave(object sender, EventArgs e)
        {
            CommonApi.BitmapRegion.CreateControlRegion(this, bmp[CommonApi.Common.LEAVE]);
        }

        private void SkinButton_MouseEnter(object sender, EventArgs e)
        {
            CommonApi.BitmapRegion.CreateControlRegion(this, bmp[CommonApi.Common.ENTER]);
        }

        private void SkinButton_MouseDown(object sender, MouseEventArgs e)
        {
            CommonApi.BitmapRegion.CreateControlRegion(this, bmp[CommonApi.Common.DOWN]);
        }
    }
}
