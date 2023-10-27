# GREED

Welcome to your seventh project, "Greed". Inspired by Matt Day's Linux game, this graphical, grid-based game will test your programming skills on a new plane, incorporating graphical elements and sophisticated algorithms, while still firmly grounded in our foundational concepts.

**Variables:** Variables in "Greed" become increasingly complex as they now represent not just simple game data, but also the game grid's state, the player's position, and the score. This experience will enhance your skills in managing more intricate data structures.

**Conditionals:** Conditionals play a vital role in determining the game's flow, such as checking for valid moves, updating the grid based on the player's move, and identifying when the game ends (when no moves are left).

**Arrays:** Arrays, or more specifically two-dimensional arrays, represent the game grid. Each element corresponds to a cell in the grid, storing its current state. This representation will necessitate more advanced array manipulation skills.

**Loops:** Loops are key for iterating over the grid, to check for possible moves, and to update the grid after each move. Nested loops will likely become a common tool, given the two-dimensional nature of the game.

**Functions:** Functions in "Greed" encapsulate tasks like generating the game grid, validating moves, calculating scores, and rendering the grid to the player. You'll find the creation of modular, efficient, and reusable functions even more critical with the increased complexity.

In keeping with previous projects, we continue to emphasize decoupling game state from visual representation. While "Greed" introduces graphical elements, the principle remains the same—your core game logic should function independently from its graphical display.

So, what's unique in "Greed"? As a grid-based game, it introduces you to spatial and graphical considerations. You'll need to develop algorithms that can traverse and manipulate a two-dimensional grid effectively. These could include pathfinding algorithms, or others that can efficiently search a grid and update its state.

"Greed" also adds the task of rendering a graphical grid to the player, introducing you to basic computer graphics concepts within the CLI context. This aspect will demand that you find creative ways to visualize the game grid using text-based graphics.

## Project Requirements:

You must work from the code in https://github.com/CodeCraftCurriculum-I/module_7_greedy

Before you write any code, you should "sketch" the pseudo code and make a flowchart for how you plan to do the following alterations to the game.

NB! Some of the requierments might require you to write code not explicitly spelled out in the requierments, use your judgment.


- Multilingual Support: Extend your program to support multiple languages.
- There is a bug in the code that places the player on the board. Find and fix. 
- Create a gameover screen that summarizes the players achivment (this new screen should redirect to the menu in some whay when the player presses anny key)
- Currently the player can backtrack, i.e. go where they have been befor, this is not allowed. If this happens the game is over. 
- Currently the game only suports vertical movment, add the code that is required for hotisontal movment.
- Currently the player can atampt to go outside the bounds of the game board, if this happens the game crashes. Make it so that it is simply game over. NB! being next to a boarder for instance the left edge and selecting left should do nothing. i.e. there needs to be a number to the left in order to go left and fail. 
- The greedy games Update function has a bit of duplication, refactor the code to avoid as much of this duplication as possible.
- Implement LARGE and XL_LARGE game modes.
- HUD update: Show time in mm:ss (minutes and secods) of play.
- HUD update: Show the percentage of max score achived
- If the player presses "r", restart the level.
- If the player presses "q", exit to menu.

### Challenge Requirements (Higher Grades):

- If the players have noe more legal moves, the game shoudld go to gameover.

- Each game mode (s,l,xl) should be possible to combine with a challenge mode time triale. i.e the player has a set amunt of time. Note that the player should still be able to play in normal mode. 

- Implement a High Score System: From the main menu it should be possible to go into a higscore view and see previous results. Remember that scores from diffrent game modes can not be directly compared. After having played the player should be able to save their initals and score.

- Create a game mode based on XL that has "death" spaces (holes) in the map. Thes spaces should at minimum be 2x2 (2 rows and 2 columns)

Throughout this module, you should keep in mind that the ultimate goal is not only to build these features but also to learn from the process. You should aim to understand how different parts of a program interact, how to troubleshoot effectively, and how to write clean, effective code.

Evryone: In your README file write about what you learnt from this module.

All your code and related files should be neatly organized in a Zip file, with the following internal structure

module_7_Greedy  
├── .vscode  
├── module_7_hangman.csproj  
├── module_7_hangman.sln  
├── \*.\*  
└── README.md  

About assesment:

Assessment for this project will be based not just on feature completion, but also on your attention to detail, the cleanliness and readability of your code, and the thoughtfulness of your README file reflections. This is your opportunity to demonstrate not just what you've learned, but how you can apply it creatively and effectively in a real programming project.


