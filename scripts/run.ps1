param(
    [string]$Conn
)

# Script de arranque: establece GESTOR_DB_CONN (si no existe) y ejecuta la app en Release.
# Uso:
#   powershell -ExecutionPolicy Bypass -File scripts/run.ps1
#   powershell -ExecutionPolicy Bypass -File scripts/run.ps1 -Conn "Server=127.0.0.1;Port=3306;Database=gestor;Uid=root;Pwd=;SslMode=None;"
#   (o crear un archivo GESTOR_DB_CONN.txt en la raíz del proyecto con la cadena de conexión)

if (-not $env:GESTOR_DB_CONN -or [string]::IsNullOrWhiteSpace($env:GESTOR_DB_CONN)) {
    if ($Conn) {
        $env:GESTOR_DB_CONN = $Conn
        Write-Host "[OK] GESTOR_DB_CONN establecido desde parámetro." -ForegroundColor Green
    }
    elseif (Test-Path -LiteralPath "./GESTOR_DB_CONN.txt") {
        $env:GESTOR_DB_CONN = Get-Content -LiteralPath "./GESTOR_DB_CONN.txt" -Raw
        Write-Host "[OK] GESTOR_DB_CONN cargado desde GESTOR_DB_CONN.txt." -ForegroundColor Green
    }
    else {
        Write-Host "[ERROR] Falta la variable GESTOR_DB_CONN." -ForegroundColor Red
        Write-Host "       Proporciónala con -Conn \"...\" o crea GESTOR_DB_CONN.txt en la raíz del proyecto." -ForegroundColor Yellow
        exit 1
    }
}

# Ejecutar la aplicación
Write-Host "Ejecutando app en Release..." -ForegroundColor Cyan
& dotnet run -c Release
