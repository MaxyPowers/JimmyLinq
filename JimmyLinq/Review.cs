using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Класс заглушка для храния отзывов о комиксах
 */
namespace JimmyLinq
{
    /// <summary>
    /// Класс описывает один конкретный отзыв на один конкретный номер комикса
    /// </summary>
    public class Review
    {
        public int Issue {  get; set; } //Номер комикса
        public Critics Critic { get; set; } //Ресуср откуда был выбран отзыв
        public double Score { get; set; } // Общий рейтин этого выпуска
    }
}
