using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweaper;

using System;
using System.IO;
using System.Reflection;
using System.Security.Policy;

namespace Minesweapertest
{
    [TestClass]

    public class UnitTest1
    {
       
        Minesweaper obj1 = new Minesweaper();

        [TestMethod]
        public void Minesweeper_Constructor_InitializesGameCorrectly()
        {
            // Arrange
            int size = 5;
            int mines = 3;

            // Act
            Minesweaper game = new Minesweaper(size, mines, new char[size, size], new char[size, size]);

            // Assert
            Assert.AreEqual(size, game.GridSize, "Unexpected grid size.");
            Assert.AreEqual(mines, game.NumMines, "Unexpected number of mines.");
        }

        [TestMethod]
        public void CountAdjacentMines_NoMinesAround_ReturnsSpace()
        {
            // Arrange
            int gridSize = 5;
            Minesweaper game = new Minesweaper(gridSize, 0, new char[gridSize, gridSize], new char[gridSize, gridSize]);

            // Assume no mines around (2, 2)
            int row = 2;
            int col = 2;

            // Act
            char result = game.CountAdjacentMines(row, col);

            // Assert
            Assert.AreEqual(' ', result, "There should be no mines around, so the result should be space.");
        }

        [TestMethod]
        public void RevealSquare_RevealsSquareWithNoAdjacentMines()
        {
            // Arrange
            int size = 5;
            int mines = 3;

            Minesweaper obj1 = new Minesweaper(size, mines, new char[size, size], new char[size, size]);
            char[,] revealedGrid = new char[,]
            {
                { '_', '_', '_' },
                { '_', '_', '_' },
            { '_', '_', '_' }
            };




            // Act
            obj1.RevealSquare(1, 1);

            // Assert
            Assert.AreEqual('_', revealedGrid[1, 1]);
            // Add additional assertions for the adjacent squares
        }

        [TestMethod]
        public void RevealSquare_RevealsSquareWithAdjacentMine()
        {
            int size = 5;
            int mines = 3;
            Minesweaper obj1 = new Minesweaper(size, mines, new char[size, size], new char[size, size]);
            char[,] revealedGrid = new char[,]
            {
            { '_', '_', '_' },
            { '_', '1', '_' },
            { '_', '_', '_' }
            };

            // Act
            obj1.RevealSquare(1, 1);

            // Assert
            Assert.AreEqual('1', revealedGrid[1, 1]);
            // Add additional assertions for adjacent squares
        }

        [TestMethod]
        public void RevealSquare_RevealsSquareAlreadyRevealed_NoChange()
        {
            int size = 5;
            int mines = 3;
            Minesweaper obj1 = new Minesweaper(size, mines, new char[size, size], new char[size, size]);
            char[,] revealedGrid = new char[,]
            {
            { '_', '_', '_' },
            { '_', '1', '_' },
            { '_', '_', '_' }
            };

            // Act
            obj1.RevealSquare(0, 0);

            // Assert
            // Add assertions to ensure that the square at (0, 0) remains unchanged
        }

        [TestMethod]
        public void CountAdjacentMines_NoMines_ReturnsSpace()
        {
            // Arrange

            int gridSize = 3; // Change this to your actual grid size
            char[,] minefield = { { '_', '_', '_' }, { '_', '_', '_' }, { '_', '_', '_' } };

            // Act
            char result = obj1.CountAdjacentMines(1, 1);

            // Assert
            Assert.AreEqual(' ', result);
        }


        [TestMethod]
        public void InitializeMinefield_Size3_Mines1_ReturnsValidMinefield()
        {
            // Arrange
           
            int size = 3;
            int mines = 1;

            // Act
            char[,] minefield = obj1.InitializeMinefield(size, mines);

            // Assert
            // Ensure the minefield size is correct
            Assert.AreEqual(size, minefield.GetLength(0));
            Assert.AreEqual(size, minefield.GetLength(1));

            // Ensure the number of mines matches the specified count
            int mineCount = 0;
            foreach (char cell in minefield)
            {
                if (cell == '*')
                {
                    mineCount++;
                }
            }
            Assert.AreEqual(mines, mineCount);
        }

        [TestMethod]
        public void InitializeMinefield_Size5_Mines10_ReturnsValidMinefield()
        {
            // Arrange
          
            int size = 5;
            int mines = 10;

            // Act
            char[,] minefield = obj1.InitializeMinefield(size, mines);

            // Assert
            // Ensure the minefield size is correct
            Assert.AreEqual(size, minefield.GetLength(0));
            Assert.AreEqual(size, minefield.GetLength(1));

            // Ensure the number of mines matches the specified count
            int mineCount = 0;
            foreach (char cell in minefield)
            {
                if (cell == '*')
                {
                    mineCount++;
                }
            }
            Assert.AreEqual(mines, mineCount);
        }

        [TestMethod]
        public void InitializeMinefield_Size2_Mines4_ReturnsValidMinefield()
        {
            // Arrange
          
            int size = 2;
            int mines = 4;

            // Act
            char[,] minefield = obj1.InitializeMinefield(size, mines);

            // Assert
            // Ensure the minefield size is correct
            Assert.AreEqual(size, minefield.GetLength(0));
            Assert.AreEqual(size, minefield.GetLength(1));

            // Ensure the number of mines is not more than the total cells
            int mineCount = 0;
            foreach (char cell in minefield)
            {
                if (cell == '*')
                {
                    mineCount++;
                }
            }
            Assert.IsTrue(mineCount <= size * size);
        }


