using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace gg_wp
{
    internal class Creature                                                // main creature class for similar stats for monster and player
    {
        public char id;
        public string name;
        public int health;
        public int damage;
        public int armor;
        public int health_potion_count;
        public void ShowStats(int i)                                        //show_stats function
        {
            switch (i)
            {
                case 0:
                    Console.Write($"{name}"); break;
                case 1:
                    Console.Write($"health - {health}"); break;
                case 2:
                    Console.Write($"damage - {damage}"); break;
                case 3:
                    Console.Write($"armor - {armor}"); break;
                case 4:
                    Console.WriteLine($"potions - {health_potion_count}"); break;
            }
        }
        public static int InputerCorecter(string choise, int[] decision_length)                 // function for choises in correct way,to escape expection in null or empty case,also decision length decides how many answers there may be
        {                                                                                       // and which answers they are
            while (true)
            {
                choise = Console.ReadLine();
                if (string.IsNullOrEmpty(choise))
                {
                    Console.WriteLine("please use valid input!");
                }
                else if (decision_length.Contains(Convert.ToInt32(choise)))
                {
                    return Convert.ToInt32(choise);
                }
                else Console.WriteLine("invalid input,please try again!");
            }
        }
        public static void Fight(Player player, Creature enemy, ConsoleKeyInfo key,ref bool end,ref int enemy_count)            // fight code for interaction
        {
            Console.Write($"you meet enemy {enemy.name},choose\n1 - fight.\n2 - retreat.\nyour choice:");
            string choise = "";
            int choice;
            choice = InputerCorecter(choise,new int[2] {1,2});
            switch (choice)
            {
                case 1:                                                                      //case when fight function choosen
                    while (enemy.health >= 0 && player.health >= 0)
                    {
                        Console.WriteLine($"\n{player.name}\nhealth - {player.health}\n");          // introducing player name and hp count
                        for (int i = 0; i < 4; i++)                                                 // indroducing enemy name and stats
                        {
                            enemy.ShowStats(i);
                            Console.WriteLine();
                        }
                        Console.Write("\nchoose you action:\n1 - attack\n2 - heal(restore 70 health)" +
                            "\n3 - block(double your armor and return 20% damage)\nyour choice:");
                        choice = InputerCorecter(choise,new int[3] {1,2,3});
                        Console.WriteLine();
                        switch (choice)
                        {
                            case 1:                                                                                                                     // attack code
                                enemy.health = Convert.ToInt32(enemy.health - player.damage / 100.0f * (100 - enemy.armor));
                                Console.WriteLine($"you dealt {Convert.ToString(Convert.ToInt32(player.damage / 100.0f * (100 - enemy.armor)))} damage.");
                                if (enemy.health <= 0)
                                {
                                    break;
                                }
                                player.health = Convert.ToInt32(player.health - enemy.damage / 100.0f * (100 - player.armor));
                                Console.WriteLine($"enemy dealt {Convert.ToString(Convert.ToInt32(enemy.damage / 100.0f * (100 - player.armor)))} damage.");
                                break;
                            case 2:                                                                                                                         // healing code(health potion use)
                                player.health += 70;
                                Console.WriteLine($"{player.name} healed for 70 hp.");
                                player.health = Convert.ToInt32(player.health - enemy.damage / 100.0f * (100 - player.armor));
                                Console.WriteLine($"enemy dealt {Convert.ToString(Convert.ToInt32(enemy.damage / 100.0f * (100 - player.armor)))} damage.");
                                break;
                            case 3:                                                                                                                         // block code
                                Console.WriteLine($"{player.name} use block\n");
                                player.health = Convert.ToInt32(player.health - enemy.damage / 100.0f * (100 - player.armor * 2));                                    // doubled arrmor and enemy hit
                                enemy.health = Convert.ToInt32(enemy.damage / 5.0f);                                                                                // reflected damage to enemy 
                                Console.WriteLine($"enemy dealt {Convert.ToString(Convert.ToInt32(enemy.damage / 100.0f * (100 - player.armor * 2)))}.\n" +
                                    $"{player.name} reflected {Convert.ToString(Convert.ToInt32(enemy.damage / 5.0f))} damage");
                                break;
                        }
                        Console.WriteLine(new string('-', 100));
                    }
                    if (enemy.health <= 0 && player.health > 0)
                    {
                        Console.WriteLine("You defeat enemy,well done!");                                                   // player winning against enemy 
                        enemy_count--;
                    }
                    else if (player.health <= 0)
                    {
                        Console.WriteLine("you are dead!next time be careful in adventure...");
                        end = false;
                        Console.ReadKey();
                        break;
                    }
                    break;
                case 2:                                                                     // case when player choose retreat
                default:
                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            player.x_position++; break;
                        case ConsoleKey.DownArrow:
                            player.x_position--; break;
                        case ConsoleKey.LeftArrow:
                            player.y_position++; break;
                        case ConsoleKey.RightArrow:
                            player.y_position--; break;
                    }
                    break;
            }
        }
    }
    internal class Player : Creature                                        // player stat class
    {
        Random rnd = new Random();
        public int x_position = 1;
        public int y_position = 1;
        public Player(string nickname)
        {
            id = '@';
            name = nickname;
            health = rnd.Next(500, 600);
            damage = rnd.Next(50, 60);
            armor = rnd.Next(20, 30);
            health_potion_count = 2;
        }
    }
    internal class Kobold : Creature                                                    //enemy creatures form there and after
    {
        Random rnd = new Random();
        public Kobold()
        {
            id = '*';
            name = "Kobold";
            health = rnd.Next(200, 250);
            damage = rnd.Next(20, 25);
            armor = rnd.Next(0, 15);
        }
    }
    internal class Goblin : Creature
    {
        Random rnd = new Random();
        public Goblin()
        {
            id = '*';
            name = "Goblin";
            health = rnd.Next(150, 200);
            damage = rnd.Next(30, 46);
            armor = 0;
        }
    }
    internal class Wolf : Creature
    {
        Random rnd = new Random();
        public Wolf()
        {
            id = '*';
            name = "wolf";
            health = rnd.Next(70, 100);
            damage = rnd.Next(50, 80);
            armor = 20;
        }

    }
}
