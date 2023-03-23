
namespace FichaTecnica
{
    partial class Administrador
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCriarBaseDados = new System.Windows.Forms.Button();
            this.btnCriarTabela = new System.Windows.Forms.Button();
            this.btnAtualizarDados = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnFechar = new System.Windows.Forms.Button();
            this.btnIncluirDados = new System.Windows.Forms.Button();
            this.btnMostrarDados = new System.Windows.Forms.Button();
            this.btnProcurarDados = new System.Windows.Forms.Button();
            this.btnEliminarDados = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.dgvDados = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDados)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCriarBaseDados
            // 
            this.btnCriarBaseDados.Location = new System.Drawing.Point(14, 32);
            this.btnCriarBaseDados.Name = "btnCriarBaseDados";
            this.btnCriarBaseDados.Size = new System.Drawing.Size(186, 38);
            this.btnCriarBaseDados.TabIndex = 0;
            this.btnCriarBaseDados.Text = "Criar Base de Dados";
            this.btnCriarBaseDados.UseVisualStyleBackColor = true;
            this.btnCriarBaseDados.Click += new System.EventHandler(this.btnCriarBaseDados_Click);
            // 
            // btnCriarTabela
            // 
            this.btnCriarTabela.Location = new System.Drawing.Point(206, 32);
            this.btnCriarTabela.Name = "btnCriarTabela";
            this.btnCriarTabela.Size = new System.Drawing.Size(208, 38);
            this.btnCriarTabela.TabIndex = 1;
            this.btnCriarTabela.Text = "Criar Tabela";
            this.btnCriarTabela.UseVisualStyleBackColor = true;
            this.btnCriarTabela.Click += new System.EventHandler(this.btnCriarTabela_Click);
            // 
            // btnAtualizarDados
            // 
            this.btnAtualizarDados.Location = new System.Drawing.Point(28, 92);
            this.btnAtualizarDados.Name = "btnAtualizarDados";
            this.btnAtualizarDados.Size = new System.Drawing.Size(247, 50);
            this.btnAtualizarDados.TabIndex = 2;
            this.btnAtualizarDados.Text = "Atualizar Dados";
            this.btnAtualizarDados.UseVisualStyleBackColor = true;
            this.btnAtualizarDados.Click += new System.EventHandler(this.btnAtualizarDados_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnFechar);
            this.panel1.Controls.Add(this.btnIncluirDados);
            this.panel1.Controls.Add(this.btnMostrarDados);
            this.panel1.Controls.Add(this.btnProcurarDados);
            this.panel1.Controls.Add(this.btnEliminarDados);
            this.panel1.Controls.Add(this.btnAtualizarDados);
            this.panel1.Controls.Add(this.btnCriarTabela);
            this.panel1.Controls.Add(this.btnCriarBaseDados);
            this.panel1.Location = new System.Drawing.Point(45, 535);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1076, 179);
            this.panel1.TabIndex = 3;
            // 
            // btnFechar
            // 
            this.btnFechar.Location = new System.Drawing.Point(814, 92);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(247, 50);
            this.btnFechar.TabIndex = 2;
            this.btnFechar.Text = "Fechar";
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // btnIncluirDados
            // 
            this.btnIncluirDados.Location = new System.Drawing.Point(720, 26);
            this.btnIncluirDados.Name = "btnIncluirDados";
            this.btnIncluirDados.Size = new System.Drawing.Size(247, 50);
            this.btnIncluirDados.TabIndex = 2;
            this.btnIncluirDados.Text = "Inserir Dados";
            this.btnIncluirDados.UseVisualStyleBackColor = true;
            this.btnIncluirDados.Click += new System.EventHandler(this.btnIncluirDados_Click);
            // 
            // btnMostrarDados
            // 
            this.btnMostrarDados.Location = new System.Drawing.Point(441, 26);
            this.btnMostrarDados.Name = "btnMostrarDados";
            this.btnMostrarDados.Size = new System.Drawing.Size(247, 50);
            this.btnMostrarDados.TabIndex = 2;
            this.btnMostrarDados.Text = "Mostrar Dados";
            this.btnMostrarDados.UseVisualStyleBackColor = true;
            this.btnMostrarDados.Click += new System.EventHandler(this.btnMostrarDados_Click);
            // 
            // btnProcurarDados
            // 
            this.btnProcurarDados.Location = new System.Drawing.Point(561, 92);
            this.btnProcurarDados.Name = "btnProcurarDados";
            this.btnProcurarDados.Size = new System.Drawing.Size(247, 50);
            this.btnProcurarDados.TabIndex = 2;
            this.btnProcurarDados.Text = "Procurar Dados";
            this.btnProcurarDados.UseVisualStyleBackColor = true;
            this.btnProcurarDados.Click += new System.EventHandler(this.btnProcurarDados_Click);
            // 
            // btnEliminarDados
            // 
            this.btnEliminarDados.Location = new System.Drawing.Point(297, 92);
            this.btnEliminarDados.Name = "btnEliminarDados";
            this.btnEliminarDados.Size = new System.Drawing.Size(247, 50);
            this.btnEliminarDados.TabIndex = 2;
            this.btnEliminarDados.Text = "Eliminar Dados";
            this.btnEliminarDados.UseVisualStyleBackColor = true;
            this.btnEliminarDados.Click += new System.EventHandler(this.btnEliminarDados_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtEmail);
            this.panel2.Controls.Add(this.txtNome);
            this.panel2.Controls.Add(this.txtID);
            this.panel2.Controls.Add(this.dgvDados);
            this.panel2.Location = new System.Drawing.Point(45, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1085, 496);
            this.panel2.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 375);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Email";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 331);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nome";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 279);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "ID";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(135, 370);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(758, 22);
            this.txtEmail.TabIndex = 3;
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(122, 328);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(772, 22);
            this.txtNome.TabIndex = 2;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(131, 279);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(108, 22);
            this.txtID.TabIndex = 1;
            // 
            // dgvDados
            // 
            this.dgvDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDados.Location = new System.Drawing.Point(66, 34);
            this.dgvDados.Name = "dgvDados";
            this.dgvDados.RowHeadersWidth = 51;
            this.dgvDados.RowTemplate.Height = 24;
            this.dgvDados.Size = new System.Drawing.Size(913, 203);
            this.dgvDados.TabIndex = 0;
            // 
            // Administrador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 746);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Administrador";
            this.Text = "Administrador";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCriarBaseDados;
        private System.Windows.Forms.Button btnCriarTabela;
        private System.Windows.Forms.Button btnAtualizarDados;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Button btnIncluirDados;
        private System.Windows.Forms.Button btnMostrarDados;
        private System.Windows.Forms.Button btnProcurarDados;
        private System.Windows.Forms.Button btnEliminarDados;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.DataGridView dgvDados;
    }
}