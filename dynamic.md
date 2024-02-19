# Dynamic elements

## Question 1

**Question:** What are the main characteristics of the objects in your game?

**Answer:**

- **The type of object** - static or dynamic. Static objects stay in place and cannot be broken, while dynamic objects can break and reveal the cat.
- **The breaking state** - is the bone breakable and how many times can it be hit before it breaks.
- **Effect on time** - how much time is deducted from the player when breaking a dynamic bone that does not contain the cat.
- **Cost** - the cost of add-ons and their effect on the game.
- **Additional features such as location** - coordinates of objects in the game space, important for the story and progress in the game.

**Question:** How will you determine the numerical characteristics so that the game is balanced?

**Answer:** To determine the numerical properties so that the game is balanced, we will follow several steps:

1. **Playtests** - We will conduct a series of tests with external users to understand the adjusted difficulty and receive feedback.
2. **Data analysis** - We will analyze data from the tests to understand which levels are perceived as too difficult or too easy and refine the features accordingly.
3. **Dynamic balance** - we will use algorithms that adjust the level of difficulty according to the player's ability, to ensure an optimal gaming experience.

**Question:** Suggest reasonable starting numbers (explain the calculation you did to arrive at these numbers).

**Answer:** We will build a simple model to suggest some starting numbers:

- **Initial time** - we will start with 40 seconds to find the cat in the first step.
- **Time reduction** - for each dynamic object broken without the cat, 5 seconds are subtracted from the time.
- **Add-on cost** - Hourglass - 10 points, Time freeze - 20 points, 2x - 30 points, Ball of wool - 40 points.

We will make a calculation based on the following data and assumptions:

**The data-**

- Points for finding a cat = 2S (each finding a cat earns the player 2S points, where S is the number of seconds left in the phase when he found the cat).
- Hourglass cost = 10 points.
- Cost of "time freeze" = 20 points.
- 2x cost = 30 points.
- Ball of wool cost = 40 points.
- Initial time = 40 seconds.
- Time reduction for incorrect dynamic object = 5 seconds reduced for each dynamic object broken without the cat.

**The discounts-**

- Dynamic objects in the first stage = 5 (the number of dynamic objects in the first stage).
- Objects hit on average = 2 (discount for the purpose of the example, the number of objects the player will hit on average before finding the cat).

**Calculation of time remaining after hitting dynamic objects that do not contain the cat:**
Remaining time = initial time - (objects damaged on average * time reduction for incorrect dynamic object)
40 - (2*5) = 30

**Points reward calculation:**
Reward points = points for finding a cat = 2S

**Total:**
(remaining time, reward points) = (2S=2\*30=60, 30)

## Question 2

**Question:** What are the locations of the main objects in your game?

**Answer:** The locations of the objects should be adapted to the environment of each stage.
Examples of possible locations:

- **Inside the house** - different rooms such as living room, kitchen, bedroom, bathroom, and on the shelves, under the table, inside cabinets or behind a sofa.
- **In the yard of the house** - including places like inside trees, behind vegetation, inside a tool shed, or near a pool of water.

**Question:** How did you determine the location of each object so that the game would be balanced and interesting?

**Answer:** Determining the location of objects in the game requires planning that takes into account several factors:

- **Challenge and progression** - it is important to ensure that the game starts relatively simply and becomes more challenging as the player progresses. For example, hiding places are simple in the early stages and more complex later.
- **Randomness and diversity** - to maintain interest, it is important to incorporate elements of randomness in the locations of objects. This can include changes to object locations each time the player starts a new stage or when replaying the same stage.
- **Matching the story** - the location of the objects should match the story and plot of the game. If the cat likes to play in the yard, it might be right to place dynamic objects there.
- **User testing** - it is important to perform tests with players to see which locations are found challenging and interesting, and which less so. This feedback can help improve and balance the game.

## Question 3

**Question:** What are the main behaviors of objects in your game?

**Answer:** The main behaviors of game objects include:

- **Breakable or Interactabl**e\*\* - Dynamic objects can be broken or interacted with by the player, for example, by clicking or throwing a ball of wool. This reveals possible hiding places for the cat or affects play time.
- **Concealment** - The cat and other items are hidden behind dynamic objects, requiring the player to search and locate them.
- **Time and Effect** - Any player action with dynamic objects can affect the time available for a phase, either lengthening or shortening it.
- **Interaction between objects** - actions with one object can affect the state of another object, for example, throwing a ball of wool changes the position of an object and reveals new areas.

**Question:** What complex phenomena are created in the world of the game, as a result of the simple rules of behavior of different objects?

**Answer:** The complex phenomena created in the game world include:

