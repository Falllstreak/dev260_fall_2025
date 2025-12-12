# Digital Media Library Organizer

> One-sentence summary of what this app does and who it's for.

This digital library application can store, organize, search, and update media and data related to it.

## What I Built (Overview)

**Problem this solves:**  
_Explain the real-world task your app supports and why it's useful (2–4 sentences)._

**Your Answer:**
I chose to build a digital media library because the structure and flow made strong visual sense to me. It also let me work with the data structures I wanted to explore throughout the class. The app helps users organize their digital media into smart, searchable categories, making it easy to store, update, and browse their collection.

**Core features:**  
_List the main features your application provides (Add, Search, List, Update, Delete, etc.)_

**Your Answer:**

- Add and delete media items

- Search the library with partial-match support

- Filter media by type or genre

- Mark items as favorites for quick access

- Create playlists and organize media into custom groups

- Update existing media entries

## How to Run

**Requirements:**  
_List required .NET version, OS requirements, and any dependencies._

**Your Answer:**
- .NET version: 9.0.102

- OS requirement: Windows or Linux

- Dependencies: None, just .NET
  
```bash
git clone <your-repo-url>
cd <your-folder>
dotnet build
```

**Run:**  
_Provide the command to run your application._

**Your Answer:**

```bash
dotnet run
```

**Sample data (if applicable):**  
_Describe where sample data lives and how to load it (e.g., JSON file path, CSV import)._

**Your Answer:**

--- There is no preloaded sample data added to the app, this must be done manually.

## Using the App (Quick Start)

**Typical workflow:**  
_Describe the typical user workflow in 2–4 steps._

**Your Answer:**

1. Choose an option from the main menu
2. Add media by entering a title, type, genre, and optional duration.
3. Favorite items to mark them as important or make them easier to find later.
4. Create playlists to organize related media into custom groups.

**Input tips:**  
_Explain case sensitivity, required fields, and how common errors are handled gracefully._

**Your Answer:**

--- The app is built with case-insensitivity in mind so that users can gracefully type in whatever style they wish. Required fields will inform the user that they must input data in a certain format to move on, or allows them to exit.

## Data Structures (Brief Summary)

> Full rationale goes in **DESIGN.md**. Here, list only what you used and the feature it powers.

**Data structures used:**  
_List each data structure and briefly explain what feature it powers._

**Your Answer:**

- `Dictionary<string, MediaItem>` → This stores all media 
- `Dictionary<string, List<MediaItem>>` → Allow for creation of playlists.
    - Dictionary allows for exact-title access, prevents duplicates, has fast lookups, and playlists are keyed by name.
- `List<MediaItem>` → This is great ordered items that are easily appended.
- `HashSet<string>` → HashSet is used for favorites and makes it so there are fast membership checks. There are no duplicates, and it's easy to toggle on and off.

---

## Manual Testing Summary

> No unit tests required. Show how you verified correctness with 3–5 test scenarios.

**Test scenarios:**  
_Describe each test scenario with steps and expected results._

**Your Answer:**

**Scenario 1: [Add a new media item]**

- Steps: 
    1. Launch the app and choose Add Media.

    2. Enter a title (“MANDY”), type (“Movie”).

    3. Leave year field empty.

    4. Leave genre field blank.

    5. Leave length field blank.

- Expected result: Confirmation that the media item has been added, and a return to the main menu.

- Actual result: "✔ Added "MANDY" (Video) successfully!" is shown to the user, followed by a return to the main menu. Total media count increased to 1.

**Scenario 2: [Search by partial title]**

- Steps:
    1. Choose Search from the main menu.

    2. Choose option 1 "Search by title" and enter a partial title such as “ndy”.

    3. Submit the search.

- Expected result: The app returns all titles containing “ndy” (case-insensitive), including “MANDY”

- Actual result: The expected item information was returned in the following format:
  MANDY (Video) | Year: N/A | Genre: N/A | Length: N/A

**Scenario 3: [Create and view a playlist]**

- Steps:
    1. Choose option 6. Playlists from main menu
    2. Create a playlist named 'Colorful'
    3. Choose View All Playlists (option 5) to verify.

- Expected result: Playlist named "Colorful" was created and shows that it has no items within.
- Actual result: The following is displayed:
  Playlist: colorful
    (empty)

**Scenario 4: [Mark Item as Favorite] (optional)**

- Steps:
    1. Choose Favorites from main menu (option 5).
    2. Type title "ANDY" and press enter.

- Expected result: MANDY gets added to favorites.

- Actual result: The console displays:
  ⭐ Added "MANDY" to favorites!
