import { defineConfig } from "tsup";

export default defineConfig({
  entry: ["src/**/*.ts"],
  format: ["cjs", "esm"], // Build for commonJS and ESmodules
  dts: false, // Generate declaration file (.d.ts)
  splitting: true,
  treeshake: true,
  sourcemap: false,
  clean: true,
});