# ===========================================================================================
# Este script tem a finalidade de gerar um relatório de cobertura de testes da aplicação
# ===========================================================================================

# Instalando reportgenerator caso necessário
Write-Host "Verificando e instalando ferramenta reportgenerator"
dotnet tool install --global dotnet-reportgenerator-globaltool | Out-Null

# Removendo pastas usadas no processo
Remove-Item -Recurse -Force coverage -ErrorAction SilentlyContinue
Remove-Item -Recurse -Force coverage-report -ErrorAction SilentlyContinue

# Executando testes com coleta de dados de cobertura e gerando relatório
dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings --results-directory coverage
reportgenerator -reports:"**\coverage.cobertura.xml" -targetdir:"coverage-report" -reporttypes:"HTML;HTMLSummary"

# Removendo pasta temporária utilizada
Remove-Item -Recurse -Force coverage -ErrorAction SilentlyContinue

# Abrindo relatório no navegador
Start-Process coverage-report\index.htm