- **Strategic dilemmas** - the player has to choose which objects to interact with and when, based on strategic considerations to make the best use of time and minimize the risk of losing time.
- **Adaptation and learning** - the player learns the patterns and behaviors of the objects and gradually improves, allowing for a more efficient handling of challenges.
- **Chain reactions** - one action can cause a chain of events, for example, breaking a dynamic object can reveal several other objects and even reveal the cat.
- **Surprise** - due to the randomness and interactions between the objects, surprises and situations that the player did not expect can occur, while creating a dynamic and interesting experience.

## Question 4

**Question:** Is there an economic system that can fit your game - internal (in-game trading) or external (out-of-game trading)?

**Answer:** The game incorporates an internal economic system that will add depth and interaction to the game experience:

- **Game Coins** - Players can collect coins or points during the game by finding the cat or completing other tasks. The coins can be used to purchase add-ons or upgrades in the game.
- **Internal Store** - Players can use the coins earned to purchase add-ons (such as hourglass, time freeze, 2x, ball of wool) that will improve the ability to complete missions and progress in the game.
- **Balance** - it is important to balance the system so that it is satisfactory and contributes to the game experience, without making the game too easy or requiring real financial expenses to progress.

**Compatibility with the game:**

- **Rewards and motivation** - the internal economic system can be used as a reward mechanism and raise the motivation level of the players to persist and invest in the game.
- **Adaptation and control** - the player feels in control when he can influence the game through strategic purchases in the internal store, and adapt the game strategy to his play style.

## Question 5

**Question:** How much and what information exactly does the player have about the state of the game at any moment?

**Answer:** The player should be aware of several parameters that affect the state of the game:

- **Time left** - the time left for the player to find the cat before the stage ends.
- **The number of points/coins** - how many points or coins the player has collected so far, which can be used to purchase add-ons or upgrades.
- **Add-on status** - which add-ons are available to the player and what their status is (active, pending, remaining amount).
- **The location of the objects and the cat** - indications of areas that have already been explored or objects that have already been broken, and hints about the potential location of the cat.

**Question:** How does the player get information about the state of the game?

**Answer:** The player receives information through several interfaces and visual feedback:

- **The user interface (UI)** - displays all the relevant data such as time left, the number of points/coins, and the status of the plugins.
- **Visual cues** - such as changes in color or brightness of objects that can signal possible interaction or locations of importance.
- **Sound effects** - cues that can help the player understand the state of the game, such as the sounds of the cat or voices indicating that there is little time left.

**Question:** What is the player's point of view on the world? Why did you choose this point of view?

**Answer:** Depending on the 2D style of play, the natural point of view might be looking at:

- **Top view** - allows the player to see the objects and the cat from above, and provides an overall sense of the room or area where they are. It is useful for finding ways to navigate and locate the cat.

## Question 6

**Question:** What is the player's method of controlling the game state: is his control direct or indirect?

**Answer:** The player's control over the game is mostly direct:

- **Direct control** - the player directly manages his character, the actions it performs (eg, walking, searching, collecting items, using add-ons), and the interaction with objects in the game. This includes clicking on the screen or using a keyboard/mouse on PCs or touching the touch screen on mobile devices.

**Question:** Is it in real time or according to turns?

**Answer:** The game will take place in real time, not according to turns:

- **Real Time** - The player needs to manage his time effectively, as time is continuously advancing and the player needs to act quickly and smartly to find the cat before the time runs out. There is no waiting for the player's turn; The challenges and the need to act quickly are part of the game.

## Question 7

**Question:** What choices will your players have to make during the game?

**Answer:** The choices are-

- **Choosing items to use** - deciding when and which add-ons to use (for example, an hourglass to add time, Time Freeze to stop time, a ball of wool to hit objects, or multiply the points).
- **Determining the search route** - which areas on the map to explore first and in what order, considering the chance of finding the cat quickly.
- **Interact with objects** - decide which objects to inspect or break to reveal new possibilities or find the cat, while managing risks involving loss of time or points.
- **Time management and rewards** - how to balance the speed with which they complete the tasks and the desire to collect additional rewards that can be useful later in the game.

**Question:** What different strategies will they be able to employ to win the game?

**Answer:** The strategies are-

- **The Conservatism Strategy** - Players can choose a cautious approach, explore every nook and cranny and use add-ons only when it's essential, to minimize risks and save time and points.
- **Risk Strategy** - Other players may prefer to take risks, breaking as many objects as possible in the hope of finding the cat quickly, making extensive use of add-ons to extend time or stop it.
- **Optimization strategy** - analyze the map and objects to identify the areas with the highest chance of finding the cat, and plan the time and supplements accordingly.
- **Diversification strategy** - using all available plugins and options equally, to obtain benefits of all kinds and to be prepared for any situation.
