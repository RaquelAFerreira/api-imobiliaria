# AluguelImoveis API

API RESTful desenvolvida com ASP.NET Core para gerenciar aluguéis de imóveis.

## ⚙️ Tecnologias Utilizadas

- [.NET 9.0](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Injeção de Dependência com `Microsoft.Extensions.DependencyInjection`
- API REST com Controllers

## 🚀 Como Executar Localmente

### Pré-requisitos

- Baixar [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- Baixar Visual Studio Code
- Baixar extensões do VS Code: C# por Microsoft e C# Dev Kit por Microsoft

### Passos

   ```bash
   git clone https://github.com/RaquelAFerreira/api-imobiliaria.git
   ```

1. [Criação do banco de dados](Docs/bancodedados.pdf)

2. Abra a pasta api-imobiliaria no VS Code ou o arquivo .csproj no Visual Studio

3. No terminal do VS Code
   ```bash
   cd AluguelImoveis
   ```
4. Altere o arquivo appsettings.json

   Modifique o atributo ConnectionStrings com as informações do seu banco de dados SQL Server
   
5. Restaure os pacotes:
```bash
   dotnet restore
```
6. Inicie a aplicação:
```bash
   dotnet run
```
7. Acesse a documentação da API em:
```bash
   http://localhost:5287/swagger
```
### Documentações

- [Swagger](http://localhost:5287/swagger/index.html)
- [Manual do sistema desktop](Docs/manual.pdf)
- [Criação do banco de dados](Docs/bancodedados.pdf)
- [Execução do front-end](https://github.com/RaquelAFerreira/desktop-imobiliaria)
