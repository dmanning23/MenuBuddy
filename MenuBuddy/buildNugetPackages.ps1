rm *.nupkg
nuget pack .\MenuBuddy.nuspec -IncludeReferencedProjects -Prop Configuration=Release
nuget push -source https://www.nuget.org -NonInteractive *.nupkg