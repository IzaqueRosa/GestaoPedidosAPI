# GestaoPedidosAPI

API REST desenvolvida em .NET 8 para gerenciamento de pedidos, clientes e produtos.

## Tecnologias utilizadas

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server 2022
- FluentValidation
- Swagger/OpenAPI
- Middleware global para tratamento de exceções
- Injeção de dependência nativa do ASP.NET Core
- Arquitetura em camadas

## Estrutura da solução

A solução foi organizada em múltiplos projetos com separação de responsabilidades:

### GestaoPedidos.Data
Responsável pela camada de persistência e acesso a dados:
- Contexto do Entity Framework
- Models/Entities
- DTOs

### GestaoPedidos.Domain
Responsável pelas regras de negócio:
- Services
- Repositories
- Interfaces
- Helpers
- Exceptions

### GestaoPedidosAPI
Responsável pela camada HTTP:
- Controllers
- Middlewares
- Validators
- Extensions

### Scripts
Contém scripts SQL para criação do banco de dados.

## Como executar o projeto

### Pré-requisitos

- .NET SDK 8
- SQL Server 2022
- Visual Studio 2022

### Configuração do banco

Executar o script localizado em:

/Scripts/create_database.sql

### Configuração da connection string

Editar o arquivo:

GestaoPedidosAPI/appsettings.json

Exemplo:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=GestaoPedidos;User Id=sa;Password=123;TrustServerCertificate=True"
}

### Executar a aplicação

A API pode ser executada pelo Visual Studio ou utilizando:

dotnet run

### Swagger

Após iniciar a aplicação:

https://localhost:{porta}/swagger

## Estratégia de validação

Foi utilizado FluentValidation para validações de entrada da API, mantendo as regras desacopladas das controllers e services.

As validações implementadas incluem:
- Campos obrigatórios
- Validação de e-mail
- Validação de limites numéricos
- Validação de CPF/CNPJ

Regras de negócio dependentes de persistência, como duplicidade de e-mail e documento, foram mantidas na camada de serviço.

## Estratégia de persistência

Foi utilizado Entity Framework Core com SQL Server 2022.

As consultas de leitura utilizam:
- projeção direta para DTO
- AsNoTracking para ganho de performance

Operações de escrita utilizam entidades rastreadas pelo Entity Framework para permitir controle automático de mudanças.

## Estratégia de estoque

O estoque do produto não permite valores negativos.

As validações são realizadas na camada de entrada utilizando FluentValidation.

A atualização de estoque foi pensada de forma simples para o escopo atual do projeto, sem controle de concorrência distribuída.

## Estratégia para valores monetários

Os valores monetários utilizam o tipo decimal no .NET e DECIMAL no SQL Server para evitar problemas de precisão comuns em tipos de ponto flutuante.

A estratégia evita inconsistências em cálculos financeiros e arredondamentos inesperados.

## Estratégia para datas e timezone

As datas são persistidas em UTC utilizando DateTime.UtcNow.

A estratégia foi adotada para evitar problemas de timezone entre ambientes e facilitar futuras integrações.

A responsabilidade de conversão para horário local fica a cargo da camada consumidora da API.

## Tratamento de erros

A aplicação utiliza middleware global para tratamento centralizado de exceções.

Exceções de negócio utilizam a classe BusinessException, retornando mensagens padronizadas para a API com status HTTP apropriado.

## Estratégia de testes

O projeto foi estruturado visando testabilidade através da separação de responsabilidades entre controllers, services e repositories.

Devido ao escopo e tempo disponível, os testes automatizados não foram totalmente implementados.

A estratégia planejada inclui:
- testes unitários para services e validators
- testes de integração para endpoints críticos
- mocks de repositories utilizando Moq

## Decisões técnicas e trade-offs

### DTOs separados das entidades
Foi adotada separação entre DTOs e entidades para evitar acoplamento da API à camada de persistência.

### Uso de FluentValidation
A validação foi desacoplada das controllers e services para melhorar organização e manutenção.

### Uso de projeção direta para DTO
As consultas utilizam Select direto para DTO visando melhor performance e menor consumo de memória.

### Não utilização de CQRS/Mediator
Considerando o escopo do projeto, optou-se por uma arquitetura mais simples e objetiva, evitando complexidade desnecessária.

## Melhorias futuras

- Implementação completa de testes automatizados
- Paginação nas consultas
- Autenticação e autorização JWT
- Cache para consultas
- Controle de concorrência para estoque
