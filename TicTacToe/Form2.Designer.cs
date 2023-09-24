using System.Collections.Generic;

namespace TicTacToe
{
    partial class Form2
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        private void InitializeComponent()
        {
            this.gridSizeTextBox = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.instructionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // gridSizeTextBox
            // 
            this.gridSizeTextBox.Location = new System.Drawing.Point(128, 84);
            this.gridSizeTextBox.Name = "gridSizeTextBox";
            this.gridSizeTextBox.Size = new System.Drawing.Size(121, 22);
            this.gridSizeTextBox.TabIndex = 2;
            this.gridSizeTextBox.Text = "3";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(128, 127);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(121, 30);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "Start";
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // instructionLabel
            // 
            this.instructionLabel.AutoSize = true;
            this.instructionLabel.Location = new System.Drawing.Point(43, 42);
            this.instructionLabel.Name = "instructionLabel";
            this.instructionLabel.Size = new System.Drawing.Size(317, 16);
            this.instructionLabel.TabIndex = 1;
            this.instructionLabel.Text = "Please type a number to choose the number of rows:";
            // 
            // Form2
            // 
            this.ClientSize = new System.Drawing.Size(403, 185);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.gridSizeTextBox);
            this.Controls.Add(this.instructionLabel);
            this.Name = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TextBox gridSizeTextBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label instructionLabel;
    }
}

