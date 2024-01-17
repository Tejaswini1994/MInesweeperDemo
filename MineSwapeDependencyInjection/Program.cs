using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MineSweaper
{
    public class Minesweaper
    {
        public char[,] minefield;
        public char[,] revealedGrid;
        public int gridSize;
        public int numMines;

        public int GridSize => gridSize;
        public int NumMines => numMines;

        



        // Dependency-injected constructor
        public Minesweaper(int size, int mines, char[,] minefield, char[,] revealedGrid)
        {
            gridSize = size;
            numMines = mines;
            this.minefield = minefield;
            this.revealedGrid = revealedGrid;
           
        }

        public Minesweaper()
        {
        }

        public void PlayGame()
        {
            bool gameOver = false;
            bool gameWon = false;
            string userInput = string.Empty;
            while (!gameOver && !gameWon)
            {
                DisplayRevealedGrid();  // Display revealed grid first
                if (string.IsNullOrEmpty(userInput))
                DisplayMinefield();


                Console.Write("Select a square to reveal (e.g., A1): ");
                userInput = Console.ReadLine().ToUpper();

                int row = userInput[0] - 'A';
                int col = int.Parse(userInput.Substring(1)) - 1;

                if (row < 0 || row >= gridSize || col < 0 || col >= gridSize)
                { 
                    Console.WriteLine("Invalid input. Please enter valid coordinates.");
                    Console.ReadLine();
                    continue;
                }

                if (minefield[row, col] == '*')
                {
                    Console.WriteLine("Game over! You hit a mine.");
                    Console.ReadLine();
                    gameOver = true;
                }
                else
                {
                    RevealSquare(row, col);

                    if (CheckWin())
                    {
                        DisplayRevealedGridNew();
                        Console.WriteLine("Congratulations! You've cleared the minefield!");
                        Console.ReadLine();
                        gameWon = true;
                    }
                }
            }
        }

        public bool SetRevealedGrid(char[,] revealedGrid)
        {
            return true;
        }

        public int SetNumMines(int mines)
        {
            return mines;
        }





        public void DisplayMinefield()
        {
            Console.WriteLine("Here is your minefield:");
            Console.Write("  ");
            for (int i = 1; i <= gridSize; i++)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();

            for (int row = 0; row < gridSize; row++)
            {
                Console.Write((char)('A' + row) + " ");
                for (int col = 0; col < gridSize; col++)
                {
                    Console.Write("_ ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void DisplayRevealedGrid()
        {
            Console.WriteLine("Here is your updated minefield:");
            Console.Write("  ");
            for (int i = 1; i <= gridSize; i++)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();

            for (int row = 0; row < gridSize; row++)
            {
                Console.Write((char)('A' + row) + " ");
                for (int col = 0; col < gridSize; col++)
                {
                    char cell = minefield[row, col] == '*' ? '_' : revealedGrid[row, col];
                    Console.Write(cell + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void RevealSquare(int row, int col)
        {
            if (revealedGrid[row, col] == '_')
            {
                revealedGrid[row, col] = CountAdjacentMines(row, col);

                if (revealedGrid[row, col] == '0')
                {
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            int newRow = row + i;
                            int newCol = col + j;

                            if (newRow >= 0 && newRow < gridSize && newCol >= 0 && newCol < gridSize)
                            {
                                RevealSquare(newRow, newCol);
                            }
                        }
                    }
                }
            }
        }

        public char CountAdjacentMines(int row, int col)
        {
            int count = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newRow = row + i;
                    int newCol = col + j;

                    if (newRow >= 0 && newRow < gridSize && newCol >= 0 && newCol < gridSize && minefield[newRow, newCol] == '*')
                    {
                        count++;
                    }
                }
            }

            return count > 0 ? (char)(count + '0') : ' ';
        }

        public bool CheckWin()
        {
            int revealedCells = 0;

            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    if (revealedGrid[row, col] != '_' && revealedGrid[row, col] != '*')
                    {
                        revealedCells++;
                    }
                }
            }

            return revealedCells == (gridSize * gridSize - numMines);
        }

        public void DisplayRevealedGridNew()
        {
            Console.WriteLine("Here is your updated minefield:");
            Console.Write("  ");
            for (int i = 1; i <= gridSize; i++)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();

            for (int row = 0; row < gridSize; row++)
            {
                Console.Write((char)('A' + row) + " ");
                for (int col = 0; col < gridSize; col++)
                {
                    char cell = minefield[row, col] == '*' ? '*' : revealedGrid[row, col];
                    Console.Write(cell + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public char[,] InitializeMinefield(int size, int mines)
        {
            char[,] field = new char[size, size];

            // Initialize minefield with mines
            Random random = new Random();
            while (mines > 0)
            {
                int row = random.Next(size);
                int col = random.Next(size);

                if (field[row, col] != '*')
                {
                    field[row, col] = '*';
                    mines--;
                }
            }

            return field;
        }

        // InitializeRevealedGrid method
        public char[,] InitializeRevealedGrid(int size)
        {
            char[,] grid = new char[size, size];

            // Fill the grid with default values
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[i, j] = '_';
                }
            }
            return grid;
        }

        
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Minesweaper mine = new Minesweaper();

            Console.WriteLine("Welcome to Minesweeper!");

            Console.Write("Enter the size of the grid (e.g., 4 for a 4x4 grid): ");
            int gridSize = int.Parse(Console.ReadLine());

            Console.Write("Enter the number of mines to place on the grid (maximum is 35% of the total squares): ");
            int numMines = int.Parse(Console.ReadLine());

           
            char[,] minefield = mine.InitializeMinefield(gridSize, numMines);
            char[,] revealedGrid = mine.InitializeRevealedGrid(gridSize);

           
            Minesweaper game = new Minesweaper(gridSize, numMines, minefield, revealedGrid);

            game.PlayGame();
        }

       
        

        
    }
}
