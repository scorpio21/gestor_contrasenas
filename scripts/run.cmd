@echo off
setlocal enabledelayedexpansion

REM Script de arranque para Windows (doble clic).
REM 1) Lee GESTOR_DB_CONN de GESTOR_DB_CONN.txt si existe
REM 2) Si no existe, usa variable de entorno ya definida
REM 3) Ejecuta la app en Release

set ROOT=%~dp0..
cd /d "%ROOT%"

if exist "GESTOR_DB_CONN.txt" (
  for /f "usebackq delims=" %%A in ("GESTOR_DB_CONN.txt") do set GESTOR_DB_CONN=%%A
  echo [OK] GESTOR_DB_CONN cargado desde GESTOR_DB_CONN.txt
) else (
  if not defined GESTOR_DB_CONN (
    echo [ERROR] Falta GESTOR_DB_CONN. Crea GESTOR_DB_CONN.txt en la raiz del proyecto o define la variable.
    echo Ejemplo: Server=127.0.0.1;Port=3306;Database=gestor;Uid=root;Pwd=;SslMode=None;
    exit /b 1
  )
)

echo Ejecutando app en Release...
call dotnet run -c Release

endlocal
