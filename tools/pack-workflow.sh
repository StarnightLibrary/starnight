if [ "$1" == "--pr" ]; then
	dotnet pack -o build -c Release src/Starnight.Caching.Providers --version-suffix "pr.$2" --include-source --include-symbols
	dotnet pack -o build -c Release src/Starnight.Internal --version-suffix "pr.$2" --include-source --include-symbols
	dotnet pack -o build -c Release src/Starnight.Caching --version-suffix "pr.$2" --include-source --include-symbols
	dotnet pack -o build -c Release src/Starnight --version-suffix "pr.$2" --include-source --include-symbols
else
	dotnet pack -o build -c Release src/Starnight.Caching.Providers --include-source --include-symbols -p:TreatWarningsAsErrors=false
	dotnet pack -o build -c Release src/Starnight.Internal --include-source --include-symbols -p:TreatWarningsAsErrors=false
	dotnet pack -o build -c Release src/Starnight.Caching --include-source --include-symbols -p:TreatWarningsAsErrors=false
	dotnet pack -o build -c Release src/Starnight --include-source --include-symbols -p:TreatWarningsAsErrors=false
fi
