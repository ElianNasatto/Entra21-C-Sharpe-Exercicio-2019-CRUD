drop table peixes;
CREATE TABLE peixes (
id INT PRIMARY KEY IDENTITY(1,1),
nome VARCHAR(40),
raca VARCHAR(40),
preco DECIMAL(9,2),
quantidade INT
);

drop table colaboradores;
CREATE TABLE colaboradores (
id INT PRIMARY KEY IDENTITY(1,1),
nome VARCHAR(40),
cpf VARCHAR(11),
salario DECIMAL(9,2),
sexo varchar(9),
cargo VARCHAR(40),
programador BIT
);
select * from colaboradores;
DELETE FROM colaboradores WHERE id = 1;


drop table clientes;
CREATE TABLE clientes (
id INT PRIMARY KEY IDENTITY(1,1),
nome        VARCHAR(80),
saldo       DECIMAL(9,2),
telefone    VARCHAR(15),
estado     VARCHAR(20),
cidade      VARCHAR(200),
cep         VARCHAR(9),
logradouro  VARCHAR(40),
numero      INT,
complemento VARCHAR(80),
nome_sujo   BIT,
altura      DECIMAL(4,2),
peso        DECIMAL(6,2)
);


