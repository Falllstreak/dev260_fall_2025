using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaLibraryApp
{
    public class MediaLibrary
    {
        private readonly Dictionary<string, MediaItem> _media = new(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _favorites = new(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, List<MediaItem>> _playlists = new(StringComparer.OrdinalIgnoreCase);

        // ───────────────────────────────────────────────
        // Add / Update / Delete Media
        // ───────────────────────────────────────────────
        public void AddMedia(string title, string type, int year = 0, string genre = "", string length = "")
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("❌ Title cannot be empty.\n");
                return;
            }

            if (_media.ContainsKey(title))
            {
                Console.WriteLine("❌ A media item with that title already exists.\n");
                return;
            }

            _media[title] = new MediaItem(title, type, year, genre, length);
            Console.WriteLine($"✔ Added \"{title}\" ({type}) successfully!\n");
        }

        public void UpdateMedia(string title, string newType = "", int year = -1, string genre = "", string length = "")
        {
            if (!_media.TryGetValue(title, out var item))
            {
                Console.WriteLine("❌ Media not found.\n");
                return;
            }

            if (!string.IsNullOrWhiteSpace(newType)) item.Type = newType;
            if (year >= 0) item.Year = year;
            if (!string.IsNullOrWhiteSpace(genre)) item.Genre = genre;
            if (!string.IsNullOrWhiteSpace(length)) item.Length = length;

            Console.WriteLine($"✔ Updated \"{title}\" successfully!\n");
        }

        public void DeleteMedia(string title)
        {
            if (!_media.Remove(title))
            {
                Console.WriteLine("❌ Media not found.\n");
                return;
            }

            _favorites.Remove(title);

            foreach (var playlist in _playlists.Values)
                playlist.RemoveAll(m => m.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            Console.WriteLine($"✔ Deleted \"{title}\" from the library.\n");
        }

        // ───────────────────────────────────────────────
        // Favorites
        // ───────────────────────────────────────────────
        public void ToggleFavorite(string title)
        {
            if (!_media.ContainsKey(title))
            {
                Console.WriteLine("❌ Media not found.\n");
                return;
            }

            if (_favorites.Contains(title))
            {
                _favorites.Remove(title);
                Console.WriteLine($"⭐ Removed \"{title}\" from favorites.\n");
            }
            else
            {
                _favorites.Add(title);
                Console.WriteLine($"⭐ Added \"{title}\" to favorites!\n");
            }
        }

        public bool IsFavorite(string title) => _favorites.Contains(title);

        public List<MediaItem> GetAllFavorites() =>
            _favorites
                .Select(t => _media.TryGetValue(t, out var item) ? item : null)
                .Where(item => item != null)
                .Select(item => item!)
                .ToList();


        // ───────────────────────────────────────────────
        // Playlists
        // ───────────────────────────────────────────────
        private static string NormalizePlaylistName(string name) => name.Trim().ToLower();

        public bool CreatePlaylist(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;

            string key = NormalizePlaylistName(name);
            if (_playlists.ContainsKey(key)) return false;

            _playlists[key] = new List<MediaItem>();
            return true;
        }

        public bool AddToPlaylist(string playlistName, string mediaTitle)
        {
            string key = NormalizePlaylistName(playlistName);
            if (!_playlists.TryGetValue(key, out var playlist)) return false;

            var item = _media.Values.FirstOrDefault(m => m.Title.Equals(mediaTitle, StringComparison.OrdinalIgnoreCase));
            if (item == null || playlist.Any(m => m.Title.Equals(mediaTitle, StringComparison.OrdinalIgnoreCase))) return false;

            playlist.Add(item);
            return true;
        }

        public bool RemoveFromPlaylist(string playlistName, string mediaTitle)
        {
            string key = NormalizePlaylistName(playlistName);
            if (!_playlists.TryGetValue(key, out var playlist)) return false;

            var item = playlist.FirstOrDefault(m => m.Title.Equals(mediaTitle, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                playlist.Remove(item);
                return true;
            }

            return false;
        }

        public List<MediaItem> GetPlaylist(string playlistName)
        {
            string key = NormalizePlaylistName(playlistName);
            return _playlists.TryGetValue(key, out var list) ? list : new List<MediaItem>();
        }

        public List<string> GetAllPlaylistNames() => _playlists.Keys.ToList();

        // ───────────────────────────────────────────────
        // Filters & Searches
        // ───────────────────────────────────────────────
        public List<MediaItem> FilterByGenre(string genre) =>
            _media.Values.Where(m => !string.IsNullOrWhiteSpace(m.Genre) &&
                                     m.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase))
                         .ToList();

        public List<MediaItem> FilterByType(string type) =>
            _media.Values.Where(m => !string.IsNullOrWhiteSpace(m.Type) &&
                                     m.Type.Equals(type, StringComparison.OrdinalIgnoreCase))
                         .ToList();

        public List<string> GetPartialMatches(string input) =>
            _media.Keys.Where(t => t.Contains(input, StringComparison.OrdinalIgnoreCase)).ToList();

        // ───────────────────────────────────────────────
        // Media Lookup & Counts
        // ───────────────────────────────────────────────
        public MediaItem? GetMediaItem(string title) => _media.TryGetValue(title, out var item) ? item : null;
        public bool MediaExists(string title) => _media.ContainsKey(title);
        public int GetTotalMediaCount() => _media.Count;
        public int GetFavoriteCount() => _favorites.Count;
    }
}