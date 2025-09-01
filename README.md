# Gestor de Contraseñas

[![.NET Build](https://github.com/scorpio21/gestor_contrasenas/actions/workflows/dotnet.yml/badge.svg)](https://github.com/scorpio21/gestor_contrasenas/actions/workflows/dotnet.yml)

Aplicación de escritorio (Windows Forms, .NET 8) para gestionar contraseñas de forma segura.

## Reglas del proyecto

Consulta las normas y convenciones de desarrollo en `docs/reglas.md`:

- [Reglas globales de codificación](docs/reglas.md)

## Requisitos

- .NET SDK 8.0
- MySQL (o servidor compatible)

## Changelog

- Ver historial de cambios en [`CHANGELOG.md`](CHANGELOG.md)

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

## Soporte High‑DPI

- La aplicación está preparada para pantallas de alta densidad:
  - `Program.cs`: `Application.SetHighDpiMode(HighDpiMode.SystemAware)`.
  - Formularios con `AutoScaleMode = Dpi` y `AutoScaleDimensions = new SizeF(96F, 96F)`.
- Verificación manual recomendada (Windows > Configuración > Pantalla > Escala):
  - 100%, 125%, 150%, 200%.
  - Formularios a revisar: `UI/MainForm`, `UI/LoginForm`, `UI/AyudaForm`.
  - Comprobar que no haya solapes/cortes y que los menús/controles respeten anclajes y tamaños mínimos.

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

### Migraciones y validación de esquema (MySQL)

- La app asegura y valida el esquema mínimo al inicio usando `Datos/Migraciones.cs`.
- Puntos de invocación:
  - `Program.cs`: intenta `Migraciones.AsegurarEsquema` si `GESTOR_DB_CONN` está configurada.
  - `Datos/MySqlRepositorio.cs`: vuelve a asegurar el esquema en su constructor para entornos donde no pase por `Program.cs`.
- El esquema base se corresponde con `usuarios` y `entradas` (columnas principales y FK), alineado con `Datos/crear_base_gestor.sql`.

## Cambios recientes (UI)

- Corrección en ventana de Ayuda (`UI/AyudaForm.*`):
  - Se elimina el `SplitterDistance` del diseñador y se calcula de forma segura tras aplicar el layout, respetando `Panel1MinSize`/`Panel2MinSize`.
  - MinSize se establecen durante `Load` diferido para evitar `InvalidOperationException` en `EndInit`.
  - Manejo de errores y logging al abrir/cargar la Ayuda para mostrar mensajes claros y registrar detalles en el log.

- Alineación y anclajes para evitar solapes al redimensionar (`UI/MainForm.Designer.cs`):
  - Controles de fortaleza (`lblFortaleza`, `pnlFortaleza`, `pnlFortalezaValor`) con Anchor = Top | Left.
  - Bloque de búsqueda (`lblBuscar`, `txtBuscar`) anclados en Top | Left y reposicionados para no solapar el listado.
  - Botones inferiores anclados en Top | Left.
  - Espaciado horizontal uniforme en la fila de acciones: `Agregar`, `Eliminar`, `Copiar`, `Refrescar`, `Importar CSV`, `Exportar CSV`.
  - Botones `Importar seguro` y `Exportar seguro` movidos a una fila inferior para evitar solape.

- Corrección de warning en `UI/GeneradorForm.*`:
  - Se inicializó el checkbox `chkaz` (rango "a-z") en `UI/GeneradorForm.Designer.cs` y se agregó al tab de contraseña.
  - Esto elimina el aviso de campo no asignado y habilita la opción de minúsculas en el generador.

### Verificación automática de contraseñas comprometidas (UI/Servicios)

- Nueva columna `Comprometida` en el listado principal que indica si la contraseña aparece en filtraciones públicas (verde "No", rojo "Sí (conteo)").
- La comprobación se realiza automáticamente al seleccionar una fila o al revelar la contraseña.
- Privacidad: se usa la API Pwned Passwords (Have I Been Pwned) con k-anonimato; nunca se envía la contraseña ni el hash completo, solo el prefijo del SHA‑1.
- Implementación:
  - Servicio: `Servicios/GestorContrasenasService.cs` (`EstaComprometidaAsync`).
  - UI: `UI/MainForm.Designer.cs` (columna `colComprometida`) y `UI/MainForm.cs` (métodos `ComprobarComprometidaAsync`, integración en selección y al revelar).

#### Exportar comprometidas a CSV (Archivo > Exportar comprometidas a CSV)

- Permite generar un CSV solo con las entradas cuya contraseña aparece comprometida, sin incluir las contraseñas en el archivo.
- Columnas exportadas: `name,url,username,comprometida,veces`.
- La comprobación usa la API Pwned Passwords con k-anonimato; se respeta el rate limit.
- Implementación:
  - Menú: `UI/MainForm.Designer.cs` (ítem `Exportar comprometidas a CSV`).
  - Lógica: `UI/MainForm.cs` (`exportarComprometidasToolStripMenuItem_Click`).

#### Menú contextual en entradas comprometidas

- Clic derecho sobre una fila comprometida muestra la opción “Abrir sitio para cambiar contraseña”.
- Abre la `LoginUrl` de la entrada en el navegador (se fuerza `https://` si no está presente).
- La opción se habilita solo si:
  - La columna `Comprometida` de la fila indica “Sí”.
  - La entrada tiene `LoginUrl` configurada.
- Implementación:
  - UI: `UI/MainForm.Designer.cs` (`contextMenuEntradas`, `abrirSitioCambiarToolStripMenuItem`).
  - Lógica: `UI/MainForm.cs` (`contextMenuEntradas_Opening`, `abrirSitioCambiarToolStripMenuItem_Click`).

#### Editar y guardar cambios de entradas

- Al seleccionar una fila del listado, los campos de edición se rellenan automáticamente:
  - `Servicio` (`txtServicio`)
  - `Usuario` (`txtUsuario`)
  - `LoginUrl` (`txtLoginUrl`)
- Tras modificar los campos, usa el botón `Guardar cambios` para actualizar la entrada seleccionada.
- Si dejas la `Contraseña` en blanco al guardar, se preserva la contraseña actual de la entrada (no se modifica).
- Implementación:
  - UI: `UI/MainForm.Designer.cs` (botón `btnGuardar`).
  - Lógica: `UI/MainForm.cs` (`lvEntradas_SelectedIndexChanged` para rellenar campos, `btnGuardar_Click` para actualizar con `GestorContrasenasService.Actualizar`).

#### Ajustes y rendimiento

- En la UI hay un checkbox: "Comprobar comprometida automáticamente" (`chkHibpAuto`).
  - Si se desactiva, la columna muestra `-` y no se realizan llamadas.
- Rate limit simple configurable en `GestorContrasenasService.HibpIntervaloMinimoMs` (por defecto 200 ms entre llamadas).

## Seguridad

- No subas credenciales reales al repositorio.
- Usa `.env` para secretos locales.
- Considera rotar credenciales si se expusieron previamente.

### Logging y manejo de errores

- La app registra eventos y errores de forma discreta (sin secretos) en:
  - `%LOCALAPPDATA%/gestor_contrasenas/logs/app.log`
- Manejadores globales configurados en `Program.cs`:
  - `Application.ThreadException`
  - `AppDomain.CurrentDomain.UnhandledException`
- Utilidad de logging: `Seguridad/Logger.cs` (`Info`, `Warn`, `Error`).

### Cifrado y versión de derivación de clave

- El cifrado usa AES-GCM con formato versionado: `[version][salt][nonce][cipher][tag]` codificado en Base64.
- Versiones soportadas:
  - v1: PBKDF2-SHA256 (compatibilidad hacia atrás para secretos existentes).
  - v2: Argon2id (por defecto para nuevos cifrados). Requiere el paquete `Konscious.Security.Cryptography.Argon2`.
- Los datos cifrados con v1 siguen descifrando correctamente; los nuevos usos generan v2 automáticamente.

## Inicio de sesión (UX)

- El inicio de sesión solicita solo email y contraseña.
- La clave maestra se recupera automáticamente desde la base de datos (almacenada cifrada con la contraseña del usuario) al iniciar sesión.
- Si el usuario aún no tiene clave maestra guardada, se muestra un formulario modal para configurarla y esta queda almacenada para futuras sesiones.
- Implementación:
  - Servicio: `Servicios/AuthService.cs` (`LoginYObtenerClave`, `ActualizarClaveMaestra`).
  - UI: `UI/LoginForm.cs` (invoca recuperación/creación) y `UI/ConfigurarClaveForm.*` (captura y confirmación de clave maestra).

### Recordar sesión (UX)

- En la pantalla de inicio hay un checkbox "Recordar sesión". Si se marca, al iniciar sesión se guarda (cifrada con DPAPI del usuario Windows) la sesión localmente.
- En el próximo arranque, si existe una sesión guardada válida, la app entra directamente a `MainForm` sin pedir credenciales.
- Cómo cerrar sesión y limpiar el recuerdo:
  - Menú `Archivo > Cerrar sesión` en `MainForm` elimina el archivo de sesión y reinicia la aplicación para volver a `LoginForm`.
- Implementación:
  - Utilidad: `Seguridad/RecordarSesion.cs` (métodos `Guardar`, `Cargar`, `Limpiar`).
  - UI: `UI/LoginForm.Designer.cs` (checkbox `chkRecordar`) y `UI/LoginForm.cs` (usa `RecordarSesion.Guardar` tras login exitoso).
  - Inicio: `Program.cs` (si `RecordarSesion.Cargar()` retorna datos, abre `MainForm` directamente).

## Ayuda (F1)

- Menú `Ayuda > Ver ayuda` o pulsando `F1` abre una ventana con índice y contenido de ayuda.
- Los documentos se cargan desde `docs/ayuda/` (formato Markdown `.md`).
- También está disponible `Ayuda > Acerca de…` con versión y enlace al repositorio.

### Auto-lock e higiene de portapapeles (UI)

- La aplicación se bloquea automáticamente tras 5 minutos de inactividad y pide reautenticación. Al reautenticar, se mantiene el mismo usuario y se actualiza la clave maestra de la sesión.
- Al copiar una contraseña, se limpia el portapapeles automáticamente a los 20 segundos (si no cambió el contenido) y también al producirse el auto-lock.
- Estos comportamientos están implementados en `UI/MainForm.cs` usando `autoLockTimer` y `clipboardTimer`.

### Búsqueda y filtrado en el listado (UI)

- Se añadió una caja de búsqueda encima del formulario para filtrar en vivo por servicio, usuario o URL de login.
- Implementación en `UI/MainForm.Designer.cs` (controles `lblBuscar`, `txtBuscar`) y `UI/MainForm.cs` (método `txtBuscar_TextChanged`).

### Medidor de fortaleza y verificación de reutilización (UI/Servicios)

- Al escribir la contraseña en `txtSecreto`, se muestra un medidor de fortaleza y una descripción: Muy débil, Débil, Media, Fuerte, Muy fuerte.
- Si la contraseña ya existe en otra entrada, se muestra una advertencia de reutilización.
- Implementación:
  - Servicio: `Servicios/GestorContrasenasService.cs` (`CalcularFortaleza`, `ExisteReutilizacionSecreto`).
  - UI: `UI/MainForm.Designer.cs` (controles `lblFortaleza`, `prgFortaleza`, `lblReutilizacion`) y `UI/MainForm.cs` (`txtSecreto_TextChanged`).

### Exportar/Importar seguro (JSON cifrado)

- Exporta todas las entradas a un archivo cifrado (`.gpass`) usando la clave maestra. No se escriben contraseñas en claro en disco.
- Importa desde un archivo cifrado previamente exportado, validando integridad y agregando entradas.
- Implementación:
  - Servicio: `Servicios/GestorContrasenasService.cs` (`ExportarJsonCifrado`, `ImportarJsonCifrado`).
  - UI: `UI/MainForm.Designer.cs` (botones `btnExportarSeguro`, `btnImportarSeguro`) y `UI/MainForm.cs` (handlers correspondientes).

## Desarrollo

- Estilo de código: español en nombres y comentarios; soluciones simples.
- No incluir artefactos de compilación en Git (`bin/`, `obj/`, `.vs/`).
- Evita duplicación y mantiene responsabilidades separadas.

## Licencia

Especifica aquí la licencia si aplica.
