# AluguelImoveis API

API RESTful desenvolvida com ASP.NET Core para gerenciar aluguéis de imóveis.

## ⚙️ Tecnologias Utilizadas

- [.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Injeção de Dependência com `Microsoft.Extensions.DependencyInjection`
- API REST com Controllers

## 🚀 Como Executar Localmente

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- (Opcional) [Visual Studio 2022](https://visualstudio.microsoft.com/) ou VS Code

### Passos

1. Clone o repositório:

   ```bash
   git clone https://github.com/RaquelAFerreira/api-imobiliaria.git
   cd AluguelImoveis

2. Altere o arquivo appsettings.json

   Modifique o atributo ConnectionStrings com as informações do seu banco de dados SQL Server

3. Restaure os pacotes:
```bash
   dotnet restore
```
4. Inicie a aplicação:
```bash
   dotnet run
```
5. Acesse a API em:
```bash
   http://localhost:5287
```
### Documentações

- [Swagger](http://localhost:5287/swagger/index.html)
- Criação de comandos no banco de dados
- Execução do front-end
- [Manual do sistema desktop](/manual.pdf)
