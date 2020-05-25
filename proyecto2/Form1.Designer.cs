namespace proyecto2
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAtras = new System.Windows.Forms.Button();
            this.btnAdelante = new System.Windows.Forms.Button();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.verToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ver_historial = new System.Windows.Forms.ToolStripMenuItem();
            this.opcionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.op_borrarHistorial = new System.Windows.Forms.ToolStripMenuItem();
            this.op_borrarCache = new System.Windows.Forms.ToolStripMenuItem();
            this.op_nuevaPestana = new System.Windows.Forms.ToolStripMenuItem();
            this.btnIr = new System.Windows.Forms.Button();
            this.btnRecargar = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabContenedor = new System.Windows.Forms.TabControl();
            this.btnDeleteTab = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.tabContenedor.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAtras
            // 
            this.btnAtras.Location = new System.Drawing.Point(9, 25);
            this.btnAtras.Margin = new System.Windows.Forms.Padding(2);
            this.btnAtras.Name = "btnAtras";
            this.btnAtras.Size = new System.Drawing.Size(23, 19);
            this.btnAtras.TabIndex = 0;
            this.btnAtras.Text = "<";
            this.btnAtras.UseVisualStyleBackColor = true;
            this.btnAtras.Click += new System.EventHandler(this.btnAtras_Click);
            // 
            // btnAdelante
            // 
            this.btnAdelante.Location = new System.Drawing.Point(37, 25);
            this.btnAdelante.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdelante.Name = "btnAdelante";
            this.btnAdelante.Size = new System.Drawing.Size(23, 19);
            this.btnAdelante.TabIndex = 1;
            this.btnAdelante.Text = ">";
            this.btnAdelante.UseVisualStyleBackColor = true;
            this.btnAdelante.Click += new System.EventHandler(this.btnAdelante_Click);
            // 
            // txtUrl
            // 
            this.txtUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUrl.Location = new System.Drawing.Point(92, 26);
            this.txtUrl.Margin = new System.Windows.Forms.Padding(2);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(808, 20);
            this.txtUrl.TabIndex = 2;
            this.txtUrl.Enter += new System.EventHandler(this.gotFocus);
            this.txtUrl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.url_KeyPress);
            this.txtUrl.Leave += new System.EventHandler(this.lostFocus);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verToolStripMenuItem,
            this.opcionesToolStripMenuItem,
            this.op_nuevaPestana});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(946, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // verToolStripMenuItem
            // 
            this.verToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ver_historial});
            this.verToolStripMenuItem.Name = "verToolStripMenuItem";
            this.verToolStripMenuItem.Size = new System.Drawing.Size(36, 20);
            this.verToolStripMenuItem.Text = "Ver";
            // 
            // ver_historial
            // 
            this.ver_historial.Name = "ver_historial";
            this.ver_historial.Size = new System.Drawing.Size(118, 22);
            this.ver_historial.Text = "Historial";
            this.ver_historial.Click += new System.EventHandler(this.ver_historial_Click);
            // 
            // opcionesToolStripMenuItem
            // 
            this.opcionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.op_borrarHistorial,
            this.op_borrarCache});
            this.opcionesToolStripMenuItem.Name = "opcionesToolStripMenuItem";
            this.opcionesToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.opcionesToolStripMenuItem.Text = "Opciones";
            // 
            // op_borrarHistorial
            // 
            this.op_borrarHistorial.Name = "op_borrarHistorial";
            this.op_borrarHistorial.Size = new System.Drawing.Size(151, 22);
            this.op_borrarHistorial.Text = "Borrar historial";
            this.op_borrarHistorial.Click += new System.EventHandler(this.op_borrarHistorial_Click);
            // 
            // op_borrarCache
            // 
            this.op_borrarCache.Name = "op_borrarCache";
            this.op_borrarCache.Size = new System.Drawing.Size(151, 22);
            this.op_borrarCache.Text = "Borrar Caché";
            this.op_borrarCache.Click += new System.EventHandler(this.op_BorrarCache_Click);
            // 
            // op_nuevaPestana
            // 
            this.op_nuevaPestana.Name = "op_nuevaPestana";
            this.op_nuevaPestana.Size = new System.Drawing.Size(97, 20);
            this.op_nuevaPestana.Text = "Nueva pestaña";
            this.op_nuevaPestana.Click += new System.EventHandler(this.op_nuevaPestana_Click);
            // 
            // btnIr
            // 
            this.btnIr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIr.Location = new System.Drawing.Point(904, 25);
            this.btnIr.Margin = new System.Windows.Forms.Padding(2);
            this.btnIr.Name = "btnIr";
            this.btnIr.Size = new System.Drawing.Size(34, 20);
            this.btnIr.TabIndex = 4;
            this.btnIr.Text = "Ir";
            this.btnIr.UseVisualStyleBackColor = true;
            this.btnIr.Click += new System.EventHandler(this.btnIr_Click);
            // 
            // btnRecargar
            // 
            this.btnRecargar.Location = new System.Drawing.Point(64, 25);
            this.btnRecargar.Margin = new System.Windows.Forms.Padding(2);
            this.btnRecargar.Name = "btnRecargar";
            this.btnRecargar.Size = new System.Drawing.Size(23, 20);
            this.btnRecargar.TabIndex = 5;
            this.btnRecargar.Text = "R";
            this.btnRecargar.UseVisualStyleBackColor = true;
            this.btnRecargar.Click += new System.EventHandler(this.btnRecargar_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(920, 428);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Inicio";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabContenedor
            // 
            this.tabContenedor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabContenedor.Controls.Add(this.tabPage1);
            this.tabContenedor.Location = new System.Drawing.Point(9, 83);
            this.tabContenedor.Margin = new System.Windows.Forms.Padding(2);
            this.tabContenedor.Name = "tabContenedor";
            this.tabContenedor.SelectedIndex = 0;
            this.tabContenedor.Size = new System.Drawing.Size(928, 454);
            this.tabContenedor.TabIndex = 6;
            this.tabContenedor.SelectedIndexChanged += new System.EventHandler(this.chage_URL);
            // 
            // btnDeleteTab
            // 
            this.btnDeleteTab.Location = new System.Drawing.Point(10, 50);
            this.btnDeleteTab.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteTab.Name = "btnDeleteTab";
            this.btnDeleteTab.Size = new System.Drawing.Size(96, 28);
            this.btnDeleteTab.TabIndex = 7;
            this.btnDeleteTab.Text = "Eliminar pestaña";
            this.btnDeleteTab.UseVisualStyleBackColor = true;
            this.btnDeleteTab.Click += new System.EventHandler(this.btnDeleteTab_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 547);
            this.Controls.Add(this.btnDeleteTab);
            this.Controls.Add(this.tabContenedor);
            this.Controls.Add(this.btnRecargar);
            this.Controls.Add(this.btnIr);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.btnAdelante);
            this.Controls.Add(this.btnAtras);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabContenedor.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAtras;
        private System.Windows.Forms.Button btnAdelante;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem opcionesToolStripMenuItem;
        private System.Windows.Forms.Button btnIr;
        private System.Windows.Forms.ToolStripMenuItem op_borrarHistorial;
        private System.Windows.Forms.Button btnRecargar;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tabContenedor;
        private System.Windows.Forms.Button btnDeleteTab;
        private System.Windows.Forms.ToolStripMenuItem op_nuevaPestana;
        private System.Windows.Forms.ToolStripMenuItem op_borrarCache;
        private System.Windows.Forms.ToolStripMenuItem verToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ver_historial;
    }
}

