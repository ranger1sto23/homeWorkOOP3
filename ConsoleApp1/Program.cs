using System;
using System.Collections.Generic;
using System.Linq;

namespace Task2
{
    public static class Program
    {
        static void Main(string[] args)
        {
            const string CommandAdd = "1";
            const string CommandBlock = "2";
            const string CommandUnlock = "3";
            const string CommandRemove = "4";
            const string CommandPrint = "5";
            const string CommandExit = "6";
            const string HintBlockPlayer = "введите номер игрока которого хотите забанить";
            const string HintUnlockPlayer = "введите номер игрока которого хотите разбанить";
            const string HintRemovePlayer = "введите номер игрока которого хотите удалить";

            Database playersDatabase = new Database();
            string command = string.Empty;

            while (command != CommandExit)
            {
                Console.WriteLine($"{CommandAdd}. добавить игрока\n" +
                    $"{CommandBlock}. забанить игрока\n" +
                    $"{CommandUnlock}. разбанить игрока\n" +
                    $"{CommandRemove}. удалить игрока\n" +
                    $"{CommandPrint}. показать игроков\n" +
                    $"{CommandExit}. выход");
                command = Console.ReadLine();

                switch (command)
                {
                    case CommandAdd:
                        AddPlayer(playersDatabase);
                        break;
                    case CommandBlock:
                        ExecuteCommand(HintBlockPlayer, playersDatabase.BlockPlayer);
                        break;
                    case CommandUnlock:
                        ExecuteCommand(HintUnlockPlayer, playersDatabase.UnlockPlayer);
                        break;
                    case CommandRemove:
                        ExecuteCommand(HintRemovePlayer, playersDatabase.RemovePlayer);
                        break;
                    case CommandPrint:
                        PrintPlayers(playersDatabase);
                        break;
                    case CommandExit:
                        break;
                }
            }
        }

        private static void ExecuteCommand(string hint, Action<int> command)
        {
            Console.WriteLine(hint);
            string input = Console.ReadLine();

            if (int.TryParse(input, out int id))
            {
                command.Invoke(id);
            }
        }

        private static void PrintPlayers(Database database)
        {
            foreach (var player in database.GetPlayers())
            {
                player.PrintInfo();
            }
        }

        private static void AddPlayer(Database database)
        {
            Console.WriteLine("введите имя");
            string name = Console.ReadLine();
            Console.WriteLine("введите уровень");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int level))
            {
                database.AddPlayer(new Player(name, level));
            }
        }
    }

    public class Database
    {
        private List<Player> _players;

        public Database()
        {
            _players = new List<Player>();
        }

        public void AddPlayer(Player player)
        {
            _players.Add(player);
        }

        public void RemovePlayer(int id)
        {
            if (TryGetPlayer(id, out Player player))
            {
                _players.Remove(player);
            }
        }

        public void BlockPlayer(int id)
        {
            if (TryGetPlayer(id, out Player player))
            {
                player.Block();
            }
        }

        public void UnlockPlayer(int id)
        {
            if (TryGetPlayer(id, out Player player))
            {
                player.Unlock();
            }
        }

        public IEnumerable<Player> GetPlayers()
        {
            foreach (var player in _players)
            {
                yield return player;
            }
        }

        private bool TryGetPlayer(int id, out Player player)
        {
            player = _players.Where(currentPlayer => currentPlayer.Id == id).FirstOrDefault();
            return player != null;
        }
    }

    public class Player
    {
        private static int _ids = 0;
        private string _name;
        private int _level;
        private bool _isBlocked;

        public int Id { get; private set; }

        public Player(string name, int level, bool isBlocked = false)
        {
            Id = ++_ids;
            _name = name;
            _level = level;
            _isBlocked = isBlocked;
        }

        public void Block()
        {
            _isBlocked = true;
        }

        public void Unlock()
        {
            _isBlocked = false;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"id={Id}, name={_name}, level={_level}, is bunned={_isBlocked}");
        }
    }
}