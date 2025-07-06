# AluguelImoveis API

API RESTful desenvolvida com ASP.NET Core para gerenciar aluguéis de imóveis.

## ⚙️ Tecnologias Utilizadas

- [.NET 9.0](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- ASP.NET Core Web API - Framework para construção da API
- Entity Framework Core - ORM para acesso a dados
- SQL Server - Banco de dados relacional
- Swagger - Documentação interativa da API
- xUnit - Framework para testes unitários
- Injeção de Dependência com `Microsoft.Extensions.DependencyInjection`

## 🚀 Como Executar Localmente

### Pré-requisitos

- Baixar [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- Baixar Visual Studio Code ou Visual Studio 2022+

- Baixar extensões recomendadas para VS Code:
   C# (Microsoft)
   C# Dev Kit (Microsoft)

### Passos

   ```bash
   git clone https://github.com/RaquelAFerreira/api-imobiliaria.git
   ```

1. Configure o banco de dados:        
- Consulte o guia [Criação do banco de dados](Docs/bancodedados.pdf)
- Atualize a connection string do atributo ConnectionStrings.DefaultConnection no arquivo appsettings.json, de acordo com os dados da sua conexão

4. Abra a pasta api-imobiliaria no VS Code ou a solução (.sln) no Visual Studio

5. No terminal do VS Code
   ```bash
   cd AluguelImoveis
   ```
   
6. Restaure os pacotes:
```bash
   dotnet restore
```
5. Inicie a aplicação:
```bash
   dotnet run
```
6. Acesse a documentação da API em:
```bash
   http://localhost:5287/swagger
```
ou

3. No Visual Studio compile a solução (Ctrl+Shift+B)

4. Execute o projeto (F5 ou clique em "Iniciar")


### 🧪 Testes
Execução dos Testes
```bash
dotnet test
```

Dependências de Teste
- xUnit - Framework de testes
- Moq - Criação de mocks
- coverlet - Análise de cobertura

### Documentações

- [Swagger](http://localhost:5287/swagger/index.html)
- [Manual do sistema desktop](Docs/manual.pdf)
- [Criação do banco de dados](Docs/bancodedados.pdf)
- [Execução do front-end](https://github.com/RaquelAFerreira/desktop-imobiliaria)
