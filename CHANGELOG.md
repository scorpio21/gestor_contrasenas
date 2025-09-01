# Changelog

Todas las modificaciones relevantes de este proyecto se documentarán en este archivo.

## [Unreleased]

- Añadido: soporte High‑DPI en Windows Forms
  - `Program.cs`: `Application.SetHighDpiMode(HighDpiMode.SystemAware)`.
  - Formularios: `AutoScaleMode = Dpi` y `AutoScaleDimensions = new SizeF(96F, 96F)` en `UI/MainForm`, `UI/LoginForm`, `UI/AyudaForm`.
  - Verificación recomendada a 100/125/150/200% de escala.

## [v0.4.0] - 2025-09-01

- Añadido: opción "Recordar sesión" (cifrada con DPAPI) para iniciar sesión automáticamente en próximos arranques.
- Añadido: menú "Archivo > Cerrar sesión" que limpia el recuerdo de sesión y reinicia la app.
- Añadido: documentación en README sobre "Recordar sesión" y cierre de sesión.
- Cambiado: `Program.cs` para soportar autologin cuando existe una sesión guardada válida.
- Mantenimiento: ajustes menores en UI y organización de menús.

---

Guías de estilo y reglas: ver `docs/reglas.md`.
