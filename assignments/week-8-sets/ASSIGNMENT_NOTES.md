# Assignment 8: Spell Checker & Vocabulary Explorer - Implementation Notes

**Name:** Edwin Pacheco

## HashSet Pattern Understanding

**How HashSet<T> operations work for spell checking:**
[Explain your understanding of how O(1) lookups, automatic uniqueness, and set-based categorization work together for efficient text analysis]
O(1) lookups let the program quickly check if a word exists in the dictionary without scanning the entire collection. Combined with automatic uniqueness, this allows efficient categorization of words into correct and misspelled sets for fast and accurate text analysis.

## Challenges and Solutions

**Biggest challenge faced:**
[Describe the most difficult part of the assignment - was it text normalization, HashSet operations, or file I/O handling?]
The file I/O handling was one of the biggest challenges as I had to remember once again how to handle that scenario.

**How you solved it:**
[Explain your solution approach and what helped you figure it out]
I did some research and looked over previous assignments dealing with file I/O handling to remember how it worked. 

**Most confusing concept:**
[What was hardest to understand about HashSet operations, text processing, or case-insensitive comparisons?]
Honestly HashSets were hard to understand in a visual sense. Visually I think stacks, queues, and lists felt simple to visualize. Hashes work in a very efficient way that took more research to see.

## Code Quality

**What you're most proud of in your implementation:**
I liked the error catching for the code and feel it handles a lot of scenarios well.

**What you would improve if you had more time:**
I think I would add more stretch options for this if I had more time!

## Testing Approach

**How you tested your implementation:**
[Describe your overall testing strategy - how did you verify spell checking worked correctly?]
I made sure to check each method at a time and checked to see if it was working as intended.

**Test scenarios you used:**
[List specific scenarios you tested, like mixed case words, punctuation handling, edge cases, etc.]
I made sure to test mixed case words, punctuation handling, and edge cases.

**Issues you discovered during testing:**
[Any bugs or problems you found and fixed during development]
There weren't many bugs that got in the way during development aside from typos.

## HashSet vs List Understanding

**When to use HashSet:**
[Explain when you would choose HashSet over List based on your experience]
HashSet over list would be a scenario where you need automatic uniqueness and fast membership checks. List would make it so O(n) searches and duplicate checks would need to be done manually compared to the automatic uniqueness.

**When to use List:**
[Explain when List is more appropriate than HashSet]
A scenario where List would be more appropriate is if duplicated are something you want as part of the list. A specific order is another area where a list might be needed.

**Performance benefits observed:**
[Describe how O(1) lookups and automatic uniqueness helped your implementation]
O(1) lookups made it so checking if a word was in the dictionary or in text was very fast. Automatic uniqueness made it so each word was only stored once and really helps with making it so a lot of extra code isn't needed to enforce this.

## Real-World Applications

**How this relates to actual spell checkers:**
[Describe how your implementation connects to tools like Microsoft Word, Google Docs, etc.]
This checker is similar to how these tools mentioned may work. It must account for any of the ways that people write, as well as mistakes that are made. There is a lot of removal and normalization going on. Certain filler words and punctuation doesn't really need to be part of this type of checker. I am curious how complex grammar checkers work.

**What you learned about text processing:**
[What insights did you gain about handling real-world text data and normalization?]
I think it's both more simple and more complex than I imagined it being. I've found, though, that it can be done in a very clean and efficient way that doesn't take a ton of resources. The complex part is the list of words, or dictionary, that has to be included. The more simple aspect than I imagined is how all of it is handled and compared to the dictionary.

## Stretch Features

None implemented

## Time Spent

**Total time:** 4 hours

**Breakdown:**
- Understanding HashSet concepts and assignment requirements: 2 hours
- Implementing the 6 core methods: 1 hour
- Testing different text files and scenarios: 30 minutes
- Debugging and fixing issues: 30 minutes
- Writing these notes: 15 minutes

**Most time-consuming part:** HashSet operations took most of the time to understand. It's a new data type for me and takes some time for me to truly understand how it's working. I think also working with the I/O files I also needed some refreshing on.

## Key Learning Outcomes

**HashSet concepts learned:**
[What did you learn about O(1) performance, automatic uniqueness, and set-based operations?]
I learned more about the value of HashSets and how good for performance it is. The automatic uniqueness really helps with cutting out some extra code that would be needed.

**Text processing insights:**
[What did you learn about normalization, tokenization, and handling real-world text data?]
Normalization is key to making something like this to work as people will naturally type in various different ways. Tokenization and cleaning of real-world text makes accurate word counting, the spell checking, and reliable analysis even when text contains punctuation, whitespace, or repeated words.

**Software engineering practices:**
[What did you learn about error handling, user interfaces, and defensive programming?]
Defensive programming and error handling is absolutely needed for something like this as there's a lot of room for tiny things to go wrong. Normalization is part of this and prevents crashes and makes things more transparent for users when providing information for how they are potentially using the program in a way that isn't intended. The clean user interface was part of this and helps one in understanding all the features.