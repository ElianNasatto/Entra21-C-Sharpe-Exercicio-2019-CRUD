using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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
        decimal SaldoFinal = 0;
        string CepFinal = "";

        private void RetiraCEP()
        {
            CepFinal = mtbCep.Text;
            CepFinal = CepFinal.Replace("-", "");
        }


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

        private void RetiraSaldo()
        {
            string saldo = mtbSaldo.Text;
            saldo = saldo.Replace("R$", "");
            SaldoFinal = Convert.ToDecimal(saldo);
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
            comando.CommandText = "SELECT uf FROM estados";
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
                cbEstado.Items.Add(linha["uf"]);
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

                comando.CommandText = @"INSERT INTO clientes (nome, saldo, telefone, estado,cidade, cep, logradouro,numero,complemento,nome_sujo,altura,peso)
                 VALUES (@NOME, @SALDO, @TELEFONE, @ESTADO,@CIDADE, @CEP, @LOGRADOURO, @NUMERO, @COMPLEMENTO, @NOME_SUJO, @ALTURA, @PESO)";
                Cliente cliente = new Cliente();
                cliente.Nome = txtNome.Text;
                RetiraSaldo();
                cliente.Saldo = SaldoFinal;
                cliente.Telefone = mtbFone.Text;
                cliente.Estado = cbEstado.SelectedItem.ToString();
                cliente.Cidade = cbCidade.SelectedItem.ToString();
                cliente.Numero = Convert.ToInt32(mtbNumero.Text);
                RetiraCEP();
                cliente.CEP = CepFinal;
                cliente.Logradouro = txtLogradouro.Text;
                cliente.Complemento = txtComplemento.Text;
                if (checkSujo.Checked == true)
                {
                    cliente.Nome_sujo = true;
                }
                else
                {
                    cliente.Nome_sujo = false;
                }

                cliente.altura = Convert.ToDecimal(mtbAltura.Text);
                cliente.peso = Convert.ToDecimal(mtbPeso.Text);

                comando.Parameters.AddWithValue("@NOME", cliente.Nome);
                comando.Parameters.AddWithValue("@SALDO", cliente.Saldo);
                comando.Parameters.AddWithValue("@TELEFONE", cliente.Telefone);
                comando.Parameters.AddWithValue("@ESTADO", cliente.Estado);
                comando.Parameters.AddWithValue("@CIDADE", cliente.Cidade);
                comando.Parameters.AddWithValue("@CEP", cliente.CEP);
                comando.Parameters.AddWithValue("@LOGRADOURO", cliente.Logradouro);
                comando.Parameters.AddWithValue("@NUMERO", cliente.Numero);
                comando.Parameters.AddWithValue("@COMPLEMENTO", cliente.Complemento);
                comando.Parameters.AddWithValue("@NOME_SUJO", cliente.Nome_sujo);
                comando.Parameters.AddWithValue("@ALTURA", cliente.altura);
                comando.Parameters.AddWithValue("@PESO", cliente.peso);

                try
                {
                    comando.ExecuteNonQuery();
                    conexao.Close();
                    MessageBox.Show("Adicionado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception erro)
                {
                    MessageBox.Show(erro.ToString());
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


        }



        private void button1_Click(object sender, EventArgs e)
        {
            cbCidade.Items.Clear();
        }

        string nomeCidade = "";

        private void mtbCep_Leave(object sender, EventArgs e)
        {
            string cep = mtbCep.Text;

            WebRequest request = WebRequest.Create($"http://viacep.com.br/ws/{cep}/json/");
            WebResponse response = request.GetResponse();

            Stream stream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream);
            string texto = streamReader.ReadToEnd();
            Endereco endereco = JsonConvert.DeserializeObject<Endereco>(texto);


            nomeCidade = endereco.Localidade;
            cbEstado.SelectedItem = endereco.Uf;

            txtLogradouro.Text = endereco.Logradouro;
            txtComplemento.Text = endereco.Complemento;
            mtbNumero.Focus();


        }

        private void cbEstado_SelectedValueChanged(object sender, EventArgs e)
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
            comando.CommandText = "SELECT id_estado FROM estados WHERE uf = @ESTADO";
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

            comando.Parameters.AddWithValue("@FKESTADO", id);


            try
            {
                tabela.Load(comando.ExecuteReader());

            }
            catch (Exception )
            {

            }



            cbCidade.Items.Clear();
            cbCidade.Visible = true;

            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                linha = tabela.Rows[i];
                cbCidade.Items.Add(linha["nome_cidade"]);
            }

            if (nomeCidade != "")
            {
                cbCidade.SelectedItem = nomeCidade;
                nomeCidade = "";
            }
        }
    }

}