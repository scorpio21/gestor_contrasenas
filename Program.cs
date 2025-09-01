using System;
using System.Windows.Forms;
using DotNetEnv;
using GestorContrasenas.Datos;
using GestorContrasenas.Seguridad;

namespace GestorContrasenas
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Configurar manejo global de excepciones
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) =>
            {
                try { Logger.Error(e.Exception, "ThreadException"); } catch { }
                MessageBox.Show("Ha ocurrido un error inesperado. Se registró en el log.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                try
                {
                    if (e.ExceptionObject is Exception ex) Logger.Error(ex, "UnhandledException");
                    else Logger.Error($"UnhandledException: {e.ExceptionObject}");
                }
                catch { }
            };
            // Cargar variables de entorno desde .env si existe
            try { Env.Load(); } catch { /* opcional: ignorar si no hay .env */ }
            // Asegurar esquema de base de datos si hay conexión configurada
            try
            {
                var conn = Environment.GetEnvironmentVariable("GESTOR_DB_CONN");
                if (!string.IsNullOrWhiteSpace(conn))
                {
                    Migraciones.AsegurarEsquema(conn);
                }
            }
            catch (Exception ex)
            {
                try { Logger.Warn($"Fallo al asegurar esquema: {ex.Message}"); } catch { }
                // Evitar bloquear el arranque por esto; el repositorio volverá a intentar
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                // Si hay sesión recordada, iniciamos directamente
                var sesion = RecordarSesion.Cargar();
                if (sesion is { } s)
                {
                    Application.Run(new UI.MainForm(s.UsuarioId, s.ClaveMaestra));
                    return;
                }
                using (var login = new UI.LoginForm())
                {
                    var result = login.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        var clave = login.ClaveMaestra;
                        var usuarioId = login.UsuarioId;
                        Application.Run(new UI.MainForm(usuarioId, clave));
                    }
                }
            }
            catch (Exception ex)
            {
                try { Logger.Error(ex, "Arranque Main"); } catch { }
                MessageBox.Show("No se pudo iniciar la aplicación. Revisa el log para más detalles.", "Error al iniciar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
