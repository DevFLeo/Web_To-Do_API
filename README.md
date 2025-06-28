# Projeto To-Do List API com .NET 8

## Sobre o Projeto

Este é um projeto de portfólio que implementa uma aplicação **Full-Stack** de Lista de Tarefas (To-Do List). O objetivo foi construir um back-end robusto e profissional usando as melhores práticas do ecossistema .NET, e um front-end limpo e interativo usando apenas tecnologias web padrão.

A aplicação permite ao utilizador gerir uma lista de tarefas, com funcionalidades para adicionar, apagar, atualizar e marcar tarefas como concluídas, tudo através de uma interface web dinâmica que consome uma API RESTful.

## Arquitetura: A Escolha por uma Web API

Este projeto foi intencionalmente desenvolvido utilizando uma arquitetura desacoplada, com um back-end de API RESTful e um front-end independente, em vez de uma aplicação monolítica (como um projeto MVC tradicional). Os motivos para esta escolha são:

* **Separação de Responsabilidades (Separation of Concerns):** O back-end foca-se exclusivamente na lógica de negócio, regras de validação e acesso a dados. O front-end foca-se exclusivamente na apresentação e na experiência do utilizador. Isto torna o código de cada parte mais limpo, mais fácil de manter e de testar de forma isolada.

* **Flexibilidade e Reutilização:** Uma API bem definida é uma "fonte de dados" reutilizável. Este mesmo back-end poderia, no futuro, servir a múltiplos clientes com o mínimo de esforço, como uma aplicação móvel (Android/iOS), uma aplicação desktop, ou até mesmo outros serviços de back-end, sem alterar uma única linha do seu código.

* **Padrão de Mercado Moderno:** A arquitetura de API desacoplada é o padrão para a grande maioria das aplicações web modernas, especialmente para SPAs (Single Page Applications) construídas com frameworks como React, Angular ou Vue.js. Este projeto demonstra proficiência neste paradigma atual.

* **Escalabilidade Independente:** Permite que o back-end e o front-end sejam escalados de forma independente. Se a API receber uma carga muito grande de pedidos, podemos alocar mais recursos de servidor apenas para ela, sem impactar a performance do front-end, e vice-versa.

## Funcionalidades

### Back-end
* API RESTful completa com todas as operações **CRUD** (Create, Read, Update, Delete).
* Arquitetura em camadas com o **Repository Pattern** para isolar o acesso a dados.
* Uso de **DTOs (Data Transfer Objects)** e **AutoMapper** para garantir um contrato de API seguro e desacoplado da base de dados.
* Validação de dados de entrada com a biblioteca **FluentValidation**.
* Sistema de **logging** estruturado com **Serilog**.
* Base de dados **SQLite** gerida com o **Entity Framework Core** e o sistema de **Migrations**.

### Front-end
* Interface reativa que atualiza sem a necessidade de recarregar a página.
* Adição, remoção e atualização do estado de tarefas de forma dinâmica.
* **Tema Claro / Escuro (Dark Mode)** com persistência da escolha do utilizador no `localStorage`.
* Design responsivo utilizando **Bootstrap 5**.

## Tecnologias Utilizadas

### Back-end
* .NET 8
* ASP.NET Core Web API
* C#
* Entity Framework Core 8
* SQLite
* AutoMapper
* FluentValidation
* Serilog

### Front-end
* HTML5
* CSS3
* JavaScript (ES6+)
* Bootstrap 5

## Como Executar o Projeto

Para executar este projeto localmente, irá precisar de:
* .NET 8 SDK
* Git

```bash
# 1. Clona o repositório
# Substitua pela URL do seu repositório
git clone [https://github.com/seu-usuario/seu-repositorio.git](https://github.com/seu-usuario/seu-repositorio.git)

# 2. Navega para a pasta do projeto
cd TodoApiPortfolio

# 3. Restaura as dependências do .NET
dotnet restore

# 4. Aplica as migrações para criar a base de dados SQLite
dotnet ef database update

# 5. Executa a aplicação
dotnet run
```
A API estará disponível em `http://localhost:5024` (a porta será indicada no console) e a interface web estará em `https://localhost:7253/index.html`.

## O que Aprendi

Este projeto foi uma jornada de aprendizagem incrível, onde pude solidificar conceitos de:
* Arquitetura de APIs RESTful e os benefícios de uma abordagem desacoplada.
* Abstração de acesso a dados com o Repository Pattern.
* A importância de DTOs para a segurança e estabilidade de um contrato de API.
* Comunicação assíncrona entre front-end e back-end com `fetch` e `async/await`.
* Manipulação dinâmica do DOM com JavaScript puro.
* Configuração de um projeto .NET do início ao fim, incluindo logging, validação e gestão de base de dados.
