using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace com.mojang.escape
{
    public partial class EscapeForm : Form
    {
        private readonly bool[] m_keys = new bool[65536];

        public EscapeForm()
        {
            InitializeComponent();
	    }

        private void EscapeForm_Load(object sender, EventArgs e)
        {
            this.canvas.Start(this.m_keys);
        }

        private void EscapeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.canvas.Stop();
        }

        private void EscapeForm_KeyDown(object sender, KeyEventArgs e)
        {
            int code = (int)e.KeyCode;
            if (code > 0 && code < this.m_keys.Length)
            {
                this.m_keys[code] = true;
            }
        }

        private void EscapeForm_KeyUp(object sender, KeyEventArgs e)
        {
            int code = (int)e.KeyCode;
            if (code > 0 && code < this.m_keys.Length)
            {
                this.m_keys[code] = false;
            }
        }

        private void EscapeForm_Deactivate(object sender, EventArgs e)
        {
            for (int i = 0; i < this.m_keys.Length; i++)
            {
                this.m_keys[i] = false;
            }
        }
    }
}
