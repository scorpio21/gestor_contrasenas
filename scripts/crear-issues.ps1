#requires -Version 5.1
<#!
Script: scripts/crear-issues.ps1
Propósito: Crear etiquetas e issues del backlog (UI/UX/Testing/Accesibilidad/Seguridad) usando GitHub CLI (gh).
Requisitos: GitHub CLI autenticado (gh auth login) y repositorio remoto configurado.
!#>

$ErrorActionPreference = 'Stop'

function EsComando($nombre) {
  $cmd = Get-Command $nombre -ErrorAction SilentlyContinue
  return $null -ne $cmd
}

Write-Host "Validando entorno..." -ForegroundColor Cyan
if (-not (EsComando 'gh')) {
  throw 'GitHub CLI (gh) no está instalado o no está en PATH. Instálalo desde https://cli.github.com/'
}

# Verificar autenticación
try {
  gh auth status | Out-Null
} catch {
  throw 'No estás autenticado en gh. Ejecuta: gh auth login'
}

# Verificar repositorio actual
try {
  $repo = (gh repo view --json nameWithOwner | ConvertFrom-Json).nameWithOwner
  if (-not $repo) { throw 'No se pudo determinar el repositorio.' }
  Write-Host "Repositorio detectado: $repo" -ForegroundColor Green
} catch {
  throw 'No se pudo validar el repositorio remoto. Asegúrate de estar en la carpeta del repo y con remoto configurado.'
}

# 1) Crear/actualizar etiquetas
Write-Host "Creando/actualizando etiquetas..." -ForegroundColor Cyan
$labels = @(
  @{ n = 'enhancement';   c = 'a2eeef'; d = 'Mejora de funcionalidad' },
  @{ n = 'UI';            c = 'c5def5'; d = 'Interfaz de usuario' },
  @{ n = 'UX';            c = 'fef2c0'; d = 'Experiencia de usuario' },
  @{ n = 'testing';       c = 'bfe5bf'; d = 'Pruebas y cobertura' },
  @{ n = 'accessibility'; c = 'bfdadc'; d = 'Accesibilidad' },
  @{ n = 'security';      c = 'e99695'; d = 'Seguridad' }
)
foreach ($l in $labels) {
  gh label edit $($l.n) --color $($l.c) --description $($l.d) 2>$null || 
  gh label create $($l.n) --color $($l.c) --description $($l.d) 2>$null
}

