# GestaoPedidosAPI

API REST desenvolvida em .NET 8 para gerenciamento de pedidos, clientes e produtos.

---

# Tecnologias utilizadas

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server 2022
- FluentValidation
- Swagger/OpenAPI
- Middleware global para tratamento de exceções
- Injeção de dependência nativa do ASP.NET Core
- Arquitetura em camadas

---

# Estrutura da solução

A solução foi organizada em múltiplos projetos com separação de responsabilidades.

## GestaoPedidos.Data

Responsável pela camada de persistência e acesso a dados:

- Contexto do Entity Framework
- Models/Entities
- DTOs

## GestaoPedidos.Domain

Responsável pelas regras de negócio:

- Services
- Repositories
- Interfaces
- Helpers
- Exceptions

## GestaoPedidosAPI

Responsável pela camada HTTP:

- Controllers
- Middlewares
- Validators
- Extensions

## Scripts

Contém scripts SQL para criação do banco de dados.

---

# Como executar o projeto

## Pré-requisitos

- .NET SDK 8
- SQL Server 2022
- Visual Studio 2022

---

## Configuração do banco

Executar o script localizado em:

```text
/Scripts/create_database.sql
```

---

## Configuração da connection string

Editar o arquivo:

```text
GestaoPedidosAPI/appsettings.json
```

Exemplo:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=GestaoPedidos;User Id=sa;Password=123;TrustServerCertificate=True"
}
```

---

## Executar a aplicação

A API pode ser executada pelo Visual Studio ou utilizando:

```bash
dotnet run
```

---

## Swagger

Após iniciar a aplicação:

```text
https://localhost:{porta}/swagger
```

---

# Estratégia de validação

Foi utilizado FluentValidation para validações de entrada da API, mantendo as regras desacopladas das controllers e services.

As validações implementadas incluem:

- Campos obrigatórios
- Validação de e-mail
- Validação de limites numéricos
- Validação de CPF/CNPJ
- Validação de status de pedido

Regras de negócio dependentes de persistência, como duplicidade de e-mail, documento e controle de estoque, foram mantidas na camada de serviço.

---

# Estratégia de persistência

Foi utilizado Entity Framework Core com SQL Server 2022.

As consultas de leitura utilizam:

- projeção direta para DTO
- AsNoTracking para ganho de performance

Operações de escrita utilizam entidades rastreadas pelo Entity Framework para permitir controle automático de mudanças.

Operações críticas, como criação de pedidos e atualização de estoque, utilizam transações explícitas do Entity Framework Core para garantir consistência dos dados.

---

# Estratégia de estoque

O estoque representa a quantidade disponível de produtos para novos pedidos.

Durante a criação do pedido:

- todos os produtos são validados previamente
- o estoque é validado considerando a soma total das quantidades do mesmo produto no pedido
- o débito de estoque ocorre dentro de transação

A criação do pedido e a baixa de estoque são executadas de forma transacional para evitar inconsistências parciais.

Em caso de cancelamento:

- o estoque é retornado apenas para pedidos ainda não enviados
- pedidos enviados não retornam estoque

Para o escopo atual do projeto não foi implementado controle de concorrência otimista/pessimista.

---

# Fluxo de status do pedido

Os pedidos possuem fluxo controlado de status utilizando enumerações e regras centralizadas.

## Status disponíveis

- Criado
- Pago
- Enviado
- Cancelado

## Transições permitidas

- Criado → Pago
- Pago → Enviado
- Criado → Cancelado

Alterações inválidas retornam erro de negócio.

Toda alteração de status gera histórico de auditoria.

---

# Estratégia para valores monetários

Os valores monetários utilizam o tipo decimal no .NET e DECIMAL no SQL Server para evitar problemas de precisão comuns em tipos de ponto flutuante.

A estratégia evita inconsistências em cálculos financeiros e arredondamentos inesperados.

---

# Estratégia para datas e timezone

As datas são persistidas em UTC utilizando DateTimeOffset.UtcNow.

A estratégia foi adotada para evitar problemas de timezone entre ambientes e facilitar futuras integrações.

A responsabilidade de conversão para horário local fica a cargo da camada consumidora da API.

---

# Tratamento de erros

A aplicação utiliza middleware global para tratamento centralizado de exceções.

Exceções de negócio utilizam a classe BusinessException, retornando mensagens padronizadas para a API com status HTTP apropriado.

---

# Estratégia de testes

O projeto foi estruturado visando testabilidade através da separação de responsabilidades entre controllers, services e repositories.

Devido ao escopo e tempo disponível, os testes automatizados não foram totalmente implementados.

A estratégia planejada inclui:

- testes unitários para services e validators
- testes de integração para endpoints críticos
- mocks de repositories utilizando Moq

---

# Regras de negócio implementadas

## Clientes

- Cadastro de clientes
- Validação de e-mail
- Validação de CPF/CNPJ
- Bloqueio de duplicidade de e-mail
- Bloqueio de duplicidade de documento
- Inativação de clientes

## Produtos

- Cadastro de produtos
- Controle de estoque
- Validação de estoque negativo
- Inativação de produtos

## Pedidos

- Criação de pedidos
- Associação obrigatória com cliente ativo
- Validação de produtos ativos
- Controle de estoque
- Cálculo automático do valor total do pedido
- Persistência do histórico de preços dos itens
- Histórico de alteração de status
- Validação de transições de status
- Bloqueio de transições inválidas
- Cancelamento com retorno de estoque

---

# Decisões técnicas e trade-offs

## DTOs separados das entidades

Foi adotada separação entre DTOs e entidades para evitar acoplamento da API à camada de persistência.

---

## Uso de FluentValidation

A validação foi desacoplada das controllers e services para melhorar organização e manutenção.

---

## Uso de projeção direta para DTO

As consultas utilizam Select direto para DTO visando melhor performance e menor consumo de memória.

---

## Uso de enums para status do pedido

Os status dos pedidos foram implementados utilizando enums no .NET e persistidos como inteiros no banco de dados para melhor performance, padronização e integridade.

---

## Não utilização de CQRS/Mediator

Considerando o escopo do projeto, optou-se por uma arquitetura mais simples e objetiva, evitando complexidade desnecessária.

---

# Melhorias futuras

- Implementação completa de testes automatizados
- Paginação nas consultas
- Autenticação e autorização JWT
- Cache para consultas
- Controle de concorrência para estoque
