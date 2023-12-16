# Grid Puzzle Solver
This app is made to create a grid and fill the grid with two different values randomly. For the demonstration, numbers 0 and 1 is used. After creating the grid, cells will be filled with -1. When the solve button is pushed, grid cells will be filled with 0 and 1 randomly. But different game objects, sprites or texts can be used.

## Rules
1. Row and column counts should be even. Otherwise you will not be able to create a grid. They don't to be the same value. They can be different.
2. There should be no more than 2 consecutive same values in a row or count. Diagonals are not a concern.
3. Count of a value should be equal to the count of the other value in a row or column. For example; if the row length is 6, the total number of 0's and 1's both should be equal to 3 in the same row.

## Solution method
This app is using Backtracking method to solve the problem. It's trying a random value in every cell and checking if it's good to go. If there would be a problem, it would go one step back and try another value. You can find the method in GameManager script and check the comments.

## Usage
Enter the number of rows and columns you want to input fields. Then push the create button to generate a grid. The create button will not be interactable if the inputs are not valid. You can change the input values and push create button to create a new one.
<br/><br/>
Push the solve button to solve the puzzle. When you press again, it will create another solution. It will not be available if there's no valid grid in the screen.