using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileFuardSetup.Page
{
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
            progressBar1.Maximum = 110;
        }
        public void UpdateProgress(int value, string message)
        {
            value = Math.Min(value, progressBar1.Maximum);

          
            progressBar1.Value = value;
            label1.Text = message;
        }
        private void ProgressForm_Load(object sender, EventArgs e)
        {

        }
    }
}
