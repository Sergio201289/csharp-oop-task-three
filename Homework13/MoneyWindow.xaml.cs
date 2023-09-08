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
    /// Логика взаимодействия для Money.xaml
    /// </summary>
    public partial class MoneyWindow : Window
    {

        public int money;   //Поле суммы денег

        /// <summary>
        /// Конструткор
        /// </summary>
        public MoneyWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик события нажатия кнопки ОК
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(MoneyBox.Text, out money)) this.Close();
        }
    }
}
