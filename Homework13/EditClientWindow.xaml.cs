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
using System.Windows.Shapes;

namespace Homework13
{
    /// <summary>
    /// Логика взаимодействия для EditClient.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        #region Конструкторы

        public ClientWindow()
        {
            InitializeComponent();
        }

        public ClientWindow(string Name, string Surname, int Age) : this()
        {
            this.NameClient.Text = Name;
            this.SurnameClient.Text = Surname;
            this.AgeClient.Text = Age.ToString();
        }
        #endregion

        /// <summary>
        /// Обработчик события нажатия клавиши OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OK_Click(object sender, RoutedEventArgs e) { this.Close(); }
    }
}
