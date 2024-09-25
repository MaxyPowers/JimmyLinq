﻿using JimmyLinq;

/* 
 * Класс комикс описывает объект комикс
 * Необходим для сортировки и выборки комиксов по заданным параметрам
 */

namespace JoesComic //Комиксы Джо
{
    public class Comic
    {
        public string Name { get; set; } //Название комикса
        public int Issue { get; set; } //Номер выпуска
        /// <summary>
        /// Возвращает строковое представление объекта Comic
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Name} (Issue #{Issue})";
        /// <summary>
        /// Заглушка. Список объектов комикс с названием и номером выпуска доступных для обработки
        /// </summary>
        public static readonly IEnumerable<Comic> Catalog = new List<Comic>
        {
            new Comic {Name = "Johnny America vs. the Pinko", Issue = 6},
            new Comic {Name = "Rock and Roll (limited edition)", Issue = 19},
            new Comic {Name = "Woman's Work", Issue = 36},
            new Comic {Name = "Hippie Madness (misprinted)", Issue = 57},
            new Comic {Name = "Revenge of the New Wave Freak (damaged)", Issue = 68},
            new Comic {Name = "Black Monday", Issue = 74},
            new Comic {Name = "Tribal Tattoo Madness", Issue = 83},
            new Comic {Name = "The Death of the Object", Issue = 97},
        };
        /// <summary>
        /// Заглушка. Список содержит ценны на каждый отдельный номер из коллекции
        /// </summary>
        public static readonly IReadOnlyDictionary<int, decimal> Prices = new Dictionary<int, decimal>
        {
            {6, 3600M},
            {19, 500M},
            {36, 650M},
            {57, 13525M},
            {68, 250M},
            {74, 75M},
            {83, 25.75M},
            {97, 35.25M},
        };
        /// <summary>
        /// Заглушка. Список с отзывами из разных источников на некоторые комиксы
        /// </summary>
        public static readonly IEnumerable<Review> Reviews = new[] 
        {
            new Review() { Issue = 36, Critic = Critics.MuddyCritic, Score = 37.6 },
            new Review() { Issue = 74, Critic = Critics.RottenTornadoes, Score = 22.8 },
            new Review() { Issue = 74, Critic = Critics.MuddyCritic, Score = 84.2 },
            new Review() { Issue = 83, Critic = Critics.RottenTornadoes, Score = 89.4 },
            new Review() { Issue = 97, Critic = Critics.MuddyCritic, Score = 98.1 },
        };
    }
}