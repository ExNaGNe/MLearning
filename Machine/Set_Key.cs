using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine
{
    public partial class Set_Key : Form
    {
        public Keys key { private set; get; }
        public Set_Key()
        {
            InitializeComponent();
        }

        private void Set_Key_KeyDown(object sender, KeyEventArgs e)
        {
            key = e.KeyCode;
            Close();
        }
    }
}
