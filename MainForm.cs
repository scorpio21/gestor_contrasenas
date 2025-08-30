#if false
using System;
using System.Windows.Forms;

public partial class MainForm : Form
{
    private PasswordManager _manager;

    public MainForm(string masterKey)
    {
        InitializeComponent();
        _manager = new PasswordManager(masterKey);
        LoadPasswords();
    }

    private void LoadPasswords()
    {
        listView1.Items.Clear();
        foreach (var (service, username, password) in _manager.GetPasswords())
        {
            var item = new ListViewItem(new[] { service, username, password });
            listView1.Items.Add(item);
        }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        _manager.AddPassword(txtService.Text, txtUsername.Text, txtPassword.Text);
        LoadPasswords();
    }
}
#endif