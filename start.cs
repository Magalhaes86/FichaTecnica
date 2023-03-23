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
    public partial class start : Form
    {
        public start()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Administrador f2 = new Administrador();
            f2.ShowDialog(); // Shows Form2
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clientes f2 = new Clientes();
            f2.ShowDialog(); // Shows Form2
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

      
        private void start_Load(object sender, EventArgs e)
        {
            timer1.Start();
            label1.Text = DateTime.Now.ToLongTimeString();
            label2.Text = DateTime.Now.ToShortDateString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddFichaTecnica f2 = new AddFichaTecnica();
            f2.ShowDialog(); // Shows Form2
        }

        private void button5_Click(object sender, EventArgs e)
        {
          
        }

        private void btlistfichatecnica_Click(object sender, EventArgs e)
        {
      
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            label4.Visible = true;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            label4.Visible = false;
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            label3.Visible = true;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            label3.Visible = false;
        }

        private void btnFechar_MouseHover(object sender, EventArgs e)
        {
            btnFechar.BackColor = Color.OrangeRed;
        }

        private void btnFechar_MouseLeave(object sender, EventArgs e)
        {
            btnFechar.BackColor = Color.Transparent;
        }
    }
}
