# EzPass

EzPass is a small WPF desktop utility for generating memorable, word-based passwords. It builds
a password from random dictionary words, optionally swaps letters for look-alike symbols
(`a` → `@`, `e` → `3`, etc.) and appends a random numeric suffix, then shows a phonetic
("Alpha Bravo Charlie...") read-out of the result to make it easy to read aloud over the phone.

## Features

- **Password generation** - combines random words from a built-in wordlist into a single
  password, with optional numeric suffix and symbol substitution.
- **Phonetic read-out** - converts the generated password into the NATO phonetic alphabet so
  it can be read out clearly over a call.
- **Bulk generator** - produces a batch of ten passwords at once (*Extra Tools → Bulk generator*).
- **Wordlist editor** - view, edit and save the wordlist used for generation
  (*Extra Tools → See wordlist*). The active wordlist is persisted per-user under
  `HKCU\SOFTWARE\EzPass` in the registry.
- **SecureNote** - optionally uploads the current password to Evolve Technologies' internal
  SecureNote service and copies the resulting one-time link to the clipboard
  (*Extra Tools → Create SecureNote*).

## Requirements

- Windows
- Visual Studio 2019/2022 with the ".NET desktop development" workload, **or** MSBuild + NuGet
  on the command line
- .NET Framework 4.7.2 (Developer Pack)

## Building

EzPass.csproj is a legacy (non-SDK-style) project using `packages.config` for its NuGet
dependencies, so it's restored and built with classic MSBuild/NuGet rather than the `dotnet` CLI.

```powershell
nuget restore EzPass.sln
msbuild EzPass.sln /p:Configuration=Release /p:Platform="Any CPU"
```

Or simply open `EzPass.sln` in Visual Studio and build/run from there (NuGet packages restore
automatically on first build).

The build uses [Fody](https://github.com/Fody/Fody)/[Costura](https://github.com/Fody/Costura) to
embed all dependencies into a single `EzPass.exe`, so the release output is a standalone
executable with no accompanying DLLs to distribute.

CI (`.github/workflows/dotnet-desktop.yml`) builds both Debug and Release configurations on every
push/PR to `master` and uploads the build output as a workflow artifact.

## Notes on password strength

The default password is two random dictionary words plus an optional three-digit numeric suffix.
This is convenient to read/type but yields a relatively small combination space - if you need a
password with meaningfully more entropy, increase the word count (`PasswordGenerator.New`'s
`wordCount` parameter) or combine it with your organisation's password manager rather than relying
on the default output alone for high-value accounts.
