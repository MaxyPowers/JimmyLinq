using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoesComic;

/*
 *Реализация сортировки комисов 
 */

namespace JimmyLinq
{
    /// <summary>
    /// Статический класс (не требует создания объекта для использования методов).
    /// Сортирует комиксы по цене и/или популярности 
    /// </summary>
    public static class ComicAnalyzer
    {
        /// <summary>
        /// Обозначить комикс дешевым или дорогим в зависимости от стоимости входящего экземляра
        /// </summary>
        /// <param name="comic">Обьект Comic из статической коллеции в классе Comic</param>
        /// <param name="prices">Словарь содержит цены комиксов, ключ - номер комикса</param>
        /// <returns>Возвращает значение перечеслиния ценовых диапазонов</returns>
        private static PriceRange CalculatePriceRange (Comic comic, IReadOnlyDictionary<int, decimal> prices)
        {
            if (prices[comic.Issue] < 100M) //Если цена данного комикса меньше 100
                return PriceRange.Cheap;          //Комикс дешевый
            else
                return PriceRange.Expensive;      //Комикс дорогой
            
        }
        /// <summary>
        /// Метод разделяет комиксы из списка на группы по ценовым диапазонам и сортирует их от меньшей цены к большей
        /// </summary>
        /// <param name="comics">Коллекция доступных комиксов</param>
        /// <param name="prices">Словарь с ценами комиксов, где ключь это номер комикса, а значение - цена</param>
        /// <returns>Возвращает группы комиксов по ценовым диапазонам</returns>
        public static IEnumerable<IGrouping<PriceRange,Comic>> GroupComicByPrice(IEnumerable<Comic> comics, IReadOnlyDictionary<int, decimal> prices)
        {
            //Обявить перечисление групп комиксов и выполнить запрос LINQ для группировки комиксов по ценовым диапазонам
            //Отсортировать комиксы по цене от меньшей к большей
            //Сгрупировать комиксы по ценовому диапазону вычисленному методом
            var grouped = comics.OrderBy(c => prices[c.Issue]).GroupBy(c => CalculatePriceRange(c, prices));
            return grouped;//Вернуть результат группировки
        }

        /// <summary>
        /// Метод создаёт строковое перечисление объеденяя комиксы с отзывами по номеру выпуска
        /// </summary>
        /// <param name="comics">Коллекция доступных комиксов</param>
        /// <param name="reviews">Перечисление рецензий на отдельные выпуски комиксов</param>
        /// <returns>Возвращает строковое перечисление с рейтингом комисков</returns>
        public static IEnumerable<string> GetReviews (IEnumerable<Comic> comics, IEnumerable<Review> reviews)
        {
            //Выполнить запрос LINQ, объеденить комискы с рецензиями по номеру выпуска
            //Отсортировать комиксы по номеру выпуска
            //Обеденить комиксы с отзывами по номеру выпуска
            //Сформировать строку с информацией о критике, номером и названием комикса и его рейтингом
            var joined = comics.OrderBy(comic => comic.Issue).Join(reviews,
                comic => comic.Issue,
                review => review.Issue,
                (comic, review) => $"{review.Critic} rated #{comic.Issue} '{comic.Name}' {review.Score:0.00}");
            return joined;  //Вернуть результат объединения
        }
    }
}
