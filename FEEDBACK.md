# Feedback - Avaliação Geral

## Front End
### Navegação
  * Pontos positivos:
    - Possui views e rotas definidas no projeto Kruger.Marketplace.MVC
    - Implementação com Razor Pages/Views

### Design
    - Será avaliado na entrega final

### Funcionalidade
  * Pontos positivos:
    - Implementação de CRUD para Categorias e Produtos
    - Uso de Bootstrap para estilização básica

## Back End
### Arquitetura
  * Pontos positivos:
    - Estrutura em camadas bem definida na pasta src

  * Pontos negativos:
    - Arquitetura excessivamente complexa com 5 camadas:
      * Kruger.Marketplace.Api
      * Kruger.Marketplace.MVC
      * Kruger.Marketplace.CrossCutting
      * Kruger.Marketplace.Business
      * Kruger.Marketplace.Data
    - Uso de padrões avançados como Unit of Work, versionamento de APIs
    - Uso de Startup.cs sendo que é um projeto .NET 6+
    - Recomendação: Deixar o arsenal técnico para desafios que exigem complexidade, entenda que fazer o certo é atender ao problema de negócio com a complexidade necessária.

### Funcionalidade
  * Pontos positivos:
    - Suporte a múltiplos bancos (SQL Server/SQLite)
    - Implementação do ASP.NET Identity

### Modelagem
  * Pontos positivos:
    - Uso do Entity Framework Core
    - Script SQL disponível para SQL Server

  * Pontos negativos:
    - Modelagem complexa

## Projeto
### Organização
  * Pontos positivos:
    - Estrutura organizada com pasta src na raiz
    - Pasta docs com coleção Postman
    - Pasta sql com scripts
    - Arquivo solution (Kruger.Marketplace.sln) na raiz
    - .gitignore adequado

  * Pontos negativos:
    - Nenhum identificado

### Documentação
  * Pontos positivos:
    - README.md extremamente detalhado com:
      * Apresentação do projeto
      * Tecnologias utilizadas
      * Estrutura do projeto
      * Instruções de execução
      * TODO list
    - Documentação da API via Swagger
    - Arquivo FEEDBACK.md presente

### Instalação
  * Pontos positivos:
    - Suporte a múltiplos ambientes (Development/Production)
    - SQLite para ambiente de desenvolvimento
    - SQL Server para outros ambientes
    - Instruções detalhadas de instalação

  * Pontos negativos:
    - Seed de dados automático nao encontrado