namespace com.mojang.escape
{
    partial class EscapeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.canvas = new com.mojang.escape.EscapeComponent();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.Location = new System.Drawing.Point(0, 0);
            this.canvas.Name = "canvas";
            this.canvas.TabIndex = 0;
            // 
            // EscapeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(170, 130);
            this.Controls.Add(this.canvas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "EscapeForm";
            this.Text = "Prelude of the Chambered!";
            this.Deactivate += new System.EventHandler(this.EscapeForm_Deactivate);
            this.Load += new System.EventHandler(this.EscapeForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.EscapeForm_KeyUp);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EscapeForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EscapeForm_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private EscapeComponent canvas;
    }
}

