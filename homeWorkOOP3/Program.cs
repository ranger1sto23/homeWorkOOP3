﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homeWorkOOP3
{
    class Database
    {
        //dsdsdsdsdsadasdasdasdas
        private List<Player> _players;
        public Database()
        {
            _players = new List<Player>();
        }

        public void AddPLayer(Player player)
        { 
            _players.Add(player);
        }

        public void BlockPlayer(int indexBlock)
        {
            if(TryGetPlayer(indexBlock, out Player player))
            _players[indexBlock].BlockPlayer();
        }

        public void UnlockPlayer(int indexUnlock)
        {
            if (TryGetPlayer(indexUnlock, out Player player))
                _players[indexUnlock].UnlockPlayer();
        }

        public void PrintPlayer()
        {
            foreach (var player in _players)
            {
                player.ShowInfo();
            }
        }

        public void RemovePlayer(int indexRemove)
        {
            if(TryGetPlayer(indexRemove, out Player player))
                _players.RemoveAt(indexRemove);
        }

        private bool TryGetPlayer(int id, out Player player  )
        { 
             player = _players.Where(currentPlayer => currentPlayer.ID == id).FirstOrDefault();
            return player != null;
        }
    }

    class Player
    {
        private static int _ids = 0;
      //  private Guid _id;
        private string _name;
        private int _level;
        private bool _flag;

        public int ID { get; private set; }

        public Player(string name,int level)
        {
            ID = ++_ids;
           //_id = Guid.NewGuid();
            _name = name;
            _level = level;
            _flag = false;

        }

        public void BlockPlayer()
        { 
            _flag = true;
        }

        public void UnlockPlayer()
        {
            _flag = false;
        }

        internal void ShowInfo()
        {
          
            Console.WriteLine($"ID - {ID} имя - {_name} уровень - {_level} флаг - {_flag} ");
        }
    }
    class Program
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

            Database database = new Database();
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

                switch ( command ) 
                {
                    case CommandAdd:
                        AddPlayer(database);
                        break;
                    case CommandBlock:
                        ExecuteCommand(HintBlockPlayer,database.BlockPlayer);
                        break;
                    case CommandUnlock:
                        ExecuteCommand(HintUnlockPlayer, database.UnlockPlayer);
                        break;
                    case CommandRemove:
                        ExecuteCommand(HintRemovePlayer, database.RemovePlayer);
                        break;

                    case CommandPrint:

                        PrintPlayer(database);
                        break;
                    case CommandExit:
                        
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            }


        }

        private static void ExecuteCommand(string hintRemovePlayer,Action<int> action)
        {
            Console.WriteLine(hintRemovePlayer);
            string input = Console.ReadLine();
            if (int.TryParse(input, out int result))
                action.Invoke(result);
        }

       

        private static void PrintPlayer(Database database)
        {
            database.PrintPlayer();
        }

        private static void AddPlayer(Database database)
        {
            Console.WriteLine("Введите ник");
            string name = Console.ReadLine();
            Console.WriteLine("Введите уровень");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int level))
            {
                database.AddPLayer(new Player(name, level));
            }

        }
    }
}
