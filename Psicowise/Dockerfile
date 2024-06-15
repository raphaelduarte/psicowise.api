# See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Psicowise/Psicowise.csproj", "Psicowise/"]
RUN dotnet restore "Psicowise/Psicowise.csproj"
COPY . .
WORKDIR "/src/Psicowise"
RUN dotnet build "Psicowise.csproj" -c Release -o /app/build

# Stage 2: Publish the application
FROM build AS publish
RUN dotnet publish "Psicowise.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Create the final image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
EXPOSE 49999
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Psicowise.dll"]
