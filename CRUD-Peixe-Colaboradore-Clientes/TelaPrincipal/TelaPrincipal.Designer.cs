namespace TelaPrincipal
{
    partial class TelaPrincipal
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TelaPrincipal));
            this.btnColaborador = new System.Windows.Forms.Button();
            this.btnCliente = new System.Windows.Forms.Button();
            this.btnPeixe = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.bancoDeDadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarCaminhoConexãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnColaborador
            // 
            this.btnColaborador.BackColor = System.Drawing.Color.White;
            this.btnColaborador.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnColaborador.FlatAppearance.BorderSize = 5;
            this.btnColaborador.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnColaborador.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnColaborador.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColaborador.Image = global::TelaPrincipal.Properties.Resources._587officebuilding_100466;
            this.btnColaborador.Location = new System.Drawing.Point(548, 36);
            this.btnColaborador.Name = "btnColaborador";
            this.btnColaborador.Size = new System.Drawing.Size(249, 370);
            this.btnColaborador.TabIndex = 2;
            this.btnColaborador.UseVisualStyleBackColor = false;
            this.btnColaborador.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnCliente
            // 
            this.btnCliente.BackColor = System.Drawing.Color.White;
            this.btnCliente.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCliente.FlatAppearance.BorderSize = 5;
            this.btnCliente.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnCliente.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCliente.Image = global::TelaPrincipal.Properties.Resources.employee_3_icon_icons_com_76973;
            this.btnCliente.Location = new System.Drawing.Point(282, 36);
            this.btnCliente.Name = "btnCliente";
            this.btnCliente.Size = new System.Drawing.Size(249, 370);
            this.btnCliente.TabIndex = 1;
            this.btnCliente.UseVisualStyleBackColor = false;
            this.btnCliente.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnPeixe
            // 
            this.btnPeixe.BackColor = System.Drawing.Color.White;
            this.btnPeixe.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnPeixe.FlatAppearance.BorderSize = 5;
            this.btnPeixe.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnPeixe.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnPeixe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPeixe.Image = global::TelaPrincipal.Properties.Resources.Fish_Food_icon_icons_com_68747;
            this.btnPeixe.Location = new System.Drawing.Point(12, 36);
            this.btnPeixe.Name = "btnPeixe";
            this.btnPeixe.Size = new System.Drawing.Size(249, 370);
            this.btnPeixe.TabIndex = 0;
            this.btnPeixe.UseVisualStyleBackColor = false;
            this.btnPeixe.Click += new System.EventHandler(this.btnPeixe_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bancoDeDadosToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(815, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // bancoDeDadosToolStripMenuItem
            // 
            this.bancoDeDadosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modificarCaminhoConexãoToolStripMenuItem});
            this.bancoDeDadosToolStripMenuItem.Name = "bancoDeDadosToolStripMenuItem";
            this.bancoDeDadosToolStripMenuItem.Size = new System.Drawing.Size(103, 20);
            this.bancoDeDadosToolStripMenuItem.Text = "Banco de dados";
            // 
            // modificarCaminhoConexãoToolStripMenuItem
            // 
            this.modificarCaminhoConexãoToolStripMenuItem.Name = "modificarCaminhoConexãoToolStripMenuItem";
            this.modificarCaminhoConexãoToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.modificarCaminhoConexãoToolStripMenuItem.Text = "Modificar caminho conexão";
            this.modificarCaminhoConexãoToolStripMenuItem.Click += new System.EventHandler(this.modificarCaminhoConexãoToolStripMenuItem_Click);
            // 
            // TelaPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(815, 418);
            this.Controls.Add(this.btnColaborador);
            this.Controls.Add(this.btnCliente);
            this.Controls.Add(this.btnPeixe);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TelaPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.Load += new System.EventHandler(this.TelaPrincipal_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPeixe;
        private System.Windows.Forms.Button btnCliente;
        private System.Windows.Forms.Button btnColaborador;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem bancoDeDadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificarCaminhoConexãoToolStripMenuItem;
    }
}

