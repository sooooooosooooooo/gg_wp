using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gg_wp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            Map map = new Map(20, 40, 1, 5);
            char[,] Map = map.CreateMap();
            Console.Write("choose nickname for your hero:");
            Player player = new Player(Console.ReadLine());
            int enemy_count = map.Enemy_count;
            Console.Clear();
            bool end = true;
            while (end)                                                            // making eternal cycle
            {
                Console.SetCursorPosition(0, 0);
                for (int i = 0; i < Map.GetLength(0); i++)                        // printing map for visualisint position
                {
                    for (int j = 0; j < Map.GetLength(1); j++)
                    {
                        Console.Write(Map[i, j]);
                    }
                    Console.WriteLine();
                }
                Console.SetCursorPosition(player.y_position, player.x_position);  // placing player on map
                Console.Write(player.id);
                for (int i = 0; i < 5; i++)                                       // showing player stats
                {
                    Console.SetCursorPosition(map.Map_width + 15, i);
                    player.ShowStats(i);
                }
                Console.SetCursorPosition(map.Map_width + 15, 6);                   // showing how many enemyes left to win
                Console.Write($"DEFEAT {enemy_count} ENEMIES TO WIN! ENEMY -> *");
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)                                                    //players moving and checking is there wall or not
                {
                    case ConsoleKey.UpArrow:
                        if (Map[player.x_position - 1, player.y_position] != '#')
                            player.x_position--;break;
                    case ConsoleKey.DownArrow:
                        if (Map[player.x_position + 1, player.y_position] != '#')
                            player.x_position++;break;
                    case ConsoleKey.LeftArrow:
                        if (Map[player.x_position, player.y_position - 1] != '#')
                            player.y_position--;break;
                    case ConsoleKey.RightArrow:
                        if (Map[player.x_position,player.y_position + 1] != '#')
                            player.y_position++;break;
                    
                }
                Console.SetCursorPosition(0, map.Map_length + 2);
                if (Map[player.x_position,player.y_position] == '&')                      //when player meet healing potion - condition
                {
                    Map[player.x_position, player.y_position] = ' ';
                    player.health_potion_count++;
                }
                else if (Map[player.x_position,player.y_position] == '*')                   // when player meet enemy - condition
                {
                    int enemy = 0;
                    string[] enemys = new string[3] {"kobold","goblin","wolf"};
                    switch (enemy)                                                         // fight code compilation
                    {
                        case 0:                                                            //code of Fight() in creatures.cs  - (there player choose fight 
                            Kobold kobold = new Kobold();                                  //or retreat and in 1st condition full fight scene code)
                            Creature.Fight( player, kobold, key, end);
                            Map[player.x_position, player.y_position] = ' ';
                            break;
                        case 1:
                            Goblin goblin = new Goblin();
                            Creature.Fight( player, goblin, key, end);
                            Map[player.x_position, player.y_position] = ' ';
                            break;
                        case 2:
                            Wolf wolf = new Wolf();
                            Creature.Fight( player, wolf, key, end);
                            Map[player.x_position, player.y_position] = ' ';
                            break;
                    }
                    enemy_count--;
                }
                Console.Clear();
                if (enemy_count == 0)                                                         // player killing all enemyes and wining announcement by prog
                {
                    Console.WriteLine("Great you defeated all enemys and save world!\n" +
                        "i guess you liked this mini game :)");
                    end = false;
                    Console.ReadKey();
                }
            }
        }
    }
}
