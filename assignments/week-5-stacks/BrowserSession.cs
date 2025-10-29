using System;
using System.Collections.Generic;

namespace Assignment5
{
    /// <summary>
    /// Manages browser navigation state with back and forward stacks
    /// </summary>
    public class BrowserSession
    {
        private Stack<WebPage> backStack;
        private Stack<WebPage> forwardStack;
        private WebPage? currentPage;

        public WebPage? CurrentPage => currentPage;
        public int BackHistoryCount => backStack.Count;
        public int ForwardHistoryCount => forwardStack.Count;
        public bool CanGoBack => backStack.Count > 0;
        public bool CanGoForward => forwardStack.Count > 0;

        public BrowserSession()
        {
            backStack = new Stack<WebPage>();
            forwardStack = new Stack<WebPage>();
            currentPage = null;
        }

        /// <summary>
        /// Navigate to a new URL
        /// TODO: Implement this method
        /// - If there's a current page, push it to back stack
        /// - Clear the forward stack (new navigation invalidates forward history)
        /// - Set the new page as current
        /// </summary>
        public void VisitUrl(string url, string title)
        {
            // If there's current page, push it to back stack history
            if (currentPage != null)
            {
                backStack.Push(currentPage);
            }

            // Visiting new page clears forward stack
            forwardStack.Clear();

            // Create and set the new current page
            currentPage = new WebPage(url, title);
        }

        /// <summary>
        /// Navigate back to previous page
        /// TODO: Implement this method
        /// - Check if back navigation is possible
        /// - Move current page to forward stack
        /// - Pop page from back stack and make it current
        /// </summary>
        public bool GoBack()
        {
            if (!CanGoBack)
                return false;

            // Move current page to forward history
            if (currentPage != null)
                forwardStack.Push(currentPage);

            // Pop page from back stack and make it current
            currentPage = backStack.Pop();
            return true;
        }

        /// <summary>
        /// Navigate forward to next page
        /// TODO: Implement this method
        /// - Check if forward navigation is possible
        /// - Move current page to back stack
        /// - Pop page from forward stack and make it current
        /// </summary>
        public bool GoForward()
        {
            if (!CanGoForward)
                return false;

            // Move current page to back history
            if (currentPage != null)
                backStack.Push(currentPage);

            // Pop from forward stack and make it current
            currentPage = forwardStack.Pop();
            return true;
        }

        /// <summary>
        /// Get navigation status information
        /// </summary>
        public string GetNavigationStatus()
        {
            var status = $"📊 Navigation Status:\n";
            status += $"   Back History: {BackHistoryCount} pages\n";
            status += $"   Forward History: {ForwardHistoryCount} pages\n";
            status += $"   Can Go Back: {(CanGoBack ? "✅ Yes" : "❌ No")}\n";
            status += $"   Can Go Forward: {(CanGoForward ? "✅ Yes" : "❌ No")}";
            return status;
        }

        /// <summary>
        /// Display back history (most recent first)
        /// TODO: Implement this method
        /// Expected output format:
        /// 📚 Back History (most recent first):
        ///    1. Google Search (https://www.google.com)
        ///    2. GitHub Homepage (https://github.com)
        ///    3. Stack Overflow (https://stackoverflow.com)
        /// 
        /// If empty, show: "   (No back history)"
        /// Use foreach to iterate through backStack (it gives LIFO order automatically)
        /// </summary>
        public void DisplayBackHistory()
        {
            // TODO: Implement back history display
            // 1. Print header: "📚 Back History (most recent first):"
            // 2. Check if backStack.Count == 0, if so print "   (No back history)" and return
            // 3. Use foreach loop with backStack to display pages
            // 4. Show position number, page title, and URL for each page
            // 5. Format: "   {position}. {page.Title} ({page.Url})"
            Console.WriteLine("Back History (most recent first):");

            if (backStack.Count == 0)
            {
                Console.WriteLine("   (No back history)");
                return;
            }
            int position = 1;
            foreach (WebPage page in backStack)
            {
                Console.WriteLine($"    {position}. {page.Title} ({page.Url})");
                position++;
            }
        }

        /// <summary>
        /// Display forward history (next page first)
        /// TODO: Implement this method
        /// Expected output format:
        /// 📖 Forward History (next page first):
        ///    1. Documentation Page (https://docs.microsoft.com)
        ///    2. YouTube (https://www.youtube.com)
        /// 
        /// If empty, show: "   (No forward history)"
        /// Use foreach to iterate through forwardStack (it gives LIFO order automatically)
        /// </summary>
        public void DisplayForwardHistory()
        {
            // TODO: Implement forward history display
            // 1. Print header: "📖 Forward History (next page first):"
            // 2. Check if forwardStack.Count == 0, if so print "   (No forward history)" and return
            // 3. Use foreach loop with forwardStack to display pages
            // 4. Show position number, page title, and URL for each page
            // 5. Format: "   {position}. {page.Title} ({page.Url})"
            Console.WriteLine("Forward History (next page first):");

            if (forwardStack.Count == 0)
            {
                Console.WriteLine("   (No forward history)");
                return;
            }

            int position = 1;
            foreach (WebPage page in forwardStack)
            {
                Console.WriteLine($"   {position}. {page.Title} ({page.Url})");
                position++;
            }
        }

        /// <summary>
        /// Clear all navigation history
        /// TODO: Implement this method
        /// Expected behavior:
        /// - Count total pages to be cleared (backStack.Count + forwardStack.Count)
        /// - Clear both backStack and forwardStack
        /// - Print confirmation: "✅ Cleared {totalCleared} pages from navigation history."
        /// Note: This does NOT clear the current page, only the navigation history
        /// </summary>
        public void ClearHistory()
        {
            // TODO: Implement clear history functionality
            // 1. Calculate total pages: int totalCleared = backStack.Count + forwardStack.Count;
            // 2. Clear both stacks: backStack.Clear() and forwardStack.Clear()
            // 3. Print confirmation message with count of cleared pages
            int totalCleared = backStack.Count + forwardStack.Count;
            backStack.Clear();
            forwardStack.Clear();

            Console.WriteLine($"Cleared {totalCleared} pages from navigation history.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}