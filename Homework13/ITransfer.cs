using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework13
{
    /// <summary>
    /// Контрвариатный интерфейс перевода денег между счетами
    /// </summary>
    /// <typeparam name="T">Счет</typeparam>
    public interface ITransfer<in T>
    {
        void Transfer(int money, T account);
    }
}