directory=$1

echo "Searching in: $directory"

for project in $(find $directory -name '*.csproj')
do
    echo "testing $project"
    dotnet test $project
done