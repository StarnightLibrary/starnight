dotnet pack -o build src/Starnight.Caching.Providers --version-suffix "pr.$1" --include-source --include-symbols
dotnet pack -o build src/Starnight.Internal --version-suffix "pr.$1" --include-source --include-symbols
dotnet pack -o build src/Starnight.Caching --version-suffix "pr.$1" --include-source --include-symbols
dotnet pack -o build src/Starnight --version-suffix "pr.$1" --include-source --include-symbols
