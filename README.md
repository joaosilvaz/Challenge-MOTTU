# 🛵 Challenge - API de Usuários para Aluguel de Motos Mottu

Este projeto foi desenvolvido como parte do Challenge da FIAP em parceria com a empresa **Mottu**, para a disciplina de **Desenvolvimento Web com ASP.NET Core**.  
O objetivo é construir uma **API RESTful** para gerenciamento de usuários, permitindo o cadastro, consulta, atualização e exclusão de perfis. Esses usuários utilizarão o sistema para realizar o aluguel de motos.

---

## 📚 Tecnologias Utilizadas

- ASP.NET Core Web API  
- Entity Framework Core  
- Banco de Dados Oracle  
- C#  
- Visual Studio  
- Swagger (para documentação e testes)  
- Postman (para testes de API)

---

## 🎯 Funcionalidades

### 👤 Usuários Mottu

- ✅ Cadastro de novos usuários  
- ✅ Listagem de todos os usuários  
- ✅ Consulta de usuário por ID  
- ✅ Consulta de usuário por e-mail  
- ✅ Atualização de dados cadastrais  
- ✅ Exclusão de usuários  

---

## 🔗 Rotas da API

### 📌 Usuários (CRUD)

| Método | Endpoint                           | Descrição                        |
|--------|------------------------------------|----------------------------------|
| GET    | `/usuarios`                        | Lista todos os usuários          |
| GET    | `/usuarios/{id}`                   | Retorna um usuário específico    |
| GET    | `/usuarios/buscar?email={email}`   | Busca um usuário pelo e-mail     |
| POST   | `/usuarios`                        | Cadastra um novo usuário         |
| PUT    | `/usuarios/{id}`                   | Atualiza os dados de um usuário  |
| DELETE | `/usuarios/{id}`                   | Remove um usuário                |

---

## 📥 Exemplo de Requisição

### 🔸 POST `/usuarios`

### 🔸 Exemplo de Requisição (POST /usuarios)

```json {
  "nome": "João Vitor",
  "email": "joao@mottu.com",
  "senha": "senhaSegura123"
````

🔸 Exemplo de Resposta (201 Created)
```json {
  "id": 1,
  "nome": "João Vitor",
  "email": "joao@mottu.com"
````

## 📦 Códigos de Resposta HTTP
Código	Descrição
- 200	OK (requisição bem-sucedida)
- 201	Created (recurso criado)
- 204	No Content (sem conteúdo)
- 400	Bad Request (erro na requisição)
- 404	Not Found (recurso não encontrado)

## 🚀 Instalação e Execução
Clone o repositório:
git clone https://github.com/seu-usuario/seu-projeto.git
Abra o projeto no Visual Studio.

Configure a string de conexão com o banco de dados no arquivo appsettings.json.

Execute a aplicação (pressionando F5) ou via terminal:

dotnet run
Acesse a documentação Swagger para testar os endpoints:

http://localhost:{porta}/swagger

## 👨‍💻 Autor
João Vitor — Desenvolvedor Full Stack
FIAP | MRM McCann | Challenge Mottu
