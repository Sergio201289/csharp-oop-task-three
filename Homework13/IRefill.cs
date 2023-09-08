using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework13
{
    /// <summary>
    /// Ковариантный интерфейс пополнения счета
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRefill<out T>
    {
        T Refill(int money);
    }
}
