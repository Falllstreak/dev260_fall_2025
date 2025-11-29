# Assignment 9: BST File System Navigator - Implementation Notes

**Name:** [Your Name]

## Binary Search Tree Pattern Understanding

**How BST operations work for file system navigation:**
[Explain your understanding of how O(log n) searches, automatic sorting through in-order traversal, and hierarchical file organization work together for efficient file management]

Answer: BSTs allows one to find files quickly because everything is sorted automatically, so we don’t have to check each file one by one. Traversing the tree in order also helps maintain a simple hierarchical view of directories and files.

## Challenges and Solutions

**Biggest challenge faced:**
[Describe the most difficult part of the assignment - was it recursive tree algorithms, custom file/directory comparison logic, or complex BST deletion?]

Answer: The DeleteItem function was definitely the toughest part since handling nodes with two children and updating the tree correctly was tricky.

**How you solved it:**
[Explain your solution approach and what helped you figure it out - research, debugging, testing strategies, etc.]

Answer: I walked through each deletion case and took note of what happened with each. I included some console logs to make sure the tree updated correctly, testing with different scenarios.

**Most confusing concept:**
[What was hardest to understand about BST operations, recursive thinking, or file system hierarchies?]

Answer: Understanding file system hierarchies within the BST was confusing at first, especially how directories and files should be ordered together.

## Code Quality

**What you're most proud of in your implementation:**
[Highlight the best aspect of your code - maybe your recursive algorithms, custom comparison logic, or efficient tree traversal]

Answer: I’m proud of the DeleteItem function working correctly and keeping the tree structure intact. The recursive traversal methods also turned out pretty efficient.

**What you would improve if you had more time:**
[Identify areas for potential improvement - perhaps better error handling, more efficient algorithms, or additional features]

Answer: I’d think I would improve error handling and input validation to make the program even more user-proof. Adding extra features like directory size summaries would also be nice. I would explore the stretch goals too.

## Real-World Applications

**How this relates to actual file systems:**
[Describe how your implementation connects to tools like Windows File Explorer, macOS Finder, database indexing, etc.]

Answer: This is like how file explorers and search functions organize and quickly locate files in directories. It’s similar to indexing in databases where hierarchical and sorted structures speed up the searches.

**What you learned about tree algorithms:**
[What insights did you gain about recursive thinking, tree traversal, and hierarchical data organization?]

Answer: I gained a decent understanding for recursion, tree traversal, and managing hierarchical data efficiently. Seeing the tree visually helped me understand how insertions and deletions affect structure.

## Stretch Features

[If you implemented any extra credit features like file pattern matching or directory size analysis, describe them here. If not, write "None implemented"]

Answer: None implemented

## Time Spent

**Total time:** 6 hours

**Breakdown:**

- Understanding BST concepts and assignment requirements: 2
- Implementing the 8 core TODO methods: 4
- Testing with different file scenarios: 1
- Debugging recursive algorithms and BST operations: 30 mins
- Writing these notes: 15 mins

**Most time-consuming part:** [Which aspect took the longest and why - recursive thinking, BST deletion, custom comparison logic, etc.]

Answer: Debugging the DeleteItem function took the longest because making sure all cases maintained the BST structure needed some careful planning and testing.