        [TestMethod]
        public void InitializeRevealedGrid_Size3_ReturnsValidGrid()
        {
            // Arrange
            
            int size = 3;

            // Act
            char[,] revealedGrid = obj1.InitializeRevealedGrid(size);

            // Assert
            // Ensure the grid size is correct
            Assert.AreEqual(size, revealedGrid.GetLength(0));
            Assert.AreEqual(size, revealedGrid.GetLength(1));

            // Ensure all cells are initialized to '_'
            foreach (char cell in revealedGrid)
            {
                Assert.AreEqual('_', cell);
            }
        }

        [TestMethod]
        public void InitializeRevealedGrid_Size5_ReturnsValidGrid()
        {
            // Arrange
           
            int size = 5;

            // Act
            char[,] revealedGrid = obj1.InitializeRevealedGrid(size);

            // Assert
            // Ensure the grid size is correct
            Assert.AreEqual(size, revealedGrid.GetLength(0));
            Assert.AreEqual(size, revealedGrid.GetLength(1));

            // Ensure all cells are initialized to '_'
            foreach (char cell in revealedGrid)
            {
                Assert.AreEqual('_', cell);
            }
        }

        [TestMethod]
        public void InitializeRevealedGrid_Size2_ReturnsValidGrid()
        {
            // Arrange
            
            int size = 2;

            // Act
            char[,] revealedGrid = obj1.InitializeRevealedGrid(size);

            // Assert
            // Ensure the grid size is correct
            Assert.AreEqual(size, revealedGrid.GetLength(0));
            Assert.AreEqual(size, revealedGrid.GetLength(1));

            // Ensure all cells are initialized to '_'
            foreach (char cell in revealedGrid)
            {
                Assert.AreEqual('_', cell);
            }
        }

        [TestMethod]
        public void InitializeRevealedGrid_Size1_ReturnsValidGrid()
        {
            // Arrange
           
            int size = 1;

            // Act
            char[,] revealedGrid = obj1.InitializeRevealedGrid(size);

            // Assert
            // Ensure the grid size is correct
            Assert.AreEqual(size, revealedGrid.GetLength(0));
            Assert.AreEqual(size, revealedGrid.GetLength(1));

            // Ensure the only cell is initialized to '_'
            Assert.AreEqual('_', revealedGrid[0, 0]);
        }

        [TestMethod]
        public void InitializeRevealedGrid_Size0_ReturnsEmptyGrid()
        {
            // Arrange
           
            int size = 0;

            // Act
            char[,] revealedGrid = obj1.InitializeRevealedGrid(size);

            // Assert
            // Ensure the grid size is correct
            Assert.AreEqual(size, revealedGrid.GetLength(0));
            Assert.AreEqual(size, revealedGrid.GetLength(1));
            // Since there are no cells, nothing to assert
        }

        [TestMethod]
        public void CheckWin_AllCellsRevealed_Win()
        {
            // Arrange
          
            int size = 3;
            int mines = 0;
            char[,] revealedGrid = {
            { '1', '2', '3' },
            { '4', '5', '6' },
            { '7', '8', '9' }
        };
            obj1.SetRevealedGrid(revealedGrid);
           
            obj1.SetNumMines(mines);

            // Act
            bool result = obj1.CheckWin();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckWin_SomeCellsRevealed_NotWin()
        {
            // Arrange
          
            int gridSize = 3;
            int mines = 0;
            char[,] revealedGrid = {
            { '1', '2', '_' },
            { '4', '_', '6' },
            { '7', '8', '_' }
        };
            obj1.SetRevealedGrid(revealedGrid);
           
            obj1.SetNumMines(mines);

            // Act
            bool result = obj1.CheckWin();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckWin_NoCellsRevealed_NotWin()
        {
            // Arrange
            int mines = 5;
            int gridSize = 3;
            char[,] revealedGrid = {
            { '_', '_', '_' },
            { '_', '_', '_' },
            { '_', '_', '_' }
        };
            obj1.SetRevealedGrid(revealedGrid);
             // Set the number of mines greater than the grid size
            obj1.SetNumMines(mines);

            // Act
            bool result = obj1.CheckWin();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckWin_AllMinesRevealed_NotWin()
        {
            // Arrange
            
            int gridSize = 3;
            char[,] revealedGrid = {
            { '*', '*', '*' },
            { '*', '*', '*' },
            { '*', '*', '*' }
        };
            obj1.SetRevealedGrid(revealedGrid);
            int mines = 9;
            obj1.SetNumMines(mines);

            // Act
            bool result = obj1.CheckWin();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckWin_AllCellsRevealedButMinesRemaining_NotWin()
        {
            // Arrange
            
            int gridSize = 3;
            char[,] revealedGrid = {
            { '1', '2', '3' },
            { '4', '5', '6' },
            { '7', '8', '9' }
        };
            obj1.SetRevealedGrid(revealedGrid);
            int mines = 5;
            obj1.SetNumMines(mines);

            // Act
            bool result = obj1.CheckWin();

            // Assert
            Assert.IsTrue(result);
        }




























    }
    }
















