using System;

namespace DndDamage
{
    class Program
    {
        private static int ToHit, DamageBonus, DamageDie, Attacks, DetailsLevel = 1;
        private static bool CritHouseRule;
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the damage calculating program. Please enter character information.");

            SetCharacter();

            while(true)
            {
                Console.WriteLine("\nMake a choice: \n 1: Attack! \n 2: Change Character \n 3: Set Details Level \n 4: Exit");
                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    while (true)
                    {
                        Console.WriteLine("What is the AC of the enemy you're attacking?");
                        string s = Console.ReadLine();
                        int ac = 0;
                        if (int.TryParse(s, out ac))
                        {
                            Attack(ac);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Input is not valid. Try again.");
                        }
                    }
                }
                else if (choice == "2")
                {
                    SetCharacter();
                }
                else if (choice == "3")
                {
                    while (true)
                    {
                        Console.WriteLine("Enter choice: \n 1: Only damage total is shown. \n 2: To-hit and damage rolls are shown. \n 3: To-hit, to-hit + hit bonus, damage, damage + damage bonus are all shown.");
                        string s = Console.ReadLine();
                        int x = 0;
                        if (int.TryParse(s, out x) && (x == 1 || x == 2 || x == 3))
                        {
                            DetailsLevel = x;
                            Console.WriteLine("Details level set.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Input is not valid. Try again.");
                        }
                    }
                }
                else if (choice == "4")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("That is not a valid choice.");
                }
            }
            Console.WriteLine("Goodbye.");
        }
        static void SetCharacter()
        {

            while(true)
            {
                Console.WriteLine("What is your character's to-hit bonus?");
                string s = Console.ReadLine();
                int x = 0;
                if (int.TryParse(s, out x))
                {
                    ToHit = x;
                    break;
                }
                else
                {
                    Console.WriteLine("Input is not valid. Try again.");
                }
            }

            while (true)
            {
                Console.WriteLine("How many attacks does your character get?");
                string s = Console.ReadLine();
                int x = 0;
                if (int.TryParse(s, out x))
                {
                    Attacks = x;
                    break;
                }
                else
                {
                    Console.WriteLine("Input is not valid. Try again.");
                }
            }

            while (true)
            {
                Console.WriteLine("What die does your character use for damage?");
                string s = Console.ReadLine();
                int x = 0;
                if (int.TryParse(s, out x))
                {
                    DamageDie = x;
                    break;
                }
                else
                {
                    Console.WriteLine("Input is not valid. Try again.");
                }
            }

            while (true)
            {
                Console.WriteLine("What is your character's damage bonus?");
                string s = Console.ReadLine();
                int x = 0;
                if (int.TryParse(s, out x))
                {
                    DamageBonus = x;
                    break;
                }
                else
                {
                    Console.WriteLine("Input is not valid. Try again.");
                }
            }

            while (true)
            {
                Console.WriteLine("Do you use the house rule where, when critting, the first damage die is maxed out? (Y or N)");
                string s = Console.ReadLine().ToUpper();
                if (s == "Y" || s == "YES")
                {
                    CritHouseRule = true;
                    break;
                }
                else if(s == "N" || s == "NO")
                {
                    CritHouseRule = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Input is not valid. Try again.");
                }
            }
        }
        static void Attack(int AC)
        {
            int hits = 0, totaldamage = 0, crits = 0;
            int roll;
            string tohitrolls = "To Hit Rolls:";
            string tohitrollsbonus = "To Hit Rolls Plus Bonus:";
            string damagerolls = "Damage Rolls:";
            string damagerollsbonus = "Damage Rolls Plus Bonus:";
            Random r = new Random();
            for (int i = 0; i < Attacks; i++)
            {
                //Roll d20
                roll = r.Next(1, 21);
                tohitrolls += " " + roll.ToString();
                tohitrollsbonus += " " + (roll + ToHit).ToString();
                if (roll == 20 && CritHouseRule)
                {
                    totaldamage += DamageDie + DamageBonus;
                    damagerolls += " " + DamageDie.ToString();
                    damagerollsbonus += " " + (DamageDie + DamageBonus).ToString();
                    hits++;
                    crits++;
                }
                else if (roll == 20 && !CritHouseRule)
                {
                    hits += 2;
                    crits++;
                }
                else if ((roll + ToHit) > AC)
                {
                    hits++;
                }
            }
            for (int i = 0; i < hits; i++)
            {
                //Roll damage die
                roll = r.Next(1, DamageDie + 1);
                damagerolls += " " + roll.ToString();
                damagerollsbonus += " " + (roll + DamageBonus).ToString();
                totaldamage += roll + DamageBonus;
            }
            int hitdisplaysubtract = 0;
            //If the crit house rule is not being used, scoring a crit acts as if getting an extra hit, so I increment hits twice.
            //However, there is still technically only one hit. So I use this so the proper amount of hits is displayed.
            if (!CritHouseRule)
            {
                hitdisplaysubtract = crits;
            }
            if (DetailsLevel == 2)
            {
                Console.WriteLine(tohitrolls);
                Console.WriteLine((hits - hitdisplaysubtract).ToString() + " hits, " + crits.ToString() + " crits.");
                Console.WriteLine(damagerolls);
            }
            else if (DetailsLevel == 3)
            {
                Console.WriteLine(tohitrolls);
                Console.WriteLine(tohitrollsbonus);
                Console.WriteLine((hits - hitdisplaysubtract).ToString() + " hits, " + crits.ToString() + " crits.");
                Console.WriteLine(damagerolls);
                Console.WriteLine(damagerollsbonus);
            }
            Console.WriteLine("Total Damage is: " + totaldamage);
        }
    }
}
