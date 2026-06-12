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
1. Because my game takes place in water, my shader graph uses post-processing effect to show the entire scene is under water. I used URP universal renderer and added it to the list of the graphic renderer list. Then I changed my main camera renderering effect into my post-processing effect. I added a full screen pass renderer feature to my post-processing effect asset and attached a new material to the full screen feature. I created a shader graph to build the effect. I used URP sample buffer node with source buffer blitsource. I also created a sample texture 2D to input my own texture (I drew it myself) and screen position node for UV input. I used multiply node to multiply the result of sample texture 2D node and URP sample buffer node. The result contributes to the fragment shader's base color. 
The effect can be seen every second in the game when the player is playing in the water (as the screen is blue). I will make the activation and deactivation in the future when the player swims up to breathe. 

<img width="1510" height="605" alt="shadergraph" src="https://github.com/user-attachments/assets/9344ffd1-45c0-4286-97f3-885f30290f8e" />


2. From the playtest, I received some suggestions (that are "optional", said by the classmates) that I think might modify the main purpose of this game, so I only implemented some useful ones：
    1. increase the size of the UI on the top right corner.
        In the play test I increased the total size of the guide as well as the oxygen level to make it clearer for player. 
    2. decrease the rate of oxygen losing
        In the playtest, I decreased the rate of losing oxygen to make the timeline effect to show quicker. However, the classmates complained that they have trouble surviving with really low oxygen. So I decreased the rate.
    3. Decrease oxygen after player finish reading the background story.
        Along with the quick losing oxygen speed, player die very soon after they finish reading the guide. I changed the oxygen level into that it will only decrease only after player click F to make background guide disappear. 
    4. Make the fish less annoying to players
        Some classmates told me that those fishes sometimes annoy them when they want to stay still in front of their target fish. Thus, I decreased the number of NPC fish that isn't related to the gameplay loop. I also decreased speeds of all the fishes in the scene to make it more easier to block the fish.

3. I already completed the basic gameplay loop of two tasks (two pairs of fish and pipe) in milestone 2, so I introduced a background story before the game to explain to the player about the background more thoroughly. In order to make the entire gameplay loop clearer for the players, I added some explanations for the player to know what they need to do after the game starts. I tried to make the story funny, and I also added a summary of what player should do during the game. The background story appears before the water scene. 

## Final Devlog
Final Devlog goes here.

1. Game loop:
- story
    - This game tells a story about a prisoner fixing the pipes when they are broken and water come in. Player will be the prisoner. Stories will be shown to the player between each game level in the full game to connect levels. 
    - In this vertical slice, story is only shown before this game level. The current story is the beginning part. 
    - The story in this vertical slice can imitate how the story can be presented to the player and how the change between scenes can be arranged. 
- quest
    - In every game level, player will be given several quests. They will have to first find the problematic pipe and then find the corresponding tool to fix this pipe. Because there are fishes in the water, fishes eat the tools. Player have to defeat the fishes to get the tool. In the full game, the number of pipes waiting to be fixed will increase as the level increase. Player will be able to get to different rooms to fix different pipes. And there will be more fishes in future levels. The number of tools required to fix one pipe will increase. 
    - In this vertical slice, I created 2 problematice pipes. One's tool is eaten by the fish in green and white. The other's tool is eaten by the fish in red and white. 
    - The pamphlet is a panel that shows all the steps that the player has to follow along the entire process of fixing ONE pipe. It will be updated with different guide toward different fishes after the player start to work for another pipe.
    - The quest in this vertical slice imitates the flow of one quest, which is the basic of future complex designs. The process of find - fight - fix is the same for all quests in the full game as well as the pamphlet. 
- combat
    - In the process of fixing a pipe, player has to kill the fish to get the tool inside their stomach. I use raycast to make sure that the player is close to and facing the fish. As long as the raycast shows that the player is facing the fish, the attack of the player will work. Every time the player can attack the fish using keycode E. Every time the fish is hurt, its eyes will turn red and there will be a "Ooh" sound. After the fish say "oh I lose", the player will automatically get the tool. 
    - The fish also has different status. It is controlled by the graph that only when player approaches the fish, they will stop and rotate themselves to show that they are nervous and scared, which means they are transfered into a new stage. In regular status, navmesh agent is used to control the fishes' movements.
    - This vertical slice already covers all the details of combat that will appear in the future levels in the full game. I would only make some fish fight back in the full game to make the level harder. 
- water and oxygen
    - While the game is located mostly under water, I used a post processing effect to show water using camera renderer setting. I also added some distortion effect of the water to make it more realistic but it is not obvious and hard to find because the distortion is very small. Therefore, I would put a screenshot of my shader graph to show the distortion effect.
    - <img width="1180" height="554" alt="Screenshot 2026-06-09 at 19 59 33" src="https://github.com/user-attachments/assets/598d73a8-d242-4843-a614-4c879b1934ea" />

    - Player can't always stay under water because they need oxygen. When player go up to get oxygen, a water surface will appear and the post processing effect will disappear. When player run out of oxygen (the total oxygen is below 10%), I use timeline and UI panel to make the screen darker. 
    - All details in this part in the vertical sliec will be the same for all future levels in the full game. 
