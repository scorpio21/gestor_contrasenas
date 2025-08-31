# Reglas globales de codificación

> Nota: Algunas secciones (CSS/JS/Imágenes) aplican solo a proyectos web. Este repositorio es una app WinForms/.NET; mantener estas reglas como referencia transversal.

## Preferencias de patrones de codificación

- Utiliza siempre el idioma español en el código.
- Prefiere siempre soluciones simples.
- Procura utilizar los mismos patrones y tecnologías que ya están en uso en el proyecto.
- Evita la duplicación de código siempre que sea posible. Antes de crear una nueva función o clase, revisa si ya existe una lógica o funcionalidad similar en el proyecto.
- Realiza solo los cambios solicitados, o asegúrate de que estén bien entendidos y relacionados con la petición.
- Al corregir un error o bug, no introduzcas un nuevo patrón o tecnología sin antes agotar todas las opciones con la implementación existente. Si lo haces, elimina la implementación anterior para evitar lógica duplicada.
- Mantén el código muy limpio y bien organizado.
- Evita escribir scripts directamente en archivos si es posible, especialmente si el script solo se va a ejecutar una vez.
- Evita tener archivos con más de 200–300 líneas de código. Refactoriza cuando se alcance ese límite.
- Usa datos simulados (mocked data) solo para pruebas y únicamente dentro de una carpeta llamada “test”, nunca en producción.
- Siempre que sea posible, usa los MCPs para buscar funciones y no repetirlas.
- Actualiza siempre el README.md con los cambios que se hayan realizado.
- Siempre que realices cambios en el código, actualiza el GitHub y el archivo .gitignore.

## Organización de CSS (si aplica a proyectos web)

- Todo el CSS debe estar en archivos separados dentro de la carpeta `/css`.
- Nunca escribir CSS embebido en archivos PHP, HTML o en línea.
- El nombre del archivo CSS debe ser descriptivo y relacionado con el componente, página o funcionalidad que estiliza (ejemplo: `horarios.css`, `calendario.css`, `navbar.css`).
- Si un nuevo bloque de interfaz requiere estilos, crea un nuevo archivo CSS si no existe uno adecuado, y enlázalo desde el HTML o PHP.

## Organización de JavaScript (si aplica a proyectos web)

- Todo el JavaScript debe estar en archivos separados dentro de la carpeta `/js`.
- Nunca escribir JavaScript embebido en archivos PHP, HTML o en línea.
- El nombre del archivo JavaScript debe ser descriptivo y relacionado con el componente, página o funcionalidad (ejemplo: `horarios.js`, `calendario.js`, `navbar.js`).
- Si un nuevo bloque de interfaz requiere JavaScript, crea un nuevo archivo JavaScript si no existe uno adecuado, y enlázalo desde el HTML o PHP.

## Organización de imágenes (si aplica a proyectos web)

- Todo el contenido de imágenes debe estar en la carpeta `/img`.
- El nombre del archivo de imagen debe ser descriptivo y relacionado con el componente, página o funcionalidad (ejemplo: `logo.png`, `icono-usuario.svg`, `fondo-header.jpg`).
- Si un nuevo bloque de interfaz requiere imágenes, crea un nuevo archivo de imagen si no existe uno adecuado, y enlázalo desde el HTML o PHP.

## Buenas prácticas generales de programación

- Usa nombres descriptivos y coherentes para variables, funciones, clases y archivos. Sigue una convención clara: `camelCase` para variables y funciones, `PascalCase` para clases, `kebab-case` o `snake_case` para archivos según el lenguaje.
- Comenta el código solo cuando sea necesario para explicar lógica compleja o decisiones importantes. Mantén los comentarios actualizados y en español.
- Documenta cada función pública con una breve descripción de su propósito, parámetros y valor de retorno.
- Realiza commits frecuentes y con mensajes claros y descriptivos. No subas archivos generados automáticamente, contraseñas ni datos sensibles al repositorio.
- Nunca expongas credenciales, contraseñas ni claves API en el código fuente. Usa variables de entorno o archivos de configuración fuera del repositorio para datos sensibles.
- Implementa manejo de errores adecuado en todas las operaciones críticas (acceso a archivos, base de datos, red, etc.). No muestres mensajes de error detallados al usuario final en producción.
- Escribe pruebas automáticas para las funciones críticas del sistema. No mezcles código de pruebas con el de producción (usa siempre carpetas separadas).
- Asegúrate de que la interfaz sea accesible para todos los usuarios (contraste, etiquetas, navegación con teclado). Prioriza la experiencia de usuario en todas las decisiones de diseño.

## Buenas prácticas específicas por tecnología

### PHP (si aplica a proyectos PHP)

- Usa siempre `<?php ... ?>` y evita el uso de etiquetas cortas.
- Utiliza tipado estricto (`declare(strict_types=1);`) cuando sea posible.
- Prefiere funciones y clases en vez de código suelto en archivos PHP.
- Escapa siempre las variables al imprimir en HTML (`htmlspecialchars`).
- Usa namespaces y autoloading si el proyecto crece.
- Separa la lógica de negocio de la presentación (MVC o similar).

### JavaScript (si aplica a proyectos web)

- Usa `let` y `const` en vez de `var`.
- Prefiere funciones de flecha (`=>`) para callbacks y funciones cortas.
- Usa módulos (`import/export`) si el entorno lo permite.
- Mantén el código JS separado del HTML y CSS.
- Valida y sanitiza siempre los datos que provienen del usuario o de la red.

### Frameworks modernos (React, Vue, Angular, Laravel, Symfony, etc.)

- Sigue las convenciones y estructura recomendada por el framework.
- No modifiques archivos del core del framework.
- Usa componentes reutilizables y desacoplados.
- Mantén las dependencias actualizadas y revisa vulnerabilidades.
- Aprovecha las herramientas de testing y linting que ofrece el framework.

### .NET

- Usa siempre `using` para referenciar espacios de nombres.
- Habilita Nullable Reference Types cuando sea posible (`<Nullable>enable</Nullable>` en el `.csproj`).
- Prefiere clases y servicios frente a lógica suelta en formularios; evita lógica en archivos del diseñador.
- Separa la lógica de negocio de la UI (servicios, repositorios, capas `Dominio/Servicios/Datos`).
- Implementa pruebas en `test/` y no mezcles con producción.
- No expongas información sensible; usa configuración segura.
