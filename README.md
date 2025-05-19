# 🛵 CP2 - API de Usuários para Aluguel de Motos Mottu

Este projeto foi desenvolvido como parte da avaliação CP2 da disciplina de Desenvolvimento Web com ASP.NET Core. O objetivo é construir uma **API RESTful para gerenciamento de usuários da empresa Mottu**, permitindo o cadastro, consulta, atualização e exclusão de perfis. Esses usuários irão utilizar o sistema para **realizar o aluguel de motos**.

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

> ⚠️ Cada usuário cadastrado representa um cliente da Mottu que poderá realizar locações de motos pelo sistema.

---

## 📦 Endpoints Disponíveis

### 📌 Usuários (CRUD)

| Método | Endpoint                                 | Descrição                          |
|--------|------------------------------------------|-------------------------------------|
| GET    | `/usuarios`                              | Lista todos os usuários cadastrados |
| GET    | `/usuarios/{id}`                         | Retorna um usuário específico       |
| GET    | `/usuarios/buscar?email={email}`         | Busca um usuário pelo e-mail        |
| POST   | `/usuarios`                              | Cadastra um novo usuário            |
| PUT    | `/usuarios/{id}`                         | Atualiza os dados de um usuário     |
| DELETE | `/usuarios/{id}`                         | Remove um usuário                   |

---

## 🔸 Exemplo de Requisição (POST /usuarios)

```json
{
  "nome": "João Vitor",
  "email": "joao@mottu.com",
  "senha": "senhaSegura123"
}
