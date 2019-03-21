using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LogViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_Open_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Kawec\source\repos\LogViewer\LogViewer\LogViewerDatabase.mdf;Integrated Security = True");
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            var patch = ofd.FileName;
            string[] lines = File.ReadLines(patch).ToArray();
            string[][] splittedLines = new string[lines.Length][];
            int length = 0;
            for(int i = 0; i < lines.Length; i++)
            {
                splittedLines[i] = lines[i].Split(',');
                if (splittedLines[i].Length > length) length = splittedLines[i].Length;
            }
            MessageBox.Show(length.ToString());
            for(int i = 0; i < length; i++)
            {
                dataGridView1.Columns.Add("Column", "");
            }
            string[] row = new string[length];
            for(int i = 0; i < splittedLines.Length; i++)
            {
                for (int j = 0; j < splittedLines[i].Length; j++)
                {
                    row[j] = splittedLines[i][j];
                }
                dataGridView1.Rows.Add(row);
                Array.Clear(row, 0, row.Length);
            }
            DataTable dtFromGrid = new DataTable();
            dtFromGrid = dataGridView1.DataSource as DataTable;

        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Kawec\source\repos\LogViewer\LogViewer\LogViewerDatabase.mdf;Integrated Security = True");
            try
            {
                con.Open();
                SqlCommand command;
                command = new SqlCommand("DROP TABLE LogX", con);
                //command.ExecuteNonQuery();
                string dbString= "CREATE TABLE LogX(X(50), Y(50), Z(50));";
                command = new SqlCommand(dbString, con);
                command.ExecuteNonQuery();
                con.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
