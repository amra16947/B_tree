using System;
using System.IO;
using System.Windows.Forms;



namespace Exercise4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public string[] data;
        public B_tree p;

        private void button1_Click(object sender, EventArgs e)//Button: Load file
        {
            p = new B_tree();
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "TXT files|*.txt";
            theDialog.InitialDirectory = @"C:\";

            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                data = File.ReadAllLines(theDialog.FileName);
                
                p.GetText(data);
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)//Insert button
        {
            int value;
            Int32.TryParse(textBox1.Text,out value);
            p.B_TREE_INSERT(value);
            MessageBox.Show("The insertion is done properly.");
            textBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e) //Search button
        {
            int value;
            Int32.TryParse(textBox2.Text, out value);
            bool exists = p.SearchValue(value);

            if(exists) MessageBox.Show("The value " + value + " exists in the tree.");
            else MessageBox.Show("The value " + value + " does not exist in the tree.");
            textBox2.Text = "";

        }

        private void button4_Click(object sender, EventArgs e)//Visalize
        {
            string output = p.GetStringForGraphwiz();
            richTextBox1.Text = output;

            MessageBox.Show("Visualization successful.You can find the code for visualization at graphwiz.txt.");

        }

        private void button5_Click(object sender, EventArgs e)//Change t
        {
            int t;
            Int32.TryParse(textBox3.Text, out t);
            p.NewParametarT(2*t-1);

            MessageBox.Show("Done.");
            textBox3.Text = "";
        }
    }
}
