using System;
using System.Collections.Generic;

namespace LR_2_queens
{
    static class Program
    {
        public static int num_queens = 8; 
        static int population_size = 100;
        static int max_ages = 100000, age = 0;
        static int num = 50;
        static bool ind_found = false;
        static Queen target = new Queen();
        static List<Queen> population = new List<Queen>(population_size);

        static void InputData()
        {
            Console.Write("Input size of board - ");
                 num_queens = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
            
            Console.Write("Input size of population - ");
                population_size = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();

            Console.Write("Input max age - ");
                max_ages = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();

            Console.Write("Input num - ");
                num = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
        }
        static void InitFirstPopuation()
        {
            for (int i = 0; i < population_size; i++)
                population.Add(new Queen());
        }
        static void Output()
        {
            Console.WriteLine("Size of board - " + num_queens);
            Console.WriteLine("Size of population - " + population_size);
            Console.WriteLine("Max age - " + max_ages);
            if (ind_found)
            {
                bool[][] board = new bool[num_queens][];
                for (int i = 0; i < num_queens; i++)
                {
                    board[i] = new bool[num_queens];
                    for (int j = 0; j < num_queens; j++)
                        board[i][j] = false;
                }

                for (int i = 0; i < num_queens; i++) 
                    board[i][target.queen[i]] = true;

                for (int i = 0; i < num_queens; i++)
                {
                    for (int j = 0; j < num_queens; j++)
                    {
                        if (!board[i][j]) Console.Write("#");
                        else Console.Write("*");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("Age - " + age);
            }
            else
            {
                Console.WriteLine("Individual not found");
            }


        }
        static void Crossingover(int f, int s)
        {
            Queen tmp_1 = new Queen();
            Queen tmp_2 = new Queen();
            int point = new Random().Next(2, num_queens - 1);
            for (int i = 0; i < point; i++)
            {
                tmp_1.queen[i] = population[f].queen[i];
                tmp_2.queen[i] = population[s].queen[i];
            }
            for (int i = point; i < num_queens; i++)
            {
                tmp_1.queen[i] = population[s].queen[i];
                tmp_2.queen[i] = population[f].queen[i];
            }

            tmp_1.Function();
            tmp_2.Function();

            if (tmp_1.func < population[f].func )
                population[f].queen = tmp_1.queen;
            if ( tmp_2.func < population[s].func)
                population[s].queen = tmp_2.queen;    
        }
        static void Random_select()
        {
            int temp;
            int[] mas = new int[num];
            int counter = 0;
            while (counter < num)
            {
                temp = Queen.rand.Next(0, population_size);

                for (int j = 0; j < counter; j++)
                {
                    if (mas[j] == temp)
                        temp = -1;
                }
                if (temp > 0)
                {
                    mas[counter] = temp;
                    counter++;
                }
            }
        for (int i = 0; i < num; i += 2)       
                Crossingover(mas[i], mas[i + 1]);     
        }
        static void Check_population()
        {
            foreach (Queen queen in population)
            {
                queen.Function();
                if (queen.func == 0)
                {
                    ind_found = true;
                    target = queen;
                    break;
                }
            }
        }
        static void Print_population()
        {
            foreach (Queen queen in population) queen.Print();
            Console.WriteLine();
        }
        static void Mutate_population()
        {
            foreach (Queen queen in population)
            {
                queen.Mutation();
            }
        }
        static int Min_func()
        {
            int mini = 10000;
            foreach (Queen queen in population)
            {
                if (queen.func < mini) 
                { 
                    mini = queen.func;
                    target = population[population.IndexOf(queen)];
                }
            }
           // target.Print();
            return mini;
        }
        static void Main(string[] args)
        {
             InputData();
            InitFirstPopuation();
            while (!ind_found && age <= max_ages)
            {
                if (age % 1000 == 0) Console.WriteLine("age = " + age + " func = " + Min_func());
                Check_population();
                Mutate_population();
                Random_select();
                age++;
            }
            Output();
            target.Print();
           
            //Расставить на доске n ферзей, чтобы они не били друг друга.
            
            Console.ReadKey();
        }
    }
}