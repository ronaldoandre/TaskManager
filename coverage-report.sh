#!/bin/bash
# ===========================================================================================
# Este script tem a finalidade de gerar um relatório de cobertura de testes da aplicação
# ===========================================================================================

# Instalando reportgenerator caso necessário
echo "Verificando e instalando ferramenta reportgenerator"
dotnet tool install --global dotnet-reportgenerator-globaltool >/dev/null

# Removendo pastas usadas no processo
rm -rf coverage
rm -rf coverage-report

# Executando testes com coleta de dados de cobertura e gerando relatório
dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings --results-directory coverage
reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"coverage-report" -reporttypes:"HTML;HTMLSummary"

# Removendo pasta temporária utilizada
rm -rf coverage

# Abrindo relatório no navegador
powershell.exe -c coverage-report/index.htm
