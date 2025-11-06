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
        /// Add a player to the appropriate queue based on game mode
        /// 
        /// Requirements:
        /// - Add player to correct queue (casualQueue, rankedQueue, or quickPlayQueue)
        /// - Call player.JoinQueue() to track queue time
        /// - Handle any validation needed
        /// </summary>
        public void AddToQueue(Player player, GameMode mode)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            // Remove player from any existing queue first
            RemoveFromAllQueues(player);

            // Add to appropriate queue based on game mode
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
                    throw new ArgumentException($"Unknown game mode: {mode}");
            }

            // Track queue join time
            player.JoinQueue();
        }

        /// <summary>
        /// Try to create a match from the specified queue
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
            Queue<Player> queue = GetQueueByMode(mode);

            // Need at least 2 players to create a match
            if (queue.Count < 2)
                return null;

            Player? player1 = null;
            Player? player2 = null;

            switch (mode)
            {
                case GameMode.Casual:
                    // Simple FIFO - take first two players
                    player1 = queue.Dequeue();
                    player2 = queue.Dequeue();
                    break;

                case GameMode.Ranked:
                    // Skill-based matching - find compatible players
                    player1 = queue.Dequeue();
                    
                    // Look for a player within ±2 skill levels
                    var rankedPlayers = new List<Player>();
                    rankedPlayers.Add(player1);
                    while (queue.Count > 0)
                    {
                        var candidate = queue.Dequeue();
                        if (CanMatchInRanked(player1, candidate))
                        {
                            player2 = candidate;
                            break;
                        }
                        rankedPlayers.Add(candidate);
                    }
                    
                    // If no match found, put all players back and return null
                    if (player2 == null)
                    {
                        foreach (var p in rankedPlayers)
                            queue.Enqueue(p);
                        return null;
                    }
                    
                    // Put back any unmatched players (except the two we matched)
                    foreach (var p in rankedPlayers.Where(p => p != player1 && p != player2))
                        queue.Enqueue(p);
                    break;

                case GameMode.QuickPlay:
                    // Prefer skill matching, but allow broader matching if queue is long
                    player1 = queue.Dequeue();
                    
                    if (queue.Count >= 4)
                    {
                        // Queue is long - take next available player for speed
                        player2 = queue.Dequeue();
                    }
                    else
                    {
                        // Queue is short - try to find skill-compatible match
                        var quickPlayPlayers = new List<Player>();
                        quickPlayPlayers.Add(player1);
                        
                        // First try to find someone within ±2 skill levels
                        bool foundSkillMatch = false;
                        while (queue.Count > 0 && !foundSkillMatch)
                        {
                            var candidate = queue.Dequeue();
                            if (CanMatchInRanked(player1, candidate))
                            {
                                player2 = candidate;
                                foundSkillMatch = true;
                            }
                            else
                            {
                                quickPlayPlayers.Add(candidate);
                            }
                        }
                        
                        // If no skill match found, take the first available player
                        if (player2 == null && quickPlayPlayers.Count > 1)
                        {
                            player2 = quickPlayPlayers[1];
                            // Put back remaining players
                            for (int i = 2; i < quickPlayPlayers.Count; i++)
                                queue.Enqueue(quickPlayPlayers[i]);
                        }
                        else if (player2 == null)
                        {
                            // No match possible, put everyone back
                            foreach (var p in quickPlayPlayers)
                                queue.Enqueue(p);
                            return null;
                        }
                    }
                    break;
            }

            // If we found both players, create the match
            if (player1 != null && player2 != null)
            {
                // Remove both players from queue tracking
                player1.LeaveQueue();
                player2.LeaveQueue();
                
                return new Match(player1, player2, mode);
            }

            return null;
        }

        /// <summary>
        /// Process a match by simulating outcome and updating statistics
        /// 
        /// Requirements:
        /// - Call match.SimulateOutcome() to determine winner
        /// - Add match to matchHistory
        /// - Increment totalMatches counter
        /// - Display match results to console
        /// </summary>
        public void ProcessMatch(Match match)
        {
            if (match == null)
                throw new ArgumentNullException(nameof(match));

            // Simulate the match outcome
            match.SimulateOutcome();

            // Add to match history
            matchHistory.Add(match);
            totalMatches++;

            // Display match results
            Console.WriteLine(match.ToDetailedString());
        }

        /// <summary>
        /// Display current status of all queues with formatting
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
            Console.WriteLine("🎮 Current Queue Status");
            Console.WriteLine("=======================");
            Console.WriteLine();

            // Display Casual Queue
            Console.WriteLine($"🕹️ Casual Queue ({casualQueue.Count} players)");
            if (casualQueue.Count == 0)
            {
                Console.WriteLine("   Queue is empty");
            }
            else
            {
                int position = 1;
                foreach (var player in casualQueue)
                {
                    Console.WriteLine($"   {position}. {player.Username} (Skill: {player.SkillRating}) - Waiting: {player.GetQueueTime()}");
                    position++;
                }
            }
            Console.WriteLine();

            // Display Ranked Queue
            Console.WriteLine($"🏆 Ranked Queue ({rankedQueue.Count} players)");
            if (rankedQueue.Count == 0)
            {
                Console.WriteLine("   Queue is empty");
            }
            else
            {
                int position = 1;
                foreach (var player in rankedQueue)
                {
                    Console.WriteLine($"   {position}. {player.Username} (Skill: {player.SkillRating}) - Waiting: {player.GetQueueTime()}");
                    position++;
                }
            }
            Console.WriteLine();

            // Display QuickPlay Queue
            Console.WriteLine($"⚡ QuickPlay Queue ({quickPlayQueue.Count} players)");
            if (quickPlayQueue.Count == 0)
            {
                Console.WriteLine("   Queue is empty");
            }
            else
            {
                int position = 1;
                foreach (var player in quickPlayQueue)
                {
                    Console.WriteLine($"   {position}. {player.Username} (Skill: {player.SkillRating}) - Waiting: {player.GetQueueTime()}");
                    position++;
                }
            }
            Console.WriteLine();

            // Display summary
            int totalQueued = casualQueue.Count + rankedQueue.Count + quickPlayQueue.Count;
            Console.WriteLine($"📊 Summary: {totalQueued} players total in queues");
        }

        /// <summary>
        /// Display detailed statistics for a specific player
        /// 
        /// Requirements:
        /// - Use player.ToDetailedString() for basic info
        /// - Add queue status (in queue, estimated wait time)
        /// - Show recent match history for this player (last 3 matches)
        /// - Handle case where player has no matches
        /// </summary>
        public void DisplayPlayerStats(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            // Display basic player information
            Console.WriteLine(player.ToDetailedString());

            // Check queue status
            bool inCasualQueue = casualQueue.Contains(player);
            bool inRankedQueue = rankedQueue.Contains(player);
            bool inQuickPlayQueue = quickPlayQueue.Contains(player);

            if (inCasualQueue || inRankedQueue || inQuickPlayQueue)
            {
                GameMode currentMode = inCasualQueue ? GameMode.Casual : 
                                      inRankedQueue ? GameMode.Ranked : GameMode.QuickPlay;
                string estimate = GetQueueEstimate(currentMode);
                Console.WriteLine($"📍 Currently in {currentMode} queue - Estimated wait: {estimate}");
            }
            else
            {
                Console.WriteLine("📍 Not currently in any queue");
            }

            // Display recent match history
            Console.WriteLine("\n📜 Recent Match History:");
            var playerMatches = matchHistory
                .Where(m => m.Player1 == player || m.Player2 == player)
                .TakeLast(3)
                .Reverse()
                .ToList();

            if (playerMatches.Count == 0)
            {
                Console.WriteLine("   No matches played yet");
            }
            else
            {
                foreach (var match in playerMatches)
                {
                    bool isWinner = match.Winner == player;
                    string result = isWinner ? "✅ WIN" : "❌ LOSS";
                    string opponent = match.Player1 == player ? match.Player2.Username : match.Player1.Username;
                    
                    Console.WriteLine($"   {result} vs {opponent} ({match.Mode}) - {match.MatchTime:MM/dd HH:mm}");
                    Console.WriteLine($"      Skill: {player.SkillRating} vs {(match.Player1 == player ? match.Player2.SkillRating : match.Player1.SkillRating)} | Quality: {match.GetMatchQuality()}");
                }
            }
        }

        /// <summary>
        /// Calculate estimated wait time for a queue
        /// 
        /// Requirements:
        /// - Return "No wait" if queue has 2+ players
        /// - Return "Short wait" if queue has 1 player
        /// - Return "Long wait" if queue is empty
        /// - For Ranked: Consider skill distribution (harder to match = longer wait)
        /// </summary>
        public string GetQueueEstimate(GameMode mode)
        {
            Queue<Player> queue = GetQueueByMode(mode);

            if (queue.Count >= 2)
            {
                return "No wait";
            }
            else if (queue.Count == 1)
            {
                // For Ranked mode, check if skill matching might be difficult
                if (mode == GameMode.Ranked)
                {
                    var player = queue.Peek();
                    // Check if there are other players in the system with compatible skill
                    var compatiblePlayers = allPlayers
                        .Where(p => p != player && Math.Abs(p.SkillRating - player.SkillRating) <= 2)
                        .Count();
                    
                    if (compatiblePlayers == 0)
                        return "Long wait (few compatible players)";
                    else
                        return "Short wait";
                }
                else
                {
                    return "Short wait";
                }
            }
            else
            {
                return "Long wait";
            }
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