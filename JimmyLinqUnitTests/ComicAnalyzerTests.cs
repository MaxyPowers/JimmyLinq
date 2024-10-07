namespace JimmyLinqUnitTests
{
    using JimmyLinq;
    using JoesComic;

    /// <summary>
    /// Тест класса ComicAnalyzer
    /// </summary>
    [TestClass]
    public class ComicAnalyzerTests
    {
        /// <summary>
        /// Тестовое перечисление каталога комиксов
        /// </summary>
        IEnumerable<Comic> testComic = new[]
        {
            new Comic() {Issue = 1, Name = "Issue 1" },
            new Comic() {Issue = 2, Name = "Issue 2" },
            new Comic() {Issue = 3, Name = "Issue 3" },
        };
        /// <summary>
        /// Метод класса должен сгрупировать и отсортировать комиксы на дешевые и дорогие
        /// </summary>
        [TestMethod]
        public void ComicAnalyzer_Should_Group_Comics()
        {
            // Словарь сожержит два дешевых и один дорой комикс, ключ - номер комикса
            var prices = new Dictionary<int, decimal>()
            {
                {1, 20M },
                {2, 10M },
                {3, 1000M }
            };
            //Вызываем метод "Сгрупировать комксы по цене", пердать в качестве агрументов: тестовый каталог (3 комикса),
            //Тестовый словарь (3 номера комиксов, 2 цены да 100М, одна цена выше 100М)
            var groups = ComicAnalyzer.GroupComicByPrice(testComic, prices);

            Assert.AreEqual(2, groups.Count());                     //Ожидается 2 группы комиксов
            Assert.AreEqual(PriceRange.Cheap, groups.First().Key);  //Ожидается ключ первой группы "Cheap"
            Assert.AreEqual(2, groups.First().First().Issue);       //Ожидается в первой группе первый комикс с номером - 2
            Assert.AreEqual("Issue 2", groups.First().First().Name);//Ожидается имя первого комикса в первой группе "Issue 2"
        }

        /// <summary>
        /// Метод должен сгенерировать список критики на доступные комиксы
        /// </summary>
        [TestMethod]
        public void ComicAnalyzer_Should_Generate_A_List_Of_Reviews()
        {
            //Массив критики на тестовые комиксы (Только номера 1 и 2)
            var testReviews = new[]
            {
                new Review() { Issue = 1, Critic = Critics.MuddyCritic, Score = 14.5},
                new Review() { Issue = 1, Critic = Critics.RottenTornadoes, Score = 59.93},
                new Review() { Issue = 2, Critic = Critics.MuddyCritic, Score = 40.3},
                new Review() { Issue = 2, Critic = Critics.RottenTornadoes, Score = 95.11},
            };
            //Массив строк содержит ожидаемый от метода результат. 
            //Нужен для сравнения с полученным результатом
            var expectedResults = new[]
            {
                "MuddyCritic rated #1 'Issue 1' 14,50",
                "RottenTornadoes rated #1 'Issue 1' 59,93",
                "MuddyCritic rated #2 'Issue 2' 40,30",
                "RottenTornadoes rated #2 'Issue 2' 95,11",
            };
            //Вызов метода "Получить критику", в качестве аргументов - тестовый список комиксов, массив критики.
            var actualResults = ComicAnalyzer.GetReviews(testComic, testReviews).ToList();
            CollectionAssert.AreEqual(expectedResults, actualResults); //Сравнить результат созданный методом с ожидаемым результатом
        }

        [TestMethod]
        public void ComicAnalyzer_Should_Handle_Weird_Review_Scores()
        {
            var testReviews = new[]
            {
                new Review() { Issue = 1, Critic = Critics.MuddyCritic, Score = -12.1212},
                new Review() { Issue = 1, Critic = Critics.RottenTornadoes, Score = 391691234.48931},
                new Review() { Issue = 2, Critic = Critics.RottenTornadoes, Score = 0},
                new Review() { Issue = 2, Critic = Critics.MuddyCritic, Score = 40.3},
                new Review() { Issue = 2, Critic = Critics.MuddyCritic, Score = 40.3},
                new Review() { Issue = 2, Critic = Critics.MuddyCritic, Score = 40.3},
                new Review() { Issue = 2, Critic = Critics.MuddyCritic, Score = 40.3},
            };

            var expectedResults = new[]
            {
                "MuddyCritic rated #1 'Issue 1' -12,12",
                "RottenTornadoes rated #1 'Issue 1' 391691234,49",
                "RottenTornadoes rated #2 'Issue 2' 0,00",
                "MuddyCritic rated #2 'Issue 2' 40,30",
                "MuddyCritic rated #2 'Issue 2' 40,30",
                "MuddyCritic rated #2 'Issue 2' 40,30",
                "MuddyCritic rated #2 'Issue 2' 40,30",
            };

            var actualResults = ComicAnalyzer.GetReviews(testComic, testReviews).ToList();
            CollectionAssert.AreEqual(expectedResults, actualResults);
        }
    }
}