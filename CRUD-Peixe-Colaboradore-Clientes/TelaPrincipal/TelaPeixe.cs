using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelaPrincipal
{
    public partial class TelaPeixe : Form
    {
        public TelaPeixe()
        {
            InitializeComponent();
        }

        // 
        //  V  A  R  I  A  V  E  I  S
        //
        //

        bool verifica = false;
        decimal precoFinal = 0;

        //
        //
        //


        private void RetiraMascaraPreco()
        {
            string preco = mtbPreco.Text;
            preco = mtbPreco.Text;
            preco = preco.Replace("R$", "");
            precoFinal = Convert.ToDecimal(preco);

        }

        private void VerificaCampos()
        {
            if (txtNome.Text == "")
            {
                MessageBox.Show("Digite o nome!");
                txtNome.Focus();
                return;
            }

            try
            {
                string opcao = cbRaca.SelectedItem.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Selecione a raça!");
                cbRaca.Focus();
                return;
            
            }

            if (mtbPreco.Text == "R$    .")
            {
                MessageBox.Show("Digite o preço!");
                mtbPreco.Focus();
                return;
            }

            if (nudQuantidade.Value == 0)
            {
                DialogResult result = MessageBox.Show("Quantidade igual a 0, deseja continuar?","Aviso!", MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);

                if (result == DialogResult.No)
                {
                    nudQuantidade.Focus();
                    return;
                }
            }

            verifica = true;

        }

        private void LimpaCampos()
        {
            txtNome.Clear();
            cbRaca.SelectedIndex = -1;
            mtbPreco.Clear();
            nudQuantidade.Value = 0;
        }


        private void TelaPeixe_Load(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            VerificaCampos();
            if (verifica == true)
            {
                verifica = false;

                Peixe peixe = new Peixe();
                peixe.Nome = txtNome.Text;
                peixe.Raca = cbRaca.SelectedItem.ToString();
                RetiraMascaraPreco();
                peixe.Preco = precoFinal;
                precoFinal = 0;
                peixe.Quantidade = Convert.ToInt32(nudQuantidade.Value);

                SqlConnection conexao = new SqlConnection();
                conexao.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=T:\Documentos\GitHub\C-Sharpe-Entra21-Exercicio-2019-CRUD\CRUD-Peixe-Colaboradore-Clientes\Banco de Dados\BD_CRUD.mdf;Integrated Security=True;Connect Timeout=30";
                try
                {
                conexao.Open();

                }
                catch (Exception)
                {
                    MessageBox.Show("Erro ao conectar ao banco de dados","Erro Banco de Dados",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    return;
                }

                SqlCommand comando = new SqlCommand();
                comando.Connection = conexao;

                comando.CommandText = @"INSERT INTO peixes (nome,raca,preco,quantidade)
                VALUES (@NOME,@RACA,@PRECO,@QUANTIDADE)";
                comando.Parameters.AddWithValue("@NOME", peixe.Nome);
                comando.Parameters.AddWithValue("@RACA", peixe.Raca);
                comando.Parameters.AddWithValue("@PRECO", peixe.Preco);
                comando.Parameters.AddWithValue("@QUANTIDADE", peixe.Quantidade);
                try
                {
                    comando.ExecuteNonQuery();
                    conexao.Close();
                    MessageBox.Show("Adicionado com sucesso","Sucesso",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Erro ao adicionar!","Erro",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    conexao.Close();
                    return;
                }

            }
        }
    }
}
