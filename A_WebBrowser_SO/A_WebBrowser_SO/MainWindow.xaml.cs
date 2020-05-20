using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace A_WebBrowser_SO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void click_irAtras(object sender, RoutedEventArgs e)
        {
            try
            {
                wb_buscador.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void click_irAdelante(object sender, RoutedEventArgs e)
        {
            try
            {
                wb_buscador.GoForward();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void click_irBuscar(object sender, RoutedEventArgs e)//hacer que si no es una direccion entonces que buscque en google como palabras normales xd
        {
            try
            {
                wb_buscador.Source = new Uri("https://" + txtb_link.Text);
            }
            catch (Exception)
            {

                if (txtb_link.Text.Length == 0)
                {
                    MessageBox.Show("Caracteres Invalidos detectados en el Link Introducido");
                }
                else
                {
                    MessageBox.Show("No se ha escrito ningun link");
                }
            }


        }

        private void click_agregarFavorito(object sender, RoutedEventArgs e)
        {

        }

        private void click_verAjustes(object sender, RoutedEventArgs e)
        {

        }
        private void clock_recargarPagina(object sender, RoutedEventArgs e)
        {
            try
            {
                wb_buscador.Source = wb_buscador.Source;
            }
            catch (Exception)
            {

            }
        }
    }
}
