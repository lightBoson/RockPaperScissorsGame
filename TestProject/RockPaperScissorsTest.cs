using NUnit.Framework;
using RockPaperScissors;
using System.Collections.Generic;
using System.IO;
using System;
using Moq;


namespace TestProject
{
    public class RockPaperScissorsTest
    {
        //Atributes 
        private Gamer _testGamerHuman = null;
        private Gamer _testGamerComputer = null;
        private Gameplay _testGameplay = null;
        [SetUp]
        public void Setup()
        {
            _testGamerHuman = new Gamer("TestGamer", GamerType.Human);
            _testGamerComputer = new Gamer("TestComputer", GamerType.Computer);
            _testGameplay = new Gameplay(_testGamerHuman, _testGamerComputer, 7);
         
        }
        //Sprawdzenie czy gracz wpisal poprawna odpowiedz R,S,T, getGamerAnswer()
        [Test]
        public void getGamerAnswer_withCorrectAnswerGamer_ResultCorrectAnswerGamer()
        {
            Answer gamerAnswer = Answer.Unknown;
            // Sprawdz czy kamien wchodzi
            var stringReaderRock = new StringReader("R");
            Console.SetIn(stringReaderRock);
            gamerAnswer = _testGameplay.getGamerAnswer(_testGamerHuman);
            Assert.AreEqual(gamerAnswer, Answer.Rock);
            // Sprawdz czy nozyce wchodza
            var stringReaderScissors = new StringReader("S");
            Console.SetIn(stringReaderScissors);
            gamerAnswer = _testGameplay.getGamerAnswer(_testGamerHuman);
            Assert.AreEqual(gamerAnswer, Answer.Scissors);
            // Sprawdz czy papier wchodzi
            var stringReaderPaper = new StringReader("P");
            Console.SetIn(stringReaderPaper);
            gamerAnswer = _testGameplay.getGamerAnswer(_testGamerHuman);
            Assert.AreEqual(gamerAnswer, Answer.Paper);

        }

        //Sprawdzenie regul ruchow wygranych, getResult()
        [Test]
        public void getResult_withBestResultWhichWins_ResultWins()
        {
            // Czy kamien wygrywa z nozycami
            Result results1 = _testGameplay.getResult(Answer.Rock, Answer.Scissors);
            Assert.AreEqual(results1, Result.FirstWins);
            // Czy nozyce wygrywaja z papierem
            Result results2 = _testGameplay.getResult(Answer.Scissors, Answer.Paper);
            Assert.AreEqual(results2, Result.FirstWins);
            // Czy papier wygrywa z kamieniem
            Result results3 = _testGameplay.getResult(Answer.Paper, Answer.Rock);
            Assert.AreEqual(results3, Result.FirstWins);
        }

        //Sprawdzenie czy rozgrywki dodaja sie main()
        [Test]
        public void main_withCumulationGameplay_ResultNumberGameplayPlusAnotherOne()
        {
            for(int i = 1; i <= 7; ++i)
            {
                var firstPlayer = new Gamer("first", GamerType.Computer);
                var secondPlayer = new Gamer("second", GamerType.Computer);
                var gameplay = new Gameplay(firstPlayer, secondPlayer,i);
                Assert.AreEqual(i, gameplay.getGameplayNumber());
            }
        }

        //Uzytkownik prowadza dobra nazwe uzytkownika, CreateUser()
        [Test]
        public void createUser_whitCreatNumePlayer_ResultCorrectNamePlayer()
        {
            // Sprawdz czy kamien wchodzi
            var stringReader = new StringReader("Karolina");
            Console.SetIn(stringReader);
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            Program.CreateUser();
            var output = stringWriter.ToString();
            Assert.AreEqual(output, "Please provide your name:\r\nGood luck Karolina!\r\n");
        }

        //Nazwa dla komputera pojawia sie losowo
        [Test]
        public void randomGamerName_withRandomNameComputer_ResultNameComputer()
        {
            List<string> names = new List<string>() { "Adam", "Robert", "Pierre", "Lucile", "Eva", "Kate" };
            for (int i = 1; i <= 5; ++i)
            {
                var computerName = Program.RandomGamerName();
                Assert.IsTrue(names.Contains(computerName));
            }
        }

        //Komputer losowo wybiera swoja odpowiedz, RandomGamerName()
        [Test]
        public void randomChoice_withRandomAnswerComputer_ResultAnswerComputer()
        {
            Answer answer = _testGamerComputer.randomChoice();
            List<Answer> answerList = new List<Answer> { Answer.Paper, Answer.Rock, Answer.Scissors };
            Assert.IsTrue(answerList.Contains(answer));
        }

        //Test z mockingiem
        [Test]
        public void mocking_gamer_class_test()
        {
            var proGamerMock = new Mock<IGamer>();
            proGamerMock.Setup(g => g.type()).Returns("ProGamer");
            var noobGamer = new Gamer("Janusz", GamerType.Computer);
            Assert.AreEqual(proGamerMock.Object.type(), "ProGamer");
            Assert.IsFalse(proGamerMock.Object.type() == noobGamer.type());
        }
    }
}