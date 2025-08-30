using System;
using System.Windows.Forms;
using DotNetEnv;

namespace GestorContrasenas
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Cargar variables de entorno desde .env si existe
            try { Env.Load(); } catch { /* opcional: ignorar si no hay .env */ }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
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
                MessageBox.Show(ex.ToString(), "Error al iniciar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
