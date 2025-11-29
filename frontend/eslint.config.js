// @ts-check
import eslint from "@eslint/js";
import tsPlugin from "@typescript-eslint/eslint-plugin";
import tsParser from "@typescript-eslint/parser";
import importPlugin from "eslint-plugin-import";
import unusedImports from "eslint-plugin-unused-imports";

export default [
  {
    files: ["src/**/*.{ts,tsx}"],
    languageOptions: {
      parser: tsParser,
      parserOptions: {
        ecmaVersion: 2022,
        sourceType: "module",
      },
    },
    plugins: {
      "@typescript-eslint": tsPlugin,
      "unused-imports": unusedImports,
      import: importPlugin,
    },
    rules: {
      ...eslint.configs.recommended.rules,
      // Style
      "max-len": [
        "error",
        {
          code: 150,
          tabWidth: 2,
          ignoreUrls: true,
          ignoreStrings: true,
          ignoreTemplateLiterals: true,
          ignoreComments: false,
        },
      ],
      // Unused imports / vars (lint must fail)
      "unused-imports/no-unused-imports": "error",
      "unused-imports/no-unused-vars": [
        "error",
        {
          vars: "all",
          varsIgnorePattern: "^_",
          args: "after-used",
          argsIgnorePattern: "^_",
        },
      ],
      "@typescript-eslint/no-unused-vars": "off",
      // Disable some rules that might conflict
      "no-undef": "off",
      "no-redeclare": "off",
      // Forbid relative parent imports like "../" and enforce absolute paths
      "import/no-relative-parent-imports": "error",
    },
  },
  {
    files: ["src/**/*.html"],
    rules: {
      // HTML files don't need TypeScript rules
    },
  },
];
