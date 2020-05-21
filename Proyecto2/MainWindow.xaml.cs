using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Proyecto2
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WebBrowser webTab = null;
        private TabItem pestana = null;
        private List<string> historial = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            webBrowser.Navigate("https://www.google.com");
            txtUrl.Text = "https://www.google.com";
            webBrowser.LoadCompleted += WebBrowser_LoadCompleted;
        }

        private void WebBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            TabItem tab = (TabItem)webBrowser.Parent;
            tab.Header = (string)webBrowser.InvokeScript("eval", "document.title.toString()");
        }

        private void nuevaPestana(string url)
        {
            pestana = new TabItem();
            tabContenedor.Items.Add(pestana);
            tabContenedor.SelectedIndex = tabContenedor.Items.Count - 1;
            webTab = new WebBrowser();
            webTab.Navigate(url);
            pestana.Content = webTab;
            webTab.LoadCompleted += WebTab_LoadCompleted;
        }

        private void btnNuevaTab_Click(object sender, RoutedEventArgs e)
        {
            this.nuevaPestana("https://www.google.com");
        }

        private void WebTab_LoadCompleted(object sender, NavigationEventArgs e)
        {
            pestana.Header = (string)webTab.InvokeScript("eval", "document.title.toString()");
            txtUrl.Text = webTab.Source.ToString();
        }

        private void cargar_pagina(WebBrowser webbrowser)
        {
            if (webbrowser != null)
            {
                try
                {
                    if (txtUrl.Text.Contains("https://"))
                    {
                        webbrowser.Navigate(txtUrl.Text);
                    }
                    else
                    {
                        webbrowser.Source = new Uri("https://" + txtUrl.Text);
                    }
                    webbrowser.LoadCompleted += Webbrowser_LoadCompleted; //Evento para sustituir la URL por la del sitio recien cargado
                }
                catch (Exception ex)
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

        private void btnIr_Click(object sender, RoutedEventArgs e)
        {
            WebBrowser webbrowser = tabContenedor.SelectedContent as WebBrowser;
            this.cargar_pagina(webbrowser);
        }

        private void Webbrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            WebBrowser webbrowser = (WebBrowser)sender;
            TabItem tab = (TabItem)webbrowser.Parent;
            txtUrl.Text = webbrowser.Source.ToString();
            tab.Header = (string) webbrowser.InvokeScript("eval", "document.title.toString()");
            historial.Add(webbrowser.Source.ToString());
        }

        private void btnAdelante_Click(object sender, RoutedEventArgs e)
        {
            WebBrowser webbrowser = tabContenedor.SelectedContent as WebBrowser;
            if (webbrowser != null)
            {
                if (webbrowser.CanGoForward)
                {
                    webbrowser.GoForward();
                }
            }
        }


        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            WebBrowser webbrowser = tabContenedor.SelectedContent as WebBrowser;
            if (webbrowser != null)
            {
                if (webbrowser.CanGoBack)
                {
                    webbrowser.GoBack();
                }
            }
        }

        private void change_URL(object sender, SelectionChangedEventArgs e) //Evento para cambiar el URL en el txt dependiendo de la pestaña seleccionada
        {
            if (e.Source is TabControl)
            {
                WebBrowser webbrowser = tabContenedor.SelectedContent as WebBrowser;
                if (webbrowser != null)
                {
                    if (webbrowser.Source != null)
                    {
                        txtUrl.Text = webbrowser.Source.ToString();
                    }
                }
            }

        }

        private void KeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                WebBrowser webbrowser = tabContenedor.SelectedContent as WebBrowser;
                this.cargar_pagina(webbrowser);
            }
        }

        private void btnDeleteTab_Click(object sender, RoutedEventArgs e)
        {
            tabContenedor.Items.Remove(tabContenedor.SelectedItem);
            if (tabContenedor.Items.Count<1)
            {
                txtUrl.Text = "";
            }
        }

        private void btnHistorial_Click(object sender, RoutedEventArgs e)
        {
            pestana = new TabItem();
            pestana.Header = "Historial";
            ListView view_historial = new ListView();
            view_historial.MouseDoubleClick += View_historial_MouseDoubleClick;
            pestana.Content = view_historial;
            for(int i = 0; i<historial.Count; i++)
            {
                view_historial.Items.Add(historial[i]);
            }
            tabContenedor.Items.Add(pestana);
            tabContenedor.SelectedIndex = tabContenedor.Items.Count - 1;
        }

        private void View_historial_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView lista_historial = (ListView)sender;
            nuevaPestana((string)lista_historial.SelectedItem);
        }
    }
}
