using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace GestorContrasenas.Seguridad
{
    /// <summary>
    /// Logger sencillo y discreto a archivo local (sin secretos).
    /// Ubicación: %LOCALAPPDATA%/gestor_contrasenas/logs/app.log
    /// </summary>
    public static class Logger
    {
        private static readonly object _lock = new();
        private static readonly string _logDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "gestor_contrasenas", "logs");
        private static readonly string _logFile = Path.Combine(_logDir, "app.log");

        public static void Info(string mensaje) => Escribir("INFO", mensaje);
        public static void Warn(string mensaje) => Escribir("WARN", mensaje);
        public static void Error(string mensaje) => Escribir("ERROR", mensaje);
        public static void Error(Exception ex, string contexto = "")
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(contexto)) sb.Append('[').Append(contexto).Append("] ");
            sb.Append(ex.GetType().FullName).Append(": ").Append(ex.Message);
            sb.AppendLine();
            sb.AppendLine(ex.StackTrace);
            if (ex.InnerException != null)
            {
                sb.AppendLine("-- InnerException --");
                sb.AppendLine(ex.InnerException.GetType().FullName + ": " + ex.InnerException.Message);
                sb.AppendLine(ex.InnerException.StackTrace);
            }
            Escribir("ERROR", sb.ToString());
        }

        private static void Escribir(string nivel, string mensaje)
        {
            try
            {
                lock (_lock)
                {
                    Directory.CreateDirectory(_logDir);
                    var linea = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{nivel}] {mensaje}";
                    File.AppendAllText(_logFile, linea + Environment.NewLine, Encoding.UTF8);
                }
            }
            catch
            {
                // Evitar fallos por logger; intentar al menos escribir a Trace
                Trace.WriteLine($"Logger fallo [{nivel}]: {mensaje}");
            }
        }
    }
}
