# Assignment 6: Game Matchmaking System - Implementation Notes

**Name:** [Your Name]

## Multi-Queue Pattern Understanding

**How the multi-queue pattern works for game matchmaking:**
[Explain your understanding of how the three different queues (Casual, Ranked, QuickPlay) work together and why each has different matching strategies]

Each game mode has its own queue so players don’t get mixed up. Casual is a relaxing experience matching the FIFO style. Ranked is competitive, so only players within 2 skill levels get paired. QuickPlay is a mix and tries to match by skill but won’t make players wait forever if the queue is too long. The three queues let the players choose speedy queues versus balanced matches depending on what they want.


## Challenges and Solutions

**Biggest challenge faced:**
[Describe the most difficult part of the assignment - was it the skill-based matching, queue management, or match processing?]

It was difficult getting the Ranked mode to match within 2 skill levels of each other.

**How you solved it:**
[Explain your solution approach and what helped you figure it out]

There's a loop that goes through the queue and checks for skill differences using a temporary list to pick a pair that works. Getting the players out of the queue after matching them helped this.

**Most confusing concept:**
[What was hardest to understand about queues, matchmaking algorithms, or game mode differences?]

Quickplay was difficult to combine both the different styles.

## Code Quality

**What you're most proud of in your implementation:**
[Highlight the best aspect of your code - maybe your skill matching logic, queue status display, or error handling]

I found that keeping some of the common pit-falls and suggestions in mind from the start helped to make the error scenarios.

**What you would improve if you had more time:**
[Identify areas for potential improvement - perhaps better algorithms, more features, or cleaner code structure]

I would probably add more features to this, allowing player skill levels to change dynamically based on wins and losses.

## Testing Approach

**How you tested your implementation:**
[Describe your overall testing strategy - how did you verify skill-based matching worked correctly?]

I created players as suggested with varying skill levels, then tested each of the modes one by one, along with each of the other features. Afterwards I tried out some edge cases to see if I could break it.

**Test scenarios you used:**
[List specific scenarios you tested, like players with different skill levels, empty queues, etc.]

I tested different skill level matching, empty queues, and each of the game modes.

**Issues you discovered during testing:**
[Any bugs or problems you found and fixed during development]

I had an error that affected accessing information in the Player class. I had to edit that file to get it to run properly.

## Game Mode Understanding

**Casual Mode matching strategy:**
[Explain how you implemented FIFO matching for Casual mode]

Casual simply took the two first entries into the queue and matched them together.

**Ranked Mode matching strategy:**
[Explain how you implemented skill-based matching (±2 levels) for Ranked mode]

Players would stay in queue as a loop searches for skill levels within 2 levels of each other. Until it finds that, players remain in queue.

**QuickPlay Mode matching strategy:**
[Explain your approach to balancing speed vs. skill matching in QuickPlay mode]

The first thing that is tried is to match people with the closest skill levels, then it matches anyone depending on how many are in queue, 4+.

## Real-World Applications

**How this relates to actual game matchmaking:**
[Describe how your implementation connects to real games like League of Legends, Overwatch, etc.]

These games listed use this type of logic to group players together in matches. People tend to want players around their skill levels to play against and this assignment does that in a simple way.

**What you learned about game industry patterns:**
[What insights did you gain about how online games handle player matching?]

Queues are great for online matchmaking! It's actually fun to learn about this as it's common when playing the games themselves to say to friends "Hey, I'm joining queue." I knew roughly what was happening behind the scenes, but now I see the actual logic that modes might go through.

## Stretch Features

[If you implemented any extra credit features like team formation or advanced analytics, describe them here. If not, write "None implemented"]

None implemented.

## Time Spent

**Total time:** [X hours]

7 hours

**Breakdown:**

- Understanding the assignment and queue concepts: [1 hour]
- Implementing the 6 core methods: [4 hours]
- Testing different game modes and scenarios: [1 hour]
- Debugging and fixing issues: [30 mins]
- Writing these notes: [30 mins]

**Most time-consuming part:** [Which aspect took the longest and why - algorithm design, debugging, testing, etc.]

I'm new to queues, and it was difficult with the quickplay mixing of the two different styles.

## Key Learning Outcomes

**Queue concepts learned:**
[What did you learn about managing multiple queues and different processing strategies?]

I learned about requiring certain conditions for queues to actually progress.

**Algorithm design insights:**
[What did you learn about designing matching algorithms and handling different requirements?]

I learned what actually goes into balancing a game, roughly.

**Software engineering practices:**
[What did you learn about error handling, user interfaces, and code organization?]

Without error handling, and clear code, the console app would break when used in slightly unexpected ways. The idea is to not allow that to happen. Clear code makes it much easier to fix when a scenario can break the app.
