# Gestor de Contraseñas

[![.NET Build](https://github.com/scorpio21/gestor_contrasenas/actions/workflows/dotnet.yml/badge.svg)](https://github.com/scorpio21/gestor_contrasenas/actions/workflows/dotnet.yml)

Aplicación de escritorio (Windows Forms, .NET 8) para gestionar contraseñas de forma segura.

## Reglas del proyecto

Consulta las normas y convenciones de desarrollo en `docs/reglas.md`:

- [Reglas globales de codificación](docs/reglas.md)

## Requisitos

- .NET SDK 8.0
- MySQL (o servidor compatible)

## Configuración

1. Crear archivo `.env` en la raíz del proyecto con la cadena de conexión:

   ```env
   GESTOR_DB_CONN=Server=127.0.0.1;Port=3306;Database=gestor;Uid=root;Pwd=;SslMode=None;Charset=utf8mb4;
   ```

   - Ajusta `Server/Database/Uid/Pwd` según tu entorno.
   - `.env` ya está ignorado por Git.

2. La app carga automáticamente las variables de `.env` en `Program.cs` usando `DotNetEnv.Env.Load()`.

## Ejecución

- Desde PowerShell:

```powershell
dotnet run -c Release
```

- Binarios:
  - `bin/Release/net8.0-windows7.0/gestor_contraseñas.exe`

## Compilación

```powershell
# Restaurar y compilar
dotnet build -c Release
```

## Pruebas

```powershell
dotnet test -c Release test/GestorContrasenas.Tests/GestorContrasenas.Tests.csproj \
  --logger "trx;LogFileName=test_results.trx" \
  --collect:"XPlat Code Coverage" \
  --results-directory TestResults
```

### Notas para tests unitarios

- El servicio `Servicios/GestorContrasenasService.cs` admite inyección de dependencias del repositorio mediante la interfaz `Datos/IEntradasRepositorio`.
- El repositorio por defecto es `Datos/MySqlRepositorio.cs`, que implementa `IEntradasRepositorio` y requiere la variable `GESTOR_DB_CONN`.
- Para pruebas, se puede usar un repositorio en memoria (ver `test/GestorContrasenas.Tests/GestorContrasenasServiceTests.cs`) para evitar tocar MySQL real.

## Integración continua (GitHub Actions)

- Workflow: `.github/workflows/dotnet.yml` (badge arriba).
- Ejecuta: restore, verificación de formato (`dotnet format --verify-no-changes`), build, tests y publica artefactos (binlog, TRX, cobertura Cobertura).
- El runner es Windows; se configura `core.autocrlf=true` y `core.eol=crlf` para finales de línea consistentes.

## Estructura del proyecto

- `Dominio/` Clases de dominio (`Usuario`, `EntradaContrasena`).
- `Datos/` Repositorios (MySQL) y contrato `IEntradasRepositorio` para facilitar pruebas.
- `Seguridad/` Servicios de cifrado y autenticación.
- `Servicios/` Lógica de negocio (auth y gestor de contraseñas).
- `UI/` Formularios WinForms.
- `scripts/` Scripts de ayuda.

## Seguridad

- No subas credenciales reales al repositorio.
- Usa `.env` para secretos locales.
- Considera rotar credenciales si se expusieron previamente.

### Cifrado y versión de derivación de clave

- El cifrado usa AES-GCM con formato versionado: `[version][salt][nonce][cipher][tag]` codificado en Base64.
- Versiones soportadas:
  - v1: PBKDF2-SHA256 (compatibilidad hacia atrás para secretos existentes).
  - v2: Argon2id (por defecto para nuevos cifrados). Requiere el paquete `Konscious.Security.Cryptography.Argon2`.
- Los datos cifrados con v1 siguen descifrando correctamente; los nuevos usos generan v2 automáticamente.

### Auto-lock e higiene de portapapeles (UI)

- La aplicación se bloquea automáticamente tras 5 minutos de inactividad y pide reautenticación. Al reautenticar, se mantiene el mismo usuario y se actualiza la clave maestra de la sesión.
- Al copiar una contraseña, se limpia el portapapeles automáticamente a los 20 segundos (si no cambió el contenido) y también al producirse el auto-lock.
- Estos comportamientos están implementados en `UI/MainForm.cs` usando `autoLockTimer` y `clipboardTimer`.

### Búsqueda y filtrado en el listado (UI)

- Se añadió una caja de búsqueda encima del formulario para filtrar en vivo por servicio, usuario o URL de login.
- Implementación en `UI/MainForm.Designer.cs` (controles `lblBuscar`, `txtBuscar`) y `UI/MainForm.cs` (método `txtBuscar_TextChanged`).

## Desarrollo

- Estilo de código: español en nombres y comentarios; soluciones simples.
- No incluir artefactos de compilación en Git (`bin/`, `obj/`, `.vs/`).
- Evita duplicación y mantiene responsabilidades separadas.

## Licencia

Especifica aquí la licencia si aplica.
