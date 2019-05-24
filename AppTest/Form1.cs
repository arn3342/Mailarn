using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using Mailarn.Data.Management;
namespace AppTest
{
    public partial class Form1 : Form
    {
        bool abc;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void Failed(string Report)
        {
            MessageBox.Show(Report);
        }
        private void result(object sender, EventArgs e)
        {
            MessageBox.Show("Success");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            abc = false;
            MessageBox.Show("Clicked");
        }
    }
}
