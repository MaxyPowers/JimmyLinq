using JoesComic;
using JimmyLinq;

/*
 * Выполнить сортировку или объединение, вывести результат на консоль
 */

public class Program
{
    static void Main(string[] args)
    {
        //Булевая переменная для управления циклом
        var done = false;
        //Цикл будет выполняться до тек пор пока done - не истина
        while (!done)
        {
            //Пользователь долеж выпбрать хочет ли он получить отсортированные по цене комиксы или рейтинги критики на некоторые выпуски
            Console.WriteLine("\nPress G to group comics by price, R to get reviews, any other key to quit\n");
            done = Console.ReadKey(true).KeyChar.ToString().ToUpper() switch
            {
                "G" => GroupComicsByPrice(),//Пользователь хочет получить группы отсортированных по цене комиксов
                "R" => GetReviews(),        // Пользователь хочет получить объедение комиксов и критики на них
                _ => true,                  //Завершить выполнение программы
            };
        }
    }

    /// <summary>
    /// Выводит на консоль отсортированые по цене группы комиксов
    /// </summary>
    /// <returns>Вернуть false для продолжения выполнения программы</returns>
    private static bool GroupComicsByPrice()
    {
        //Инициализировать перечисление групп комиксов отсортированых по цене с помощью метода GroupComicByPrice класса ComicAnalyzer
        var groups = ComicAnalyzer.GroupComicByPrice(Comic.Catalog, Comic.Prices);
        //Перебрать группы комиксов
        foreach (var group in groups)
        {
            Console.WriteLine($"{group.Key} comics:"); //Вывести на консоль ключь группы
            foreach (var comic in group)
                Console.WriteLine($"#{comic.Issue} {comic.Name}: {Comic.Prices[comic.Issue]:c}"); //Вывести на консоль номер, имя и стоимость комикса
        }
        return false; //Вернуть false для продолжения выполнения программы
    }

    /// <summary>
    /// Вывести на консоль рейтинги комиксов
    /// </summary>
    /// <returns>Возвращает false для продолжения выполнения программы</returns>
    private static bool GetReviews()
    {
        //Инициализировать строковое перечисление с информацией о критике, номером и рейтингом комикса
        var reviews = ComicAnalyzer.GetReviews(Comic.Catalog, Comic.Reviews);
        //Перебрать перечисление с критикой комиксов
        foreach (var review in reviews)
            Console.WriteLine(review); //Вывести на консоль строку с информацией о критике номерм и рейтингом комикса
        return false; //Вернуть false для продолжения выполнения программы
    }
}