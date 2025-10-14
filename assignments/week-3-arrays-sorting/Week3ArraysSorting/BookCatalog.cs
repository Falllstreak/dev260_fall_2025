using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace Week3ArraysSorting
{
    /// <summary>
    /// Book Catalog implementation for Assignment 2 Part B
    /// Demonstrates recursive sorting and multi-dimensional indexing for fast lookups
    /// 
    /// Learning Focus:
    /// - Recursive sorting algorithms (QuickSort or MergeSort)
    /// - Multi-dimensional array indexing for performance
    /// - String normalization and binary search
    /// - File I/O and CLI interaction
    /// </summary>
    public class BookCatalog
    {
        #region Data Structures
        
        // Book storage arrays - parallel arrays that stay synchronized
        private string[] originalTitles;    // Original book titles for display
        private string[] normalizedTitles;  // Normalized titles for sorting/searching
        
        // Multi-dimensional index for O(1) lookup by first two letters (A-Z x A-Z = 26x26)
        private int[,] startIndex;  // Starting position for each letter pair in sorted array
        private int[,] endIndex;    // Ending position for each letter pair in sorted array
        
        // Book count tracker
        private int bookCount;

        #endregion

        /// <summary>
        /// Constructor - Initialize the book catalog
        /// Sets up data structures for book storage and multi-dimensional indexing
        /// </summary>
        public BookCatalog()
        {
            // Initialize arrays (will be resized when books are loaded)
            originalTitles = Array.Empty<string>();
            normalizedTitles = Array.Empty<string>();

            // Initialize multi-dimensional index arrays (26x26 for A-Z x A-Z)
            startIndex = new int[26, 26];
            endIndex = new int[26, 26];

            // Initialize all index ranges as empty (-1 indicates no books for that letter pair)
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    startIndex[i, j] = -1;  // -1 means no books start with this letter pair
                    endIndex[i, j] = -1;    // -1 means no books end with this letter pair
                }
            }

            // Reset book count
            bookCount = 0;

            Console.WriteLine("BookCatalog initialized - Ready to load books and build index");
        }

        /// <summary>
        /// Load books from file and build sorted index
        /// </summary>
        /// <param name="filePath">Path to books.txt file</param>
        public void LoadBooks(string filePath = "books.txt")
        {
            try
            {
                Console.WriteLine($"Loading books from: {filePath}");
                
                // Step 1 - Load books from file
                LoadBooksFromFile(filePath);
                
                // TODO: Step 2 - Sort using recursive algorithm
                SortBooksRecursively();
                
                // TODO: Step 3 - Build multi-dimensional index
                BuildMultiDimensionalIndex();
                
                Console.WriteLine($"Successfully loaded and indexed {bookCount} books.");
                Console.WriteLine("Index built for A-Z x A-Z (26x26) letter pairs.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading books: {ex.Message}");
                throw;
            }
        }
        
        /// <summary>
        /// Start interactive lookup session
        /// TODO: Implement the CLI loop
        /// </summary>
        public void StartLookupSession()
        {
            Console.Clear();
            Console.WriteLine("=== BOOK CATALOG LOOKUP (Part B) ===");
            Console.WriteLine();
            
            // TODO: Check if books are loaded
            if (bookCount == 0)
            {
                Console.WriteLine("No books loaded! Please load a book file first.");
                return;
            }
            
            DisplayLookupInstructions();
            
            // TODO: Implement lookup loop
            bool keepLooking = true;
            
            while (keepLooking)
            {
                Console.WriteLine();
                Console.Write("Enter a book title (or 'exit'): ");
                string? query = Console.ReadLine();
                
                // TODO: Handle exit condition
                if (string.IsNullOrEmpty(query) || query.ToLowerInvariant() == "exit")
                {
                    keepLooking = false;
                    continue;
                }
                
                // TODO: Perform lookup
                PerformLookup(query);
            }
            
            Console.WriteLine("Returning to main menu...");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        
        /// <summary>
        /// Load book titles from text file
        /// </summary>
        /// <param name="filePath">Path to the books file</param>
        private void LoadBooksFromFile(string filePath)
        {
            // Check if file exists
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Book file not found: {filePath}");
            }
            
            Console.WriteLine($"Reading book titles from: {filePath}");
            
            try
            {
                // Read all lines from file
                string[] lines = File.ReadAllLines(filePath);
                
                // Filter out empty lines and whitespace-only lines
                var validLines = new List<string>();
                foreach (string line in lines)
                {
                    string trimmedLine = line.Trim();
                    if (!string.IsNullOrEmpty(trimmedLine))
                    {
                        validLines.Add(trimmedLine);
                    }
                }
                
                // Initialize arrays with the correct size
                bookCount = validLines.Count;
                originalTitles = new string[bookCount];
                normalizedTitles = new string[bookCount];
                
                // Store both original and normalized versions
                for (int i = 0; i < bookCount; i++)
                {
                    originalTitles[i] = validLines[i]; // Keep original formatting for display
                    normalizedTitles[i] = NormalizeTitle(originalTitles[i]); // Normalized for sorting/indexing
                }
                
                Console.WriteLine($"Successfully loaded {bookCount} book titles.");
            }
            catch (IOException ex)
            {
                throw new IOException($"Error reading file '{filePath}': {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error loading books from '{filePath}': {ex.Message}", ex);
            }
        }
        
        /// <summary>
        /// Normalize book title for consistent sorting and indexing
        /// </summary>
        /// <param name="title">Original book title</param>
        /// <returns>Normalized title for sorting/indexing</returns>
        private string NormalizeTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return "";
            }
            
            // Step 1: Trim whitespace and convert to uppercase
            string normalized = title.Trim().ToUpperInvariant();
            
            // Step 2: Optional - Remove leading articles for better sorting
            // This helps group books by their main title rather than article
            string[] articles = { "THE ", "A ", "AN " };
            
            foreach (string article in articles)
            {
                if (normalized.StartsWith(article))
                {
                    normalized = normalized.Substring(article.Length).Trim();
                    break; // Only remove the first article found
                }
            }
            
            // Step 3: Handle edge case where title was only articles
            if (string.IsNullOrEmpty(normalized))
            {
                return title.Trim().ToUpperInvariant(); // Return original if normalization results in empty
            }
            
            return normalized;
        }

        /// <summary>
        /// Sort books using recursive algorithm (QuickSort OR MergeSort)
        /// TODO: Choose ONE recursive sorting algorithm to implement
        /// </summary>
        private void SortBooksRecursively()
        {
            Console.WriteLine("TODO: Implement recursive sorting algorithm");
            Console.WriteLine("Choose ONE to implement:");
            Console.WriteLine("1. QuickSort - Choose pivot strategy and document it");
            Console.WriteLine("2. MergeSort - Implement recursive split/merge");
            Console.WriteLine();
            Console.WriteLine("Requirements:");
            Console.WriteLine("- Must be YOUR recursive implementation");
            Console.WriteLine("- Cannot use Array.Sort() or LINQ");
            Console.WriteLine("- Sort both arrays in parallel (original and normalized)");
            Console.WriteLine("- Document Big-O time/space complexity in README");

            // TODO: Call your chosen sorting algorithm
            // QuickSort(normalizedTitles, originalTitles, 0, bookCount - 1);
            // Example: MergeSort(normalizedTitles, originalTitles, 0, bookCount - 1);

            QuickSort(normalizedTitles, originalTitles, 0, bookCount - 1);
            Console.WriteLine("Books sorted recursively using QuickSort.");

        }

        /// <summary>
        /// Build multi-dimensional index over sorted data
        /// TODO: Create 26x26 index for first two letters
        /// </summary>
        private void BuildMultiDimensionalIndex()
        {
            Console.WriteLine("TODO: Build multi-dimensional index");
            Console.WriteLine("Requirements:");
            Console.WriteLine("- Create int[,] startIndex and int[,] endIndex arrays (26x26)");
            Console.WriteLine("- Map A-Z to indices 0-25");
            Console.WriteLine("- Handle non-letter starts (map to index 0 or create 27th bucket)");
            Console.WriteLine("- Scan sorted array once to record [start,end) ranges");
            Console.WriteLine("- Empty ranges should have start > end or start = -1");

            // TODO: Initialize index arrays
            // TODO: Scan sorted titles and record boundaries for each letter pair

            // Example structure:
            // // Scan sorted array and build ranges
            // for (int bookIndex = 0; bookIndex < bookCount; bookIndex++)
            // {
            //     // Get first two letters and update index ranges
            // }

            // Step 0: Reset all ranges to -1 (no books for that prefix yet)
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    startIndex[i, j] = -1;
                    endIndex[i, j] = -1;
                }
            }

            // Step 1: Scan through the sorted array of normalized titles
            for (int k = 0; k < bookCount; k++)
            {
                string title = normalizedTitles[k];

                // Skip titles shorter than 2 letters
                if (title.Length < 2) continue;

                int first = GetLetterIndex(title[0]);   // 0-25 for A-Z
                int second = GetLetterIndex(title[1]);  // 0-25 for A-Z

                // Step 2: Set startIndex if this is the first time seeing this prefix
                if (startIndex[first, second] == -1)
                {
                    startIndex[first, second] = k;
                }

                // Step 3: Update endIndex as the last occurrence of this prefix
                endIndex[first, second] = k;
            }

            Console.WriteLine("Multi-dimensional index built for A-Z x A-Z.");
        }

        private int GetLetterIndex(char c)
        {
            if (char.IsLetter(c))
                return char.ToUpper(c) - 'A';
            return 0;
        }
        
        private int BinarySearchInRange(string query, int start, int end)
        {
            // Initialize low and high pointers for binary search
            int low = start;
            int high = end;

            // Loop until the search space is empty
            while (low <= high)
            {
                // Find the middle index of the current subarray
                int mid = (low + high) / 2;

                // Compare the query with the middle element
                int cmp = string.Compare(normalizedTitles[mid], query, StringComparison.Ordinal);

                if (cmp == 0)
                {
                    // Exact match found
                    return mid;
                }
                else if (cmp < 0)
                {
                    // Query is greater than middle element, search in the right half
                    low = mid + 1;
                }
                else
                {
                    // Query is less than middle element, search in the left half
                    high = mid - 1;
                }
            }

            // No exact match found
            return -1;
        }
        
        /// <summary>
        /// Perform lookup with exact match and suggestions
        /// TODO: Implement indexed lookup with binary search
        /// </summary>
        /// <param name="query">User's search query</param>
        private void PerformLookup(string query)
        {
            // TODO: Normalize query same way as indexing
            string normalizedQuery = NormalizeTitle(query);

            if (string.IsNullOrEmpty(normalizedQuery))
            {
                Console.WriteLine("Invalid input. Please enter a valid book title.");
                return;
            }

            // Step 2: Get first two letters and map them to 0-25 indices
            int first = GetLetterIndex(normalizedQuery[0]);
            int second = GetLetterIndex(normalizedQuery.Length > 1 ? normalizedQuery[1] : normalizedQuery[0]);

            // Step 3: Look up the start/end range from the 2D index
            int start = startIndex[first, second];
            int end = endIndex[first, second];

            // Step 4: Handle empty range
            if (start == -1 || end == -1)
            {
                Console.WriteLine($"No books found starting with \"{query}\".");
                return;
            }

            // Step 5: Perform binary search within this range
            int foundIndex = BinarySearchInRange(normalizedQuery, start, end);

            if (foundIndex != -1)
            {
                // Exact match found, display original title
                Console.WriteLine($"Book found: {originalTitles[foundIndex]}");
            }
            else
            {
                // No exact match found, provide suggestions
                Console.WriteLine($"No exact match found for \"{query}\". Suggestions:");

                int maxSuggestions = 5;
                int suggestionCount = 0;

                // Scan the indexed range and collect titles that start with the same query prefix
                for (int i = start; i <= end && suggestionCount < maxSuggestions; i++)
                {
                    string candidate = normalizedTitles[i];

                    // Check if candidate starts with normalized query
                    if (candidate.StartsWith(normalizedQuery, StringComparison.Ordinal))
                    {
                        Console.WriteLine($"- {originalTitles[i]}");
                        suggestionCount++;
                    }
                }

                // If no suggestions were found, optionally show the first few titles in the range
                if (suggestionCount == 0)
                {
                    Console.WriteLine($"- {originalTitles[start]}");
                    suggestionCount++;
                    for (int i = start + 1; i <= end && suggestionCount < maxSuggestions; i++)
                    {
                        Console.WriteLine($"- {originalTitles[i]}");
                        suggestionCount++;
                    }
                }
            }
            
            // Console.WriteLine($"TODO: Perform lookup for '{query}'");
            // Console.WriteLine("Requirements:");
            // Console.WriteLine("1. Get first 1-2 letters of normalized query");
            // Console.WriteLine("2. Look up [start,end) range from 2D index in O(1)");
            // Console.WriteLine("3. If empty range, show suggestions from nearby ranges");
            // Console.WriteLine("4. If non-empty range, binary search within slice");
            // Console.WriteLine("5. Show exact match or helpful suggestions");
            // Console.WriteLine("6. Always display original titles (not normalized)");
            
            // TODO: Extract first two letters for indexing
            // TODO: Get start/end range from 2D index
            // TODO: If range is empty, find suggestions
            // TODO: If range exists, binary search for exact match
            // TODO: Display results using original titles
        }
        
        /// <summary>
        /// Display lookup instructions
        /// TODO: Customize instructions for your implementation
        /// </summary>
        private void DisplayLookupInstructions()
        {
            Console.WriteLine("BOOK LOOKUP INSTRUCTIONS:");
            Console.WriteLine("- Enter any book title to search");
            Console.WriteLine("- Exact matches will be shown if found");
            Console.WriteLine("- Suggestions provided for partial/no matches");
            Console.WriteLine("- Type 'exit' to return to main menu");
            Console.WriteLine();
            Console.WriteLine($"Catalog contains {bookCount} books, sorted and indexed for fast lookup.");
        }

        // TODO: Add your sorting algorithm methods
        // Choose ONE to implement:

        /// <summary>
        /// QuickSort implementation (Option 1)
        /// TODO: Implement if you choose QuickSort
        /// </summary>
        private void QuickSort(string[] normalizedArray, string[] originalArray, int low, int high)
        {
            // TODO: Implement recursive QuickSort
            // TODO: Choose and document pivot strategy
            // TODO: Ensure both arrays stay synchronized
            
            // Base case: if the subarray has 1 or 0 elements, it is already sorted
            if (low < high)
            {
                // Partition the subarray and get pivot index
                int pivotIndex = Partition(normalizedArray, originalArray, low, high);

                // Recursively sort left subarray (elements <= pivot)
                QuickSort(normalizedArray, originalArray, low, pivotIndex - 1);

                // Recursively sort right subarray (elements > pivot)
                QuickSort(normalizedArray, originalArray, pivotIndex + 1, high);
            }

        }
        
        private int Partition(string[] normalizedArray, string[] originalArray, int low, int high)
        {
            // Choose pivot as the last element in the subarray
            string pivot = normalizedArray[high];

            // Boundary for elements smaller than or equal to pivot
            int boundaryIndex = low - 1;

            // Iterate through subarray
            for (int currentIndex = low; currentIndex < high; currentIndex++)
            {
                // Compare current element with pivot
                if (string.Compare(normalizedArray[currentIndex], pivot, StringComparison.Ordinal) <= 0)
                {
                    boundaryIndex++; // Move boundary forward
                    Swap(normalizedArray, originalArray, boundaryIndex, currentIndex);
                }
            }

            // Place pivot after the last smaller element
            Swap(normalizedArray, originalArray, boundaryIndex + 1, high);

            return boundaryIndex + 1;
        }

        private void Swap(string[] normalizedArray, string[] originalArray, int i, int j)
        {
            // Swap in normalized array
            string tempNormalized = normalizedArray[i];
            normalizedArray[i] = normalizedArray[j];
            normalizedArray[j] = tempNormalized;

            // Swap in original array
            string tempOriginal = originalArray[i];
            originalArray[i] = originalArray[j];
            originalArray[j] = tempOriginal;
        }
        
        /// <summary>
        /// MergeSort implementation (Option 2)  
        /// TODO: Implement if you choose MergeSort
        /// </summary>
        // private void MergeSort(string[] normalizedArray, string[] originalArray, int left, int right)
        // {
        //     // TODO: Implement recursive MergeSort
        //     // TODO: Handle O(n) extra space requirement
        //     // TODO: Ensure both arrays stay synchronized
        // }
        
        // TODO: Add helper methods as needed
        // Examples:
        // - GetLetterIndex(char letter) - Convert A-Z to 0-25
        // - BinarySearchInRange(string query, int start, int end)
        // - FindSuggestions(string query, int maxSuggestions)
        // - SwapElements(int index1, int index2) - For QuickSort
        // - MergeArrays(...) - For MergeSort
    }
}