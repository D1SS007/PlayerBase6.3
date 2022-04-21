using System;
using System.Collections.Generic;

namespace PlayerBase6._3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Base playersBase = new Base();
            playersBase.Work();
        }
    }

    class Base
    {
        private bool _isWorking = true;

        private List<Player> _players = new List<Player>();  
        
        public void Work()
        {
            while (_isWorking)
            {
                Console.WriteLine("1 - Показать всех игроков\n2 - Добавить игрока\n3 - Блокировка игрока\n4 - Разблокировка игрока\n5 - Удаление игрока\n6 - Выход");

                bool successfullyConverted = CanNumberBeConverted(out int result);

                if (successfullyConverted)
                {
                    switch (result)
                    {
                        case 1:
                            ShowAllPlayers();
                            break;

                        case 2:
                            AddNewPlayer();
                            break;

                        case 3:
                            BanPlayer();
                            break;

                        case 4:
                            UnBanPlayer();
                            break;

                        case 5:
                            DeletePalyer();
                            break;

                        case 6:
                            _isWorking = false;
                            break;

                        default:
                            Console.WriteLine("неправильно введенные двнные");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Данные некорректны");
                    Patch();                    
                }                
            }
        }

        private void Patch()
        {
            Console.WriteLine("Нажмите любую кнопку для продолжения");
            Console.ReadKey();
            Console.Clear();
        }

        private bool CanNumberBeConverted(out int result)
        {
            string userInput = Console.ReadLine();
            bool successfullyConverted = int.TryParse(userInput, out result);
            return successfullyConverted;
        }

        private void ShowAllPlayers()
        {
            if(_players.Count > 0)
            {
                Console.WriteLine("\nНомер |  Имя  |  Уровень | Статус");

                for (int i = 0; i < _players.Count; i++)
                {                    
                    _players[i].ShowPlayerInfo();
                }
            }
            else
            {
                Console.WriteLine("В базе нет игроков");
                Patch();
            }            
        }

        private void AddNewPlayer()
        {
            Console.WriteLine("Введите имя");
            string userInputName = Console.ReadLine();
            Console.WriteLine("Введите уровень"); 
            bool successfullyConverted = CanNumberBeConverted(out int result);

            if (successfullyConverted)
            {
                if(result > 0 && result < int.MaxValue)
                {
                    _players.Add(new Player(result, userInputName));
                    Console.WriteLine("Игрок успешно добавлен");
                    Patch();
                }
                else
                {
                    Console.WriteLine("Некорректные данные");
                    Patch();
                }
            }
            else
            {
                Console.WriteLine("Некорректные данные");
                Patch();
            }            
        }       

        private void BanPlayer()
        {           
            if (_players.Count > 0)
            {
                ShowAllPlayers();
                Console.WriteLine("Введите порядковый номер игрока которого хотите заблокировать");
                bool successfullyConverted = CanNumberBeConverted(out int result);

                if (successfullyConverted)
                {
                    if (_players[result - 1].IsBanned == false)
                    {
                        _players[result - 1].Ban();
                        Console.WriteLine($"Игрок {_players[result-1].Name} успешно заблокирован ");
                        Patch();
                    }
                    else
                    {
                        Console.WriteLine("Игрок уже заблокирован");
                        Patch();
                    }
                }
                else
                {
                    Console.WriteLine("Вы ввели некорректные данные");
                    Patch();
                }
            }
            else
            {
                Console.WriteLine("Ваш сервер пустой");
                Patch();
            }
        }

        private void UnBanPlayer()
        {
            if (_players.Count > 0)
            {
                ShowAllPlayers();
                Console.WriteLine("Введите порядковый номер игрока которого хотите разблокировать");
                bool successfullyConverted = CanNumberBeConverted( out int result); 

                if (successfullyConverted)
                {
                    if (_players[result - 1].IsBanned == true)
                    {
                        _players[result - 1].UnBan();
                        Console.WriteLine($"Игрок {_players[result - 1].Name} успешно разблокирован ");
                        Patch();
                    }
                    else
                    {
                        Console.WriteLine("Игрок не заблокирован");
                        Patch();
                    }
                }
                else
                {
                    Console.WriteLine("Вы ввели некорректные данные");
                    Patch();
                }
            }
            else
            {
                Console.WriteLine("Ваш сервер пустой");
                Patch();
            }
        }

        private void DeletePalyer()
        {
            if (_players.Count > 0)
            {
                ShowAllPlayers();
                Console.WriteLine("Кого хотите удалить из базы?");
                bool successfullyConverted = CanNumberBeConverted(out int result);
                
                if (successfullyConverted)
                {
                    Console.WriteLine("Игрок успешно удален");
                    _players.RemoveAt(result - 1);
                    Patch();
                }
                else
                {
                    Console.WriteLine("Некорректные данные");
                    Patch();
                }               
            }
            else
            {
                Console.WriteLine("База данных пустая");
                Patch();
            }
        } 
    }

    class Player
    {
        public static int Ids { get; private set; }
        public int Id { get; private set; }
        public int Level { get; private set; }
        public string Name { get; private set; }
        public bool IsBanned { get; private set; }

        public Player(/*int id,*/ int level, string name)
        {
            Id = ++Ids;
            Level = level;
            Name = name;
            IsBanned = false;
        }

        public void Ban()
        {
            IsBanned = true;
        }

        public void UnBan()
        {
            IsBanned = false;
        }        

        public void ShowPlayerInfo()
        {  
            if(IsBanned == true)
            {
                Console.WriteLine($"{Id}     | {Name} |    {Level}    | Заблокирован");
            }
            else
            {
                Console.WriteLine($"{Id}     | {Name} |    {Level}    | Разблокирован");
            }
            Console.WriteLine();
        }
    }
}
