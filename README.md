
[Kruger.Marketplace] - Marketplace Simples com MVC e API RESTful

---

# 1. Apresentação:

Bem-vindo ao repositório do projeto [Kruger.Marketplace]. Este projeto é uma entrega do MBA DevXpert Full Stack .NET e é referente ao módulo Introdução ao Desenvolvimento ASP.NET Core. O objetivo principal desenvolver uma aplicação de marketplace que permite aos usuários criar, editar, visualizar e excluir categorias e produtos, tanto através de uma interface web utilizando MVC quanto através de uma API RESTful. Os usuários não autenticados podem consultar produtos cadastrados, mas para operações de escrita, é necessário autenticação.

Autor
Cristian Kruger


# 2. Proposta do Projeto:

## O projeto consiste em:

- **Aplicação MVC:** Interface web para interação com o marketplace com uso do (.Net8).

- **API RESTful:** Exposição dos recursos do marketplace para integração com outras aplicações ou desenvolvimento de front-ends alternativos (.Net8).

- **Autenticação e Autorização:** Implementação do ASP.NET Identity para diferenciar usuários autenticados e não autenticados.

- **Camada CrossCutting:** Camada para reaproveitamento de código que seria duplicado tanto pelo MVC quanto pela API (ViewModel, Injeção de dependência e outras Configurações).

- **Camada de Negócios:** Aplicação de regras de negócio utilizando o FluentValidation e boas práticas de desenvolvimento (SOLID).

- **Acesso a Dados:** Implementação de acesso ao banco de dados através do ORM Entity Framework Core.

---
# 3. Tecnologias Utilizadas:

- Linguagem de Programação: C#
- Frameworks e pacotes:
  - ASP.NET Core MVC
  - ASP.NET Core Web API
  - Entity Framework Core
  - FluentValidation
  - Automapper
  - Padrão Repository
  - Padrão Unit Of Work
- Banco de Dados: SQL Server/SQLite
- Autenticação e Autorização:
  - ASP.NET Core Identity
  - JWT (JSON Web Token) para autenticação na API
- Front-end:
  - Razor Pages/Views
  - HTML/CSS/Bootstrap para estilização básica
- Documentação da API: Swagger
- Coleção Postman para testes da API  

# 4. Estrutura do Projeto:
## A estrutura do projeto é organizada da seguinte forma:

+-- docs/
|   +-- postman/ [](→ Coleção postman com requisições para API)
+-- sql/ 
|   +-- script_marketplace.sql [](→ Script idempotente do banco de dados (exclusivo SQL Server))
+-- src/
|   +-- Kruger.Marketplace.Api/ [](→ API RESTful.)
|   +-- Kruger.Marketplace.MVC/ [](→ Projeto MVC.)
|   +-- Kruger.Marketplace.CrossCutting/ [](→ Projeto para configuração de ViewModels consumidas pela API e MVC e Configurações communs aos projetos MVC e API.)
|   +-- Kruger.Marketplace.Business/ [](→ Mapeamento das entidades, Aplicação de validações e regras de negócio seguindo as boas práticas do SOLID.)
|   +-- Kruger.Marketplace.Data/ [](→ Mapeamento de Modelos de Dados, e Configuração do EF Core.)
+-- .gitignore [](→ Confguração de quais arquivos o Git não deve versionar.)
+-- FEEDBACK.md [](→ Arquivo para Consolidação dos Feedbacks)
+-- Kruger.Marketplace.sln [](→ solution do projeto)
+-- README.md [](→ Arquivo de Documentação/Wiki do Projeto)


# 5. Funcionalidades Implementadas:

- **CRUD para Categorias e Produtos:** Permite criar, editar, visualizar e excluir Categorias e Produtos.
- **Autenticação:** Diferenciação entre usuários autenticados e não autenticados.
- **API RESTful:** Exposição de endpoints para operações CRUD via API.
- **Documentação da API:** Documentação automática dos endpoints da API utilizando Swagger.

# 6. Como Executar o Projeto:

## Pré-requisitos:

- .NET SDK 8.0 ou superior
- SQL Server (ambiente não development)
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git

## Passos para Execução
#### Clone o Repositório:
```
git clone https://github.com/cristiankruger/Kruger.Marketplace.git
cd Kruger.Marketplace
``` 

#### Configuração do Banco de Dados:
  
No arquivo `appsettings.json`, configure a string de conexão do SQL Server (caso deseje executar em modo não "development"). Para execução em modo "Development" (debug), basta executar o projeto (irá subir uma instancia do `SQLite`).

Execute o projeto para que a configuração do Seed crie o banco e popule com os dados básicos.

#### Executar a Aplicação MVC:

```
cd src/Kruger.Marketplace.MVC/
dotnet run
```
**Abra o browser e acesse a aplicação em: http://localhost:5288**

#### Executar a API:
```
cd src/Kruger.Marketplace.Api/
dotnet run
```
**Abra o browser e acesse a documentação da API em: http://localhost:5187/swagger**
  
# 7. Instruções de Configuração

**JWT para API:** As chaves de configuração do JWT estão no appsettings.json.

**Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core (Não é necessário aplicar o comando update-database devido a configuração do projeto)

# 8. Documentação da API

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em através do link [swagger](http://localhost:5187/swagger)

# 9. Avaliação

Este projeto é parte de um curso acadêmico e não aceita contribuições externas.

Para feedbacks ou dúvidas utilize o recurso de Issues

O arquivo FEEDBACK.md é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.