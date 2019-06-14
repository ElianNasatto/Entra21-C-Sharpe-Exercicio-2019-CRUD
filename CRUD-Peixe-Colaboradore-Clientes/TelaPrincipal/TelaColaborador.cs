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
    public partial class TelaColaborador : Form
    {
        public TelaColaborador()
        {
            InitializeComponent();
        }

        bool verifica = false;

        private void VerificaCampos()
        {

            if (txtNome.Text == "")
            {
                MessageBox.Show("Digite um nome!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }

            if (mtbCPF.Text == "   .   .   -")
            {
                DialogResult result = MessageBox.Show("Voçê não digitou o CPF, deseja continuar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    mtbCPF.Focus();
                    return;
                }
            }
            try
            {
                if ((mtbSalario.Text == "R$        .") || (mtbSalario.Text == "R$00000.00"))
                {
                    MessageBox.Show("Você deve digitar o salario corretamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    mtbSalario.Focus();
                    return;
                }
                else
                {
                    string salario = mtbSalario.Text;
                    salario = salario.Replace("R$", "");
                    decimal salarioTeste = Convert.ToDecimal(salario);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Você deve digitar o salario!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbSalario.Focus();
                return;
            }


            if ((rbFeminino.Checked == false) && (rbMasculino.Checked == false))
            {
                MessageBox.Show("Você deve selecionar o sexo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string cargo = cbCargo.SelectedItem.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Você deve selecionar o cargo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbCargo.Focus();
                return;
            }




            verifica = true;

        }

        string cpfFinal = "";

        private void RetiraCPF()
        {
            string cpf = mtbCPF.Text;
            cpf = cpf.Replace(".", "");
            cpf = cpf.Replace("-", "");
            cpfFinal = cpf;
        }

        decimal salarioFinal = 0;

        private void RetiraSalario()
        {
            string salario = mtbSalario.Text;
            salario = salario.Replace("R$", "");
            salarioFinal = Convert.ToDecimal(salario);
        }

        private void LimpaCampos()
        {
            txtNome.Clear();
            mtbCPF.Clear();
            cpfFinal = "";
            mtbSalario.Clear();
            salarioFinal = 0;
            rbFeminino.Checked = false;
            rbMasculino.Checked = false;
            cbCargo.SelectedIndex = -1;
            chbProgramador.Checked = false;
            verifica = false;


        }

        private void AtualizaTabela()
        {
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
                return;
            }

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;

            comando.CommandText = "SELECT * FROM colaboradores";

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            conexao.Close();
            dataGridView1.Rows.Clear();

            for (int i = 0; i < tabela.Rows.Count; i++)
            {

                DataRow linha = tabela.Rows[i];
                Colaborador colaborador = new Colaborador();
                colaborador.Id = Convert.ToInt32(linha["id"]);
                colaborador.Nome = linha["nome"].ToString();
                colaborador.Cpf = linha["cpf"].ToString();
                colaborador.Salario = Convert.ToDecimal(linha["salario"]);
                if (linha["sexo"].ToString() == "Masculino")
                {
                    colaborador.Sexo = "Masculino";

                }
                else
                {
                    colaborador.Sexo = "Feminino";
                }
                colaborador.Cargo = linha["cargo"].ToString();
                if (Convert.ToBoolean(linha["programador"]) == true)
                {
                    colaborador.Programador = true;
                }
                else
                {
                    colaborador.Programador = false;
                }

                dataGridView1.Rows.Add(new string[] { colaborador.Id.ToString(), colaborador.Nome, colaborador.Cpf, colaborador.Salario.ToString(), colaborador.Sexo.ToString(), colaborador.Cargo, colaborador.Programador.ToString() });

            }



        }


        private void btnSalvar_Click(object sender, EventArgs e)
        {
            VerificaCampos();
            if (verifica == true)
            {
                verifica = false;

                SqlConnection conexao = new SqlConnection();
                TelaCaminhoConexao tela = new TelaCaminhoConexao();
                conexao.ConnectionString = tela.caminho;

                try
                {
                    conexao.Open();
                }
                catch (Exception)
                {
                    MessageBox.Show("Erro ao conectar no banco de dados", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SqlCommand comando = new SqlCommand();
                comando.Connection = conexao;

                comando.CommandText = @"INSERT INTO colaboradores (nome, cpf,salario,sexo,cargo,programador) 
                    values (@NOME,@CPF,@SALARIO,@SEXO,@CARGO,@PROGRAMADOR)";

                RetiraCPF();
                RetiraSalario();
                Colaborador colaborador = new Colaborador();
                colaborador.Nome = txtNome.Text;
                colaborador.Cpf = cpfFinal;
                colaborador.Salario = salarioFinal;
                if (rbMasculino.Checked == true)
                {
                    colaborador.Sexo = "Masculino";
                }
                else
                {
                    colaborador.Sexo = "Feminino";
                }

                colaborador.Cargo = cbCargo.SelectedItem.ToString();

                if (chbProgramador.Checked == true)
                {
                    colaborador.Programador = true;
                }
                else
                {
                    colaborador.Programador = false;

                }

                comando.Parameters.AddWithValue("@NOME", colaborador.Nome);
                comando.Parameters.AddWithValue("@CPF", colaborador.Cpf);
                comando.Parameters.AddWithValue("@SALARIO", colaborador.Salario);
                comando.Parameters.AddWithValue("@SEXO", colaborador.Sexo);
                comando.Parameters.AddWithValue("@CARGO", colaborador.Cargo);
                comando.Parameters.AddWithValue("@PROGRAMADOR", colaborador.Programador);
                try
                {
                    comando.ExecuteNonQuery();
                    conexao.Close();
                    MessageBox.Show("Adicionado com sucesso", "Adicionado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception erro)
                {
                    conexao.Close();
                    MessageBox.Show(erro.ToString());
                    MessageBox.Show("Não foi possivel adicionar", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                LimpaCampos();
                AtualizaTabela();
            }
        }

        int idAlterar = 0;

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
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
                return;
            }

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;

            comando.CommandText = "SELECT * FROM colaboradores WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", dataGridView1.CurrentRow.Cells[0].Value);
            DataTable tabela = new DataTable();
            try
            {
                tabela.Load(comando.ExecuteReader());

            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possivel recuper o registro selecionado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            conexao.Close();
            DataRow linha = tabela.Rows[0];

            Colaborador colaborador = new Colaborador();
            colaborador.Id = Convert.ToInt32(linha["id"]);
            colaborador.Nome = linha["nome"].ToString();
            colaborador.Cpf = linha["cpf"].ToString();
            colaborador.Salario = Convert.ToDecimal(linha["salario"]);
            colaborador.Sexo = linha["sexo"].ToString();
            colaborador.Cargo = linha["cargo"].ToString();
            colaborador.Programador = Convert.ToBoolean(linha["programador"]);

            idAlterar = colaborador.Id;
            txtNome.Text = colaborador.Nome;
            mtbCPF.Text = colaborador.Cpf;
            mtbSalario.Text = colaborador.Salario.ToString();
            if (colaborador.Sexo == "Masculino")
            {
                rbMasculino.Checked = true;
            }
            else
            {
                rbFeminino.Checked = true;
            }
            cbCargo.Text = colaborador.Cargo;
            if (colaborador.Programador == true)
            {
                chbProgramador.Checked = true;
            }
            else
            {
                chbProgramador.Checked = false;
            }

            btnSalvar.Visible = false;
            btnAlterar.Visible = true;
            btnExcluir.Visible = false;
            txtPesquisa.Visible = false;
            label6.Visible = false;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Deseja alterar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {



                VerificaCampos();
                if (verifica == true)
                {
                    verifica = false;

                    SqlConnection conexao = new SqlConnection();
                    TelaCaminhoConexao form = new TelaCaminhoConexao();
                    conexao.ConnectionString = form.caminho;
                    try
                    {
                        conexao.Open();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Não foi possivel conectar ao banco de dados, a operação foi cancelada", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    SqlCommand comando = new SqlCommand();
                    comando.Connection = conexao;

                    comando.CommandText = "UPDATE colaboradores SET nome = @NOME, Cpf = @CPF, Salario = @SALARIO,sexo = @SEXO,Cargo = @CARGO, Programador = @PROGRAMADOR WHERE id = @ID";
                    Colaborador colaborador = new Colaborador();

                    colaborador.Id = idAlterar;

                    colaborador.Nome = txtNome.Text;
                    RetiraCPF();
                    colaborador.Cpf = cpfFinal;
                    RetiraSalario();
                    colaborador.Salario = salarioFinal;
                    if (rbMasculino.Checked == true)
                    {
                        colaborador.Sexo = "Masculino";
                    }
                    else
                    {
                        colaborador.Sexo = "Feminino";
                    }

                    colaborador.Cargo = cbCargo.SelectedItem.ToString();

                    if (chbProgramador.Checked == true)
                    {
                        colaborador.Programador = true;
                    }
                    else
                    {
                        colaborador.Programador = false;

                    }

                    comando.Parameters.AddWithValue("@ID", colaborador.Id);
                    comando.Parameters.AddWithValue("@NOME", colaborador.Nome);
                    comando.Parameters.AddWithValue("@CPF", colaborador.Cpf);
                    comando.Parameters.AddWithValue("@SALARIO", colaborador.Salario);
                    comando.Parameters.AddWithValue("@SEXO", colaborador.Sexo);
                    comando.Parameters.AddWithValue("@CARGO", colaborador.Cargo);
                    comando.Parameters.AddWithValue("@PROGRAMADOR", colaborador.Programador);

                    try
                    {
                        comando.ExecuteNonQuery();
                        MessageBox.Show("Alterado com sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpaCampos();
                        btnAlterar.Visible = false;
                        btnSalvar.Visible = true;
                        btnExcluir.Visible = true;
                        conexao.Close();
                    }
                    catch (Exception erro)
                    {
                        MessageBox.Show(erro.ToString());
                        MessageBox.Show("Não foi possivel alterar, a operação foi cancelada", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LimpaCampos();
                        btnAlterar.Visible = false;
                        btnSalvar.Visible = true;
                        btnExcluir.Visible = true;
                        txtPesquisa.Visible = true;
                        label6.Visible = true;
                        conexao.Close();
                    }

                }
            }
            else
            {
                LimpaCampos();
                btnAlterar.Visible = false;
                btnSalvar.Visible = true;
                btnExcluir.Visible = true;
                txtPesquisa.Visible = true;
                label6.Visible = true;
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Deseja excluir?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Você deve adicionar um colaborador primeiro", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {


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
                        return;
                    }


                    SqlCommand comando = new SqlCommand();
                    comando.Connection = conexao;

                    comando.CommandText = "DELETE FROM colaboradores WHERE id = @ID";
                    comando.Parameters.AddWithValue("@ID", dataGridView1.CurrentRow.Cells[0].Value);

                    try
                    {
                        comando.ExecuteNonQuery();
                        conexao.Close();
                        MessageBox.Show("Removido com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception erro)
                    {
                        MessageBox.Show(erro.ToString());
                        conexao.Close();
                        MessageBox.Show("Não foi possivel excluir o registro", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    AtualizaTabela();
                }

            }
        }

        private void TelaColaborador_Load(object sender, EventArgs e)
        {
            AtualizaTabela();
        }

        private void txtPesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string pesquisa = txtPesquisa.Text.ToUpper();
                if (pesquisa == "")
                {
                    AtualizaTabela();
                }
                else
                {
                    SqlConnection conexao = new SqlConnection();
                    TelaCaminhoConexao form = new TelaCaminhoConexao();
                    conexao.ConnectionString = form.caminho;

                    try
                    {
                        conexao.Open();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Não foi possivel buscar pois o banco não esta conectado corretamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    SqlCommand comando = new SqlCommand();
                    comando.Connection = conexao;

                    comando.CommandText = "SELECT * FROM colaboradores";
                    DataTable tabela = new DataTable();
                    try
                    {
                        tabela.Load(comando.ExecuteReader());
                        conexao.Close();

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Não foi possivel buscar no banco", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        conexao.Close();
                    }
                    dataGridView1.Rows.Clear();

                    for (int i = 0; i < tabela.Rows.Count; i++)
                    {
                        DataRow linha = tabela.Rows[i];
                        if ((linha["id"].ToString().ToUpper() == pesquisa) || (linha["nome"].ToString().ToUpper() == pesquisa)
                            || (linha["cpf"].ToString() == pesquisa) || (linha["salario"].ToString() == pesquisa)
                            || (linha["sexo"].ToString().ToUpper() == pesquisa) || (linha["cargo"].ToString().ToUpper() == pesquisa))
                        {
                            Colaborador colaborador = new Colaborador();
                            colaborador.Id = Convert.ToInt32(linha["id"]);
                            colaborador.Nome = linha["nome"].ToString();
                            colaborador.Cpf = linha["cpf"].ToString();
                            colaborador.Salario = Convert.ToDecimal(linha["salario"]);
                            colaborador.Sexo = linha["sexo"].ToString();
                            colaborador.Cargo = linha["cargo"].ToString();
                            colaborador.Programador = Convert.ToBoolean(linha["programador"]);

                            dataGridView1.Rows.Add(new string[] { colaborador.Id.ToString(), colaborador.Nome, colaborador.Cpf, colaborador.Salario.ToString(), colaborador.Sexo.ToString(), colaborador.Cargo, colaborador.Programador.ToString() });


                        }

                    }
                }

            }
        }
    }
}

