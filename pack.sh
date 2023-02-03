mkdir -p build

for var in "$@"
do
	case $var in

		caching-providers)
			dotnet pack -o build -c Release src/Starnight.Caching.Providers --include-source --include-symbols -p:TreatWarningsAsErrors=false
			;;

		internal)
			dotnet pack -o build -c Release src/Starnight.Internal --include-source --include-symbols -p:TreatWarningsAsErrors=false
			;;

		caching)
			dotnet pack -o build -c Release src/Starnight.Caching --include-source --include-symbols -p:TreatWarningsAsErrors=false
			;;

		main)
			dotnet pack -o build -c Release src/Starnight --include-source --include-symbols -p:TreatWarningsAsErrors=false
			;;
	esac
done
