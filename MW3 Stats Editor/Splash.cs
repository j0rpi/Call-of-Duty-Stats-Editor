using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MW3_Stats_Editor
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            label10.Parent = pictureBox1;
            label12.Parent = pictureBox1;
            this.TopMost = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
