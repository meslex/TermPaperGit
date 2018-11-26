using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TermPaperForm_v1
{
    public partial class ReaderLog : Form
    {
        private Reader reader;
        public ReaderLog(Reader r)
        {
            InitializeComponent();
            this.reader = r;
        }

        private void ReaderLog_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = reader.Log;
        }
    }
}
