param(
    [Parameter(Mandatory = $true)][string]$projectName,
    [Parameter(Mandatory = $true)][string]$projectType
)

##############################
# create folder for solution
##############################

New-Item -ItemType Directory -Path "$projectName"
Push-Location "$projectName"

##############################
# create solution structure
##############################

New-Item -ItemType Directory -Path src
New-Item -ItemType Directory -Path test
New-Item -ItemType Directory -Path samples
New-Item -ItemType Directory -Path docs
New-Item -ItemType Directory -Path tools

##############################
# download .gitignore
##############################

$url = "https://raw.githubusercontent.com/github/gitignore/master/VisualStudio.gitignore"
$file = ".gitignore"
Invoke-WebRequest -Uri $url -OutFile $file

##############################
# create README.md
##############################

"# $projectName" >> README.md

##############################
# create solution
##############################

dotnet new sln -n "$projectName"

##############################
# create initial project
##############################

Push-Location src

dotnet new "$projectType" -n "$projectName"

##############################
# create tests project
##############################

Pop-Location
Push-Location test

dotnet new xunit -n "$projectName.Tests"

##############################
# add projects to solution
##############################

Pop-Location

dotnet sln add "src\$projectName\$projectName.csproj"
dotnet sln add "test\$projectName.Tests\$projectName.Tests.csproj"

##############################
# init git
# see https://github.com/ArslanBilal/Git-Cheat-Sheet#git-flow
##############################

git flow init -d
