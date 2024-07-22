# FROM: Specifies the base image to use for this stage.
# mcr.microsoft.com/dotnet/sdk:8.0: This is the .NET SDK image for .NET 8.0. It contains all the tools needed to build .NET applications.
# AS build: Names this stage "build". This name is used later to refer to this stage when copying files.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# WORKDIR: Sets the working directory for the following instructions. If the directory does not exist, it will be created.
# /src: This is the directory within the container where the application code will be placed and where commands will be executed.
WORKDIR /src

# COPY: Copies files or directories from the host machine to the container.
# BlogSystem.csproj .: Copies the BlogSystem.csproj file from the current directory on the host to the working directory (/src) in the container.
COPY BlogSystem.csproj .

# RUN: Executes a command inside the container.
# dotnet restore: Restores the NuGet packages specified in the BlogSystem.csproj file.
RUN dotnet restore

# COPY: Copies the rest of the application code from the host machine to the container’s working directory (/src).
# . (first dot): Refers to the current directory on the host machine.
# . (second dot): Refers to the working directory in the container.
COPY . .

# RUN: Executes a command inside the container.
# dotnet publish -c release -o /app: Builds and publishes the .NET application.
# -c release: Specifies the configuration to use for building the application (Release mode).
# -o /app: Specifies the output directory where the compiled application will be placed.
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0

# FROM: Specifies the base image for this stage.
# mcr.microsoft.com/dotnet/aspnet:8.0: This is the .NET runtime image for .NET 8.0. It contains the runtime environment necessary to run .NET applications, but not the build tools.
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# WORKDIR: Sets the working directory for the following instructions.
# /app: This is the directory in the container where the application files will be placed.
WORKDIR /app

# COPY --from=build: Copies files from the stage named "build" (from the previous stage).
# /app: This is the directory in the "build" stage where the published application files are located.
# .: Copies these files to the working directory (/app) in the current stage.
COPY --from=build /app .

# ENTRYPOINT: Defines the command that will be run when a container starts.
# ["dotnet", "BlogSystem.dll"]: Specifies that the container should run the dotnet command with BlogSystem.dll as the argument, which launches the .NET application.
ENTRYPOINT ["dotnet", "BlogSystem.dll"]