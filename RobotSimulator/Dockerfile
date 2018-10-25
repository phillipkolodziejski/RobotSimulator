FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY RobotSimulator/RobotSimulator.csproj RobotSimulator/
COPY RobotSimulator/instructions.txt instructions.txt
RUN dotnet restore RobotSimulator/RobotSimulator.csproj
COPY . .
WORKDIR /src/RobotSimulator
COPY RobotSimulator/instructions.txt /app/instructions.txt
RUN dotnet build RobotSimulator.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish RobotSimulator.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RobotSimulator.dll"]
