
[Kruger.Marketplace] - Marketplace Simples com MVC e API RESTful

---

# :rocket: 1. Apresentação:

Bem-vindo ao repositório do projeto [Kruger.Marketplace]. Este projeto é uma entrega do MBA DevXpert Full Stack .NET e é referente ao módulo Introdução ao Desenvolvimento ASP.NET Core. O objetivo principal desenvolver uma aplicação de marketplace que permite aos usuários criar, editar, visualizar e excluir categorias e produtos, tanto através de uma interface web utilizando MVC quanto através de uma API RESTful. Os usuários não autenticados podem consultar produtos cadastrados, mas para operações de escrita, é necessário autenticação.

Autor
Cristian Kruger


# :gear: 2. Proposta do Projeto:

## O projeto consiste em:

**:heavy_check_mark: Aplicação MVC:** Interface web para interação com o marketplace com uso do (.Net8).<br>

**:heavy_check_mark: API RESTful:** Exposição dos recursos do marketplace para integração com outras aplicações ou desenvolvimento de front-ends alternativos (.Net8).<br>

**:heavy_check_mark: Autenticação e Autorização:** Implementação do ASP.NET Identity para diferenciar usuários autenticados e não autenticados.<br>

**:heavy_check_mark: Camada Core:** Camada para reaproveitamento de código que seria duplicado tanto pelo MVC quanto pela API (ViewModel, Injeção de dependência e outras Configurações). Aplicação de regras de negócio utilizando o FluentValidation e boas práticas de desenvolvimento (SOLID). Implementação de acesso ao banco de dados através do ORM Entity Framework Core.<br>

---
# :gear: 3. Tecnologias Utilizadas:

- Linguagem de Programação: C#
- Frameworks e pacotes:
  - ASP.NET Core MVC
  - ASP.NET Core Web API
  - Entity Framework Core
  - FluentValidation
  - Automapper
  - Padrão Repository
- Banco de Dados: SQL Server/SQLite
- Autenticação e Autorização:
  - ASP.NET Core Identity
  - JWT (JSON Web Token) para autenticação na API
- Front-end:
  - Razor Pages/Views
  - HTML/CSS/Bootstrap para estilização básica
- Documentação da API: Swagger
- Coleção Postman para testes da API

# :gear: 4. Estrutura do Projeto:
##  A estrutura do projeto é organizada da seguinte forma:
```
|-- docs/
|   |-- postman/                 → Coleção postman com requisições para API
|-- sql/ 
|   |-- script_marketplace.sql   → Script idempotente do banco de dados (exclusivo SQL Server)
|-- src/
|   |-- Kruger.Marketplace.Api/  → API RESTful.
|   |-- Kruger.Marketplace.MVC/  → Projeto MVC.
|   |-- Kruger.Marketplace.Core/
|       |-- Application/         → Configuração de ViewModels consumidas pela API e MVC e Configurações communs aos projetos MVC e API.
|   |-- Kruger.Marketplace.Core/
|       |-- Business/            → Mapeamento das entidades, Aplicação de validações e regras de negócio seguindo as boas práticas do SOLID.
|   |-- Kruger.Marketplace.Core/
|       |-- Data/                → Mapeamento de Modelos de Dados, Configuração do EF Core e Seed do banco de dados (/Seed/SeedDatabase.cs).
|-- .gitignore                   → Confguração de quais arquivos o Git não deve versionar.
|-- FEEDBACK.md                  → Arquivo para Consolidação dos Feedbacks
|-- Kruger.Marketplace.sln       → solution do projeto
|-- README.md                    → Arquivo de Documentação/Wiki do Projeto
```

# :gear: 5. Funcionalidades Implementadas:

- **:heavy_check_mark: CRUD para Categorias e Produtos:** Permite criar, editar, visualizar e excluir Categorias e Produtos.
- **:heavy_check_mark: Autenticação:** Diferenciação entre usuários autenticados e não autenticados.
- **:heavy_check_mark: API RESTful:** Exposição de endpoints para operações CRUD via API.
- **:heavy_check_mark: Documentação da API:** Documentação automática dos endpoints da API utilizando Swagger.

# :gear: 6. Como Executar o Projeto:

##  Pré-requisitos:

- .NET SDK 8.0 ou superior
- SQL Server (ambiente não development)
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git

##  Passos para Execução
#### Clone o Repositório:
```
git clone https://github.com/cristiankruger/Kruger.Marketplace.git
cd Kruger.Marketplace
``` 

####  Configuração do Banco de Dados:
  
No arquivo `appsettings.json`, configure a string de conexão do SQL Server (caso deseje executar em modo não "development"). Para execução em modo "Development" (debug), basta executar o projeto (irá subir uma instancia do `SQLite`).

Execute o projeto para que a configuração do Seed crie o banco e popule com os dados básicos.

**:warning: As Migrations são aplicadas de forma automática através do método de extensão `MigrateDatabase() => src/Kruger.Marketplace.Core/Application/Configurations/DatabaseConfig.cs`;**<br>
**:warning: Uma carga inicial é feita na base de dados através do método `OnModelCreating() => src/Kruger.Marketplace.Core/Data/Context/AppDbContext.cs`, com base no método `Seed(modelBuilder) => src/Kruger.Marketplace.Core/Data/Seed/SeedDatabase.cs`;**<br>
**:warning: Credenciais default do banco: usuário &rarr; `teste@teste.com` | senha &rarr; `@Aa12345`**<br>

####  Executar a Aplicação MVC:

```
cd src/Kruger.Marketplace.MVC/
dotnet run --environment=Development
```
**Abra o browser e acesse a aplicação em: http://localhost:5288**

####  Executar a API:
```
cd src/Kruger.Marketplace.Api/
dotnet run --environment=Development
```
**Abra o browser e acesse a documentação da API em: http://localhost:5187/swagger**
  
# :gear: 7. Instruções de Configuração

**JWT para API:** As chaves de configuração do JWT estão no appsettings.json.

**Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core (Não é necessário aplicar o comando update-database devido a configuração do projeto)

# :gear: 8. Documentação da API

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em através do link [http://localhost:5187/swagger](http://localhost:5187/swagger)

**:warning: Obs.: Em ambientes não `development`, é necessário informar usuario e senha para expor a página do swagger, devido à implementação do securityMiddleware. Por default, essas credenciais são `admin` e `123` e podem ser alteradas através do nó `AppCredentials` no `appsettings.[ambiente].json`**

# :gear: 9. Avaliação

Este projeto é parte de um curso acadêmico e não aceita contribuições externas.

Para feedbacks ou dúvidas utilize o recurso de Issues

O arquivo FEEDBACK.md é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.

# :gear: 10. TODO:

- Upload de imagens para um serviço externo (AWS S3 ou Azure Blob Storage)
