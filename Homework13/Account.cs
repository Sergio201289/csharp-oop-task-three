using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework13
{
    /// <summary>
    /// Класс счета
    /// </summary>
    public class Account
    {
        #region Поля

        int balance;                //Поле баланса

        static int increment = 0;   //Поле инкремента

        int numberOfAccount;        //Поле номера счета

        DateTime dateTime;          //Поле даты и времени

        bool status;                //Поле статуса
        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        public Account()
        {
            increment++;
            numberOfAccount = increment;
            dateTime = DateTime.Now;
            balance = default;
        }

        #region Свойства

        public int Balance { get { return balance; } set { balance = value; } }                         //Свойство баланса
        public int NumberOfAccount { get { return numberOfAccount; } set { numberOfAccount = value; } } //Свойство номера счета
        public DateTime DateTime { get { return dateTime; } }                                           //Свойство даты и времени
        public bool Status { get { return status; } set { status = value; } }                           //Свойство статуса
        #endregion
    }
}