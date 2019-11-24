using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OutLineDrawings
{
    public partial class Form1 : Form
    {
        Graphics gfx;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(105, 70);
            gfx = Graphics.FromImage(bitmap);
            
            var pen = new Pen(Brushes.Purple);
            gfx.DrawLine(pen, 0, 70, 105, 70);
            gfx.DrawLine(pen, 0, 70, 0, 35);
            gfx.DrawLine(pen, 0, 35, 35, 35);
            gfx.DrawLine(pen, 35, 35, 35, 70);
            gfx.DrawLine(pen, 35, 70, 70, 70);
            gfx.DrawLine(pen, 70, 70, 70, 35);
            gfx.DrawLine(pen, 70, 35, 105, 35);
            gfx.DrawLine(pen, 105, 35, 105, 0);

            bitmap.Save("tPieceBorder");
        }
    }
}
