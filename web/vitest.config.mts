import { defineConfig } from "vitest/config";
import react from "@vitejs/plugin-react";

export default defineConfig({
  plugins: [react()],
  // Resolves the "@/..." imports from tsconfig.json.
  resolve: { tsconfigPaths: true },
  test: {
    environment: "jsdom",
    setupFiles: ["./vitest.setup.ts"],
    // CSS Modules: return the real class names so tests can check them if needed.
    css: { modules: { classNameStrategy: "non-scoped" } },
    // Readable output locally and in GitHub Actions: one line per test, plus annotations and a job
    // summary when running in CI.
    reporters: process.env.GITHUB_ACTIONS ? ["verbose", "github-actions"] : ["verbose"],
  },
});
