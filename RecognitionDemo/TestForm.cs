﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RecognitionDemo
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Move(object sender, EventArgs e)
        {
            MessageBox.Show("Hello!");
        }
    }
}
