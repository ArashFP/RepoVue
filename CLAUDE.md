# RepoVue

DevSecOps dashboard for GitHub repos: CI/CD, security, testing, Docker, dependencies, deployments and
GitHub data in one place. Next.js frontend in `web/`, ASP.NET Core API in `api/`, tests in `tests/`.
See README.md for how to run things and the roadmap.

## Layout

```
web/                        Next.js (App Router, TypeScript), Radix Primitives + Radix Icons, CSS Modules
  app/globals.css           shared design values (colors, spacing, radius, fonts, shadows) + base styles
  components/ui/            our wrappers around Radix primitives (one place for their styling)
api/RepoVue.Api/            ASP.NET Core API, EF Core (PostgreSQL), SignalR
tests/RepoVue.Api.Tests/    backend integration tests (xUnit + Testcontainers)
docker-compose.yml          local PostgreSQL on port 5433
```

## Styling (always)

- **Before writing or changing any styles, read `web/app/globals.css`** and reuse what's there.
  Don't isolate styles in a module when they're shared.
- Colors, spacing, radius, font sizes and shadows come from the CSS variables in `globals.css`
  (`var(--color-primary)`), never hard-coded values in a module.
- A style goes in a component's `*.module.css` only when it's specific to that component. If something
  is needed in two or more places, it goes into `globals.css` (add a variable or a shared class), not
  copied between modules.
- Use Radix Primitives through the wrappers in `web/components/ui/`. If a wrapper doesn't exist yet,
  create it there (`Dialog.tsx` + `Dialog.module.css`) instead of styling Radix inline in a page.
- Icons come from `@radix-ui/react-icons`. If one is missing, add a small SVG component in
  `web/components/icons/`, don't add a second icon library.
- No Tailwind, no Radix Themes.

## Tests (always)

- **Every change comes with tests.** New endpoint, new component with logic, bug fix: add or update the
  tests that cover it in the same change. A bug fix starts with a test that fails without the fix.
- Backend: integration tests against a real PostgreSQL (Testcontainers) through the real HTTP pipeline,
  not mocks of our own code. Mock only outside services (GitHub, Anthropic, Lemon Squeezy).
- Frontend: Vitest + React Testing Library for components and logic.
- **Test names read as sentences** so a failure is clear in GitHub Actions without opening the code,
  e.g. `Webhook_with_wrong_signature_is_rejected_with_401`, `it("shows demo data without logging in")`.
- **CI must be easy to read.** Each test area is its own named step in `.github/workflows/ci.yml`
  (e.g. "Tests: webhooks", "Tests: sign-in"), so the run page shows what passed or failed. A new test
  area gets its own step. Test results also go into the job summary.

## Git workflow (always)

- **Never commit to `main`.** Before making any change to the repo, create a branch from an up-to-date `main`:
  ```
  git checkout main && git pull --ff-only && git checkout -b <type>/<short-description>
  ```
- Name the branch after the work, kebab-case, with a type prefix:
  - `feature/` new functionality (`feature/github-sign-in`)
  - `fix/` bug fixes (`fix/webhook-signature-check`)
  - `chore/` tooling, config, docs, dependencies (`chore/update-next`)
  - `test/` tests only
- If already on a branch for the same piece of work, keep using it. A new, unrelated request gets a new branch.
- **Never stage, commit or push on your own.** Leave all changes unstaged so the user can review them
  in their editor first. Creating the branch is fine.
- **Only `/ship` commits and pushes**, when the user runs it: commit, run the CI checks on the commit,
  push, give the user the pull request link. The user merges when CI is green, then the branch is deleted.

## Money and limits (always)

RepoVue runs on a hard budget (€10/month to start). Anything that costs money (GitHub API calls,
AI calls, background syncs) goes through the plan quotas and limits in the database. Never add a code
path that calls a paid service without checking the limits first. Limits are config values, not
hard-coded numbers.

## Checks (same as CI in `.github/workflows/ci.yml`)

- API: `dotnet build` and `dotnet test` from the repo root (needs Docker for Testcontainers).
- Web: `cd web && npm run typecheck && npm run lint && npm test && npm run build`.
- Docker: `docker build -t repovue-api api/`.
- Next.js changes often: read `web/AGENTS.md` and the docs in `web/node_modules/next/dist/docs/`
  before using Next.js APIs.

## Gotchas

- Dev DB connection uses `127.0.0.1`, not `localhost` (a WSL relay grabs `::1`). Port **5433**, so it
  doesn't clash with Grocify's database on 5432.
- Node comes from nvm inside WSL. If `which node` shows `/mnt/...`, the Windows Node is being used by mistake.
- Ports: API on **5080**, web on **3000**. The user runs both themselves; don't stop their processes.
  Use other ports for test copies.
