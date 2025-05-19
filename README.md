🛵 CP2 - API de Usuários para Aluguel de Motos Mottu
Este projeto foi desenvolvido como parte do Challenge da disciplina de Desenvolvimento Web com ASP.NET Core. O objetivo é construir uma API RESTful para gerenciamento de usuários da empresa Mottu, permitindo o cadastro, consulta, atualização e exclusão de perfis. Esses usuários irão utilizar o sistema para realizar o aluguel de motos.

📚 Tecnologias Utilizadas
ASP.NET Core Web API

Entity Framework Core

Banco de Dados Oracle

C#

Visual Studio

Swagger (para documentação e testes)

Postman (para testes de API)

🎯 Funcionalidades
👤 Usuários Mottu
✅ Cadastro de novos usuários

✅ Listagem de todos os usuários

✅ Consulta de usuário por ID

✅ Consulta de usuário por e-mail

✅ Atualização de dados cadastrais

✅ Exclusão de usuários

⚠️ Cada usuário cadastrado representa um cliente da Mottu que poderá realizar locações de motos pelo sistema.

📦 Endpoints Disponíveis
📌 Usuários (CRUD)
Método	Endpoint	Descrição
GET	/usuarios	Lista todos os usuários cadastrados
GET	/usuarios/{id}	Retorna um usuário específico
GET	/usuarios/buscar?email={email}	Busca um usuário pelo e-mail
POST	/usuarios	Cadastra um novo usuário
PUT	/usuarios/{id}	Atualiza os dados de um usuário
DELETE	/usuarios/{id}	Remove um usuário

🔸 Exemplo de Requisição (POST /usuarios)
json
Copiar
Editar
{
  "nome": "João Vitor",
  "email": "joao@mottu.com",
  "senha": "senhaSegura123"
}
🔸 Exemplo de Resposta (GET /usuarios/1)
json
Copiar
Editar
{
  "id": 1,
  "nome": "João Vitor",
  "email": "joao@mottu.com",
  "senha": "senhaSegura123"
}
🛠️ Como Executar o Projeto
Clone o repositório:

bash
Copiar
Editar
git clone https://github.com/seu-usuario/seu-repositorio.git
Configure a string de conexão com Oracle no appsettings.json:

json
Copiar
Editar
"ConnectionStrings": {
  "DefaultConnection": "User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=SEU_SERVIDOR"
}
Aplique as migrações e atualize o banco:

bash
Copiar
Editar
dotnet ef migrations add InitialCreate
dotnet ef database update
Execute o projeto:

bash
Copiar
Editar
dotnet run
Acesse a documentação Swagger:

bash
Copiar
Editar
https://localhost:{porta}/swagger
