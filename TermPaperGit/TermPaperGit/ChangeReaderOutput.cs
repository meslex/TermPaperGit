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
    public partial class ChangeReaderOutput : Form
    {
        private List<Reader> readers;
        public Reader Result { get; set; }
        public ChangeReaderOutput(List<Reader> readers)
        {
            InitializeComponent();

            this.readers = readers;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Result = readers.Find(delegate (Reader r)
            {
                return r.Name.Equals(comboBox1.SelectedItem);
            });
            this.DialogResult = DialogResult.OK;
        }

        private void ChangeReaderOutput_Load(object sender, EventArgs e)
        {
            foreach(Reader r in readers)
            {
                comboBox1.Items.Add(r.Name);
            }
        }
    }
}
