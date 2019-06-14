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


        bool verifica = false;
        decimal precoFinal = 0;
        int idAtualizar = 0;

        

        private void AtualizaTabela()
        {
           
            SqlConnection conexao = new SqlConnection();
            TelaCaminhoConexao form = new TelaCaminhoConexao();
            conexao.ConnectionString = $@"{form.caminho}";
            try
            {
                conexao.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao conectar no banco, não será possivel ver os dados cadastrados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;

            comando.CommandText = "SELECT * FROM peixes";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            conexao.Close();

            dataGridView1.Rows.Clear();

            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow linha = tabela.Rows[i];
                Peixe peixe = new Peixe();
                peixe.Id = Convert.ToInt32(linha["id"]);
                peixe.Nome = linha["nome"].ToString();
                peixe.Raca = linha["raca"].ToString();
                peixe.Preco = Convert.ToDecimal(linha["preco"]);
                peixe.Quantidade = Convert.ToInt32(linha["quantidade"]);
                dataGridView1.Rows.Add(new string[] { peixe.Id.ToString(), peixe.Nome, peixe.Raca, peixe.Preco.ToString(), peixe.Quantidade.ToString() });
            }


        }


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
                MessageBox.Show("Digite o nome!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }

            try
            {
                string opcao = cbRaca.SelectedItem.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Selecione a raça!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbRaca.Focus();
                return;

            }

            if (mtbPreco.Text == "R$    .")
            {
                MessageBox.Show("Digite o preço!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbPreco.Focus();
                return;
            }

            if (nudQuantidade.Value == 0)
            {
                DialogResult result = MessageBox.Show("Quantidade igual a 0, deseja continuar?", "Aviso!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

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
            AtualizaTabela();
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
                TelaCaminhoConexao form = new TelaCaminhoConexao();
                conexao.ConnectionString = $@"{form.caminho}";
                try
                {
                    conexao.Open();

                }
                catch (Exception)
                {
                    MessageBox.Show("Erro ao conectar ao banco de dados", "Erro Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    MessageBox.Show("Adicionado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpaCampos();
                    AtualizaTabela();
                }
                catch (Exception)
                {
                    MessageBox.Show("Erro ao adicionar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    conexao.Close();
                    return;
                }

            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            SqlConnection conexao = new SqlConnection();
            TelaCaminhoConexao form = new TelaCaminhoConexao();
            conexao.ConnectionString = $@"{form.caminho}";

            try
            {
                conexao.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possivel conectar ao banco de dados!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;

            comando.CommandText = "SELECT * FROM peixes WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", dataGridView1.CurrentRow.Cells[0].Value);

            DataTable tabela = new DataTable();

            tabela.Load(comando.ExecuteReader());
            Peixe peixe = new Peixe();


            DataRow linha = tabela.Rows[0];
            peixe.Id = Convert.ToInt32(linha["id"]);
            peixe.Nome = linha["nome"].ToString();
            peixe.Raca = linha["raca"].ToString();
            peixe.Preco = Convert.ToDecimal(linha["preco"]);
            peixe.Quantidade = Convert.ToInt32(linha["quantidade"]);

            idAtualizar = peixe.Id;
            txtNome.Text = peixe.Nome;
            cbRaca.Text = peixe.Raca;
            mtbPreco.Text = peixe.Preco.ToString();
            nudQuantidade.Value = peixe.Quantidade;

            btnSalvar.Visible = false;
            btnAlterar.Visible = true;
            btnExcluir.Visible = false;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Realmente deseja apagar?","Aviso",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {



                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Você deve inserir um peixe primeiro", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {

                    SqlConnection conexao = new SqlConnection();
                    TelaCaminhoConexao form = new TelaCaminhoConexao();
                    conexao.ConnectionString = $@"{form.caminho}";
                    try
                    {
                        conexao.Open();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Erro ao conectar ao banco de dados", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    SqlCommand comando = new SqlCommand();
                    comando.Connection = conexao;

                    comando.CommandText = "DELETE FROM peixes WHERE id = @ID";
                    comando.Parameters.AddWithValue("@ID", dataGridView1.CurrentRow.Cells[0].Value);
                    try
                    {
                        MessageBox.Show("Excluido com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        comando.ExecuteNonQuery();
                        conexao.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Erro ao deletar", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        conexao.Close();
                        return;
                    }

                    AtualizaTabela();
                }
            }

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Deseja alterar?", "Alterar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {


                SqlConnection conexao = new SqlConnection();
                TelaCaminhoConexao form = new TelaCaminhoConexao();
                conexao.ConnectionString = $@"{form.caminho}";

                try
                {
                    conexao.Open();
                }
                catch (Exception)
                {
                    MessageBox.Show("Erro ao conectar ao banco de dados", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SqlCommand comando = new SqlCommand();
                comando.Connection = conexao;
                VerificaCampos();
                if (verifica == true)
                {
                    verifica = false;
                    comando.CommandText = "UPDATE peixes SET nome = @NOME, raca = @RACA, preco = @PRECO, quantidade = @QUANTIDADE WHERE id = @ID";

                    Peixe peixe = new Peixe();
                    peixe.Id = idAtualizar;
                    peixe.Nome = txtNome.Text;
                    peixe.Raca = cbRaca.SelectedItem.ToString();

                    RetiraMascaraPreco();
                    peixe.Preco = precoFinal;
                    precoFinal = 0;

                    peixe.Quantidade = Convert.ToInt32(nudQuantidade.Value);

                    comando.Parameters.AddWithValue("@ID", peixe.Id);
                    comando.Parameters.AddWithValue("@NOME", peixe.Nome);
                    comando.Parameters.AddWithValue("@RACA", peixe.Raca);
                    comando.Parameters.AddWithValue("@PRECO", peixe.Preco);
                    comando.Parameters.AddWithValue("@QUANTIDADE", peixe.Quantidade);

                    try
                    {
                        comando.ExecuteNonQuery();
                        // MessageBox.Show("Atualizado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        idAtualizar = 0;
                        AtualizaTabela();
                        LimpaCampos();
                        conexao.Close();
                        btnAlterar.Visible = false;
                        btnSalvar.Visible = true;
                        btnExcluir.Visible = true;
                    }
                    catch (Exception erro)
                    {
                        MessageBox.Show(erro.ToString());
                        MessageBox.Show("Erro ao atualizar", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        AtualizaTabela();
                        LimpaCampos();
                        conexao.Close();
                        btnAlterar.Visible = false;
                        btnSalvar.Visible = true;
                        btnExcluir.Visible = true;
                        return;
                    }
                }
            }
            else
            {
                idAtualizar = 0;
                AtualizaTabela();
                LimpaCampos();
                btnAlterar.Visible = false;
                btnSalvar.Visible = true;
                btnExcluir.Visible = true;
            }



        }

        private void PesquisarTabela()
        {
            if (txtPesquisa.Text == "")
            {
                AtualizaTabela();
            }
            else
            {
                SqlConnection conexao = new SqlConnection();
                TelaCaminhoConexao form = new TelaCaminhoConexao();
                conexao.ConnectionString = $@"{form.caminho}";

                try
                {
                    conexao.Open();
                }
                catch (Exception)
                {
                    MessageBox.Show("Não foi possivel conectar ao banco de dados", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SqlCommand comando = new SqlCommand();
                comando.Connection = conexao;

                comando.CommandText = "SELECT * FROM peixes";
                DataTable tabela = new DataTable();
                tabela.Load(comando.ExecuteReader());

                conexao.Close();

                dataGridView1.Rows.Clear();

                for (int i = 0; i < tabela.Rows.Count; i++)
                {
                    DataRow linha = tabela.Rows[i];
                    string pesquisa = txtPesquisa.Text;
                    if ((linha["id"].ToString() == pesquisa) || (linha["nome"].ToString().Contains(pesquisa) == true) || (linha["raca"].ToString() == pesquisa)
                        || (linha["preco"].ToString() == pesquisa) || (linha["quantidade"].ToString() == pesquisa))
                    {


                        Peixe peixe = new Peixe();
                        peixe.Id = Convert.ToInt32(linha["id"]);
                        peixe.Nome = linha["nome"].ToString();
                        peixe.Raca = linha["raca"].ToString();
                        peixe.Preco = Convert.ToDecimal(linha["preco"]);
                        peixe.Quantidade = Convert.ToInt32(linha["quantidade"]);
                        dataGridView1.Rows.Add(new string[] { peixe.Id.ToString(), peixe.Nome, peixe.Raca, peixe.Preco.ToString(), peixe.Quantidade.ToString() });
                    }
                }

            }
        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {


        }

        private void txtPesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PesquisarTabela();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
