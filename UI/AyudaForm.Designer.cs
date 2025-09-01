using System.Windows.Forms;

namespace GestorContrasenas.UI
{
    partial class AyudaForm
    {
        private System.ComponentModel.IContainer components = null;
        private SplitContainer splitContainer1;
        private TreeView tvIndice;
        private WebBrowser wbContenido;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            splitContainer1 = new SplitContainer();
            tvIndice = new TreeView();
            wbContenido = new WebBrowser();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // panel izquierda
            splitContainer1.Panel1.Controls.Add(tvIndice);
            // panel derecha
            splitContainer1.Panel2.Controls.Add(wbContenido);
            // Los MinSize se establecerán en Load cuando haya tamaño real
            splitContainer1.Panel1MinSize = 0;
            splitContainer1.Panel2MinSize = 0;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.SplitterWidth = 6;
            splitContainer1.TabIndex = 0;
            // 
            // tvIndice
            // 
            tvIndice.Dock = DockStyle.Fill;
            tvIndice.HideSelection = false;
            tvIndice.FullRowSelect = true;
            tvIndice.AfterSelect += tvIndice_AfterSelect;
            // 
            // wbContenido
            // 
            wbContenido.Dock = DockStyle.Fill;
            wbContenido.AllowWebBrowserDrop = false;
            wbContenido.IsWebBrowserContextMenuEnabled = false;
            wbContenido.WebBrowserShortcutsEnabled = true;
            // 
            // AyudaForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(900, 600);
            Controls.Add(splitContainer1);
            Name = "AyudaForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Ayuda";
            MinimumSize = new System.Drawing.Size(600, 400);
            Load += AyudaForm_Load;
            FormClosing += AyudaForm_FormClosing;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
