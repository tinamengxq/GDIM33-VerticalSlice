# GDIM33 Vertical Slice
## Milestone 1 Devlog
1. Player script graph
I used scripting graphs for my player to control its movement. It first get movement input from player input. I edited the player input action: I added Z and C input to move the player up and down. And I changed W and S to move forward and backward. I also changed A and D to move leftward and rightward. (A and D might be leftward and rightward originally?) Therefore, for the WASD movement under the action "Move", I import "Move" input on hold and get vector in x and y. Because I use cursor to change the direction of the player, if I just define a new vector of transform translate for the player, its WASD will always restricted to one direction. Therefore, I went through every possible node under transform an found get right and get forward. I suppose these two can help me get the direction of the right relative to the player itself. Then, I multiplies the direction with the scalar get from the move vector to get a new vector used for the direction change in transform. Later, I multiply the sum of direction change with Time.deltaTime and the object variable speed that I created in teh script graph. I add the result of the calculation to current position and reset the position of the player. Below the WASD graph, I also created a UpDown graph. Because I don't think it will be necessary to let the player rotate itself. So the upward and downward are in the same directions with the world scale. Then I just multiply the direction vector by speed and Time.deltaTime and add it to current position. Then I reset the position of the player.

2. [Break-down](https://docs.google.com/drawings/d/1DVmkJuQFL1z7H0Tt06RomRasbgyEpttTVnYgcJZo0co/edit?usp=sharing)
Some of the changes in my break-down chart are presented by yellow circles. These include the unity system stuff and the graphs. And other changes are presented as words next to the red lines. About my unity system stuff and the graphs, the unity systems refers to the NavMesh and the Timeline that are included in W5 pre-learning quiz. For the NavMesh, I will use code to randomly assign a position at the same height level and use NavMesh Agent to guide the NPC fish who don't have tools with them to that position. After one or two seconds, the script will generate another random position within certain area. And for the Timeline, I linked it to my game controller as I plan to create a state graph on my game controller except for controlling the cursor. I plan for creating some particle effect and some changes in camera direction when the game state detected the amount of oxygen decrease under 10%. These content are the updates in my break-down that I haven't done. 

And for those that I did for milestone 1, I added game state graph for NPC fishes. Those NPC fishes are the fishes with the tools. In the fish state graph, once the player get closer to the fish, the fish will perform some kinds of tense reactions, which means that a transition to tense state is triggered. To make it obvious and show that the fish is really nervous, I change the transform rotate of the fish in the state graph. So you can see the fish rotating really quick. In addition, I added player script graph for movement. I already explained it in the first bullet point of the milestone 1 devlog. The state machine on game controller that controls the cursor is half set. I only locked the cursor to the center of the screen. And it will move as player control if play click esc and want to use it. 


## Milestone 2 Devlog
Milestone 2 Devlog goes here.
## Milestone 3 Devlog
Milestone 3 Devlog goes here.
## Milestone 4 Devlog
Milestone 4 Devlog goes here.
## Final Devlog
Final Devlog goes here.


## Open-source assets
- [Pipe](https://assetstore.unity.com/packages/3d/props/industrial/modular-pipeline-pack-70776)
- [Fish](https://assetstore.unity.com/packages/3d/characters/animals/fish/low-poly-fish-339618#content)
- [Wall](https://polyhaven.com/a/stained_pine)
- [Terrain](https://assetstore.unity.com/packages/3d/environments/landscapes/terrain-sample-asset-pack-145808)