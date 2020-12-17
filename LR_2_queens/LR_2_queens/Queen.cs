using System;

namespace LR_2_queens
{
    class Queen
    {
        public int[] queen;
        public int func;
        public static int chance = 15;

        public static Random rand = new Random(DateTime.Now.Millisecond);
        
        public Queen()
        {
            queen = new int[Program.num_queens];
            for (int i = 0; i < Program.num_queens; i++)
                queen[i] = rand.Next(0, Program.num_queens);
            Function();
        }

        public void Function()
        {
            func = 0;
            for (int i = 0; i < Program.num_queens-1; i++)
            {
                for (int j = i + 1; j < Program.num_queens; j++)
                    if (
                        (queen[i] == queen[j]) ||
                        (i + 1 - queen[i] == j + 1 - queen[j]) ||
                        (i + 1 - queen[j] == j + 1 - queen[i])
                        )
            func++;
            }
        }

        public void Print()
        {
            for (int i = 0; i < Program.num_queens; i++)
                Console.Write(queen[i] + "|");
            Console.Write("     "+ func);
            Console.WriteLine();
        }
        public void Mutation()
        {
            if (rand.Next(chance) == 5)
                queen[rand.Next(Program.num_queens)] = rand.Next(Program.num_queens);
        }

    }
}
