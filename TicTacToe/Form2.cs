using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form2 : Form
    {
        public int SelectedGridSize { get; private set; }

        public Form2()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (int.TryParse(gridSizeTextBox.Text, out var value))
            {
                SelectedGridSize = value;
                Close();
            }
        }
    }
}