#if false
using System;
using System.Windows.Forms;

public partial class LoginForm : Form
{
    public string MasterKey { get; private set; }

    public LoginForm()
    {
        InitializeComponent();
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        if (txtPassword.Text.Length < 6)
        {
            MessageBox.Show("La contraseña debe tener al menos 6 caracteres.");
            return;
        }
        MasterKey = txtPassword.Text;
        DialogResult = DialogResult.OK;
        Close();
    }
}
#endif