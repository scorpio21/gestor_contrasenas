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
  - `bin/Release/net8.0-windows/gestor_contraseñas.exe`

## Compilación

```powershell
# Restaurar y compilar
dotnet build -c Release
```

## Estructura del proyecto

- `Dominio/` Clases de dominio (`Usuario`, `EntradaContrasena`).
- `Datos/` Repositorios (MySQL).
- `Seguridad/` Servicios de cifrado y autenticación.
- `Servicios/` Lógica de negocio (auth y gestor de contraseñas).
- `UI/` Formularios WinForms.
- `scripts/` Scripts de ayuda.

## Seguridad

- No subas credenciales reales al repositorio.
- Usa `.env` para secretos locales.
- Considera rotar credenciales si se expusieron previamente.

## Desarrollo

- Estilo de código: español en nombres y comentarios; soluciones simples.
- No incluir artefactos de compilación en Git (`bin/`, `obj/`, `.vs/`).
- Evita duplicación y mantiene responsabilidades separadas.

## Licencia

Especifica aquí la licencia si aplica.
