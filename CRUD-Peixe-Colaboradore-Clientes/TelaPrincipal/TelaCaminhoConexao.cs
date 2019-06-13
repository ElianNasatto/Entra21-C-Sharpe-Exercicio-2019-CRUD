using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelaPrincipal
{
    public partial class TelaCaminhoConexao : Form
    {
        public TelaCaminhoConexao()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            caminho = textBox1.Text;
            MessageBox.Show("Caminho alterado com sucesso!","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
        public string caminho = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\elian\Documents\GitHub\C-Sharpe-Entra21-Exercicio-2019-CRUD\CRUD-Peixe-Colaboradore-Clientes\Banco de Dados\BD_CRUD.mdf;Integrated Security=True;Connect Timeout=30";
    }
}