2. In about a paragraph, describe how your rendering effect is activated from gameplay logic. Either attach a screenshot of the relevant Graph OR cite the relevant C# file(s) so we can find them in your repo. Accurately describe your system with technical terms.
When player go up above water, the water post processing effect will be deactivated. The rendering effect will be activated when player go back into the water. In the slides of W9 I found an example that said I can change the rendering setting of the camera. Thus, I googled how to change the renderer the camera, which results in using GetUniversalAdditionalCameraData() to get the rendering setting of the camera and change the rendering setting based on the rendering list that this setting have access to, which is the renderer lsit in URP-High Fiedlity. I make changes in rendering setting according to the relative position between the camera.position.y and watersurface.position.y in the script CameraController.cs. The following code shows how I make changes. In the renderer list in URP-High Fidelity, 0 is the renderer without the post processing effect, and 1 is the renderer with the post processing effect.
<img width="357" height="178" alt="Screenshot 2026-06-09 at 19 59 02" src="https://github.com/user-attachments/assets/ca729c16-c3ca-4d1c-b6f5-eea0e3bc3ecb" />

```
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraController : MonoBehaviour
{
    //codes for movement
    [SerializeField] private Camera _camera;
    private UniversalAdditionalCameraData cameraRenderer;
    [SerializeField] private Transform water;
    [SerializeField] private GameObject waterSurface;
    void Start()
    {
        //codes for movement
        cameraRenderer = _camera.GetUniversalAdditionalCameraData();
    }
    void Update()
    {
        //codes for movement

        //oxygen
        if (water.position.y > transform.position.y)
        {
            ChangeCamera(1); 
            Debug.Log("Active water effect");
            waterSurface.SetActive(false);
        }
        else
        {
            ChangeCamera(0);
            Debug.Log("Deactivate water effect");
            waterSurface.SetActive(true);
        }
    }

    public void ChangeCamera(int renderer)
    {
        cameraRenderer.SetRenderer(renderer);
        Debug.Log(renderer);
    }
}
```
3. I have several steps when breaking down a large project:
    1. write a pitch document draft that includes a list of all the ideas and systems I want to make and use. 
    2. write some specific explanations on how these tools/systems can be used in my game
    3. come up with an entire game mechanism that includes all the tools and systems and write a summary paragraph of how this game will be shown to the player.
    4. Now we have a pitch document that is kinda complicated. Thus, create a bubble diagram and put all the tools and systems and possible scripts and methods in the diagram. 
        - I believe a bubble graph is the fastest way for me to figure out what methods in each scripts I will be coding and why. I can also help me figure out if I need any event for the game. By separating the bubble diagram into different parts, I can focus on writing scripts part by part. Also, a bubble diagram can help me have an idea about how much effort I shall spend on this large project. 
    5. Link relations between each bubble.
        - It is important to put all the possible relations into the bubble graph because it can sometimes help minimize the scope for this large project. I sometimes find some collapse between funcitons when coding without having all the links listed before. Also, these links can help me figure out which gameobject(s) should I attach these codes to and how they can be linked to graphs (possibly).
        - This is why I always draw very complicated break down graph for my game projects. 
        - Also, a complicated bubble graph shortens the time that I have to spend reminding what to write in a script when coding. This is because when drawing a bubble graph, I have already spritually experienced the process of making this game. Therefore, I can clearly tell most of the details in the game. 
    6. Figure out if there are complicated systems or tools or not. If yes, for each tool, list a task step break down and make sure to follow it step by step when making the game. 
        - It was the first time for me to break down new systems step by step so specifically. While I was new to Timeline and kinda new to NavMesh, I think a step-by-step break down is very useful for me to find some directions and keep my brain clear when I was worried. Also, having a list of break down can control my idea and stop me from coming up with new things to add into the game. 
    7. Figure out if I will be using event or not. If yes, draw a flow chart with several lines to show how the event will work (who call? call who?). For each event, list a list of steps in the order of coding. 
        - While an event often links codes in way more than 2 scripts, it is important for me to figure out what is exactly happening when an event is called. Writing it done in a flow chart is much better than crtl/cmd + F and/or "go to definition" every time I want to use this event. It can also help me when coding because then I can check if I have attached all the methods in to all the events in all the codes. 
        - This is a method I figured out when coding for a large project other than this one. In that project, I also created a 3D combat system that uses a lot of events. I was exhausted when finding where did I put the related codes at that time. Thus, I created this method of drawing flow chart. In this large project, I didn't use many events, but I still found that this method helped me a lot. 

## Open-source assets
- [Pipe](https://assetstore.unity.com/packages/3d/props/industrial/modular-pipeline-pack-70776)
- [Fish](https://assetstore.unity.com/packages/3d/characters/animals/fish/low-poly-fish-339618#content)
- [Wall](https://polyhaven.com/a/stained_pine)
- [Terrain](https://assetstore.unity.com/packages/3d/environments/landscapes/terrain-sample-asset-pack-145808)
- [Finish SFX](https://freesound.org/people/jivatma07/sounds/122255/)
- [Hurt SFX](https://freesound.org/people/MrEchobot/sounds/745185/)
- [BGM](https://freesound.org/people/plasterbrain/sounds/464918/)
