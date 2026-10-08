import { describe, expect, it } from "vitest";
import { render, screen, within } from "@testing-library/react";
import Home from "@/app/page";
import { SECTIONS } from "@/lib/sections";

describe("Home page", () => {
  it("shows the RepoVue heading", () => {
    render(<Home />);

    expect(screen.getByRole("heading", { level: 1, name: "RepoVue" })).toBeInTheDocument();
  });

  it("lists all eight dashboard sections", () => {
    render(<Home />);

    const list = screen.getByRole("list", { name: "Dashboard sections" });
    const items = within(list).getAllByRole("listitem").map((item) => item.textContent);
    expect(items).toEqual([...SECTIONS]);
    expect(items).toHaveLength(8);
  });
});
