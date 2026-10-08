// The dashboard sections, in the order they appear in the navigation.
export const SECTIONS = [
  "Project health",
  "CI/CD",
  "Security",
  "Testing",
  "Docker",
  "Dependencies",
  "Deployments",
  "GitHub data",
] as const;

export type Section = (typeof SECTIONS)[number];
