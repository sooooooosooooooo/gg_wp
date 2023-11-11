using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gg_wp
{
    internal class MainFunctions
    {
        public static void ShowMap(char[,] map)                                // function of showing map in console
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i,j]);
                }
                Console.WriteLine();
            }
        }
        public static void PlayerMovement(ConsoleKeyInfo key,Player player,char[,] map)         // function for players movement on the map
        {
            switch(key.Key)                                                                     // game take only arrow movements not (aswd)
            {                                                                                   // in this function we check is in the direction wall or not,if yes we 
                case ConsoleKey.UpArrow:                                                        // dont let player stand in that position
                    if (map[player.x_position - 1, player.y_position] != '#')
                        player.x_position--; break;
                case ConsoleKey.DownArrow:
                    if (map[player.x_position + 1, player.y_position] != '#')
                        player.x_position++; break;
                case ConsoleKey.LeftArrow:
                    if (map[player.x_position, player.y_position - 1] != '#')
                        player.y_position--; break;
                case ConsoleKey.RightArrow:
                    if (map[player.x_position, player.y_position + 1] != '#')
                        player.y_position++; break;
            }
        }
        public static void EnemyMeetingAndFight(int enemy,Player player,ref int enemy_count, char[,] map,ref bool end,ConsoleKeyInfo key)
        {
            switch (enemy)                                                                                                                  // fight code function
            {
                case 0:                                                                                              //code of Fight() in creatures.cs  - (there player choose fight 
                    Kobold kobold = new Kobold();                                                                    //or retreat and in 1st condition full fight scene code)
                    Creature.Fight(player, kobold, key, ref end, ref enemy_count);
                    map[player.x_position, player.y_position] = ' ';
                    break;
                case 1:
                    Goblin goblin = new Goblin();
                    Creature.Fight(player, goblin, key, ref end, ref enemy_count);
                    map[player.x_position, player.y_position] = ' ';
                    break;
                case 2:
                    Wolf wolf = new Wolf();
                    Creature.Fight(player, wolf, key, ref end, ref enemy_count);
                    map[player.x_position, player.y_position] = ' ';
                    break;
            }
            
        }
        public static void GameOver(ref bool end,int enemy_count)
        {
            if (enemy_count == 0)                                                         // player killing all enemyes and wining announcement function
            {
                Console.WriteLine("Great you defeated all enemys and save world!\n" +
                    "i guess you liked this mini game :)");
                end = false;
                Console.ReadKey();
            }
        }
    }
}
