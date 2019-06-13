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
            this.btnColaborador.Location = new System.Drawing.Point(548, 25);
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
            this.btnCliente.Location = new System.Drawing.Point(282, 25);
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
            this.btnPeixe.Location = new System.Drawing.Point(12, 25);
            this.btnPeixe.Name = "btnPeixe";
            this.btnPeixe.Size = new System.Drawing.Size(249, 370);
            this.btnPeixe.TabIndex = 0;
            this.btnPeixe.UseVisualStyleBackColor = false;
            this.btnPeixe.Click += new System.EventHandler(this.btnPeixe_Click);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TelaPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPeixe;
        private System.Windows.Forms.Button btnCliente;
        private System.Windows.Forms.Button btnColaborador;
    }
}

