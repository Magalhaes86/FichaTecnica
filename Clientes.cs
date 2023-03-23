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
    public partial class Clientes : Form
    {

        private static SQLiteConnection sqliteConnection;

        public Clientes()
        {
            InitializeComponent();
        }

      
        private static SQLiteConnection DbConnection()
        {
            sqliteConnection = new SQLiteConnection("Data Source=C:\\FichaTecnicaSoft\\BaseDados\\FichaTecnica.sqlite; Version=3;");
            sqliteConnection.Open();
            return sqliteConnection;
        }



        SQLiteDataAdapter da = null;
        DataTable dt = new DataTable();



        private bool Valida()
        {
            if (string.IsNullOrEmpty(txtID.Text) && string.IsNullOrEmpty(txtNome.Text) && string.IsNullOrEmpty(txtEmail.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void LimpaDados()
        {
            txtID.Text = "";
            txtNome.Text = "";
            txtEmail.Text = "";
            textCodSage.Text = "";
            txttlm.Text = "";
        }

        private void LimpaDadosPesquisa()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";

        }

        private void dgvDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvDados.Rows[e.RowIndex];
                txtID.Text = row.Cells["Id"].Value.ToString();
                textCodSage.Text = row.Cells["CodSage"].Value.ToString();
                txtNome.Text = row.Cells["Nome"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txttlm.Text = row.Cells["Tlm"].Value.ToString();
                
            }
        }

        private void ExibirDados()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = DalHelper.GetClientes();
                dgvDados.DataSource = dt;

                dgvDados.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvDados.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvDados.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvDados.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvDados.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


                //  dgvDados.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        private void btnMostrarDados_Click(object sender, EventArgs e)
        {
         
        
        }

        private void btnIncluirDados_Click(object sender, EventArgs e)
        {
           
            if (txtNome.Text == "")
            {
                MessageBox.Show("Tem de inserir no minimo, o nome do cliente", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }

            try
            {
                Cliente cli = new Cliente();
                cli.Id = Convert.ToInt32(txtID.Text);
                cli.CodSage = textCodSage.Text;
                cli.Nome = txtNome.Text;
                cli.Email = txtEmail.Text;
                cli.Tlm = txttlm.Text;

                DalHelper.Add(cli);

                ExibirDados();
                LimpaDados();
                btnIncluirDados.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        private void btnAtualizarDados_Click(object sender, EventArgs e)
        {
            if (!Valida())
            {
                MessageBox.Show("Informe os dados cliente a atualizar");
                return;
            }

            try
            {
                Cliente cli = new Cliente();
                cli.Id = Convert.ToInt32(txtID.Text);
                cli.CodSage = textCodSage.Text;
                cli.Nome = txtNome.Text;
                cli.Email = txtEmail.Text;
                cli.Tlm = txttlm.Text;
                DalHelper.Update(cli);
                ExibirDados();
                LimpaDados();


                btnAtualizarDados.Enabled = false;
                btnEliminarDados.Visible = false;
                btnIncluirDados.Enabled = false;
              

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }


private void btnEliminarDados_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Deseja ELIMINAR a Linha selecionada?", "Eliminar Linha", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                      {

            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Informe o ID do cliente a ser Excluído");
                return;
            }
            try
            {
                int codigo = Convert.ToInt32(txtID.Text);
                DalHelper.Delete(codigo);
                ExibirDados();
                LimpaDados();
                btnAtualizarDados.Enabled = false;
                btnEliminarDados.Visible = false;
                btnIncluirDados.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
            }
            else if (dialogResult == DialogResult.No)
               {
                textCodSage.Focus();


                txtID.Text = "";
                textCodSage.Text = "";
                txtNome.Text = "";
                txtEmail.Text = "";
                txttlm.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox6.Text = "";
                btnAtualizarDados.Enabled = false;
                btnEliminarDados.Visible = false;
                btnIncluirDados.Enabled = false;

            }
        }
        private void btnProcurarDados_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Informe o ID do cliente a ser Localizado");
                return;
            }
            try
            {
                DataTable dt = new DataTable();
                int codigo = Convert.ToInt32(txtID.Text);

                dt = DalHelper.GetCliente(codigo);
                dgvDados.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            const string mensagem = "Deseja Encerrar ?";
            const string titulo = "Encerrar";
            var resultado = MessageBox.Show(mensagem, titulo,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void dgvDados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            btedit.Enabled = true;


        }

        private void btnCriarBaseDados_Click(object sender, EventArgs e)
        {

        }

        private void btnCriarTabela_Click(object sender, EventArgs e)
        {

        }

        private void NewId2()
        {

            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();


            using (var cmd = DbConnection().CreateCommand())
            {
                cmd.CommandText = "SELECT MAX(id) + 1 as Id FROM Clientes";
                da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                da.Fill(dt);
                txtID.Text = cmd.ExecuteScalar().ToString();

                if (txtID.Text.Length == 0)
                {
                    txtID.Text = "1";
                }

            }
        }
        private void btnNovo_Click(object sender, EventArgs e)
        {
            //Valida();

                LimpaDados();
            NewId2();
            btnIncluirDados.Enabled = true;
            btnIncluirDados.Visible = true;
            btnAtualizarDados.Enabled = false;

        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            ExibirDados();
        }


        private void btedit_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Deseja Editar a Linha selecionada?", "Editar Linha", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                DataGridViewCell cell = null;
                foreach (DataGridViewCell selectedCell in dgvDados.SelectedCells)
                {
                    cell = selectedCell;
                    break;
                }
                if (cell != null)
                {
                    DataGridViewRow row = cell.OwningRow;


                    txtID.Text = row.Cells[0].Value.ToString();
                    textCodSage.Text = row.Cells[1].Value.ToString();
                    txtNome.Text = row.Cells[2].Value.ToString();
                    txtEmail.Text = row.Cells[3].Value.ToString();
                    txttlm.Text = row.Cells[4].Value.ToString();

                    btedit.Enabled = false;
                    btnAtualizarDados.Enabled = true;
                    btnEliminarDados.Visible = true;
                        btnEliminarDados.Enabled = true;
                                  }


            }
            else if (dialogResult == DialogResult.No)
            {
                //caso pretenda fazer outra coisa qualuqer.
                textBox1.Focus();
                btedit.Enabled = false;
                //button7.Enabled = false;
            }
        }

        private void dgvDados_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            btedit.Enabled = true;
        }

   

        private void btnClose_MouseHover(object sender, EventArgs e)
        {
            label12.Visible = true;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            label12.Visible = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           

            start fstart = (start)Application.OpenForms["start"];
            
            this.Hide();
         //   fstart.ShowDialog();
            this.Close();


        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dgvDados.DataSource;
            bs.Filter = string.Format("CONVERT(" + dgvDados.Columns[0].DataPropertyName + ", System.String) like '%" + textBox3.Text.Replace("'", "''") + "%'");
            dgvDados.DataSource = bs;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dgvDados.DataSource;
            bs.Filter = string.Format("CONVERT(" + dgvDados.Columns[1].DataPropertyName + ", System.String) like '%" + textBox1.Text.Replace("'", "''") + "%'");
            dgvDados.DataSource = bs;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dgvDados.DataSource;
            bs.Filter = string.Format("CONVERT(" + dgvDados.Columns[2].DataPropertyName + ", System.String) like '%" + textBox2.Text.Replace("'", "''") + "%'");
            dgvDados.DataSource = bs;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dgvDados.DataSource;
            bs.Filter = string.Format("CONVERT(" + dgvDados.Columns[3].DataPropertyName + ", System.String) like '%" + textBox4.Text.Replace("'", "''") + "%'");
            dgvDados.DataSource = bs;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dgvDados.DataSource;
            bs.Filter = string.Format("CONVERT(" + dgvDados.Columns[4].DataPropertyName + ", System.String) like '%" + textBox6.Text.Replace("'", "''") + "%'");
            dgvDados.DataSource = bs;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //  1 2 3 4 6 
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";

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
