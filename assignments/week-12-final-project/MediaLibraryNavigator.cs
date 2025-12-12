using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace MediaLibraryApp
{
    public class MediaLibraryNavigator
    {
        private MediaLibrary _library = new MediaLibrary();

        public void Start()
        {
            bool running = true;

            while (running)
            {
                ShowMenu();
                string input = Console.ReadLine() ?? "";
                Console.WriteLine();

                switch (input.Trim().ToLower())
                {
                    case "1":
                    case "add":
                    case "add media":
                        AddMedia();
                        break;

                    case "2":
                    case "view":
                    case "search":
                        ViewOrSearchMedia();
                        break;

                    case "3":
                    case "update":
                        UpdateMedia();
                        break;

                    case "4":
                    case "delete":
                        DeleteMedia();
                        break;

                    case "5":
                    case "favorites":
                    case "fav":
                        ManageFavorites();
                        break;

                    case "6":
                    case "playlists":
                        ManagePlaylists();
                        break;

                    case "7":
                    case "exit":
                        running = false;
                        Console.WriteLine("Exiting Digital Media Library Organizer...");
                        break;

                    default:
                        Console.WriteLine("Invalid option — try again.\n");
                        break;
                }
            }
        }

        private void ShowMenu()
        {
            int totalTracks = _library.GetTotalMediaCount();
            int totalFavorites = _library.GetFavoriteCount();

            Console.WriteLine("┌─ Digital Media Library Organizer ─────────────────────────────┐");
            Console.WriteLine("│ 1.➕ Add Media      │ 2.🔎 View/Search   │ 3.📝 Update Media  │");
            Console.WriteLine("│ 4.➖ Delete Media   │ 5.✨ Favorites     │ 6.🎵 Playlists     │");
            Console.WriteLine("│                                          │ 7.🚪 Exit          │");
            Console.WriteLine("└───────────────────────────────────────────────────────────────┘");
            Console.WriteLine($"   └─    Total Media Count: {totalTracks} | Favorites: {totalFavorites}    ─┘");
            Console.Write("\nChoose operation (number or name): ");
        }

        // ───────────────────────────────────────────────
        // Add / Update Media
        // ───────────────────────────────────────────────

        private void AddMedia()
        {
            Console.Write("Enter media title: ");
            string title = Console.ReadLine() ?? "";

            string type = ReadValidatedType();

            int year = ReadValidatedYear(allowBlank: true);

            Console.Write("Enter genre (or leave blank): ");
            string genre = Console.ReadLine() ?? "";

            string length = ReadValidatedLength();

            _library.AddMedia(title, type, year, genre, length);
        }

        private void UpdateMedia()
        {
            Console.Write("Enter media title to update: ");
            string title = Console.ReadLine() ?? "";

            string type = ReadValidatedType(allowBlank: true);
            int year = ReadValidatedYear(allowBlank: true);

            Console.Write("Enter new genre (or leave blank to keep current): ");
            string genre = Console.ReadLine() ?? "";

            string length = ReadValidatedLength(allowBlank: true);

            _library.UpdateMedia(title, type, year, genre, length);
        }

        // ───────────────────────────────────────────────
        // Input Helpers
        // ───────────────────────────────────────────────

        private string ReadValidatedType(bool allowBlank = false)
        {
            string[] allowedTypes = { "Song", "Video", "Audiobook" };
            string type = "";

            while (true)
            {
                Console.Write("Enter media type (Song, Video, Audiobook" + (allowBlank ? ", leave blank to keep current" : "") + "): ");
                type = (Console.ReadLine() ?? "").Trim();

                if (allowBlank && string.IsNullOrWhiteSpace(type))
                    return ""; // keep current

                if (allowedTypes.Any(t => t.Equals(type, StringComparison.OrdinalIgnoreCase)))
                    return allowedTypes.First(t => t.Equals(type, StringComparison.OrdinalIgnoreCase));

                Console.WriteLine("❌ Invalid media type. Please try again.\n");
            }
        }

        private int ReadValidatedYear(bool allowBlank = false)
        {
            int year = allowBlank ? -1 : 0;
            while (true)
            {
                Console.Write("Enter year (YYYY" + (allowBlank ? ", leave blank to keep current" : "") + "): ");
                string input = (Console.ReadLine() ?? "").Trim();

                if (allowBlank && string.IsNullOrWhiteSpace(input))
                    return -1; // sentinel for "keep current"

                if (input.Length == 4 && int.TryParse(input, out year))
                    return year;

                Console.WriteLine("❌ Invalid year. Please enter a 4-digit number (e.g., 1986).\n");
            }
        }

        private string ReadValidatedLength(bool allowBlank = false)
        {
            string length = "";
            while (true)
            {
                Console.Write("Enter length (MM:SS or HH:MM:SS" + (allowBlank ? ", leave blank to keep current" : "") + "): ");
                length = (Console.ReadLine() ?? "").Trim();

                if (allowBlank && string.IsNullOrWhiteSpace(length))
                    return "";

                if (string.IsNullOrWhiteSpace(length) ||
                    Regex.IsMatch(length, @"^(\d{1,2}:)?[0-5]?\d:[0-5]\d$"))
                    return length;

                Console.WriteLine("❌ Invalid length format. Example: 3:45 or 1:02:30\n");
            }
        }

        // ───────────────────────────────────────────────
        // View/Search / Filter / Favorites
        // ───────────────────────────────────────────────

        private void ViewOrSearchMedia()
        {
            Console.WriteLine("View/Search Media");
            Console.WriteLine("1) Search by title");
            Console.WriteLine("2) Filter by genre");
            Console.WriteLine("3) Filter by type");
            Console.WriteLine("4) View favorites only");
            Console.WriteLine("Leave blank to return to main menu.\n");

            Console.Write("Choose option: ");
            string input = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(input))
                return;

            switch (input.Trim())
            {
                case "1":
                case "title":
                    SearchByTitle();
                    break;
                case "2":
                case "genre":
                    FilterByGenre();
                    break;
                case "3":
                case "type":
                    FilterByType();
                    break;
                case "4":
                case "favorites":
                    ViewFavoritesOnly();
                    break;
                default:
                    Console.WriteLine("❌ Invalid option.\n");
                    break;
            }
        }

        private void SearchByTitle()
        {
            Console.Write("Enter title to search (partial allowed, leave blank to return): ");
            string input = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(input))
                return;

            var matches = _library.GetPartialMatches(input);

            if (matches.Count == 0)
                Console.WriteLine("❌ No media matched.\n");
            else
            {
                foreach (var title in matches)
                {
                    var item = _library.GetMediaItem(title);
                    if (item != null)
                    {
                        string favMark = _library.IsFavorite(title) ? "⭐" : " ";
                        Console.WriteLine($"{favMark} {item}");
                    }
                }
                Console.WriteLine();
            }
        }

        private void FilterByGenre()
        {
            Console.Write("Enter genre to filter by (leave blank to return): ");
            string genre = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(genre)) return;

            var filtered = _library.FilterByGenre(genre);
            if (filtered.Count == 0)
                Console.WriteLine($"❌ No media found for genre \"{genre}\".\n");
            else
                foreach (var item in filtered)
                    Console.WriteLine(item);

            Console.WriteLine();
        }

        private void FilterByType()
        {
            Console.Write("Enter media type to filter by (leave blank to return): ");
            string type = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(type)) return;

            var filtered = _library.FilterByType(type);
            if (filtered.Count == 0)
                Console.WriteLine($"❌ No media found of type \"{type}\".\n");
            else
                foreach (var item in filtered)
                    Console.WriteLine(item);

            Console.WriteLine();
        }

        private void ViewFavoritesOnly()
        {
            var favorites = _library.GetAllFavorites();
            if (favorites.Count == 0)
                Console.WriteLine("No favorites found.\n");
            else
                foreach (var item in favorites)
                    Console.WriteLine(item);

            Console.WriteLine();
        }

        // ───────────────────────────────────────────────
        // Delete / Favorites / Playlists
        // ───────────────────────────────────────────────

        private void DeleteMedia()
        {
            Console.Write("Enter media title to delete: ");
            string title = Console.ReadLine() ?? "";
            _library.DeleteMedia(title);
        }

        private void ManageFavorites()
        {
            Console.WriteLine("✨ Favorites Menu");
            Console.WriteLine("Enter the title of the media to add/remove from favorites.");
            Console.WriteLine("Leave blank to return to the main menu.\n");

            while (true)
            {
                Console.Write("Title: ");
                string input = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(input))
                    return;

                var matches = _library.GetPartialMatches(input);

                if (matches.Count == 0)
                {
                    Console.WriteLine($"❌ No media matched \"{input}\". Please try again or leave blank to return.\n");
                }
                else if (matches.Count == 1)
                {
                    _library.ToggleFavorite(matches[0]);
                    return;
                }
                else
                {
                    Console.WriteLine("Multiple matches found:");
                    for (int i = 0; i < matches.Count; i++)
                        Console.WriteLine($"{i + 1}) {matches[i]}");

                    Console.Write("Select number to toggle favorite, or leave blank to cancel: ");
                    string choice = Console.ReadLine() ?? "";

                    if (string.IsNullOrWhiteSpace(choice))
                        continue;

                    if (int.TryParse(choice, out int index) && index >= 1 && index <= matches.Count)
                    {
                        _library.ToggleFavorite(matches[index - 1]);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("❌ Invalid selection. Try again.\n");
                    }
                }
            }
        }

        private void ManagePlaylists()
        {
            while (true)
            {
                Console.WriteLine("\n┌─ Playlist Manager ───────────────────────┐");
                Console.WriteLine("│ 1. Create playlist                       │");
                Console.WriteLine("│ 2. Add to Playlist                       │");
                Console.WriteLine("│ 3. Remove from Playlist                  │");
                Console.WriteLine("│ 4. View Playlist                         │");
                Console.WriteLine("│ 5. View All Playlists                    │");
                Console.WriteLine("│ 6. Back to Main Menu                     │");
                Console.WriteLine("└──────────────────────────────────────────┘");
                Console.Write("Choose option: ");

                string choice = (Console.ReadLine() ?? "").Trim().ToLower();

                switch (choice)
                {
                    case "1":
                    case "create":
                        CreatePlaylistMenu();
                        break;
                    case "2":
                    case "add":
                        AddToPlaylistMenu();
                        break;
                    case "3":
                    case "remove":
                        RemoveFromPlaylistMenu();
                        break;
                    case "4":
                    case "view":
                        ViewPlaylistMenu();
                        break;
                    case "5":
                    case "all":
                        ViewAllPlaylistsMenu();
                        break;
                    case "6":
                    case "back":
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("❌ Invalid choice.\n");
                        break;
                }
            }
        }

        private void CreatePlaylistMenu()
        {
            Console.Write("Enter new playlist name (leave blank to cancel): ");
            string name = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Cancelled.\n");
                return;
            }

            // Store lowercase internally, original for display
            string key = name.ToLower();
            if (_library.CreatePlaylist(key))
                Console.WriteLine($"✔ Playlist \"{name}\" created.\n");
            else
                Console.WriteLine("❌ Could not create playlist (maybe already exists or invalid name).\n");
        }

        private void AddToPlaylistMenu()
        {
            var playlists = _library.GetAllPlaylistNames();
            if (playlists.Count == 0)
            {
                Console.WriteLine("\n(No playlists exist yet. You must create one first.)");
                return;
            }

            Console.Write("\nEnter playlist name: ");
            string playlist = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(playlist)) return;

            if (!playlists.Contains(playlist, StringComparer.OrdinalIgnoreCase))
            {
                Console.WriteLine($"❌ Playlist \"{playlist}\" does not exist. Create it first.\n");
                return;
            }

            Console.Write("Enter media title to add: ");
            string input = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(input)) return;

            var matches = _library.GetPartialMatches(input);
            if (matches.Count == 0)
            {
                Console.WriteLine($"❌ No media matched \"{input}\".\n");
                return;
            }

            if (matches.Count == 1)
            {
                if (_library.AddToPlaylist(playlist, matches[0]))
                    Console.WriteLine($"✔ Added \"{matches[0]}\" to \"{playlist}\".\n");
                else
                    Console.WriteLine("❌ Could not add media (duplicate? incorrect playlist?).\n");
                return;
            }

            Console.WriteLine("\nMultiple matches found:");
            for (int i = 0; i < matches.Count; i++)
                Console.WriteLine($"{i + 1}) {matches[i]}");

            Console.Write("Select number to add (leave blank to cancel): ");
            string choice = Console.ReadLine() ?? "";
            if (int.TryParse(choice, out int index) && index >= 1 && index <= matches.Count)
            {
                if (_library.AddToPlaylist(playlist, matches[index - 1]))
                    Console.WriteLine($"✔ Added \"{matches[index - 1]}\" to \"{playlist}\".\n");
                else
                    Console.WriteLine("❌ Could not add media (duplicate? incorrect playlist?).\n");
            }
            else if (!string.IsNullOrWhiteSpace(choice))
            {
                Console.WriteLine("❌ Invalid selection. Cancelled.\n");
            }
        }

        private void RemoveFromPlaylistMenu()
        {
            Console.Write("Enter playlist name: ");
            string playlist = Console.ReadLine() ?? "";
            Console.Write("Enter media title to remove: ");
            string title = Console.ReadLine() ?? "";

            if (_library.RemoveFromPlaylist(playlist, title))
                Console.WriteLine($"✔ Removed \"{title}\" from \"{playlist}\".\n");
            else
                Console.WriteLine("❌ Could not remove media (check playlist name and media title).\n");
        }

        private void ViewPlaylistMenu()
        {
            Console.Write("\nEnter playlist name (partial allowed, or leave blank to cancel): ");
            string input = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(input)) return;

            var playlists = _library.GetAllPlaylistNames()
                                    .Where(p => p.Contains(input, StringComparison.OrdinalIgnoreCase))
                                    .ToList();

            if (playlists.Count == 0)
            {
                Console.WriteLine("❌ No playlists matched that name.\n");
                return;
            }

            if (playlists.Count == 1)
            {
                ShowPlaylistContents(playlists[0]);
                return;
            }

            Console.WriteLine("\nMultiple playlists matched:");
            for (int i = 0; i < playlists.Count; i++)
                Console.WriteLine($"{i + 1}) {playlists[i]}");

            Console.Write("Select number (leave blank to cancel): ");
            string choice = Console.ReadLine() ?? "";
            if (int.TryParse(choice, out int id) && id >= 1 && id <= playlists.Count)
                ShowPlaylistContents(playlists[id - 1]);
        }

        private void ViewAllPlaylistsMenu()
        {
            var playlistNames = _library.GetAllPlaylistNames();
            if (playlistNames.Count == 0)
            {
                Console.WriteLine("❌ No playlists created yet.\n");
                return;
            }

            Console.WriteLine("── All Playlists ──────────────────────────────");
            foreach (var name in playlistNames)
            {
                Console.WriteLine($"\nPlaylist: {name}");
                var items = _library.GetPlaylist(name);
                if (items.Count == 0)
                    Console.WriteLine("   (empty)");
                else
                    foreach (var item in items)
                    {
                        string favMark = _library.IsFavorite(item.Title) ? "⭐" : " ";
                        Console.WriteLine($"   {favMark} {item}");
                    }
            }
            Console.WriteLine("──────────────────────────────────────────────\n");
        }

        private void ShowPlaylistContents(string playlist)
        {
            var items = _library.GetPlaylist(playlist);

            Console.WriteLine($"\n── Playlist: {playlist} ──");
            if (items.Count == 0)
            {
                Console.WriteLine("(empty)\n");
                return;
            }

            foreach (var item in items)
            {
                string fav = _library.IsFavorite(item.Title) ? "⭐" : " ";
                Console.WriteLine($" {fav} {item}");
            }
            Console.WriteLine();
        }
    }
}