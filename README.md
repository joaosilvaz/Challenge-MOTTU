# 🛵 Challenge - Sistema de Aluguel de Motos Mottu

Este projeto foi desenvolvido como parte do Challenge da FIAP em parceria com a empresa **Mottu**, para a disciplina de **Desenvolvimento Web com ASP.NET Core**.  
O objetivo é construir uma **API RESTful** para gerenciamento de usuários, pendings e bikes. Esses usuários utilizarão o sistema para realizar o aluguel de motos.

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

## 👥 Integrantes
- João Vitor da Silva Nascimento - RM554694 - TURMA 2TDSPZ
- Rafael Souza Bezerra - RM555357 - TURMA 2TDSPZ
- Guilherme Alves Pedroso - RM557888 - TURMA 2TDSPZ

---

## 🏍️ Domínio Escolhido: Sistema de Gerenciamento de Frota de Motos

A escolha do domínio de gerenciamento de frota de motos se justifica pela complexidade adequada para demonstrar relacionamentos entre entidades, integrações com banco de dados e regras de negócio específicas do setor de mobilidade.

### 📌 Entidades Principais

-Usuário – Representa os clientes que alugam as motos no sistema.

-Moto – Veículos da frota, identificados por placa e chassi, com controle de disponibilidade.

-Pendência (Aluguel) – Representa o contrato de aluguel entre usuário e moto, com status (Pendente, Aprovado, Concluido, Cancelado), data de início e fim obrigatórias.

### 🏗️ Arquitetura Técnica

-ASP.NET Core Web API – Framework robusto, multiplataforma e de alta performance.

-Entity Framework Core – ORM moderno com suporte completo ao Oracle, abstraindo queries SQL.

-Oracle Database – Banco empresarial com alta confiabilidade e suporte a grandes volumes de dados.

-DTOs (Data Transfer Objects) – Separação clara entre modelos de domínio e contratos de API.

-Swagger/OpenAPI – Documentação automática e interativa para desenvolvedores.

-HATEOAS – Inclusão de links de navegação nos responses para enriquecer a experiência da API.

-Paginação – Implementada para performance e escalabilidade em grandes volumes de registros.

---

## 🎯 Funcionalidades

### 👤 Usuários Mottu

- ✅ Cadastro de novos usuários  
- ✅ Listagem de todos os usuários  
- ✅ Consulta de usuário por ID  
- ✅ Consulta de usuário por e-mail  
- ✅ Atualização de dados cadastrais  
- ✅ Exclusão de usuários

### 👤 Bikes Mottu

- ✅ Cadastro de novas motos 
- ✅ Listagem de todos as motos  
- ✅ Consulta de moto por ID  
- ✅ Consulta de moto por e-mail  
- ✅ Atualização de dados cadastrais  
- ✅ Exclusão de motos
  
### 👤 Pendings Mottu

- ✅ Cadastro de novos aluguéis 
- ✅ Listagem de todos os aluguéis  
- ✅ Consulta de aluguel por ID  
- ✅ Consulta de aluguel por e-mail  
- ✅ Atualização de dados cadastrais  
- ✅ Exclusão de aluguel  
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


### 📌 Bikes (CRUD)

| Método | Endpoint                           | Descrição                        |
|--------|------------------------------------|----------------------------------|
| GET    | `/bikes`                        | Lista todos as motos          |
| GET    | `/bikes/{id}`                   | Retorna uma moto específico    |
| POST   | `/bikes`                        | Cadastra uma moto         |
| PUT    | `/bikes/{id}/disponibilidade`   | Atualiza a disponibilidade da moto  |
| DELETE | `/bikes/{id}`                   | Remove uma moto                |


### 📌 Pendings (CRUD)

| Método | Endpoint                           | Descrição                        |
|--------|------------------------------------|----------------------------------|
| GET    | `/pendings`                        | Lista todos os aluguéis          |
| GET    | `/pendings/{id}`                   | Retorna um aluguel específico    |
| POST   | `/pendings`                        | Cadastra um novo aluguel         |
| PUT    | `/pendings/{id}`                   | Atualiza o data fim do aluguel   |
| DELETE | `/pendings/{id}`                   | Remove um aluguel                |
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
  "email": "joao@email.com",
  "links": {
    "self": "/usuarios/1",
    "update": "/usuarios/1",
    "delete": "/usuarios/1"
  }
````

### 🔸 POST `/bikes`

### 🔸 Exemplo de Requisição (POST /bikes)

```json {
   "modelo": "BMW G 310 R",
    "placa": "TUV9X65",
    "chassi": "WB10J12345L678901",
    "ano": 2023
````

🔸 Exemplo de Resposta (201 Created)
```json {
  "id": 1,
  "modelo": "BMW G 310 R",
  "placa": "TUV9X65",
  "chassi": "WB10J12345L678901",
  "ano": 2023
  "disponivel": true
````

### 🔸 POST `/pendings`

### 🔸 Exemplo de Requisição (POST /pendings)

```json {
  "usuarioId": 2,
  "bikeId": 2,
  "dataInicio": "2025-09-30T20:00:00.073Z",
  "dataFim": "2025-10-30T20:00:00.073Z"
````

🔸 Exemplo de Resposta (201 Created)
```json {
  {
  "items": [
    {
      "id": 2,
      "status": "Concluido",
      "dataInicio": "2025-09-30T08:00:00+00:00",
      "dataFim": "2025-09-30T23:56:02.760225+00:00",
      "usuarioId": 2,
      "usuario": {
        "id": 2,
        "nome": "Maria Clara",
        "email": "maria@email.com",
        "links": {
          "self": "/usuarios/2",
          "update": "/usuarios/2",
          "delete": "/usuarios/2"
        }
      },
      "bikeId": 2,
      "bike": {
        "id": 2,
        "modelo": "Honda CG 160",
        "placa": "ABC1D23",
        "chassi": "9C2KC1234L5678901",
        "ano": 2021,
        "disponivel": false
      },
      "links": {
        "self": "/pendings/2",
        "finalizar": "/pendings/2/finalizar",
        "delete": "/pendings/2"
      }
    }
````

## 📦 Códigos de Resposta HTTP
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


## FIAP | Challenge Mottu