The Favorites counter below the main menu is updated to 1.

---

## Known Limitations

**Limitations and edge cases:**  
_Describe any edge cases not handled, performance caveats, or known issues._

**Your Answer:**

- There is no data persistance built into the application currently.
- Playlist titles are normalized to lowercase.
- Length input is limited and may not include room for hours, minutes, and seconds.

## Comparers & String Handling

**Keys comparer:**  
_Describe what string comparer you used (e.g., StringComparer.OrdinalIgnoreCase) and why._

**Your Answer:**
All dictionaries and the HashSet use StringComparer.OrdinalIgnoreCase to allow case-insensitive lookups. This allows users to type media titles, playlist names, or genres in any capitalization and still match existing entries.

**Normalization:**  
_Explain how you normalize strings (trim whitespace, consistent casing, duplicate checks)._

**Your Answer:**
1. Playlist names are normalized to lowercase and trimmed to avoid duplicates.

2. Media titles and other user inputs are trimmed of leading and trailing whitespace.

3. Duplicate checks are used by the case-insensitive dictionaries and HashSet for favorites, ensuring no duplicate media or playlists are created.

---

## Credits & AI Disclosure

**Resources:**  
_List any articles, documentation, or code snippets you referenced or adapted._

**Your Answer:**
1. I used previous assignments to help guide me with structure and common pitfalls to avoid.
2. Along with that included some Microsoft documentation for the data structures.

- **AI usage (if any):**  
   _Describe what you asked AI tools, what code they influenced, and how you verified correctness._

  **Your Answer:**

1. I asked AI tools for suggestions on the any input validation I may have missed or hadn't already considered.

2. I verified the correctness of the AI suggestions by testing each validation implementation right after I tweaked the code.

## Challenges and Solutions

**Biggest challenge faced:**  
_Describe the most difficult part of the project - was it choosing the right data structures, implementing search functionality, handling edge cases, designing the user interface, or understanding a specific algorithm?_

**Your Answer:**
The most difficult part of the project was implementing the partial matches for searches, and making that it a feature across the application where it made sense. For example, within the Delete Media menu, you cannot use partial matches to delete, since this should require more precision because the choice is more consequential.

**How you solved it:**  
_Explain your solution approach and what helped you figure it out - research, consulting documentation, debugging with breakpoints, testing with simple examples, refactoring your design, etc._

**Your Answer:**
I referenced some of the past assignments that used partial searches within it. It helped me to implement it here.

**Most confusing concept:**  
_What was hardest to understand about data structures, algorithm complexity, key comparers, normalization, or organizing your code architecture?_

**Your Answer:**
The most confusing concept was making sure that the updates and deletions from playlists remained consistent across the entire application.

## Code Quality

**What you're most proud of in your implementation:**  
_Highlight the best aspect of your code - maybe your data structure choices, clean architecture, efficient algorithms, intuitive user interface, thorough error handling, or elegant solution to a complex problem._

**Your Answer:**
I think the data sctructure choices made me really proud of the application. This course overall taught me many more tools and where it's most appropraite to use each of them. Although there will be lifelong learning ahead, I'm glad to now say that I have started on cleaner and more useful code.

**What you would improve if you had more time:**  
_Identify areas for potential improvement - perhaps adding more features, optimizing performance, improving error handling, adding data persistence, refactoring for better maintainability, or enhancing the user experience._

**Your Answer:**
The section that describes the limitations of the application is basically all that I would improve. On top of that, I would like to experiment with managing actual files in this sort of way, rather than media meta data.

## Real-World Applications

**How this relates to real-world systems:**  
_Describe how your implementation connects to actual software systems - e.g., inventory management, customer databases, e-commerce platforms, social networks, task managers, or other applications in the industry._

**Your Answer:**
This application reminds me of a site like Listal. It's all about keeping information about media that you consume in an easy to view and filter way. It can help those keep track of their long history of consuming content and can showcase that to others whenever they wish.

**What you learned about data structures and algorithms:**  
_What insights did you gain about choosing appropriate data structures, performance tradeoffs, Big-O complexity in practice, the importance of good key design, or how data structures enable specific features?_

**Your Answer:**
Using the right data structures can really help with keeping the code a lot more clean. It's similar to using the right tool for the right job. It's possible to use other tools in certain scenarios, but you are ultimately sacrificing time or performance.

## Submission Checklist

- [✔] Public GitHub repository link submitted
- [✔] README.md completed (this file)
- [✔] DESIGN.md completed
- [✔] Source code included and builds successfully
- [X] (Optional) Slide deck or 5–10 minute demo video link (unlisted)

**Demo Video Link (optional):**
