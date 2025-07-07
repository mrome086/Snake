using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PA5_Draft
{
    public partial class Apples : Form
    {

        public int count;
        public Apples()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Apple_Click(object sender, EventArgs e)
        {
            count =  count + 1;
            countApple.Text = Convert.ToString(count);
            MainForm.instance.NumberOfApples = count;
        }
    }
}
