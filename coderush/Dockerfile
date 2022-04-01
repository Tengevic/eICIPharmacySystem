#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ["coderush/coderush.csproj", "coderush/"]
RUN dotnet restore "coderush/coderush.csproj"
COPY . .
WORKDIR "/src/coderush"
RUN dotnet build "coderush.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "coderush.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "coderush.dll"]