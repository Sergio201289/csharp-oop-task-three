using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework13
{
    /// <summary>
    /// Параметризированный класс, который принимает в качестве входящего параметра любой счет и задает модель работы с ним.
    /// </summary>
    /// <typeparam name="T">Счет</typeparam>
    class Session<T> : IRefill<T>, ITransfer<T>
        where T: Account, new()
    {
        /// <summary>
        /// Поле текущего счета
        /// </summary>
        private T sessionAccount;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Account"></param>
        public Session(T Account)
        {
            sessionAccount = Account;
        }

        #region Методы

        /// <summary>
        /// Метод открытия счета
        /// </summary>
        public void Open()
        {
            sessionAccount.Status = true;
        }

        /// <summary>
        /// Метод закрытия счета
        /// </summary>
        public void Close()
        {
            sessionAccount.Status = false;
        }

        /// <summary>
        /// Метод пополнения счета. Реализует ковариантный интерфейс.
        /// </summary>
        /// <param name="Amount">Сумма</param>
        /// <returns></returns>
        public T Refill(int Amount)
        {
            sessionAccount.Balance += Amount;
            return sessionAccount;
        }

        /// <summary>
        /// Метод перевода денег между счетами. Реализует контрвариантный интерфейс.
        /// </summary>
        /// <param name="Amount"></param>
        /// <param name="SendersAccount"></param>
        public void Transfer(int Amount, T SendersAccount)
        {
            SendersAccount.Balance -= Amount;
            sessionAccount.Balance += Amount;
        }
        
        #endregion
    }
}
