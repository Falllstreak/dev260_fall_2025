namespace Assignment6
{
    /// <summary>
    /// Main matchmaking system managing queues and matches
    /// Students implement the core methods in this class
    /// </summary>
    public class MatchmakingSystem
    {
        // Data structures for managing the matchmaking system
        private Queue<Player> casualQueue = new Queue<Player>();
        private Queue<Player> rankedQueue = new Queue<Player>();
        private Queue<Player> quickPlayQueue = new Queue<Player>();
        private List<Player> allPlayers = new List<Player>();
        private List<Match> matchHistory = new List<Match>();

        // Statistics tracking
        private int totalMatches = 0;
        private DateTime systemStartTime = DateTime.Now;

        /// <summary>
        /// Create a new player and add to the system
        /// </summary>
        public Player CreatePlayer(string username, int skillRating, GameMode preferredMode = GameMode.Casual)
        {
            // Check for duplicate usernames
            if (allPlayers.Any(p => p.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException($"Player with username '{username}' already exists");
            }

            var player = new Player(username, skillRating, preferredMode);
            allPlayers.Add(player);
            return player;
        }

        /// <summary>
        /// Get all players in the system
        /// </summary>
        public List<Player> GetAllPlayers() => allPlayers.ToList();

        /// <summary>
        /// Get match history
        /// </summary>
        public List<Match> GetMatchHistory() => matchHistory.ToList();

        /// <summary>
        /// Get system statistics
        /// </summary>
        public string GetSystemStats()
        {
            var uptime = DateTime.Now - systemStartTime;
            var avgMatchQuality = matchHistory.Count > 0 
                ? matchHistory.Average(m => m.SkillDifference) 
                : 0;

            return $"""
                🎮 Matchmaking System Statistics
                ================================
                Total Players: {allPlayers.Count}
                Total Matches: {totalMatches}
                System Uptime: {uptime.ToString("hh\\:mm\\:ss")}
                
                Queue Status:
                - Casual: {casualQueue.Count} players
                - Ranked: {rankedQueue.Count} players  
                - QuickPlay: {quickPlayQueue.Count} players
                
                Match Quality:
                - Average Skill Difference: {avgMatchQuality:F1}
                - Recent Matches: {Math.Min(5, matchHistory.Count)}
                """;
        }

        // ============================================
        // STUDENT IMPLEMENTATION METHODS (TO DO)
        // ============================================

        /// <summary>
        /// TODO: Add a player to the appropriate queue based on game mode
        /// 
        /// Requirements:
        /// - Add player to correct queue (casualQueue, rankedQueue, or quickPlayQueue)
        /// - Call player.JoinQueue() to track queue time
        /// - Handle any validation needed
        /// </summary>
        public void AddToQueue(Player player, GameMode mode)
        {
            // TODO: Implement this method
            // Hint: Use switch statement on mode to select correct queue
            // Don't forget to call player.JoinQueue()!
            if (player == null)
                throw new ArgumentNullException(nameof(player), "Player cannot be null");

            // Sets the join time so GetQueueEstimate() can use it
            player.QueueJoinTime = DateTime.Now;

            // Remove player from any queue they might already be in
            RemoveFromAllQueues(player);

            // Add to correct queue
            switch (mode)
            {
                case GameMode.Casual:
                    casualQueue.Enqueue(player);
                    break;
                case GameMode.Ranked:
                    rankedQueue.Enqueue(player);
                    break;
                case GameMode.QuickPlay:
                    quickPlayQueue.Enqueue(player);
                    break;
                default:
                    throw new ArgumentException($"Invalid game mode: {mode}");
            }

            // Track when the player joined the queue
            player.JoinQueue();
        }

        /// <summary>
        /// TODO: Try to create a match from the specified queue
        /// 
        /// Requirements:
        /// - Return null if not enough players (need at least 2)
        /// - For Casual: Any two players can match (simple FIFO)
        /// - For Ranked: Only players within ±2 skill levels can match
        /// - For QuickPlay: Prefer skill matching, but allow any match if queue > 4 players
        /// - Remove matched players from queue and call LeaveQueue() on them
        /// - Return new Match object if successful
        /// </summary>
        public Match? TryCreateMatch(GameMode mode)
        {
            // TODO: Implement this method
            // Hint: Different logic needed for each mode
            // Remember to check queue count first!
            var queue = GetQueueByMode(mode);

            // Must have at least 2 players
            if (queue.Count < 2)
                return null;

            Player? player1 = null;
            Player? player2 = null;

            switch (mode)
            {
                case GameMode.Casual:
                    // FIFO: just take the first two
                    player1 = queue.Dequeue();
                    player2 = queue.Dequeue();
                    break;

                case GameMode.Ranked:
                    // Find the first pair within ±2 skill levels
                    var rankedList = queue.ToList();
                    bool found = false;

                    for (int i = 0; i < rankedList.Count - 1; i++)
                    {
                        for (int j = i + 1; j < rankedList.Count; j++)
                        {
                            if (CanMatchInRanked(rankedList[i], rankedList[j]))
                            {
                                player1 = rankedList[i];
                                player2 = rankedList[j];
                                found = true;
                                break;
                            }
                        }
                        if (found) break;
                    }

                    if (!found) return null; // No suitable match found

                    // Remove selected players from the queue
                    if (player1 != null) RemoveFromAllQueues(player1);
                    if (player2 != null) RemoveFromAllQueues(player2);
                    break;

                case GameMode.QuickPlay:
                    var quickList = queue.ToList();

                    // Try to find the closest skill match first
                    quickList.Sort((a, b) => a.SkillRating.CompareTo(b.SkillRating));
                    player1 = quickList[0];
                    player2 = quickList[1];

                    // If queue > 4, just pick first two for speed
                    if (quickList.Count > 4)
                    {
                        player1 = quickList[0];
                        player2 = quickList[1];
                    }

                    RemoveFromAllQueues(player1);
                    RemoveFromAllQueues(player2);
                    break;

                default:
                    throw new ArgumentException($"Unknown game mode: {mode}");
            }

            if (player1 == null || player2 == null)
                return null;

            return new Match(player1, player2, mode);
        }

        /// <summary>
        /// TODO: Process a match by simulating outcome and updating statistics
        /// 
        /// Requirements:
        /// - Call match.SimulateOutcome() to determine winner
        /// - Add match to matchHistory
        /// - Increment totalMatches counter
        /// - Display match results to console
        /// </summary>
        public void ProcessMatch(Match match)
        {
            // TODO: Implement this method
            // Hint: Very straightforward - simulate, record, display
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            // Simulate the outcome of the match
            match.SimulateOutcome();

            // Record the match in history
            matchHistory.Add(match);

            // Increment total matches counter
            totalMatches++;

            // Display the match result
            Console.WriteLine($"\n🎯 Match Processed: {match}");
            Console.WriteLine(match.ToDetailedString());
        }

        /// <summary>
        /// TODO: Display current status of all queues with formatting
        /// 
        /// Requirements:
        /// - Show header "Current Queue Status"
        /// - For each queue (Casual, Ranked, QuickPlay):
        ///   - Show queue name and player count
        ///   - List players with position numbers and queue times
        ///   - Handle empty queues gracefully
        /// - Use proper formatting and emojis for readability
        /// </summary>
        public void DisplayQueueStatus()
        {
            // TODO: Implement this method
            // Hint: Loop through each queue and display formatted information
            Console.WriteLine("\n---Current Queue Status---");
            Console.WriteLine("--------------------------");

            // Helper function added to display a single queue
            void ShowQueue(Queue<Player> queue, string queueName)
            {
                Console.WriteLine($"\n{queueName} Queue - {queue.Count} player(s)");

                if (queue.Count == 0)
                {
                    Console.WriteLine("   (empty)");
                    return;
                }

                int position = 1;
                foreach (var player in queue)
                {
                    // Displays the username, position, skill, and time in the queue
                    TimeSpan timeInQueue = DateTime.Now - player.QueueJoinTime;
                    Console.WriteLine($"   {position}. {player.Username} (Skill: {player.SkillRating})⏱️ {timeInQueue:mm\\:ss}");
                    position++;
                }
            }

            // Displays each queue
            ShowQueue(casualQueue, "Casual");
            ShowQueue(rankedQueue, "Ranked");
            ShowQueue(quickPlayQueue, "QuickPlay");
        }

        /// <summary>
        /// TODO: Display detailed statistics for a specific player
        /// 
        /// Requirements:
        /// - Use player.ToDetailedString() for basic info
        /// - Add queue status (in queue, estimated wait time)
        /// - Show recent match history for this player (last 3 matches)
        /// - Handle case where player has no matches
        /// </summary>
        public void DisplayPlayerStats(Player player)
        {
            // TODO: Implement this method
            // Hint: Combine player info with match history filtering
            if (player == null)
            {
                Console.WriteLine("Invalid player.");
                return;
            }

            Console.WriteLine($"\nPlayer Statistics: {player.Username}");
            Console.WriteLine("==================================");

            // Player informa
            Console.WriteLine(player.ToDetailedString());

            // Queue status
            string inQueue = "Not in any queue";
            string waitTime = "N/A";

            if (casualQueue.Contains(player))
            {
                inQueue = "Casual";
                waitTime = GetQueueEstimate(GameMode.Casual);
            }
            else if (rankedQueue.Contains(player))
            {
                inQueue = "Ranked";
                waitTime = GetQueueEstimate(GameMode.Ranked);
            }
            else if (quickPlayQueue.Contains(player))
            {
                inQueue = "QuickPlay";
                waitTime = GetQueueEstimate(GameMode.QuickPlay);
            }

            Console.WriteLine($"\nQueue Status: {inQueue}");
            Console.WriteLine($"Estimated Wait Time: {waitTime}");

            // Recent 3 matches
            var recentMatches = matchHistory
                .Where(m => m.Player1 == player || m.Player2 == player)
                .TakeLast(3)
                .Reverse()
                .ToList();

            Console.WriteLine("\nRecent Matches:");

            if (recentMatches.Count == 0)
            {
                Console.WriteLine("   (No matches played yet)");
            }
            else
            {
                foreach (var match in recentMatches)
                {
                    Console.WriteLine($"   {match.GetSummary()}");
                }
            }
        }

        /// <summary>
        /// TODO: Calculate estimated wait time for a queue
        /// 
        /// Requirements:
        /// - Return "No wait" if queue has 2+ players
        /// - Return "Short wait" if queue has 1 player
        /// - Return "Long wait" if queue is empty
        /// - For Ranked: Consider skill distribution (harder to match = longer wait)
        /// </summary>
        public string GetQueueEstimate(GameMode mode)
        {
            // TODO: Implement this method
            // Hint: Check queue counts and apply mode-specific logic
            // Get the appropriate queue for the selected game mode
            var queue = GetQueueByMode(mode);

            // If 2 or more players are waiting, a match can happen immediately
            if (queue.Count >= 2)
                return "No wait";

            // If only 1 player is in the queue, estimate wait time
            else if (queue.Count == 1)
            {
                var waitingPlayer = queue.Peek();

                // Special case for Ranked: extreme skill levels may take longer to match
                if (mode == GameMode.Ranked)
                {
                    if (waitingPlayer.SkillRating <= 2 || waitingPlayer.SkillRating >= 9)
                        return "Long wait"; // Rare skill levels = harder to find match
                    return "Short wait";    // Most players find match quickly
                }

                // Casual and QuickPlay: any single player likely waits a short time
                return "Short wait";
            }

            // No players in the queue, so anyone joining will wait for others
            else // queue.Count == 0
                return "Long wait";
        }

        // ============================================
        // HELPER METHODS (PROVIDED)
        // ============================================

        /// <summary>
        /// Helper: Check if two players can match in Ranked mode (±2 skill levels)
        /// </summary>
        private bool CanMatchInRanked(Player player1, Player player2)
        {
            return Math.Abs(player1.SkillRating - player2.SkillRating) <= 2;
        }

        /// <summary>
        /// Helper: Remove player from all queues (useful for cleanup)
        /// </summary>
        private void RemoveFromAllQueues(Player player)
        {
            // Create temporary lists to avoid modifying collections during iteration
            var casualPlayers = casualQueue.ToList();
            var rankedPlayers = rankedQueue.ToList();
            var quickPlayPlayers = quickPlayQueue.ToList();

            // Clear and rebuild queues without the specified player
            casualQueue.Clear();
            foreach (var p in casualPlayers.Where(p => p != player))
                casualQueue.Enqueue(p);

            rankedQueue.Clear();
            foreach (var p in rankedPlayers.Where(p => p != player))
                rankedQueue.Enqueue(p);

            quickPlayQueue.Clear();
            foreach (var p in quickPlayPlayers.Where(p => p != player))
                quickPlayQueue.Enqueue(p);

            player.LeaveQueue();
        }

        /// <summary>
        /// Helper: Get queue by mode (useful for generic operations)
        /// </summary>
        private Queue<Player> GetQueueByMode(GameMode mode)
        {
            return mode switch
            {
                GameMode.Casual => casualQueue,
                GameMode.Ranked => rankedQueue,
                GameMode.QuickPlay => quickPlayQueue,
                _ => throw new ArgumentException($"Unknown game mode: {mode}")
            };
        }
    }
}