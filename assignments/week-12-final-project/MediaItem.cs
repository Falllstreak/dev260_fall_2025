namespace MediaLibraryApp
{
    public class MediaItem
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Length { get; set; } // e.g. "3:54" or "2h 16m"

        public MediaItem(string title, string type, int year = 0, string genre = "", string length = "")
        {
            Title = title;
            Type = type;
            Year = year;
            Genre = genre;
            Length = length;
        }

        public override string ToString()
        {
            string yearString   = Year > 0 ? Year.ToString() : "N/A";
            string genreString  = string.IsNullOrWhiteSpace(Genre) ? "N/A" : Genre;
            string lengthString = string.IsNullOrWhiteSpace(Length) ? "N/A" : Length;

            return $"{Title} ({Type}) | Year: {yearString} | Genre: {genreString} | Length: {lengthString}";
        }
    }
}