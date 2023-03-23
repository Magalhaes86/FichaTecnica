using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FichaTecnica
{
    public partial class listclientes : Form



    {
        public listclientes()
        {
            InitializeComponent();
        }


        private void ExibirDados()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = DalHelper.GetClientes();
                dgw.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }


        private void listclientes_Load(object sender, EventArgs e)
        {
            ExibirDados();
            dgw.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgw.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgw.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgw.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgw.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        //    ExibirDados();
        }

        AddFichaTecnica fr = new AddFichaTecnica();

      

        private void dgw_CellClick(object sender, DataGridViewCellEventArgs e)
        {



            AddFichaTecnica fr = (AddFichaTecnica)Application.OpenForms["AddFichaTecnica"];
            fr.textcodcliente.Text = this.dgw.CurrentRow.Cells[0].Value.ToString();
            fr.textcodsage.Text = this.dgw.CurrentRow.Cells[1].Value.ToString();
            fr.textNomeCliente.Text = this.dgw.CurrentRow.Cells[2].Value.ToString();

            fr.tbtlm.Text = this.dgw.CurrentRow.Cells[4].Value.ToString();
            //AddFichaTecnica fr = (AddFichaTecnica)Application.OpenForms["AddFichaTecnica"];
            //      int row = e.RowIndex;
            //      fr.textcodcliente.Text = Convert.ToString(dgw[0, row].Value);
            //      fr.textcodsage.Text = Convert.ToString(dgw[1, row].Value);
            //      fr.textNomeCliente.Text = Convert.ToString(dgw[2, row].Value);
            Close();
            }

   
      

      

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
          
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dgw.DataSource;
            bs.Filter = string.Format("CONVERT(" + dgw.Columns[0].DataPropertyName + ", System.String) like '%" + textBox7.Text.Replace("'", "''") + "%'");
            dgw.DataSource = bs;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dgw.DataSource;
            bs.Filter = string.Format("CONVERT(" + dgw.Columns[1].DataPropertyName + ", System.String) like '%" + textBox8.Text.Replace("'", "''") + "%'");
            dgw.DataSource = bs;
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dgw.DataSource;
            bs.Filter = string.Format("CONVERT(" + dgw.Columns[2].DataPropertyName + ", System.String) like '%" + textBox9.Text.Replace("'", "''") + "%'");
            dgw.DataSource = bs;
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dgw.DataSource;
            bs.Filter = string.Format("CONVERT(" + dgw.Columns[3].DataPropertyName + ", System.String) like '%" + textBox10.Text.Replace("'", "''") + "%'");
            dgw.DataSource = bs;
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dgw.DataSource;
            bs.Filter = string.Format("CONVERT(" + dgw.Columns[4].DataPropertyName + ", System.String) like '%" + textBox11.Text.Replace("'", "''") + "%'");
            dgw.DataSource = bs;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";

        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            label16.Visible = true;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            label16.Visible = false;
        }
    }
    }





