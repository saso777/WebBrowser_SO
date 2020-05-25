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
        //Thread hiloGlobal;
        bool txtUrlSelected = false;
        //bool isDescargando = false;

        private WebBrowser webTab = null;
        private TabPage pestana = null;
        private List<string> historial = new List<string>();
        private List<string> keys = new List<string>(); //Lista donde se tendran las keys para borrar la cache 
        private MemoryCache cache;
        private string URL; // Variable para asignar una referencia en cache al url que escriba el usuario, sea absoluto o no.


        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            tabContenedor.Controls.Clear();
            nuevaPestana("https://www.google.com/");
            cache = new MemoryCache("Cache");
            URL = "";
            //iniciarHiloDeHilos();//este se ejecuta cuando es un solo hilo para todos lo procesos
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
        }*
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
            webTab.Navigated += WebTab_Navigated;
            webTab.FileDownload += descargarArchivo;

            //tabContenedor.SelectedIndex = tabContenedor.TabPages.Count - 1;
            tabContenedor.SelectTab(tabContenedor.Controls.Count - 1);
            iniciarHiloDeHilos(ref pestana);
            /*
            var tp = ((TabPage)(tabContenedor.Controls[tabContenedor.Controls.Count - 1]));
            iniciarHiloDeHilos(ref tp);
            tabContenedor.Controls.Remove((tabContenedor.Controls[tabContenedor.Controls.Count - 1]));
            tabContenedor.Controls.Add(tp);*/
        }

        private void WebTab_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            ((TabPage)((WebBrowser)sender).Parent).Text = "Cargando...";
        }
        //un solo hilo todos los procesos...
        /*public void iniciarHiloDeHilos()
        {
            hilos.Add(new Thread(evaluador_ciclo));
            hilos[hilos.Count - 1].Start();
        }
        public void evaluador_ciclo()
        {
            while (!this.IsDisposed)
            {
                evaluador();
                Thread.Sleep(500);
            }
        }
        public void evaluador()
        {
            //evaluamos que el link del txtUrl sea correspondiente a la pagina
            //evaluamos que el nombre la tab sea igual al titulo del webBrowser...
            if (tabContenedor.Controls.Count > 0)//si hay mas de un tab...
            {
                if (tabContenedor.SelectedTab.Name != "historial")//si es diferente de historial hacemos....
                {
                    try
                    {
                        txtUrl.Text = ((WebBrowser)(tabContenedor.SelectedTab.Controls[0])).Url.AbsoluteUri;
                    }
                    catch (Exception) { }
                }
                else//sino, entonces blanqueamos el txtUrl...
                {
                    txtUrl.Text = "";
                }

                for (int i = 0; i < tabContenedor.Controls.Count; i++) {
                    if(tabContenedor.SelectedTab.Name != "historial")
                    {
                        try
                        {
                            ((TabPage)(tabContenedor.Controls[i])).Text = ((WebBrowser)(((TabPage)(tabContenedor.Controls[i])).Controls[0])).DocumentTitle;
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
        }*/

        //un hilo por procesos....
        public void iniciarHiloDeHilos(ref TabPage tp)
        {
            var t = tp;
            hilos.Add(new Thread(() => evaluador_ciclo(ref t)));
            hilos[hilos.Count - 1].Start();
            tp = t;
        }
        public void evaluador_ciclo(ref TabPage tp)
        {
            while (!tp.IsDisposed && !this.IsDisposed)
            {
                evaluador(ref tp);
                Thread.Sleep(500);
            }
        }
        public void evaluador(ref TabPage tp)
        {
            if (!txtUrlSelected)
            {
                if (tp.Name != "historial")
                {
                    if (tp == tabContenedor.SelectedTab)
                    {
                        try
                        {
                            txtUrl.Text = ((WebBrowser)(tp.Controls[0])).Url.AbsoluteUri;
                        }
                        catch (Exception) { }
                    }
                    try
                    {
                        //mutex.WaitOne();
                        tp.Text = ((WebBrowser)(tp.Controls[0])).DocumentTitle;
                        //mutex.ReleaseMutex();
                    }
                    catch (Exception) { }
                }
                else
                {
                    if (tabContenedor.SelectedTab == tp)
                    {
                        try
                        {
                            txtUrl.Text = "";
                        }
                        catch (Exception) { }
                    }
                }
            }
        }





        private void WebTab_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser webbrowser = (WebBrowser)sender;
            //pestana.Text = webbrowser.DocumentTitle;
            ((TabPage)(webbrowser.Parent)).Text = webbrowser.DocumentTitle;

            if (webbrowser.ReadyState == WebBrowserReadyState.Complete)
            {
                if (!webbrowser.Url.AbsoluteUri.Equals("about:blank"))
                {
                    try
                    {
                        txtUrl.Text = webTab.Url.AbsoluteUri;
                    }
                    catch (Exception) { }

                }
                //txtUrl.BackColor = Color.White;
            }
            else
            {
                ((TabPage)(((WebBrowser)sender).Parent)).Text = "Cargando...";
            }
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            if(tabContenedor.Controls.Count > 0)
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
        }

        private void btnAdelante_Click(object sender, EventArgs e)
        {
            if(tabContenedor.Controls.Count > 0)
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

            if(webbrowser.ReadyState == WebBrowserReadyState.Complete)
            {
                mutex.WaitOne();
                cache.Set(txtUrl.Text, webbrowser.DocumentStream, DateTimeOffset.Parse("11:59 PM"));
                if (!historial.Contains(txtUrl.Text))//Descomentar esto si Sigue el error de que se agregan varias veces el mismo url al historial.........................
                {
                ((TabPage)((WebBrowser)sender).Parent).Text = webbrowser.DocumentTitle;
                    historial.Add(webbrowser.Url.AbsoluteUri);
                }
                mutex.ReleaseMutex();
            }
            else
            {
                ((TabPage)((WebBrowser)sender).Parent).Text = "Cargando...";
            }
        }

        private void Webbrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser webbrowser = (WebBrowser)sender;
            txtUrl.Text = webbrowser.Url.AbsoluteUri;
            ((TabPage)((WebBrowser)sender).Parent).Text = webbrowser.DocumentTitle;
            if (webbrowser.ReadyState == WebBrowserReadyState.Complete)
            {
                mutex.WaitOne();
                if (!historial.Contains(txtUrl.Text))
                {
                    historial.Add(webbrowser.Url.AbsoluteUri);
                }
                cache.Set(txtUrl.Text, webbrowser.DocumentStream, DateTimeOffset.Parse("11:59 PM"));
                if (!URL.Equals(txtUrl.Text))
                {
                    cache.Set(URL, webbrowser.DocumentStream, DateTimeOffset.Parse("11:59 PM"));
                    keys.Add(URL);
                }
                keys.Add(txtUrl.Text);
                URL = "";
                mutex.ReleaseMutex();
            }
            else
            {
                ((TabPage)((WebBrowser)(sender)).Parent).Text = "Cargando...";
            }

        }

        private void btnIr_Click(object sender, EventArgs e)
        {
            if (tabContenedor.Controls.Count > 0)
            {
                WebBrowser webbrowser = tabContenedor.SelectedTab.Controls[0] as WebBrowser;
                URL = txtUrl.Text;
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
                    URL = txtUrl.Text;
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
            MessageBox.Show("Historial eliminado.");
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
            view_historial.View = View.List;
            for (int i = 0; i < historial.Count; i++)
            {
                view_historial.Items.Add(historial[i]);
            }
            tabContenedor.Controls.Add(pestana);
            tabContenedor.SelectedIndex = tabContenedor.TabPages.Count - 1;
            iniciarHiloDeHilos(ref pestana);
        }

        private void View_historial_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView lista_historial = (ListView)sender;
            nuevaPestana(lista_historial.SelectedItems[0].Text);
        }

        private void btnDeleteTab_Click(object sender, EventArgs e)
        {
            mutex.WaitOne();
            if (tabContenedor.SelectedTab != null)
            {
                if (tabContenedor.SelectedTab.Name != "historial")
                {
                    try
                    {
                        
                        ((WebBrowser)(tabContenedor.SelectedTab.Controls[0])).Dispose();
                        
                    }
                    catch (Exception) { }
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
            mutex.ReleaseMutex();
        }

        private void op_BorrarCache_Click(object sender, EventArgs e)
        {
            for (int i = 0; i<keys.Count; i++)
            {
                cache.Remove(keys[i]);
            }
            keys.Clear();
            MessageBox.Show("Cache eliminada.");
        }

        private void descargarArchivo(object sender, EventArgs e)
        {
            /*
            btnAdelante.BackColor = Color.Aqua;
            /*if (isDescargando)
            {
                ((CancelEventArgs)(((WebBrowser)sender).ActiveXInstance));
            }
            else
            {
                isDescargando = true;
            }*/
        }

        private void gotFocus(object sender, EventArgs e)//esto lo hice debido a que al usar el txtUrl.Focuse o ContainsFocused, no me detectaba cuando se seleccionaba o se enfocaba adecuadamente, pr esto decidi crear esa variable lastimosamente.
        {
            txtUrlSelected = true;
            txtUrl.SelectAll();
        }

        private void lostFocus(object sender, EventArgs e)
        {
            txtUrlSelected = false;
        }
    }
}