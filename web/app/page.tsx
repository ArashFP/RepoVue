import { SECTIONS } from "@/lib/sections";
import styles from "./page.module.css";

export default function Home() {
  return (
    <main className={styles.page}>
      <h1>RepoVue</h1>
      <p className="text-muted">
        A DevSecOps dashboard for your GitHub repos. Work in progress.
      </p>
      <ul className={styles.sections} aria-label="Dashboard sections">
        {SECTIONS.map((section) => (
          <li key={section} className={styles.section}>
            {section}
          </li>
        ))}
      </ul>
    </main>
  );
}
