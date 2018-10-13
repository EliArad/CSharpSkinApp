using CommonApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpSkinApp
{
    public partial class Form1 : Form
    {

        // To store the location of previous mouse left click in the form
        // so that we can use it to calculate the new form location during dragging
        private Point prevLeftClick;

        // To determine if it is the first time entry for every dragging of the form
        private bool isFirst = true;

        // Acts like a gate to do allow or deny
        private bool toBlock = true;

        string dir = @"Images\";
        Bitmap bmpFrmBack; 

        public Form1()
        {
            InitializeComponent();

            bmpFrmBack = new Bitmap(dir + "transparent.gif");
            BitmapRegion.CreateControlRegion(this, bmpFrmBack);
            this.Width = bmpFrmBack.Width;
            this.Height = bmpFrmBack.Height;

            btn1.SetSkin(dir, "max");
            btn2.SetSkin(dir, "max");


        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            // Check if dragging of the form has occurred
            if (e.Button == MouseButtons.Left)
            {
                // If this is the first mouse move event for left click dragging of the form,
                // store the current point clicked so that we can use it to calculate the form's
                // new location in subsequent mouse move events due to left click dragging of the form
                if (isFirst == true)
                {
                    // Store previous left click position
                    prevLeftClick = new Point(e.X, e.Y);

                    // Subsequent mouse move events will not be treated as first time, until the
                    // left mouse click is released or other mouse click occur
                    isFirst = false;
                }

                // On subsequent mouse move events with left mouse click down. (During dragging of form)
                else
                {
                    // This flag here is to allow alternate processing for dragging the form because it
                    // causes serious flicking when u allow every such events to change the form's location.
                    // You can try commenting this out to see what i mean
                    if (toBlock == false)
                        this.Location = new Point(this.Location.X + e.X - prevLeftClick.X, this.Location.Y + e.Y - prevLeftClick.Y);

                    // Store new previous left click position
                    prevLeftClick = new Point(e.X, e.Y);

                    // Allow or deny next mouse move dragging event
                    toBlock = !toBlock;
                }
            }

            // This is a new mouse move event so reset flag
            else
                isFirst = true;
        }

        private void btn2_Click(object sender, EventArgs e)
        {

        }

        private void btn1_Click(object sender, EventArgs e)
        {
            btn1.Toggle();
        }
    }
}
