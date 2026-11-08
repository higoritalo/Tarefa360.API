# Projeto360

Projeto de gerenciamento de tarefas em .NET 10, organizado em cinco camadas/projetos.

## Requisitos
- .NET SDK 10.0 instalado
- VS Code ou Visual Studio
- Internet na primeira restauração dos pacotes NuGet

## Como executar
1. Extraia a pasta `Projeto360` para um local simples, por exemplo `C:\Projetos\Projeto360`.
2. Abra essa pasta no VS Code.
3. Abra o terminal na pasta onde está `Projeto360.sln`.
4. Execute:

```powershell
dotnet restore
dotnet build
dotnet run --project src/Projeto360.Api
```

5. Abra o endereço HTTPS/HTTP mostrado no terminal e acrescente `/swagger`.

O banco SQLite `projeto360.db` será criado automaticamente na pasta de execução da API.

## Endpoints
- `GET /api/tarefa` - lista tarefas
- `GET /api/tarefa/{id}` - consulta uma tarefa
- `POST /api/tarefa` - cria tarefa (JSON: `{ "titulo": "Estudar arquitetura" }`)
- `PUT /api/tarefa/{id}` - atualiza título e situação (JSON: `{ "titulo": "Estudar .NET", "concluida": true }`)
- `PATCH /api/tarefa/{id}/concluir` - marca como concluída
- `DELETE /api/tarefa/{id}` - exclui tarefa

## Organização
- `Projeto360.Dominio`: entidade e regras centrais.
- `Projeto360.Aplicacao`: contratos e DTOs usados pelos casos de uso.
- `Projeto360.Repositorio`: Entity Framework Core e SQLite.
- `Projeto360.Servicos`: regras de aplicação/orquestração dos casos de uso.
- `Projeto360.Api`: endpoints HTTP, Swagger e configuração de dependências.

Observação: esta é uma reconstrução funcional baseada nos elementos que conseguimos recuperar das conversas, não uma cópia confirmada do enunciado original da Itera 360.
