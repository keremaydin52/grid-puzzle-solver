# Grid Puzzle Solver

This app allows you to create a grid and fill it with two different values at random. For demonstration purposes, the numbers **0** and **1** are used. Once the grid is created, all cells start with a value of **-1**. When you press the **Solve** button, the grid will be filled with a random combination of 0s and 1s that obey the puzzle rules. You can also use other game objects, sprites, or text instead of numbers.

## Rules

1. **Both row and column counts must be even.** You won't be able to generate a grid otherwise. The row and column values can be different from each other, but they both must be even numbers.
2. **No more than two consecutive identical values** are allowed in any row or column (e.g., `0 0 0` or `1 1 1` is invalid). Diagonal sequences are not checked.
3. **Each row and each column must contain an equal number of 0s and 1s.** For example, in a row of 6 cells, there must be exactly three 0s and three 1s.

## Solution Method

The app uses a **backtracking algorithm** to solve the puzzle. It tries assigning random values to each cell while checking the rules. If a conflict is found, it backtracks (reverts) to a previous step and tries a different value. You can review this method in the `GameManager` script—comments are provided for clarity.

## How to Use

1. Enter even numbers for the number of rows and columns in the input fields.
2. Click the **Create** button to generate a grid. The button will only be enabled if your inputs are valid. You can change the input values anytime and press Create again to generate a new grid.
3. Click the **Solve** button to find a valid solution. Pressing it again will generate a different valid solution. The Solve button will only be active if a valid grid has been created.
