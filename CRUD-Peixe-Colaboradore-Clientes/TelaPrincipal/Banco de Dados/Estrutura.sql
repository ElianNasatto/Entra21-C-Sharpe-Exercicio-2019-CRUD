drop table peixes;
CREATE TABLE peixes (
id INT PRIMARY KEY IDENTITY(1,1),
nome VARCHAR(40),
raca VARCHAR(40),
preco DECIMAL(9,2),
quantidade INT
);

CREATE TABLE colaboradores (
id INT PRIMARY KEY IDENTITY(1,1),
nome VARCHAR(40),
cpf VARCHAR(11),
salario DECIMAL(6,2),
sexo CHAR,
cargo VARCHAR(20),
programador BIT
);

CREATE TABLE clientes (
id INT PRIMARY KEY IDENTITY(1,1),
nome VARCHAR(40),
saldo DECIMAL(6,2),
telefone VARCHAR(9),
estaddo VARCHAR(2),
cidade VARCHAR(40),
cep VARCHAR(8),
logradouro VARCHAR(40),
numero INT,
complemento VARCHAR(80),
nome_sujo BIT,
altura DECIMAL(3,1),
peso DECIMAL(3,1)
);