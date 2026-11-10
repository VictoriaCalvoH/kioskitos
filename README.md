# Kioskitos

Kioskitos is a web platform for independent professionals and freelancers in
Uruguay's creative and digital sectors. It centralizes the management of
clients, projects, tasks, estimates, tracked hours, contracts, and collections
in a single interface.

## Functional scope

- Modular management of clients, projects, stages, and tasks.
- Estimate generation and tracking.
- Worked-hours tracking.
- Formalization of contractual agreements with clients.
- Project and collection status tracking.
- Google Calendar integration.

## Architecture

The project is organized as a monorepo:

- `/frontend`: web application built with Next.js and TypeScript.
- `/backend`: REST API built with .NET and C#.
- PostgreSQL: relational database.

## Structure

```text
kioskitos/
├── frontend/
└── backend/
```

## Workflow

- `main` is protected: direct commits are not allowed.
- Every change must be submitted through a Pull Request with at least one approval.

## Branch naming

- `feat/<ticket>-<description>` - for example, `feat/KIOS-12-user-login`
- `fix/<ticket>-<description>` - for example, `fix/KIOS-30-cart-error`

Use hyphen-separated words and avoid spaces.

## Environment variables

Do not commit `.env` files. Use `.env.example` files as references.

## Local development with Docker

Create the local environment file from the example and start all services:

```bash
cp .env.example .env
docker compose up --build
```

The services are available at:

- Frontend: [http://localhost:3000](http://localhost:3000)
- Backend: [http://localhost:8080](http://localhost:8080)
- PostgreSQL: `localhost:5432`

PostgreSQL data is persisted in the `postgres_data` Docker volume. The backend
waits for PostgreSQL to pass its health check before starting.

## Database migrations

EF Core Tools are installed as a local tool (`backend/.config/dotnet-tools.json`).
From `backend/`:

```bash
dotnet tool restore
docker compose -f ../docker-compose.yml up -d postgres
set -a; source ../.env; set +a   # loads ConnectionStrings__Default

dotnet ef database update \
  --project src/Kioskitos.Infrastructure \
  --startup-project src/Kioskitos.API
```

To create a new migration:

```bash
dotnet ef migrations add <Name> \
  --project src/Kioskitos.Infrastructure \
  --startup-project src/Kioskitos.API \
  --output-dir Persistence/Migrations
```

## Integration tests

Tests that need the database read `ConnectionStrings__Default` and are skipped
when it is not set. With Postgres running and `.env` loaded: `dotnet test`.

## Frontend

For frontend configuration and available commands, see
[frontend/README.md](./frontend/README.md).
