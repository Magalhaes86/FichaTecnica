using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace FichaTecnica
{
    public partial class Listagemdefichas : Form
    {

        String connectString;
        SQLiteConnection conn;
        SQLiteDataAdapter adapter;
        DataTable dt;

        int index = 0; // for loop over dataset


        private static SQLiteConnection sqliteConnection;


        public Listagemdefichas()
        {
            InitializeComponent();
            connectString = @"Data Source=C:\\FichaTecnicaSoft\\BaseDados\\FichaTecnica.sqlite ;Version=3";
        }

        private void Listagemdefichas_Load(object sender, EventArgs e)
        {
            Getdata1();
        }


        private static SQLiteConnection DbConnection()
        {
            sqliteConnection = new SQLiteConnection("Data Source=C:\\FichaTecnicaSoft\\BaseDados\\FichaTecnica.sqlite; Version=3;");
            sqliteConnection.Open();
            return sqliteConnection;
        }


        public void Getdata1()
        {
            using (conn = new SQLiteConnection(connectString))
            {
                try
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        

                      //  MessageBox.Show("Connected to database");
                        dt = new DataTable();
                        // adapter = new SQLiteDataAdapter("SELECT * FROM Clientes", conn);Data
                        adapter = new SQLiteDataAdapter("SELECT FichaTecnica.Cod,FichaTecnica.CodCliente,FichaTecnica.CodClienteSage,FichaTecnica.NomeCliente,FichaTecnica.NumeroEncomenda, FichaTecnica.Data,FichaTecnica.Observacoes,      LinhasFichaTecnica.Linha,                LinhasFichaTecnica.Maquina,LinhasFichaTecnica.Quantidade,LinhasFichaTecnica.CompraSChanfro  ,LinhasFichaTecnica.CompraCChamfro,LinhasFichaTecnica.Ferro       ,LinhasFichaTecnica.Final,LinhasFichaTecnica.Colas ,LinhasFichaTecnica.Compound FROM FichaTecnica inner join LinhasFichaTecnica on FichaTecnica.Cod=LinhasFichaTecnica.NumeroDocumento GROUP BY FichaTecnica.Cod", conn);

                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;

                        // FichaTecnica
                        dataGridView1.Columns[0].HeaderText = "Cod";
                        dataGridView1.Columns[1].HeaderText = "CodCliente";
                        dataGridView1.Columns[2].HeaderText = "CodClienteSage";
                        dataGridView1.Columns[3].HeaderText = "NomeCliente";
                        dataGridView1.Columns[4].HeaderText = "NumeroEncomenda";

                        dataGridView1.Columns[5].HeaderText = "Data";
                        dataGridView1.Columns[6].HeaderText = "Observacoes";




                        // LinhasFichaTecnica
                        dataGridView1.Columns[7].HeaderText = "Linha";
                 //       dataGridView1.Columns[8].HeaderText = "NumeroDocumento";
                        dataGridView1.Columns[8].HeaderText = "Maquina";
                        dataGridView1.Columns[9].HeaderText = "Quantidade";

                        dataGridView1.Columns[10].HeaderText = "CompraSChanfro";
                        dataGridView1.Columns[11].HeaderText = "CompraCChamfro";
                        dataGridView1.Columns[12].HeaderText = "Ferro";


                        dataGridView1.Columns[13].HeaderText = "Final";
                        dataGridView1.Columns[14].HeaderText = "Colas";
                        dataGridView1.Columns[15].HeaderText = "Compound";


                        dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView1.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView1.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView1.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView1.Columns[12].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView1.Columns[13].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView1.Columns[14].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridView1.Columns[15].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                        conn.Close();


                        

                    }
                }
                catch (Exception ex)
                {
             MessageBox.Show(ex.Message);
                }
            }


        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            Getdata1();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["Cod"].Value.ToString();
                textBox2.Text = row.Cells["CodCliente"].Value.ToString();
                textBox3.Text = row.Cells["CodClienteSage"].Value.ToString();
                textBox4.Text = row.Cells["NomeCliente"].Value.ToString();
                textBox5.Text = row.Cells["Maquina"].Value.ToString();
                
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Escolha na listagem os dados a Exportar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //dataGridView1.Rows.Clear();
            }
            else
            {

           AddFichaTecnica fr = (AddFichaTecnica)Application.OpenForms["AddFichaTecnica"];
            fr.txtST_ID.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
              

                fr.add2.Enabled = true;

             
                fr.btnUpdate.Enabled = true;
                //    fr.textcodsage.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                //  fr.textNomeCliente.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();


                fr.ObterDadosCabecalho();
                fr.ObterDadosLinhas();
      


               Close();
        }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {


            BindingSource bs = new BindingSource();
            bs.DataSource = dataGridView1.DataSource;
            bs.Filter = string.Format("CONVERT(" + dataGridView1.Columns[0].DataPropertyName + ", System.String) like '%" + textBox6.Text.Replace("'", "''") + "%'");
            dataGridView1.DataSource = bs;
        
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dataGridView1.DataSource;
            bs.Filter = string.Format("CONVERT(" + dataGridView1.Columns[1].DataPropertyName + ", System.String) like '%" + textBox7.Text.Replace("'", "''") + "%'");
            dataGridView1.DataSource = bs;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dataGridView1.DataSource;
            bs.Filter = string.Format("CONVERT(" + dataGridView1.Columns[2].DataPropertyName + ", System.String) like '%" + textBox8.Text.Replace("'", "''") + "%'");
            dataGridView1.DataSource = bs;
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dataGridView1.DataSource;
            bs.Filter = string.Format("CONVERT(" + dataGridView1.Columns[3].DataPropertyName + ", System.String) like '%" + textBox9.Text.Replace("'", "''") + "%'");
            dataGridView1.DataSource = bs;
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dataGridView1.DataSource;
            bs.Filter = string.Format("CONVERT(" + dataGridView1.Columns[4].DataPropertyName + ", System.String) like '%" + textBox10.Text.Replace("'", "''") + "%'");
            dataGridView1.DataSource = bs;
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dataGridView1.DataSource;
            bs.Filter = string.Format("CONVERT(" + dataGridView1.Columns[9].DataPropertyName + ", System.String) like '%" + textBox11.Text.Replace("'", "''") + "%'");
            dataGridView1.DataSource = bs;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
       

        }




        private void dtp1_ValueChanged(object sender, EventArgs e)
        {

            
        }

        private void dtp2_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dataGridView1.DataSource;
            bs.Filter = string.Format("CONVERT(" + dataGridView1.Columns[5].DataPropertyName + ", System.String) like '%" + textBox12.Text.Replace("'", "''") + "%'");
            dataGridView1.DataSource = bs;
        }

        private void btnExportExcel_MouseHover(object sender, EventArgs e)
        {
            label14.Visible = true;
        }

        private void btnExportExcel_MouseLeave(object sender, EventArgs e)
        {
            label14.Visible = false;
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            label16.Visible = true;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            label16.Visible = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

          

            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";



        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
        }
    }
    }

