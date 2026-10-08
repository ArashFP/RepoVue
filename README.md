# RepoVue

A DevSecOps dashboard for your GitHub repos. Connect your account and see how your projects are doing:
CI/CD runs, security alerts, tests, Docker, dependencies and deployments, all in one place.

```
web/        Next.js frontend (TypeScript, Radix, CSS Modules)
api/        ASP.NET Core API, EF Core + PostgreSQL, SignalR
tests/      Backend tests
```

## Status

Just getting started. Working on the base setup, see the [roadmap](#roadmap).

## What it shows

| Section | What you see |
|---|---|
| Project health | One score per repo, built from everything below |
| CI/CD | Workflow runs, what failed, how long things take |
| Security | Dependabot, code scanning and secret scanning alerts |
| Testing | Test results per run |
| Docker | Is the app containerized, does the image build, image size, known vulnerabilities |
| Dependencies | Outdated and vulnerable packages |
| Deployments | Deployment history per environment |
| GitHub data | Pull requests, reviews, branch protection and other repo settings |

## Running it

You need these installed in WSL (Ubuntu):

| Tool | Version |
|---|---|
| Node.js | 24 (through nvm) |
| .NET SDK | 10 |
| Docker Desktop | any recent, with WSL integration on |

The first time, install the frontend's packages:

```
cd web && npm install && cd ..
```

Then, from the repo root, in two terminals:

```
npm run backend     # starts the database and the API on http://localhost:5080 (check /health)
npm run frontend    # starts the website on http://localhost:3000
```

| Command | What it does |
|---|---|
| `npm run backend` | Starts the database (Docker) and waits until it's ready, then runs the API |
| `npm run frontend` | Runs the Next.js dev server |
| `npm run db` / `npm run db:stop` | Only start or stop the database |
| `npm test` | All tests, API and frontend |

## Tests

```
npm test                    # everything
dotnet test                 # only API tests, needs Docker running (each test class gets its own database)
cd web && npm test          # only frontend tests
```

CI runs the same tests on every pull request, plus a lint, a build and a vulnerability scan of the
API's Docker image.

## Roadmap

This is where RepoVue is going. The order might change along the way.

### 1. Base setup (working on it now)
- Next.js frontend, ASP.NET Core API and PostgreSQL, all running with Docker
- Sign in with GitHub and install the RepoVue GitHub App on your repos
- Demo mode, so you can look around without logging in
- Rate limits and usage limits so nobody can drain the servers

### 2. CI/CD and GitHub data
- See every workflow run, what failed and how long things take
- Open pull requests, reviews and how old they are
- Live updates when a run finishes

### 3. Security
- Dependabot, code scanning and secret scanning alerts in one place
- Repo checks like branch protection, required reviews and a SECURITY.md

### 4. Testing, Docker and dependencies
- Test results per step, and per test when the repo uploads a test report
- Is the app in a container, does the image build, how big is it and does it have known vulnerabilities
- Outdated and vulnerable packages

### 5. Deployments and project health
- Deployment history per environment
- One health score per project, built from everything above

### 6. AI error analysis
- When a run fails, there's an "Analyze with AI" button on it
- Click it and Claude reads the logs and the recent commits
- It points to the commit that most likely broke it and suggests a fix
- Nothing gets sent to the AI unless you click the button

### 7. Going live
- Frontend on Vercel, API on Azure, database on Neon
- Free plan and a Pro plan for more repos and more AI analyses

### 8. Mascot
- Give RepoVue a mascot for the site, the README and marketing
- Small animations, like when a build passes or fails

### 9. Teams
- A Team plan for whole GitHub organizations
- More people on one dashboard, with roles like viewer and admin
