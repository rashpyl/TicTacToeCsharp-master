using System.Collections.Generic;

namespace TicTacToe
{
    partial class Form1
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

            this.components = new System.ComponentModel.Container();

            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonRestart = new System.Windows.Forms.Button();
            this.CPUTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();

            
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label1.ForeColor = System.Drawing.Color.DarkCyan;
            this.label1.Location = new System.Drawing.Point(55, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 36);
            this.label1.TabIndex = 9;
            this.label1.Text = "Human wins:";
            
            
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label2.ForeColor = System.Drawing.Color.Crimson;
            this.label2.Location = new System.Drawing.Point(497, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 36);
            this.label2.TabIndex = 10;
            this.label2.Text = "CPU wins:";


            this.buttonRestart.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.buttonRestart.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.buttonRestart.Location = new System.Drawing.Point(291, 603);
            this.buttonRestart.Name = "buttonRestart";
            this.buttonRestart.Size = new System.Drawing.Size(157, 69);
            this.buttonRestart.TabIndex = 11;
            this.buttonRestart.Text = "Restart";
            this.buttonRestart.UseVisualStyleBackColor = false;
            this.buttonRestart.Click += new System.EventHandler(this.RestartGame);
            
            
            this.CPUTimer.Interval = 500;
            this.CPUTimer.Tick += new System.EventHandler(this.CPUmove);
            
            
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 684);
            this.Controls.Add(this.buttonRestart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);

            this.Name = "Form1";
            this.Text = "TicTacToe";
            this.ResumeLayout(false);
            this.PerformLayout();
        }


        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonRestart;
        private System.Windows.Forms.Timer CPUTimer;
    }
}

