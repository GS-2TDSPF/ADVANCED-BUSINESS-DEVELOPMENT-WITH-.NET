# 🛰️ OrbitAlert — .NET API

> **FIAP Global Solution 2026/1 · Turma 2TDS Fevereiro · Tema: Economia Espacial**

---

## 📋 Descrição

API REST desenvolvida em **ASP.NET Core** seguindo **Clean Architecture** com 4 camadas independentes. Conecta no mesmo banco Oracle da solução Java, expondo os dados do OrbitAlert — plataforma de alertas precoces de desastres naturais com dados orbitais Sentinel-1 e IA generativa.

---

## 👥 Equipe

| Nome | RM |
|---|---|
| Gabriel Sbrana Campos | RM 565849 |
| Moisés Waidemann | RM 563719 |
| Thiago Rodrigues da Mota | RM 563650 |
| Richard Freitas | RM 566127 |

---

## 🔗 Links

| Recurso | Link |
|---|---|
| 📦 **GitHub** | `https://github.com/GS-2TDSPF/ADVANCED-BUSINESS-DEVELOPMENT-WITH-.NET` |
| 📄 **Swagger** | `http://localhost:5084/swagger/index.html` |
| 🎥 **Vídeo de Apresentação** | *(link após gravação)* |
| 🎥 **Vídeo do pitch** | *https://youtu.be/96OZFLMUHDs * |

---

## 🏗️ Arquitetura — Clean Architecture

```
OrbitAlert.Domain          → Entidades, Enums
OrbitAlert.Application     → DTOs, Interfaces (Repositórios + Services)
OrbitAlert.Infrastructure  → EF Core, Configurations, Repositories, Migrations
OrbitAlert.API             → Controllers, GlobalExceptionHandler, Program.cs
```

```
OrbitAlert/
├── OrbitAlert.Domain/
│   ├── Entities/   → 11 entidades com private setters, construtor e Transferir()
│   └── Enum/       → StatusAlertaEnum, TipoPerfilEnum, TipoNotificacaoEnum
├── OrbitAlert.Application/
│   ├── DTO/
│   │   ├── Requests/   → 11 records com ToEntity()
│   │   └── Responses/  → 12 records com static ToDTO()
│   └── Interfaces/
│       ├── Repositories/ → 11 interfaces
│       └── Services/     → 11 arquivos (interface + implementação no mesmo arquivo)
├── OrbitAlert.Infrastructure/
│   └── Persistence/
│       ├── Configuration/ → 11 IEntityTypeConfiguration com SEQ_X.NEXTVAL
│       ├── Repositories/  → 11 repositórios concretos
│       ├── Migrations/    → Migration Initial (Up vazio — tabelas criadas pelo Java)
│       └── OrbitAlertContext.cs
└── OrbitAlert.API/
    ├── Controllers/  → 11 controllers com primary constructor injection
    ├── Exceptions/   → GlobalExceptionHandler com switch expression
    ├── Swagger/      → SwaggerExampleSchemaFilter
    └── Program.cs
```

### Stack

| Camada | Tecnologia |
|---|---|
| Framework | .NET 10 / ASP.NET Core |
| ORM | Entity Framework Core 9 + Oracle |
| Banco | Oracle DB FIAP (mesmo do Java) |
| Documentação | Swashbuckle / Swagger |
| Padrão | Clean Architecture + Repository Pattern |

---

## 🗄️ Banco de Dados

Usa as mesmas tabelas Oracle criadas pelos scripts SQL do projeto Java. O EF Core **não recria as tabelas** — a migration `Initial` tem o método `Up()` vazio por design.

### Tabelas Mapeadas

```
TB_USUARIO            TB_MUNICIPIO
TB_USUARIO_MUNICIPIO  TB_ZONA_RISCO
TB_ESTACAO_IOT        TB_LEITURA_IOT
TB_TIPO_ALERTA        TB_ALERTA
TB_ANALISE_IA         TB_HISTORICO_ALERTA
TB_NOTIFICACAO
```

### Relacionamentos

| Tipo | Relação |
|---|---|
| 1:N | Municipio → ZonaRisco, ZonaRisco → Alerta, Alerta → HistoricoAlerta |
| 1:1 | Alerta ↔ AnaliseIa |
| N:N | Usuario ↔ Municipio (via TB_USUARIO_MUNICIPIO com chave composta) |

---

## 🚀 Como Rodar

### Pré-requisitos

- .NET 10 SDK
- Rider ou Visual Studio 2022+
- Acesso ao Oracle FIAP (`oracle.fiap.com.br`) via rede FIAP ou VPN

### Passos

**1. Clone o repositório**
```bash
git clone https://github.com/orbitalert-gs/orbitalert-dotnet.git
cd orbitalert-dotnet
```

**2. Configure a connection string no `appsettings.Development.json`**
```json
{
  "ConnectionStrings": {
    "OrbitAlertOracle": "Data Source=oracle.fiap.com.br:1521/orcl;User ID=SEU_RM;Password=SUA_SENHA;"
  }
}
```

