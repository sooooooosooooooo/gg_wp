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
            Map map = new Map(20, 40, 10, 10);
            char[,] Map = map.CreateMap();                                         // generating map and placing in 2d array,generation in map.cs
            Console.Write("choose nickname for your hero:");
            Player player = new Player(Console.ReadLine());                         // answering for a hero name to plaier
            int enemy_count = map.Enemy_count;
            Console.Clear();
            bool end = true;
            while (end)                                                            // making eternal cycle
            {
                Console.SetCursorPosition(0, 0);
                MainFunctions.ShowMap(Map);                                        // showing map with ShowMap() from MainFunctions.cs
                Console.SetCursorPosition(player.y_position, player.x_position);   // placing player on map,player stats in Creature.cs 
                Console.Write(player.id);
                for (int i = 0; i < 5; i++)                                       // showing player stats,function in creatures.cs - ShowStats()
                {
                    Console.SetCursorPosition(map.Map_width + 15, i);
                    player.ShowStats(i);
                }
                Console.SetCursorPosition(map.Map_width + 15, 6);                     // showing how many enemyes left to win
                Console.Write($"DEFEAT {enemy_count} ENEMIES TO WIN! ENEMY -> *");
                ConsoleKeyInfo key = Console.ReadKey();
                MainFunctions.PlayerMovement(key, player, Map);                         // player movements,function in MainFunctions.cs - PlayerMovement()
                Console.SetCursorPosition(0, map.Map_length + 2);
                if (Map[player.x_position,player.y_position] == '&')                      //when player meet healing potion - condition
                {
                    Map[player.x_position, player.y_position] = ' ';
                    player.health_potion_count++;
                }
                else if (Map[player.x_position,player.y_position] == '*')                   // when player meet enemy - condition
                {
                    int enemy = rnd.Next(0,3);
                    string[] enemys = new string[3] {"kobold","goblin","wolf"};
                    MainFunctions.EnemyMeetingAndFight(enemy, player, ref enemy_count, Map,ref end, key);          // enemy meeting and fight scene,function in MainFunctions.cs - EnemyMeetingAndFight()
                }
                Console.Clear();
                MainFunctions.GameOver(ref end, enemy_count);                                                       // game over contidion,function in MainFunctions.cs - GameOver()
            }
        }
    }
}
