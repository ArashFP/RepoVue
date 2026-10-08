---
name: ship
description: Finish the current change in RepoVue - commit to the feature branch, run the CI checks on that commit, push it and give the user the pull request link. Use when the user says /ship, "ship it", "push this" or "make a PR".
---

# Ship the current branch

Only run this when the user invokes it (`/ship`, "ship it"). It is the only time changes are staged,
committed and pushed: the user reviews the unstaged changes in their editor before shipping.

1. **Check the branch.**
   - If `main` has no commits yet (brand new repo): commit only the project's starting files to `main`
     and push it, so there is something to open a pull request against. Then continue on a new branch.
   - If on `main`, stop: create a branch named after the work (see CLAUDE.md naming rules), then
     continue. Never push to `main` otherwise.

2. **Review what's being committed.** `git status` and `git diff`; make sure no secrets, `.env` files,
   `*.pem` keys, build output or stray debug code are included, and that every new file is added.

3. **Commit** with a message whose first line summarizes the change, and a short body of what and why.
   Small follow-up commits on the same branch are fine.

4. **Run the CI checks on the commit, before pushing.** Check out the commit in a clean temporary
   worktree (`git worktree add --detach <tmp> HEAD`), so git-ignored files and uncommitted changes can't
   hide failures and the user's running API doesn't lock the build. In it, run the same steps as
   `.github/workflows/ci.yml`, only for the jobs touched by the change (all of them if unsure):
   - API (`api/`, `tests/`, `RepoVue.slnx`): `dotnet build --configuration Release`, then
     `dotnet test --no-build --configuration Release` (needs Docker running).
   - Web (`web/`): `npm ci`, `npm run typecheck`, `npm run lint`, `npm test`, `npm run build`,
     `npm audit --omit=dev --audit-level=high`.
   - Docker image (`api/`): `docker build -t repovue-api-check api/`.
   Remove the worktree afterwards (`git worktree remove --force <tmp>`).
   If anything fails, don't push: undo the commit with `git reset HEAD~1` (changes stay in the working
   tree, unstaged), fix the problem without staging, show the user the failure and the fix, and let them
   review and run `/ship` again.

5. **Push**: `git push -u origin <branch>`.

6. **Tell the user**, briefly:
   - the branch name and what was committed
   - the PR link: `https://github.com/ArashFP/RepoVue/pull/new/<branch>` (or the existing PR if one is already open)
   - that they should merge once the API, Web and Docker image checks are green, and that the branch can
     be deleted after merging
