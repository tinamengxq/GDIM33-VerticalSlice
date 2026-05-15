# GDIM33 Vertical Slice
## Milestone 1 Devlog
1. Player script graph
I used scripting graphs for my player to control its movement. It first get movement input from player input. I edited the player input action: I added Z and C input to move the player up and down. And I changed W and S to move forward and backward. I also changed A and D to move leftward and rightward. (A and D might be leftward and rightward originally?) Therefore, for the WASD movement under the action "Move", I import "Move" input on hold and get vector in x and y. Because I use cursor to change the direction of the player, if I just define a new vector of transform translate for the player, its WASD will always restricted to one direction. Therefore, I went through every possible node under transform an found get right and get forward. I suppose these two can help me get the direction of the right relative to the player itself. Then, I multiplies the direction with the scalar get from the move vector to get a new vector used for the direction change in transform. Later, I multiply the sum of direction change with Time.deltaTime and the object variable speed that I created in teh script graph. I add the result of the calculation to current position and reset the position of the player. Below the WASD graph, I also created a UpDown graph. Because I don't think it will be necessary to let the player rotate itself. So the upward and downward are in the same directions with the world scale. Then I just multiply the direction vector by speed and Time.deltaTime and add it to current position. Then I reset the position of the player.

2. [Break-down](https://docs.google.com/drawings/d/1DVmkJuQFL1z7H0Tt06RomRasbgyEpttTVnYgcJZo0co/edit?usp=sharing)
Some of the changes in my break-down chart are presented by yellow circles. These include the unity system stuff and the graphs. And other changes are presented as words next to the red lines. About my unity system stuff and the graphs, the unity systems refers to the NavMesh and the Timeline that are included in W5 pre-learning quiz. For the NavMesh, I will use code to randomly assign a position at the same height level and use NavMesh Agent to guide the NPC fish who don't have tools with them to that position. After one or two seconds, the script will generate another random position within certain area. And for the Timeline, I linked it to my game controller as I plan to create a state graph on my game controller except for controlling the cursor. I plan for creating some particle effect and some changes in camera direction when the game state detected the amount of oxygen decrease under 10%. These content are the updates in my break-down that I haven't done. 

And for those that I did for milestone 1, I added game state graph for NPC fishes. Those NPC fishes are the fishes with the tools. In the fish state graph, once the player get closer to the fish, the fish will perform some kinds of tense reactions, which means that a transition to tense state is triggered. To make it obvious and show that the fish is really nervous, I change the transform rotate of the fish in the state graph. So you can see the fish rotating really quick. In addition, I added player script graph for movement. I already explained it in the first bullet point of the milestone 1 devlog. The state machine on game controller that controls the cursor is half set. I only locked the cursor to the center of the screen. And it will move as player control if play click esc and want to use it. 


## Milestone 2 Devlog
1. Timeline Feature
- Break down summary
    1. learn how to create and use timeline
    2. create oxygen level in gamecontroller and link it to timeline
    3. use timeline to change sceen status when oxygen level < 10%
- Break down details
    1. go to youtube and unity manual to learn timeline
    2. to go gamecontroller.cs to create oxygen level
    3. code the logic for the oxygen level to decrease every frame and increase when getting to water surface
    4. create a timeline
    5. add UI to be possible changes on the screen
    6. link the UI to the timeline
    7. use timeline to control the changes in alpha of the UI panel
    8. record the alpha changes every second gradually, and set it to repeat during the time when oxygen level < 10%>
    9. write code to call timeline operation when the event (oxygen < 10%) is triggered'

2. Yes, the break-down activity really helps! Because I have no idea what is timeline, I may not be able to find any direction when starting to use timeline function in unity. When I am headless, I may think of many things at the same time. Listing all the details help me simplify what I am thinking right now about timeline, and also help me calm down to make a complicated task more elegant. I think I can improve my process to write the break downs. As I know nothing about a new system, I can first write a small break down to show how I want to finish the work. And then, I go and watch video instructions and read unity websites to know the actual steps to use the system. After learning a bit about the system, I shall improve my break down to be more detailed and more correct.

3. I defined a new custom event in C#, and call it in visual scripting graph. My custom event is OnIncreaseO2. This is triggered when player collide with an is triggered invisible object at the top of the room. I follow the instruction in W4 prelearning and the example script in W4 prelearning unity scene to define the event: 
```
public static class EventNames
{
    public static string IncreaseOxygen = "IncreaseOxygen";
}

[UnitTitle("On Increase O2")]
[UnitCategory("Events\\MyEvents")]
public class GraphLinkO2 : EventUnit<GameController>
{
    protected override bool register => true;

    public ValueOutput result {get; private set;}
    public override EventHook GetHook(GraphReference graphReference)
    {
        return new EventHook(EventNames.IncreaseOxygen);
    }

    protected override void Definition()
    {
        base.Definition();
        result = ValueOutput<GameController>("gameController");
    }

    protected override void AssignArguments(Flow flow, GameController data)
    {
        base.AssignArguments(flow, data);
        flow.SetValue(result , data);
    }
}
```
The above is my code. This custom event will link to a method written in GameController.cs. Later, I want to define how my event will be triggered. I go to the script written for that invisible is triggered game object and added the following line.
```
public class WallGetO2 : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventBus.Trigger(EventNames.IncreaseOxygen, GameController.Instance);
        }
    }
}
```
This defines when will the custom event be triggered. After coding all of this, I create a node in the game controller script machine graph, and link it to the IncreaseO2() method as shown in the picture below.

<img width="471" height="147" alt="graph" src="https://github.com/user-attachments/assets/63f55116-387a-4a21-965d-f8150d6cb5bc" />



4. I integrated both timeline and NavMesh, but I think timeline is better?
- Timeline
    - I create a timeline to change the color of the screen when the oxygen is below 10%. 
    - If you want to see the timeline effect, you can wait will oxygen is 10%, and you will se the screen get darker, and then a bit brighter, and then darker, a bit brighter, and so on. But you should be aware that if your oxygen is decreased to 0%, you will lose the game. 
    - When you go up to gain oxygen, you will find that the timeline effect disappear once your oxygen is greater than 10%.

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
