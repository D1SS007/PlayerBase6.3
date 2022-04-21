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

                int userInput = Convert.ToInt32(Console.ReadLine());

                switch (userInput)
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
        }

        private void PrintPlugMassage()
        {
            Console.WriteLine("Нажмите любую кнопку для продолжения");
            Console.ReadKey();
            Console.Clear();
        }

        private void PrintPlugNegativeMassage()
        {
            Console.WriteLine("Некорректные данные\nНажмите любую кнопку для продолжения");
            Console.ReadKey();
            Console.Clear();
        }

        private bool CanNumberBeConverted(out int result)
        {
            string userInput = Console.ReadLine();
            bool successfullyConverted = int.TryParse(userInput, out result);
            return successfullyConverted;
        }

        private bool DoesPlayerExist(out int value)
        {
            value = 0;
            CanNumberBeConverted( out int result);

            if(result >= 1 && result < _players.Count + 1)
            {
                return true;
            }
            else
            {
                return false;
            }
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
                PrintPlugMassage();
            }            
        }

        private void AddNewPlayer()
        {
            Console.WriteLine("Введите имя");
            string userInputName = Console.ReadLine();
            Console.WriteLine("Введите уровень"); 
            bool successfullyConverted = CanNumberBeConverted(out int playerLevel);

            if (successfullyConverted)
            {
                if(playerLevel > 0 && playerLevel < int.MaxValue)
                {
                    _players.Add(new Player(playerLevel, userInputName));
                    Console.WriteLine("Игрок успешно добавлен");
                    PrintPlugMassage();
                }
                else
                {                    
                    PrintPlugNegativeMassage();
                }
            }
            else
            {
                PrintPlugNegativeMassage();
            }            
        }       

        private void BanPlayer()
        {           
            if (_players.Count > 0)
            {
                ShowAllPlayers();
                Console.WriteLine("Введите порядковый номер игрока которого хотите заблокировать");
                
                if (DoesPlayerExist(out int result))
                {
                    if (_players[result - 1].IsBanned == false)
                    {
                        _players[result - 1].Ban();
                        Console.WriteLine($"Игрок {_players[result-1].Name} успешно заблокирован ");
                        PrintPlugMassage();
                    }
                    else
                    {
                        Console.WriteLine("Игрок уже заблокирован");
                        PrintPlugMassage();
                    }
                }
                else
                {
                    PrintPlugNegativeMassage();
                }
            }
            else
            {
                Console.WriteLine("Ваш сервер пустой");
                PrintPlugMassage();
            }
        }

        private void UnBanPlayer()
        {
            if (_players.Count > 0)
            {
                ShowAllPlayers();
                Console.WriteLine("Введите порядковый номер игрока которого хотите разблокировать");                

                if (DoesPlayerExist(out int result))
                {
                    if (_players[result - 1].IsBanned == true)
                    {
                        _players[result - 1].UnBan();
                        Console.WriteLine($"Игрок {_players[result - 1].Name} успешно разблокирован ");
                        PrintPlugMassage();
                    }
                    else
                    {
                        Console.WriteLine("Игрок не заблокирован");
                        PrintPlugMassage();
                    }
                }
                else
                {
                    PrintPlugNegativeMassage();
                }
            }
            else
            {
                Console.WriteLine("Ваш сервер пустой");
                PrintPlugMassage();
            }
        }

        private void DeletePalyer()
        {
            if (_players.Count > 0)
            {
                ShowAllPlayers();
                Console.WriteLine("Кого хотите удалить из базы?");
                
                if (DoesPlayerExist(out int result))
                {
                    Console.WriteLine("Игрок успешно удален");
                    _players.RemoveAt(result - 1);
                    PrintPlugMassage();
                }
                else
                {
                    PrintPlugNegativeMassage();
                }               
            }
            else
            {
                Console.WriteLine("База данных пустая");
                PrintPlugMassage();
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

        public Player(int level, string name)
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
