using CommonApi;
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
    public class SkinCheckBox : Button
    {
       
        // To store the location of previous mouse left click in the form
        // so that we can use it to calculate the new form location during dragging
        private Point prevLeftClick;

        // To determine if it is the first time entry for every dragging of the form
        private bool isFirst = true;

        // Acts like a gate to do allow or deny
        private bool toBlock = true;
        private Bitmap[] bmp = new Bitmap[4];
        bool m_isEnter = false;
        bool m_checked = false;

        public SkinCheckBox()
        {
        
        }

        public void SetSkin(string dir, string buttonName)
        {
            if (File.Exists(dir + buttonName + "_normal.png") == true)
                bmp[0] = new Bitmap(dir + buttonName + "_normal.png");
            else
                throw (new SystemException("File: " + dir + buttonName + "_normal.png" + " does not exists"));

            if (File.Exists(dir + buttonName + "_enter.png") == true)
                bmp[1] = new Bitmap(dir + buttonName + "_enter.png");
            else
                throw (new SystemException("File: " + dir + buttonName + "_enter.png" + " does not exists"));

            if (File.Exists(dir + buttonName + "_press.png") == true)
                bmp[2] = new Bitmap(dir + buttonName + "_press.png");
            else
                throw (new SystemException("File: " + dir + buttonName + "_press.png" + " does not exists"));

            if (File.Exists(dir + buttonName + "_disable.png") == true)
                bmp[3] = new Bitmap(dir + buttonName + "_disable.png");
            else
                throw (new SystemException("File: " + dir + buttonName + "_disable.png" + " does not exists"));

            this.MouseEnter += SkinCheckBox_MouseEnter;
            this.MouseLeave += SkinCheckBox_MouseLeave;

            BitmapRegion.CreateControlRegion(this, bmp[0]);
        }


        public void UpdateSkin(int state, string dir, string buttonName)
        {
            if (state == 1)
            {
                bmp[2] = new Bitmap(dir +  buttonName + ".png");
            }
        }
        private void SkinCheckBox_MouseLeave(object sender, EventArgs e)
        {
            onEnter = false;
        }

        private void SkinCheckBox_MouseEnter(object sender, EventArgs e)
        {
            onEnter = true;
        }

        public bool onEnter
        {
            set
            {
                m_isEnter = value;   
                if (m_isEnter == true && m_checked == false)
                {
                    BitmapRegion.CreateControlRegion(this, bmp[CommonApi.Common.ENTER]);
                }
                if (m_isEnter == false && m_checked == false)
                {
                    BitmapRegion.CreateControlRegion(this, bmp[CommonApi.Common.LEAVE]);
                }
            }
        }
        public void Checked(Action<bool> cb)
        {
            m_checked = !m_checked;
            if (m_checked == true)
            {
                BitmapRegion.CreateControlRegion(this, bmp[CommonApi.Common.DOWN]);
            }
            else
            {
                if (m_isEnter == false)
                    BitmapRegion.CreateControlRegion(this, bmp[CommonApi.Common.UP]);
                else
                    BitmapRegion.CreateControlRegion(this, bmp[CommonApi.Common.ENTER]);
            }
            cb(m_checked);
        }

        public bool GetChecked()
        {
            return m_checked;
        }
        public void Toggle()
        {
            m_checked = !m_checked;
            Checked(m_checked);
        }


        public void Checked(bool c)
        {
            m_checked = c;
            if (c)
            {
                BitmapRegion.CreateControlRegion(this, bmp[CommonApi.Common.DOWN]);
            }
            else
            {
                if (m_isEnter == false)
                    BitmapRegion.CreateControlRegion(this, bmp[CommonApi.Common.LEAVE]);
                else
                    BitmapRegion.CreateControlRegion(this, bmp[CommonApi.Common.ENTER]);
            }
        }

    }
}