**3. Aplique a migration**
```bash
dotnet ef database update --project OrbitAlert.Infrastructure --startup-project OrbitAlert.API
```

> A migration `Initial` tem o `Up()` vazio — apenas registra no `__EFMigrationsHistory` que o banco está sincronizado. As tabelas já existem no Oracle.

**4. Rode a aplicação**
```bash
dotnet run --project OrbitAlert.API
```

**5. Acesse o Swagger**
```
http://localhost:5000/swagger
```

---

## 📡 Endpoints

| Controller | Rota Base | Endpoints extras |
|---|---|---|
| Usuários | `api/usuarios` | — |
| Municípios | `api/municipios` | `GET /estado?nmEstado=` |
| Vínculos | `api/usuariosmunicipios` | `GET /usuario/{id}`, `GET /municipio/{id}` |
| Zonas de Risco | `api/zonasrisco` | `GET /municipio/{id}` |
| Estações IoT | `api/estacoesiot` | `GET /zona/{id}` |
| Leituras IoT | `api/leiturasiot` | `GET /estacao/{id}` |
| Tipos de Alerta | `api/tiposalerta` | — |
| Alertas | `api/alertas` | `GET /status?status=`, `GET /municipio/{id}` |
| Análises IA | `api/analisesia` | `GET /alerta/{id}` |
| Histórico | `api/historicosalerta` | `GET /alerta/{id}` |
| Notificações | `api/notificacoes` | `GET /usuario/{id}` |

Padrão de cada controller:

```
POST   /api/{recurso}        → criar  → 201 Created
GET    /api/{recurso}        → listar → 200 OK
GET    /api/{recurso}/{id}   → buscar → 200 OK / 404 Not Found
PUT    /api/{recurso}/{id}   → editar → 200 OK / 404 Not Found
DELETE /api/{recurso}/{id}   → apagar → 204 No Content / 404 Not Found
```

---

## 🧪 Exemplo de uso

**Criar um município:**
```http
POST /api/municipios
Content-Type: application/json

{
  "nmMunicipio": "Petrópolis",
  "nmEstado": "Rio de Janeiro",
  "nrLatitude": -22.5053,
  "nrLongitude": -43.1786,
  "nrPopulacao": 306500,
  "stAtivo": "S"
}
```

**Criar um alerta:**
```http
POST /api/alertas
Content-Type: application/json

{
  "nrNivelRisco": 4,
  "stStatus": "ATIVO",
  "dsObservacao": "Precipitação acumulada de 80mm nas últimas 6h."
}
```

**Buscar alertas por município:**
```http
GET /api/alertas/municipio/1
```

**Buscar alertas por status:**
```http
GET /api/alertas/status?status=ATIVO
```

---

## ⚠️ Tratamento de Erros

O `GlobalExceptionHandler` captura todas as exceções e retorna `ProblemDetails` padronizado:

| Exceção | HTTP | Quando ocorre |
|---|---|---|
| `ArgumentException` | 400 | Dado inválido na entidade |
| `ArgumentNullException` | 400 | Campo obrigatório nulo |
| `InvalidOperationException` | 400 | Operação não permitida |
| `KeyNotFoundException` | 404 | Recurso não encontrado |
| `UnauthorizedAccessException` | 401 | Acesso não autorizado |
| `OracleException` | 502 | Banco indisponível |
| Qualquer outra | 500 | Erro interno |

Resposta de erro:
```json
{
  "type": "about:blank",
  "title": "Recurso nao encontrado",
  "status": 404,
  "detail": "Alerta com id '99' não encontrado.",
  "instance": "/api/alertas/99"
}
```

---

## ✅ Requisitos Técnicos Atendidos

- [x] API REST com verbos HTTP corretos e status codes (200, 201, 204, 400, 404, 500)
- [x] Clean Architecture com 4 camadas independentes
- [x] Persistência Oracle com Entity Framework Core 9
- [x] Relacionamentos 1:N e N:N mapeados
- [x] Migration aplicada corretamente
- [x] Repository Pattern com interfaces em Application e implementações em Infrastructure
- [x] DTOs separados das entidades (`record` com `ToEntity()` e `static ToDTO()`)
- [x] Entidades com private setters, construtor validado e método `Transferir()`
- [x] GlobalExceptionHandler com `IExceptionHandler` e switch expression
- [x] Swagger com exemplos de payload via `SwaggerExampleSchemaFilter`
- [x] Configurações EF Core com `IEntityTypeConfiguration` e `SEQ_X.NEXTVAL`

---

## 🔗 Repositórios do projeto

| Disciplina | Status |
|---|---|
| Java Advanced | ✅ |
| **.NET ← você está aqui** | ✅ |
| Banco de Dados | ✅ |
| Mobile | 🔧 |
| IoT | 🔧 |
| DevOps | 🔧 |
| Compliance & QA | ✅ |

---

## 📚 Disciplina

**Advanced Business Development with .NET** — FIAP 2026 · 1º Semestre · Turma 2TDS Fevereiro
Global Solution 2026/1 — Tema: **Economia Espacial**
