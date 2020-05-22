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
        private List<WebBrowser> navegadores = new List<WebBrowser>();//este es nada mas una lista de las ventanas que hay.
        //tener esta lista no esta funcionando porque no cambia los web browsers por referencia...
        private Mutex mutex = new Mutex();
        private bool isDescargando = false;




        private WebBrowser webTab = null;
        private TabPage pestana = null;
        private List<string> historial = new List<string>();
        private MemoryCache cache;

        public Form1()
        {
            InitializeComponent();
            webBrowser.Navigate("https://www.google.com/");
            webBrowser.DocumentCompleted += WebBrowser_DocumentCompleted; ;
            cache = new MemoryCache("Cache");


            navegadores.Add(webBrowser);
        }

        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            TabPage tab = (TabPage)webBrowser.Parent;
            tab.Text = webBrowser.DocumentTitle;
            txtUrl.Text = webBrowser.Url.AbsoluteUri;
            if (!historial.Contains(webBrowser.Url.AbsoluteUri))
            {
                historial.Add(webBrowser.Url.AbsoluteUri);
            }
        }

        private void nuevaPestana(string url)
        {
            pestana = new TabPage();
            tabContenedor.Controls.Add(pestana);
            webTab = new WebBrowser();
            webTab.Navigate(url);
            pestana.Controls.Add(webTab);
            webTab.Dock = DockStyle.Fill;
            webTab.DocumentCompleted += WebTab_DocumentCompleted;


            navegadores.Add(webTab);
            //webTab.FileDownload += new EventHandler(evento_SoloUnaDescarga(webTab, webTab.);
            tabContenedor.SelectedIndex = tabContenedor.TabPages.Count - 1;
            //crearHiloDeVentana();

        }


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
                        
                        webbrowser.DocumentStream = (Stream)cache.Get(txtUrl.Text);
                        webbrowser.DocumentCompleted += Webbrowser_DocumentCompleted1;
                        Mutex mutex = new Mutex();
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
            cache.Set(txtUrl.Text, webbrowser.DocumentStream, DateTimeOffset.Parse("11:59 PM"));
                        if (!historial.Contains(txtUrl.Text))
            {
                historial.Add(webbrowser.Url.AbsoluteUri);
            }
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
            mutex.WaitOne();
            WebBrowser webbrowser = tabContenedor.SelectedTab.Controls[0] as WebBrowser;
            this.cargar_pagina(webbrowser);
            mutex.ReleaseMutex();
        }

        private void url_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (tabContenedor.SelectedTab == null)
                {
                    this.nuevaPestana("https://www.google.com");
                }
                    WebBrowser webbrowser = tabContenedor.SelectedTab.Controls[0] as WebBrowser;
                  this.cargar_pagina(webbrowser);
            }
        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            WebBrowser webbrowser = tabContenedor.SelectedTab.Controls[0] as WebBrowser;
            webbrowser.Refresh();
        }

        private void chage_URL(object sender, EventArgs e)
        {
            if (tabContenedor.SelectedTab != null)
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
                //eliminar aqui el webBrowser que se encuentre en la lista(esto deb ser temporal...)
                //navegadores[tabContenedor.SelectedIndex].Stop();
                //navegadores[tabContenedor.SelectedIndex] = null;
                //navegadores.Remove(navegadores[tabContenedor.SelectedIndex]);
                //tabContenedor.SelectedTab.Controls.Clear();

                tabContenedor.TabPages.Remove(tabContenedor.SelectedTab);
            }
            
            if (tabContenedor.TabPages.Count < 1)
            {
                txtUrl.Text = "";
            }
        }

        /*public void crearHiloDeVentana()//creamos el hilo que llevara 
        {
            Thread hilo = new Thread();
            hventanas.Add(hilo);
            hventanas[hventanas.Count - 1].Start();
        }*/
        public void agregarVentanaListaDeHilos()
        {

        }
        public void evento_SoloUnaDescarga(object sender, CancelEventArgs e)
        {
            mutex.WaitOne();

            isDescargando = true;
            //hacer que la variable de que se esta descargando se haga verdadera y que cuando la descarga termiae que esta se haga false para poder permitir descargar otro archivo.

            mutex.ReleaseMutex();
        }

        private void SaveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

    }
}
