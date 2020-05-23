using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace proyecto2
{
    public partial class Form1 : Form
    {
        Mutex mutex = new Mutex();
        List<Thread> hilos = new List<Thread>();
        //List<WebBrowser> wbs = new List<WebBrowser>();
        //Thread hiloGlobal;

        private WebBrowser webTab = null;
        private TabPage pestana = null;
        private List<string> historial = new List<string>();
        private MemoryCache cache;


        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            tabContenedor.Controls.Clear();
            nuevaPestana("https://www.google.com/");
            cache = new MemoryCache("Cache");
            //iniciarHiloGlobal();
        }

        /*public void iniciarHiloGlobal()
        {
            hiloGlobal = new Thread(revisar_ciclo);
            hiloGlobal.Start();
        }
        public void revisar_ciclo()
        {

            while (this != null)
            {
                revisar();
            }

        }
        public void revisar()
        {
            wbs.Clear();
            for (int i = 0; i < tabContenedor.Controls.Count; i++)
            {
                if (tabContenedor.Controls[i].Name != "historial")
                {
                    wbs.Add(((WebBrowser)(tabContenedor.Controls[i].Controls[0])));
                }
            }
            Thread.Sleep(100);
        }*/
        /*private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            TabPage tab = (TabPage)webBrowser.Parent;
            tab.Text = webBrowser.DocumentTitle;
            txtUrl.Text = webBrowser.Url.AbsoluteUri;
            if (!historial.Contains(webBrowser.Url.AbsoluteUri))
            {
                historial.Add(webBrowser.Url.AbsoluteUri);
            }
        }*/

        private void nuevaPestana(string url)
        {
            pestana = new TabPage();
            tabContenedor.Controls.Add(pestana);
            webTab = new WebBrowser();
            webTab.Navigate(url);
            pestana.Controls.Add(webTab);
            webTab.Dock = DockStyle.Fill;
            webTab.DocumentCompleted += WebTab_DocumentCompleted;
            tabContenedor.SelectedIndex = tabContenedor.TabPages.Count - 1;

            //iniciarHiloWebBrowser(ref pestana);
        }

        /*public void iniciarHiloWebBrowser(ref TabPage tp)
        {
            var t = tp;
            hilos.Add(new Thread(() => evaluador_ciclo(ref t)));
            hilos[hilos.Count - 1].Start();
            tp = t;
        }
        public void evaluador_ciclo(ref TabPage tp)
        {
            while (!tp.IsDisposed && tp != null)
            {
                evaluador(ref tp);
                Thread.Sleep(500);
            }
        }
        public void evaluador(ref TabPage tp)
        {
            //if (!((TabPage)wb.Parent).Text.Equals(wb.DocumentTitle))
            txtUrl.Text = tabContenedor.Controls.IndexOf(tp).ToString();
            //if ((tp.Text != ((WebBrowser)(tp.Controls[0])).DocumentText))
            if(/*((tp.Controls[0]) as WebBrowser).IsBusy == (false)*/ /*true)
            {
                tp.Text = ((WebBrowser)(tp.Controls[0])).DocumentTitle.ToString() ;
            }
            else
            {
                tp.Text = "Cargando...";
            }
            
        }
        */
        private void WebTab_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser webbrowser = (WebBrowser)sender;
            pestana.Text = webTab.DocumentTitle;
            if (!webbrowser.Url.AbsoluteUri.Equals("about:blank"))
            {
                try
                {
                    txtUrl.Text = webTab.Url.AbsoluteUri;
                }
                catch (Exception) { }

            }
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            WebBrowser webbrowser = tabContenedor.SelectedTab.Controls[0] as WebBrowser;
            if (webbrowser != null)
            {
                if (webbrowser.CanGoBack)
                {
                    webbrowser.GoBack();
                }
            }
        }

        private void btnAdelante_Click(object sender, EventArgs e)
        {
            WebBrowser webbrowser = tabContenedor.SelectedTab.Controls[0] as WebBrowser;
            if (webbrowser != null)
            {
                if (webbrowser.CanGoForward)
                {
                    webbrowser.GoForward();
                }
            }

        }

        private void cargar_pagina(WebBrowser webbrowser)
        {
            if (webbrowser != null)
            {
                try
                {
                    string url = txtUrl.Text;
                    if (cache.Get(txtUrl.Text) == null)
                    {
                        if (txtUrl.Text.Contains("https://"))
                        {
                            webbrowser.Navigate(txtUrl.Text);
                        }
                        else
                        {
                            webbrowser.Navigate("https://" + txtUrl.Text);
                        }
                        webbrowser.DocumentCompleted += Webbrowser_DocumentCompleted; ; //Evento para sustituir la URL por la del sitio recien cargado
                    }
                    else
                    {
                        mutex.WaitOne();
                        webbrowser.DocumentStream = (Stream)cache.Get(txtUrl.Text);
                        webbrowser.DocumentCompleted += Webbrowser_DocumentCompleted1;
                        mutex.ReleaseMutex();
                    }

                }
                catch (Exception)
                {

                    if (txtUrl.Text.Length == 0)
                    {
                        MessageBox.Show("Caracteres Invalidos detectados en el Link Introducido");
                    }
                    else
                    {
                        MessageBox.Show("No se ha escrito ningun link");
                    }
                }
            }
        }

        private void Webbrowser_DocumentCompleted1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser webbrowser = (WebBrowser)sender;

            mutex.WaitOne();
            cache.Set(txtUrl.Text, webbrowser.DocumentStream, DateTimeOffset.Parse("11:59 PM"));
            if (!historial.Contains(txtUrl.Text))
            {
                historial.Add(webbrowser.Url.AbsoluteUri);
            }
            mutex.ReleaseMutex();
        }

        private void Webbrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser webbrowser = (WebBrowser)sender;
            TabPage tab = (TabPage)webbrowser.Parent;
            txtUrl.Text = webbrowser.Url.AbsoluteUri;
            tab.Text = webbrowser.DocumentTitle;

            mutex.WaitOne();
            if (!historial.Contains(txtUrl.Text))
            {
                historial.Add(webbrowser.Url.AbsoluteUri);
            }
            cache.Set(txtUrl.Text, webbrowser.DocumentStream, DateTimeOffset.Parse("11:59 PM"));
            mutex.ReleaseMutex();

        }

        private void btnIr_Click(object sender, EventArgs e)
        {
            if (tabContenedor.Controls.Count > 0)
            {
                WebBrowser webbrowser = tabContenedor.SelectedTab.Controls[0] as WebBrowser;
                this.cargar_pagina(webbrowser);
            }
        }

        private void url_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (tabContenedor.SelectedTab == null)
                {
                    this.nuevaPestana("https://www.google.com");
                }
                if (tabContenedor.Controls.Count > 0)
                {
                    WebBrowser webbrowser = tabContenedor.SelectedTab.Controls[0] as WebBrowser;
                    this.cargar_pagina(webbrowser);
                }

            }
        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            if (tabContenedor.Controls.Count > 0)
            {
                WebBrowser webbrowser = tabContenedor.SelectedTab.Controls[0] as WebBrowser;
                if (webbrowser != null)
                {
                    webbrowser.Refresh();
                }
            }
        }

        private void chage_URL(object sender, EventArgs e)
        {
            if (tabContenedor.SelectedTab != null)
            {
                if (tabContenedor.SelectedTab.Controls.Count > 0)
                {
                    WebBrowser webbrowser = tabContenedor.SelectedTab.Controls[0] as WebBrowser;
                    if (webbrowser != null)
                    {
                        if (webbrowser.Url != null)
                        {
                            if (!webbrowser.Url.AbsoluteUri.Equals("about:blank"))
                            {
                                txtUrl.Text = webbrowser.Url.AbsoluteUri;
                            }
                        }
                    }
                }
            }
        }

        private void op_nuevaPestana_Click(object sender, EventArgs e)
        {
            this.nuevaPestana("https://www.google.com");
        }

        private void op_borrarHistorial_Click(object sender, EventArgs e)
        {
            historial.Clear();
        }

        private void ver_historial_Click(object sender, EventArgs e)
        {
            pestana = new TabPage();
            pestana.Text = "Historial";
            pestana.Name = "historial";
            ListView view_historial = new ListView();
            view_historial.MouseDoubleClick += View_historial_MouseDoubleClick; ;
            pestana.Controls.Add(view_historial);
            view_historial.Dock = DockStyle.Fill;
            for (int i = 0; i < historial.Count; i++)
            {
                view_historial.Items.Add(historial[i]);
            }
            tabContenedor.Controls.Add(pestana);
            tabContenedor.SelectedIndex = tabContenedor.TabPages.Count - 1;
        }

        private void View_historial_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView lista_historial = (ListView)sender;
            nuevaPestana(lista_historial.SelectedItems[0].Text);
        }

        private void btnDeleteTab_Click(object sender, EventArgs e)
        {
            if (tabContenedor.SelectedTab != null)
            {
                if (tabContenedor.SelectedTab.Name != "historial")
                {
                    ((WebBrowser)(tabContenedor.SelectedTab.Controls[0])).Dispose();
                }
                tabContenedor.TabPages.Remove(tabContenedor.SelectedTab);
                if (tabContenedor.Controls.Count > 0)
                {
                    tabContenedor.SelectedTab = ((TabPage)(tabContenedor.Controls[tabContenedor.Controls.Count - 1]));
                }
            }

            if (tabContenedor.TabPages.Count < 1)
            {
                txtUrl.Text = "";
            }
        }

        private void op_BorrarCache_Click(object sender, EventArgs e)
        {

        }

        private void descargarArchvo(object sender, EventArgs e)
        {


        }

    }
}