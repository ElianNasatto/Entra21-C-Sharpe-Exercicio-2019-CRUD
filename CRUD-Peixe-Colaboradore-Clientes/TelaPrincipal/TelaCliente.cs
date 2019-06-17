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
    public partial class TelaCliente : Form
    {
        public TelaCliente()
        {
            InitializeComponent();
        }

        bool verificado = false;
        private void VerificaCampos()
        {
            if (txtNome.Text == "")
            {
                MessageBox.Show("Você deve digitar um nome", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }

            if (mtbAltura.Text == " .")
            {
                DialogResult result = MessageBox.Show("Você não digitou a altura, deseja continuar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    mtbAltura.Focus();
                    return;
                }

            }

            if (mtbPeso.Text == "   .")
            {
                DialogResult result = MessageBox.Show("Você não digitou o peso, deseja continuar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    mtbPeso.Focus();
                    return;
                }

            }

            if (mtbFone.Text == "()     -")
            {
                MessageBox.Show("Você não digitou o telefone", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbFone.Focus();
                return;
            }

            if (mtbSaldo.Text == "R$     .")
            {
                MessageBox.Show("Você nao digitou o saldo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (mtbCep.Text == "     -")
            {
                DialogResult result = MessageBox.Show("Você não digitou o cep, deseja continuar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    mtbCep.Focus();
                    return;
                }
            }

            if (mtbNumero.Text == "")
            {
                MessageBox.Show("Você não digitou o numero da residencia", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            verificado = true;

        }

        private void LimpaCampos()
        {
            txtNome.Clear();
            mtbAltura.Clear();
            mtbPeso.Clear();
            mtbFone.Clear();
            mtbSaldo.Clear();
            checkSujo.Checked = false;
            mtbCep.Clear();
            cbEstado.SelectedItem = 0;
            cbCidade.SelectedItem = 0;
            mtbNumero.Clear();
            txtLogradouro.Clear();
            txtComplemento.Clear();

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cbCargo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TelaCliente_Load(object sender, EventArgs e)
        {

            SqlConnection conexao = new SqlConnection();
            TelaCaminhoConexao form = new TelaCaminhoConexao();
            conexao.ConnectionString = form.caminho;
            TelaCliente tela = new TelaCliente();

            try
            {
                conexao.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possivel conectar no banco de dados", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tela.Close();
            }

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = "SELECT nome_estado FROM estados";
            DataTable tabela = new DataTable();
            try
            {
                tabela.Load(comando.ExecuteReader());

            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possivel buscar os estados", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tela.Close();
            }
            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow linha = tabela.Rows[i];
                cbEstado.Items.Add(linha["nome_estado"]);
            }

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            VerificaCampos();
            if (verificado == true)
            {
                verificado = false;

                SqlConnection conexao = new SqlConnection();
                TelaCaminhoConexao form = new TelaCaminhoConexao();
                conexao.ConnectionString = form.caminho;

                try
                {
                    conexao.Open();
                }
                catch (Exception)
                {
                    MessageBox.Show("Não foi possivel conectar ao banco de dados", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                SqlCommand comando = new SqlCommand();
                comando.Connection = conexao;

                comando.CommandText = @"INSERT INTO clientes (nome, saldo, telefone, estaddo, cep, logradouro,numero,complemento,nome_sujo,altura,peso 
                 VALUES (@NOME, @SALDO, @TELEFONE, @ESTADO, @CEP, @LOGRADOURO, @NUMERO, @COMPLEMENTO, @NOME_SUJO, @ALTURA, @PESO";

                try
                {
                    comando.ExecuteNonQuery();
                    conexao.Close();
                    MessageBox.Show("Adicionado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    conexao.Close();
                    MessageBox.Show("Não foi possivel adicionar", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void cbCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cbEstado_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SqlConnection conexao = new SqlConnection();
            TelaCaminhoConexao tela = new TelaCaminhoConexao();
            conexao.ConnectionString = tela.caminho;
            try
            {
                conexao.Open();

            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possivel adicionar as cidades", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSalvar.Visible = false;

            }
            cbCidade.Enabled = true;
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = "SELECT id_estado FROM estados WHERE nome_estado = @ESTADO";
            comando.Parameters.AddWithValue("@ESTADO", cbEstado.SelectedItem.ToString());
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            DataRow linha = tabela.Rows[0];
            int id = Convert.ToInt32(linha["id_estado"]);
            comando.Connection.Close();
            conexao.Close();

            conexao.Open();
            comando.Connection = conexao;

            tabela.Clear();
            comando.CommandText = "SELECT nome_cidade FROM cidades WHERE fk_estado = @FKESTADO";
            string teste = cbEstado.SelectedItem.ToString();
            comando.Parameters.AddWithValue("@FKESTADO", teste);


            try
            {
            tabela.Load(comando.ExecuteReader());

            }
            catch (Exception)
            {

            }



            cbCidade.Items.Clear();
            cbCidade.Visible = true;

            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                 linha = tabela.Rows[i];
                cbCidade.Items.Add(linha["nome_cidade"]);
            }




        }

        private void button1_Click(object sender, EventArgs e)
        {
            cbCidade.Items.Clear();
        }
    }

}