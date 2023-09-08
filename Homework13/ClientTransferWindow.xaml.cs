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
    /// Логика взаимодействия для ClientTransferWindow.xaml
    /// </summary>
    public partial class ClientTransferWindow : Window
    {
        #region Поля

        ITransfer<Deposit> transferDeposit;         //Интерфейс перевода на депозитный счет

        ITransfer<Nondeposit> transferNondeposit;   //Интерфейс перевода на недепозитный счет

        MoneyWindow moneyWindow;                    //Экземпляр окна ввода суммы
        #endregion

        #region Конструкторы

        public ClientTransferWindow()
        {
            InitializeComponent();
            ClientsListView.ItemsSource = Repository.Clients;
        }
        public ClientTransferWindow(ITransfer<Deposit> Transfer) : this()
        {
            transferDeposit = Transfer;
        }
        public ClientTransferWindow(ITransfer<Nondeposit> Transfer) : this()
        {
            transferNondeposit = Transfer;
        }
        #endregion

        /// <summary>
        /// Обработчик события нажатия клавиши ОК
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsListView.SelectedItem != null)
            {
                moneyWindow = new MoneyWindow();
                moneyWindow.Show();
                moneyWindow.OKButton.Click += OKButtonMoney_Click;
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши ОК в окне ввода суммы денег
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButtonMoney_Click(object sender, RoutedEventArgs e)
        {
            if(transferDeposit != null) transferDeposit.Transfer(moneyWindow.money, (ClientsListView.SelectedItem as Client).Deposit);
            if (transferNondeposit != null) transferNondeposit.Transfer(moneyWindow.money, (ClientsListView.SelectedItem as Client).Nondeposit);
            this.Close();
        }
    }
}