# 2) Definir issues (título, cuerpo, etiquetas)
Write-Host "Creando issues..." -ForegroundColor Cyan
$issues = @(
  @{ Title = 'UI: Normalizar márgenes, paddings y tamaños en `UI/MainForm`'; Labels=@('enhancement','UI','UX'); Body = @'
Objetivo
Unificar espaciados y alineaciones para una apariencia consistente.

Criterios de aceptación
- [ ] Márgenes 8–12 px consistentes en bloques principales.
- [ ] Alineación en grilla; sin superposición al redimensionar.
- [ ] Revisado en 100%, 125% y 150% de escala.

Notas
Ajustar en `UI/MainForm.Designer.cs`. Evitar posiciones absolutas dispares.
'@ }
  @{ Title = 'Layout: Migrar secciones a `TableLayoutPanel`/`FlowLayoutPanel`'; Labels=@('enhancement','UI'); Body = @'
Objetivo
Mejorar responsividad reemplazando ubicaciones absolutas por contenedores de layout.

Criterios de aceptación
- [ ] Acciones, listado y panel de detalles usan `TableLayoutPanel`/`FlowLayoutPanel` donde aplique.
- [ ] `Anchor` solo en casos necesarios; sin deformaciones al cambiar tamaño.
- [ ] Sin regresiones funcionales.

Notas
Actualizar `UI/MainForm.Designer.cs`. Mantener nombres y eventos existentes.
'@ }
  @{ Title = 'DPI/High-DPI: Activar y validar escalado'; Labels=@('enhancement','UI'); Body = @'
Objetivo
Garantizar nitidez y correcto escalado en 125–200% DPI.

Criterios de aceptación
- [ ] `AutoScaleMode` configurado a `Dpi` o `Font` según convenga.
- [ ] Fuentes y controles sin recortes a 125%, 150%, 200%.
- [ ] Iconos/imagenes nítidos (si aplica).

Notas
Revisar `UI/MainForm`, `UI/LoginForm`, `UI/AyudaForm`.
'@ }
  @{ Title = 'Tema visual: Claro/Oscuro y paleta coherente'; Labels=@('enhancement','UI','accessibility'); Body = @'
Objetivo
Añadir tema claro/oscuro coherente y contraste adecuado.

Criterios de aceptación
- [ ] Toggle de tema (persistente en sesión).
- [ ] Colores aplicados a controles base; contraste AA mínimo.
- [ ] Sin pérdida de accesibilidad en ambos temas.

Notas
Mantener cambios simples (sin frameworks); centralizar colores en una clase utilitaria.
'@ }
  @{ Title = 'Accesibilidad: Aceleradores, TabIndex y nombres accesibles'; Labels=@('enhancement','accessibility','UX'); Body = @'
Objetivo
Mejorar navegación por teclado y accesibilidad.

Criterios de aceptación
- [ ] Aceleradores (&) en menús/botones principales.
- [ ] `TabIndex` ordenado y consistente.
- [ ] `AccessibleName`/`AccessibleDescription` en campos clave.

Notas
Aplicar en `UI/MainForm`, `UI/LoginForm`.
'@ }
  @{ Title = 'ListView: Autosize de columnas y ordenación por encabezado'; Labels=@('enhancement','UI','UX'); Body = @'
Objetivo
Mejorar lectura y navegación del listado.

Criterios de aceptación
- [ ] Click en encabezado ordena asc/desc y muestra indicador.
- [ ] Columnas autoajustan ancho al contenido/ventana.
- [ ] Persistir anchos al reiniciar (opcional).

Notas
Modificar `UI/MainForm` manejo del `ListView`.
'@ }
  @{ Title = 'ListView: Búsqueda y filtros rápidos'; Labels=@('enhancement','UX'); Body = @'
Objetivo
Facilitar encontrar entradas por texto y estado.

Criterios de aceptación
- [ ] Cuadro de búsqueda filtra por servicio/usuario/notas.
- [ ] Filtros por fortaleza, reutilización y comprometidas (HIBP).
- [ ] Reseteo rápido de filtros.

Notas
Mantener lógica de filtro separada para testear.
'@ }
  @{ Title = 'Indicadores de seguridad en la lista'; Labels=@('enhancement','security','UI','UX'); Body = @'
Objetivo
Hacer visibles estados de seguridad por entrada.

Criterios de aceptación
- [ ] Ícono/badge para débil, reutilizada, comprometida.
- [ ] Tooltip con explicación breve y recomendación.
- [ ] Leyenda en la ayuda.

Notas
Requiere mapear resultados de `GestorContrasenasService`.
'@ }
  @{ Title = 'Importación/Exportación: Progreso, validaciones y mensajes'; Labels=@('enhancement','UX'); Body = @'
Objetivo
Mejorar UX y feedback en import/export.

Criterios de aceptación
- [ ] Diálogo con progreso y opción cancelar (si procede).
- [ ] Validación previa con resumen (conteo y campos problemáticos).
- [ ] Mensajes de éxito/error consistentes.

Notas
Revisar rutas en `Servicios/GestorContrasenasService.cs` y UI asociada.
'@ }
  @{ Title = 'HIBP: Feedback de estado y control'; Labels=@('enhancement','security','UX'); Body = @'
Objetivo
Dar visibilidad del estado de consulta HIBP.

Criterios de aceptación
- [ ] Barra de estado con “Consultando…/OK/Error”.
- [ ] Opción cancelar o desactivar temporalmente.
- [ ] Manejo de 200/429/timeout con mensajes claros.

Notas
Mockear en tests para escenarios de red.
'@ }
  @{ Title = 'Clipboard y Auto-lock: Contadores visibles y control manual'; Labels=@('enhancement','security','UX','UI'); Body = @'
Objetivo
Hacer visibles los timers de seguridad y ofrecer acción manual.

Criterios de aceptación
- [ ] Barra de estado con countdown de limpieza y auto-bloqueo.
- [ ] Botón “Limpiar ahora”.
- [ ] Configuración de tiempos en opciones.

Notas
Integrar con timers existentes en `UI/MainForm`.
'@ }
  @{ Title = 'Ayuda: Índice, breadcrumbs y navegación de enlaces'; Labels=@('enhancement','UX','UI'); Body = @'
Objetivo
Mejorar exploración de la ayuda integrada.

Criterios de aceptación
- [ ] Panel de índice con `docs/ayuda/inicio.md` y `atajos.md`.
- [ ] Breadcrumbs simples.
- [ ] Navegación de links internos de Markdown.

Notas
Extender `UI/AyudaForm.cs` (ya usa Markdig).
'@ }
  @{ Title = 'Persistencia de estado de UI (ventana y columnas)'; Labels=@('enhancement','UX'); Body = @'
Objetivo
Recordar preferencias del usuario entre sesiones.

Criterios de aceptación
- [ ] Guardar/restaurar tamaño/posición de `MainForm`.
- [ ] Guardar anchos de columnas y paneles.
- [ ] Manejo seguro ante valores inválidos.

Notas
Seguir patrón de `AyudaForm` (splitter) para consistencia.
'@ }
  @{ Title = 'Mensajería de UI y logging coherente'; Labels=@('enhancement','UX','security'); Body = @'
Objetivo
Unificar textos y categorías de mensajes/logs.

Criterios de aceptación
- [ ] Tipos de mensajes estándar (info, advertencia, error).
- [ ] Logs con categorías claras y datos mínimos necesarios.
- [ ] Sin exponer detalles sensibles en UI.

Notas
Apoyarse en `Seguridad/Logger.cs`.
'@ }
  @{ Title = 'Tests: `CifradoService`, `GestorContrasenasService` e Import/Export'; Labels=@('enhancement','testing','security'); Body = @'
Objetivo
Aumentar cobertura en servicios críticos.

Criterios de aceptación
- [ ] Vectores deterministas por versión (PBKDF2, Argon2id).
- [ ] CRUD, fortaleza, reutilización y mock HIBP (200/429/timeout).
- [ ] Roundtrip de import/export con validaciones y errores.

Notas
Añadir en `test/GestorContrasenas.Tests/`. No mezclar con producción.
'@ }
)

$created = 0
foreach ($i in $issues) {
  $args = @('issue','create','--title', $i.Title, '--body', $i.Body)
  foreach ($lab in $i.Labels) { $args += @('--label', $lab) }
  & gh @args
  if ($LASTEXITCODE -eq 0) { $created++ }
}

Write-Host ("Issues creados: {0} de {1}" -f $created, $issues.Count) -ForegroundColor Green
