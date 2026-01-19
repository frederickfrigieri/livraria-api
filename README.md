# Editora — Projeto de Exemplo (Arquitetura em Camadas)

Este repositório contém um projeto .NET organizado em camadas (layers) com foco em clareza arquitetural, testabilidade e separação de responsabilidades. O objetivo é demonstrar práticas comuns em aplicações empresariais: camadas Domain, Application, Infrastructure e uma API Web (WebApi), além de testes automatizados.

## Visão geral da arquitetura

A solução está dividida nas seguintes camadas/projetos:

- `WebApi/` — camada de apresentação (API HTTP). Aqui ficam controllers, configurações de middleware e a entrada da aplicação (`Program.cs`).
- `Application/` — camada de aplicação (Application Services). Contém serviços que orquestram casos de uso, DTOs, validações (FluentValidation) e exposições para a camada de apresentação.
- `Domain/` — camada de domínio. Contém entidades, regras de negócio, exceções de domínio e interfaces de repositório (contratos). Esta camada não conhece infraestrutura externa.
- `Infrastructure/` — implementação de infra (persistência, EF Core, repositórios concretos, Unit of Work e migrations).
- `*.Tests/` — projetos de teste (`Domain.Tests`, `Application.Tests`) com testes de unidade e validação de regras de domínio e serviços de aplicação.

Essa separação permite um desenvolvimento orientado a testes, fácil manutenção e substituição de infraestrutura (por exemplo trocar EF Core por outro provedor sem tocar regras de negócio).

## Padrões e decisões de design

- Arquitetura em camadas (Layered Architecture).
- Padrões Repository e Unit of Work: `Domain.Interfaces` define contratos como `ILivroRepository` e `IUnitOfWork`; `Infrastructure` contém implementações concretas usando EF Core.
- Serviços de aplicação (Application Services): orquestram validação (FluentValidation), acesso a repositórios e operações transacionais via `IUnitOfWork`.
- DTOs / Requests: `Application.Dtos` contém objetos como `LivroRequest` usados para comunicação entre API e camada de aplicação.
- Validação: FluentValidation é usado para validar `LivroRequest` antes de executar regras de domínio.
- Exceções: existe uma hierarquia simples de exceções de aplicação e de domínio para sinalizar erros esperados (validação, entidade não encontrada, regras de negócio).
- Testes: xUnit para testes, Moq para mocks.

## Tecnologias e dependências principais

- .NET 10 (TargetFramework: net10.0)
- C# 12+ (conforme SDK)
- Entity Framework Core (migrations e DbContext em `Infrastructure`) para persistência
- FluentValidation — validação de DTOs
- Moq — mocks nos testes de aplicação
- xUnit — framework de testes
- coverlet.collector — cobertura de testes (presente nas dependências de teste)

> Consulte os arquivos `*.csproj` para versões detalhadas.

## Estrutura de diretórios (resumo)

- Application/
  - Application.csproj
  - `LivroAppService.cs` (serviço de aplicação)
  - `Dtos/` (`LivroRequest.cs`)
  - `Validators/` (Validators como `LivroValidator`)
  - `Exceptions/` (exceções de aplicação)

- Domain/
  - Domain.csproj
  - Entities/ (entidades: `Livro`, `Autor`, `Assunto`)
  - Interfaces/ (contratos de repositório e unit of work)
  - Exceptions/ (exceções específicas de domínio)

- Infrastructure/
  - Infrastructure.csproj
  - Databases/
    - `AppDbContext.cs`
    - `UnitOfWork.cs`
    - Repositories/ (implementações de repositórios)
    - Migrations/ (migrations do EF Core)

- WebApi/
  - WebApi.csproj
  - `Program.cs`, controllers, middleware

- Application.Tests/ e Domain.Tests/ — projetos de teste com testes de unidade

## Como rodar a aplicação localmente (desenvolvimento)

Requisitos:
- .NET 10 SDK instalado (confirme com `dotnet --version`)
- SQL Server / outro provedor configurado para o `AppDbContext` (ver `appsettings.json`)

Buildar a solução:

```powershell
# Na raiz do repositório
dotnet build
```

Executar a API (modo Development):

```powershell
# No diretório WebApi
dotnet run --project .\WebApi\WebApi.csproj
```

Configuração de BD e migrations (exemplo com EF Core CLI):

```powershell
# Gerar/migrar o banco (execute na raiz do projeto Infrastructure se preferir)
# Atualize as strings de conexão em WebApi/appsettings.json ou em Infrastructure
dotnet ef database update --project .\Infrastructure\Infrastructure.csproj --startup-project .\WebApi\WebApi.csproj
```

(Se você usar outro provedor que não seja SQL Server, adapte os comandos e a connection string.)

## Como rodar os testes

```powershell
# Rodar todos os testes
dotnet test

# Rodar apenas o projeto de testes da aplicação
dotnet test .\Application.Tests\Application.Tests.csproj

# Rodar apenas o projeto de testes de domínio
dotnet test .\Domain.Tests\Domain.Tests.csproj
```

## Exemplos de contratos / casos de uso importantes

- `LivroAppService.Cadastrar(LivroRequest request)`
  - Entrada: `LivroRequest` (Título, Editora, Edição, AnoPublicacao, AutoresCodigo[], AssuntosCodigo[])
  - Comportamento:
    - Valida o request via `IValidator<LivroRequest>` (FluentValidation). Se inválido, lança `Application.Exceptions.ValidationException` com a lista de erros.
    - Consulta `IAutorRepository` e `IAssuntoRepository` para obter entidades relacionadas; se não encontrar, lança `Application.Exceptions.ApplicationException` com o `CodeError` apropriado.
    - Cria a entidade `Livro` (aplica regras de domínio que podem lançar `DomainException`), adiciona via `ILivroRepository.Adicionar` e persiste via `IUnitOfWork.Save()`.
  - Saída: `int` — código/identificador do novo livro (preenchido pela camada de persistência).

## Observações sobre testes e estratégias de mocking

- Nos testes de `Application`, mocks de `IValidator<>`, repositórios e `IUnitOfWork` são usados para isolar a lógica de orquestração.
- Nos testes de `Domain`, crie instâncias reais das entidades para verificar regras de negócio (ex.: validações do construtor de `Livro`, métodos `AdicionarAutor` e `AdicionarAssunto`).
