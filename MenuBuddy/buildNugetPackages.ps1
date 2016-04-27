nuget pack .\MenuBuddy.nuspec -IncludeReferencedProjects -Prop Configuration=Release
nuget push *.nupkg