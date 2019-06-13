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
    public partial class TelaPrincipal : Form
    {
        public TelaPrincipal()
        {
            InitializeComponent();
        }

        private void btnPeixe_Click(object sender, EventArgs e)
        {
            TelaPeixe form = new TelaPeixe();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TelaCliente form = new TelaCliente();
            form.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            TelaColaborador form = new TelaColaborador();
            form.Show();
        }

        private void TelaPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void modificarCaminhoConexãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TelaCaminhoConexao form = new TelaCaminhoConexao();
            form.Show();
        }
    }
}
