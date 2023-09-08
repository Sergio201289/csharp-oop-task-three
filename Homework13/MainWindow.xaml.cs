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

namespace Homework13
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Поля

        ClientWindow ClientWindow;  //Поле клиентского окна

        IRefill<Account> refill;    //Поле интерфейса пополнения счета

        MoneyWindow money;          //Поле окна ввода суммы денег
        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            ClientsListView.ItemsSource = Repository.Clients;
        }

        #region Обработчики событий и методы

        /// <summary>
        /// Обработчик события нажатия кнопки Открыть
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenRepository_Click(object sender, RoutedEventArgs e)
        {
            Repository.OpenRepository();
            ClientsListView.ItemsSource = Repository.Clients;
        }

        /// <summary>
        /// Обработчик события кнопки Сохранить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveRepository_Click(object sender, RoutedEventArgs e)
        {
            Repository.SaveRepository();
        }

        /// <summary>
        /// Обработчик события изменения выбранного элемента в листе клиентов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientsListView.SelectedItem != null)
            {
                DepositAccount.Items.Clear();
                DepositAccount.Items.Add((ClientsListView.SelectedItem as Client).Deposit);
                
            }
            if (ClientsListView.SelectedItem != null)
            {
                NonDepositAccount.Items.Clear();
                NonDepositAccount.Items.Add((ClientsListView.SelectedItem as Client).Nondeposit);
            }
        }

        /// <summary>
        /// Обработчик события кнопки открытия счета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenAccount_Click(object sender, RoutedEventArgs e)
        {
            if (DepositAccount.SelectedItem != null && !(DepositAccount.SelectedItem as Account).Status)
            {
                Session<Deposit> session = new Session<Deposit>(DepositAccount.SelectedItem as Deposit);
                session.Open();
                DepositAccount.Items.Refresh();
            }
            if (NonDepositAccount.SelectedItem != null && !(NonDepositAccount.SelectedItem as Account).Status)
            {
                Session<Nondeposit> session = new Session<Nondeposit>(NonDepositAccount.SelectedItem as Nondeposit);
                session.Open();
                NonDepositAccount.Items.Refresh();
            }

        }

        /// <summary>
        /// Обработчик события кнопки закрытия счета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseAccount_Click(object sender, RoutedEventArgs e)
        {
            if (DepositAccount.SelectedItem != null && (DepositAccount.SelectedItem as Account).Status)
            {
                Session<Deposit> session = new Session<Deposit>(DepositAccount.SelectedItem as Deposit);
                session.Close();
                DepositAccount.Items.Refresh();
            }
            if (NonDepositAccount.SelectedItem != null && (NonDepositAccount.SelectedItem as Account).Status)
            {
                Session<Nondeposit> session = new Session<Nondeposit>(NonDepositAccount.SelectedItem as Nondeposit);
                session.Close();
                NonDepositAccount.Items.Refresh();
            }
        }

        /// <summary>
        /// Обработчик события кнопки Редактировать клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsListView.SelectedItem != null)
            {
                ClientWindow = new ClientWindow((ClientsListView.SelectedItem as Client).Name, (ClientsListView.SelectedItem as Client).Surname, (ClientsListView.SelectedItem as Client).Age);
                ClientWindow.Show();
                ClientWindow.OK.Click += EditClient;
            }
        }

        /// <summary>
        /// Метод, подписанный на событие нажатия клавиши ОК в окне редактирования клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditClient(object sender, RoutedEventArgs e)
        {
            Repository.Clients.RemoveAt(ClientsListView.SelectedIndex);
            Repository.Clients.Insert(ClientsListView.SelectedIndex, new Client(ClientWindow.NameClient.Text, ClientWindow.SurnameClient.Text, Convert.ToInt32(ClientWindow.AgeClient.Text)));
            ClientsListView.Items.Refresh();
        }

        /// <summary>
        /// Обработчик события кнопки Добавить клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow = new ClientWindow();
            ClientWindow.Show();
            ClientWindow.OK.Click += AddClient;
        }

        /// <summary>
        /// Метод, подписанный на событие нажатия кнопки ОК в окне добавления клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddClient(object sender, RoutedEventArgs e)
        {
            Repository.Clients.Add(new Client(ClientWindow.NameClient.Text, ClientWindow.SurnameClient.Text, Convert.ToInt32(ClientWindow.AgeClient.Text)));
            ClientsListView.Items.Refresh();
        }

        /// <summary>
        /// Обработчик события кнопки Удалить клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteClientButton_Click(object sender, RoutedEventArgs e)
        {
            if(ClientsListView.SelectedItem != null) Repository.Clients.RemoveAt(ClientsListView.SelectedIndex);
            ClientsListView.Items.Refresh();
        }

        /// <summary>
        /// Обработчик события выбора элемента в листе Недепозитного счета, со снятием выделения в листе Депозитного счета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NonDepositAccount_GotFocus(object sender, RoutedEventArgs e)
        {
            DepositAccount.UnselectAll();
        }

        /// <summary>
        /// Обработчик события выбора элемента в листе Депозитного счета, со снятием выделения в листе Недепозитного счета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepositAccount_GotFocus(object sender, RoutedEventArgs e)
        {
            NonDepositAccount.UnselectAll();
        }

        /// <summary>
        /// Обработчик события кнопки Пополнения счета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refill_Click(object sender, RoutedEventArgs e)
        {
            money = new MoneyWindow();
            money.OKButton.Click += OKButtonRefillClick;
            if (DepositAccount.SelectedItem != null && (DepositAccount.SelectedItem as Deposit).Status)
            {
                refill = new Session<Deposit>(DepositAccount.SelectedItem as Deposit);
                money.Show();
            }
            if(NonDepositAccount.SelectedItem != null && (NonDepositAccount.SelectedItem as Nondeposit).Status)
            {
                refill = new Session<Nondeposit>(NonDepositAccount.SelectedItem as Nondeposit);
                money.Show();
            }
        }

        /// <summary>
        /// Метод, подписанный на событие нажатия кнопки ОК в окне ввода суммы денег
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButtonRefillClick(object sender, RoutedEventArgs e)
        {
            refill.Refill(Convert.ToInt32(money.MoneyBox.Text));
            DepositAccount.Items.Refresh();
            NonDepositAccount.Items.Refresh();
        }

        /// <summary>
        /// Обработчик события нажатия кнопки Перевода денег со счета на счет
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Transfer_Click(object sender, RoutedEventArgs e)
        {
            ClientTransferWindow clientTransferWindow = default;
            if (DepositAccount.SelectedItem != null && (DepositAccount.SelectedItem as Deposit).Status)
            {
                ITransfer<Deposit> transfer = new Session<Account>(DepositAccount.SelectedItem as Deposit);
                clientTransferWindow = new ClientTransferWindow(transfer);
                clientTransferWindow.Show();
            }
            if(NonDepositAccount.SelectedItem != null && (NonDepositAccount.SelectedItem as Nondeposit).Status)
            {
                ITransfer<Nondeposit> transfer = new Session<Account>(NonDepositAccount.SelectedItem as Nondeposit);
                clientTransferWindow = new ClientTransferWindow(transfer);
                clientTransferWindow.Show();
            }
        }
        #endregion
    }
}