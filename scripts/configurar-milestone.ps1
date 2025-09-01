#requires -Version 5.1
<#!
Script: scripts/configurar-milestone.ps1
Propósito: Crear el milestone v0.5.0 (si no existe) y asignar issues prioritarios:
 - DPI/High-DPI: Activar y validar escalado
 - ListView: Autosize de columnas y ordenación por encabezado
 - Persistencia de estado de UI (ventana y columnas)
Requisitos: GitHub CLI autenticado (gh auth login) y repo remoto configurado.
!#>

param(
  [string]$Milestone = 'v0.5.0',
  [string]$Descripcion = 'UI/UX improvements y pruebas',
  [string]$FechaLimite = '2025-10-15'
)

$ErrorActionPreference = 'Stop'

function EsComando($nombre) {
  $cmd = Get-Command $nombre -ErrorAction SilentlyContinue
  return $null -ne $cmd
}

Write-Host "Validando entorno..." -ForegroundColor Cyan
if (-not (EsComando 'gh')) { throw 'GitHub CLI (gh) no está instalado o no está en PATH.' }
try { gh auth status | Out-Null } catch { throw 'No estás autenticado. Ejecuta: gh auth login' }

# Asegurar milestone
Write-Host "Creando milestone si no existe: $Milestone" -ForegroundColor Cyan
# Intentar crear; si ya existe, ignorar error
try {
  gh milestone create $Milestone --description $Descripcion --due-date $FechaLimite | Out-Null
} catch { }

# Obtener issues por título (busca abiertos)
Write-Host "Buscando issues prioritarios por título..." -ForegroundColor Cyan
function BuscarIssuePorTitulo([string]$pattern) {
  $json = gh issue list --state open --json number,title | ConvertFrom-Json
  $match = $json | Where-Object { $_.title -like $pattern } | Select-Object -First 1
  return $match
}

$targets = @(
  @{ Nombre='DPI'; Patron='DPI/High-DPI*' },
  @{ Nombre='ListViewAutosize'; Patron='ListView: Autosize de columnas y ordenación por encabezado*' },
  @{ Nombre='PersistenciaUI'; Patron='Persistencia de estado de UI (ventana y columnas)*' }
)

$encontrados = @{}
foreach ($t in $targets) {
  $i = BuscarIssuePorTitulo $t.Patron
  if ($null -eq $i) { Write-Warning "No se encontró issue para: $($t.Nombre)" } else {
    $encontrados[$t.Nombre] = $i.number
    Write-Host ("{0}: #{1}" -f $t.Nombre, $i.number) -ForegroundColor Green
  }
}

if ($encontrados.Count -eq 0) { throw 'No se encontraron issues para asignar.' }

# Asignar al milestone
Write-Host "Asignando issues al milestone $Milestone..." -ForegroundColor Cyan
$asignados = 0
foreach ($kv in $encontrados.GetEnumerator()) {
  gh issue edit $kv.Value --milestone $Milestone | Out-Null
  $asignados++
}
Write-Host ("Issues asignados: {0}" -f $asignados) -ForegroundColor Green
