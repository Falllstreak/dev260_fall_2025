# Project Design & Rationale

**Instructions:** Replace prompts with your content. Be specific and concise. If something doesn't apply, write "N/A" and explain briefly.

---

## Data Model & Entities

**Core entities:**  
_List your main entities with key fields, identifiers, and relationships (1–2 lines each)._

**Your Answer:**

**Entity A:**

- Name: MediaItem
- Key fields: Title, Type, Year, Genre, Length
- Identifiers: Title (string, case-insensitive)
- Relationships: Belongs to zero or more playlists and may be marked as favorite.

**Entity B (if applicable):**

- Name: Playlist
- Key fields: Name, List of MediaItem references
- Identifiers: Name (string, normalized to lowercase)
- Relationships: This contains multiple MediaItem entities.

**Identifiers (keys) and why they're chosen:**  
_Explain your choice of keys (e.g., string Id, composite key, case-insensitive, etc.)._

**Your Answer:**
Title as the primary key for media makes it so it works well with exact and partial searches. The Playlists uses normalized Name as it's key to prevent any duplicates and keeping the playlist searches simple.
---

## Data Structures — Choices & Justification

_List only the meaningful data structures you chose. For each, state the purpose, the role it plays in your app, why it fits, and alternatives considered._

### Structure #1

**Chosen Data Structure:**  
_Name the data structure (e.g., Dictionary<string, Customer>)._

**Your Answer:**
- `Dictionary<string, MediaItem>`

**Purpose / Role in App:**  
_What user action or feature does it power?_

**Your Answer:**
This allows for storage of media data in a library, it has fast lookups, and can be updated, added, and deleted by title.

**Why it fits:**  
_Explain access patterns, typical size, performance/Big-O, memory, simplicity._

**Your Answer:**
Media lookups and updates are O(1) on average. The library sizes are typicall hundres of items. The memory usage is low and performance is great. Dictionary makes it so it prevents duplicates naturally.

**Alternatives considered:**  
_List alternatives (e.g., List<T>, SortedDictionary, custom tree) and why you didn't choose them._

**Your Answer:**
List<MediaItem> could have been another option, but it would have been slower for lookups. SortedDictionary could have allowed for ordered traversal but the complexity wasn't needed for this particular application I felt.
---

### Structure #2

**Chosen Data Structure:**  
_Name the data structure._

**Your Answer:**
- `Dictionary<string, List<MediaItem>>`

**Purpose / Role in App:**  
_What user action or feature does it power?_

**Your Answer:**
This powers the playlists and is keyed by a normalized name. Each playlist is mapped to a list of media items for ordered access.

**Why it fits:**  
_Explain access patterns, typical size, performance/Big-O, memory, simplicity._

**Your Answer:**
Dictionaries can allow O(1) playlist retreval by name, while the lists provide ordered collections for the user. Typical playlists are rather limited in their number to capture a certain type of media, so the list operations can be pretty fast.

**Alternatives considered:**  
_List alternatives and why you didn't choose them._

**Your Answer:**
I think the scale of the app informed a lot of decisions. HashSets were considered and would have enforced uniqueness, but the ordering was important for playlists. A linked structure or a tree were other options, but honestly felt like too much for the purposes of a media application.

---

### Structure #3

**Chosen Data Structure:**  
_Name the data structure._

**Your Answer:**
`HashSet<string>`

**Purpose / Role in App:**  
_What user action or feature does it power?_

**Your Answer:**
This powers the favorites function super efficiently. It allows for fast membership checks and made it easy to toggle and item on and off as a favorite.

**Why it fits:**  
_Explain access patterns, typical size, performance/Big-O, memory, simplicity._

**Your Answer:**
HashSet has a O(1) average performance for lookups, adding and removing. It avoids duplicates, which makes sense for a favorite feature.

**Alternatives considered:**  
_List alternatives and why you didn't choose them._

**Your Answer:**
Other alternatives would be a List<string>. The check would have been an O(n) in this scenario which isn't as good if one had a large amount of favorites.

---

### Additional Structures (if applicable)

_Add more sections if you used additional structures like Queue for workflows, Stack for undo, HashSet for uniqueness, Graph for relationships, BST/SortedDictionary for ordered views, etc._

**Your Answer:**

---

## Comparers & String Handling

**Comparer choices:**  
_Explain what comparers you used and why (e.g., StringComparer.OrdinalIgnoreCase for keys)._

**For keys:**
StringComparer.OrdinalIgnoreCase for Media titles and Playlist names to ensure case-insensitivity access.

**For display sorting (if different):**

**Normalization rules:**  
_Describe how you normalize strings (trim whitespace, collapse duplicates, canonicalize casing)._

**Your Answer:**
N/A. Both Media and Playlists are displayed in insertion order and are not sorted.

**Bad key examples avoided:**  
_List examples of bad key choices and why you avoided them (e.g., non-unique names, culture-varying text, trailing spaces, substrings that can change)._

--- I avoided trailing spaces to make simple space bar hits not be a deal breaker, and culture-varying text to avoid locale issues.

## Performance Considerations

**Expected data scale:**  
_Describe the expected size of your data (e.g., 100 items, 10,000 items)._

**Your Answer:**
I would say a library includes around 50 to 500 items. Playlists would be about five to 50 items each.

**Performance bottlenecks identified:**  
_List any potential performance issues and how you addressed them._

**Your Answer:**
Partial searches requires scanning all titles O(n) which could become slow for super large libraries.

**Big-O analysis of core operations:**  
_Provide time complexity for your main operations (Add, Search, List, Update, Delete)._

**Your Answer:**

- Add: O(1)
- Search: O(n) for partial matches and O(1) for exact matches
- List: O(n) for full library view
- Update: O(1)
- Delete: O(1)

---

## Design Tradeoffs & Decisions

**Key design decisions:**  
_Explain major design choices and why you made them._

**Your Answer:**
I chose dictionaries for the O(1) access and HashSets for the O(1) favorites toggling. I kept playlists as lists because I wanted an ordered display. The case-insensitive string handling was for improving usability.

**Tradeoffs made:**  
_Describe any tradeoffs between simplicity vs performance, memory vs speed, etc._

**Your Answer:**
I made the tradeoff between simplicity and sorting. Insertion order is used instead of having stored collections. The memory vs speed tradeoff presented itself in the extra memory used, but it made for favorite toggling and lookups faster. This seemed more important for the potential size of the data this application should be handling.

**What you would do differently with more time:**  
_Reflect on what you might change or improve._

**Your Answer:**

With more time put towards the project I want to add persistence to the data. Allowing people to share or import playlists would also be fun. I'd also want to add in sorted views for playlists and the library. I would also like to experiment with stacks to include undo and redo for deletions just as a safety net for users.
