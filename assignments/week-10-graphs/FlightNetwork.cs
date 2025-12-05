using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment10
{
    /// <summary>
    /// Represents a flight route network using a graph data structure.
    /// Uses adjacency list representation for efficient storage and traversal.
    /// </summary>
    public class FlightNetwork
    {
        // Graph vertices: Dictionary of airport codes to Airport objects
        private Dictionary<string, Airport> airports;

        // Graph edges: Adjacency list mapping origin airport codes to lists of outgoing flights
        private Dictionary<string, List<Flight>> routes;

        // Airport code to city name mapping
        private static readonly Dictionary<string, string> airportCities = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "SEA", "Seattle" },
            { "PDX", "Portland" },
            { "SFO", "San Francisco" },
            { "LAX", "Los Angeles" },
            { "LAS", "Las Vegas" },
            { "PHX", "Phoenix" },
            { "DEN", "Denver" },
            { "DFW", "Dallas" },
            { "IAH", "Houston" },
            { "ORD", "Chicago" },
            { "MSP", "Minneapolis" },
            { "DTW", "Detroit" },
            { "ATL", "Atlanta" },
            { "MIA", "Miami" },
            { "JFK", "New York" },
            { "BOS", "Boston" }
        };

        /// <summary>
        /// Initializes a new empty flight network graph
        /// </summary>
        public FlightNetwork()
        {
            airports = new Dictionary<string, Airport>(StringComparer.OrdinalIgnoreCase);
            routes = new Dictionary<string, List<Flight>>(StringComparer.OrdinalIgnoreCase);
        }

        #region Graph Construction Methods (Implement During Lab)

        /// <summary>
        /// TODO LAB #1: Add Airport (Vertex) to Graph
        /// 
        /// Add an airport as a vertex in the graph data structure.
        /// Requirements:
        /// - Validate the airport parameter (check for null)
        /// - Validate the airport code (check for null or whitespace)
        /// - Convert airport code to uppercase for consistency
        /// - Add airport to the airports dictionary (avoid duplicates)
        /// - Initialize empty adjacency list in routes dictionary for this airport
        /// 
        /// Key Concepts:
        /// - Vertices in a graph represent entities (airports)
        /// - Dictionary provides O(1) lookup by airport code
        /// - Each vertex needs an adjacency list initialized (even if empty)
        /// - Case-insensitive comparison using ToUpperInvariant()
        /// </summary>
        /// <param name="airport">Airport object to add</param>
        public void AddAirport(Airport airport)
        {
            // TODO LAB: Implement airport addition
            // Hint: Check if airport is null or airport.Code is null/whitespace
            // Hint: Display error message and return if invalid
            // Hint: Convert code to uppercase: airport.Code.ToUpperInvariant()
            // Hint: Check if airports dictionary already contains this code
            // Hint: If not present, add to airports dictionary
            // Hint: Also initialize empty List<Flight> in routes dictionary
            
            throw new NotImplementedException("AddAirport method not yet implemented");
        }

        /// <summary>
        /// TODO LAB #2: Add Flight (Directed Edge) to Graph
        /// 
        /// Add a flight as a directed edge in the graph.
        /// Requirements:
        /// - Validate the flight parameter and its origin/destination
        /// - Convert airport codes to uppercase
        /// - Ensure both origin and destination airports exist (create if needed)
        /// - Add the flight to the origin airport's adjacency list
        /// 
        /// Key Concepts:
        /// - Edges in a graph represent relationships (flights between airports)
        /// - Directed edge: flight goes FROM origin TO destination (one-way)
        /// - Adjacency list: routes[origin] contains all flights FROM that airport
        /// - Auto-create airports if they don't exist (using airportCities mapping)
        /// </summary>
        /// <param name="flight">Flight object to add</param>
        public void AddFlight(Flight flight)
        {
            // TODO LAB: Implement flight addition
            // Hint: Validate flight is not null and both Origin and Destination are not null/whitespace
            // Hint: Convert both airport codes to uppercase
            // Hint: Check if origin airport exists, if not create it:
            //   - Look up city name in airportCities dictionary
            //   - Call AddAirport with new Airport object
            // Hint: Do the same for destination airport
            // Hint: Ensure routes dictionary has a list for origin airport
            // Hint: Add the flight to routes[origin] list
            
            throw new NotImplementedException("AddFlight method not yet implemented");
        }

        /// <summary>
        /// TODO LAB #3: Load Flight Data from CSV File
        /// 
        /// Parse a CSV file and populate the graph with flights.
        /// CSV Format: Origin,Destination,Airline,Duration,Cost
        /// Requirements:
        /// - Check if file exists (throw FileNotFoundException if not)
        /// - Read all lines from the file
        /// - Skip the header row (first line)
        /// - Parse each data row and extract flight information
        /// - Create Flight objects and add them to the graph
        /// - Handle parsing errors gracefully
        /// - Display summary of loaded flights
        /// 
        /// Key Concepts:
        /// - File I/O with File.ReadAllLines()
        /// - CSV parsing with string.Split(',')
        /// - Error handling with try-catch
        /// - Graph construction from external data
        /// </summary>
        /// <param name="filename">Path to the CSV file</param>
        public void LoadFlightsFromCSV(string filename)
        {
            // TODO LAB: Implement CSV loading
            // Hint: Check if File.Exists(filename), if not throw FileNotFoundException
            // Hint: Use File.ReadAllLines(filename) to read all lines
            // Hint: Check if lines array is empty
            // Hint: Create counter variable for flights loaded
            // Hint: Loop from i=1 (skip header) to lines.Length
            // Hint: For each line:
            //   - Trim whitespace
            //   - Skip if empty
            //   - Use try-catch for parsing errors
            //   - Split by comma: line.Split(',')
            //   - Check if parts.Length >= 5
            //   - Extract: origin, destination, airline, duration, cost
            //   - Parse duration as int, cost as decimal
            //   - Create new Flight object
            //   - Call AddFlight(flight)
            //   - Increment counter
            // Hint: Display success message with count
            
            throw new NotImplementedException("LoadFlightsFromCSV method not yet implemented");
        }

        /// <summary>
        /// TODO LAB #4: Display All Airports in Network
        /// 
        /// Display a formatted list of all airports with connection counts.
        /// Requirements:
        /// - Check if there are any airports in the network
        /// - Display a header with total count
        /// - List all airports sorted alphabetically by code
        /// - Show airport code, city, and number of outgoing flights
        /// - Format output for readability
        /// 
        /// Key Concepts:
        /// - Graph traversal (iterating over vertices)
        /// - LINQ OrderBy() for sorting
        /// - String formatting with alignment (-5, -20 for left-align)
        /// - Counting edges (degree) for each vertex
        /// </summary>
        public void DisplayAllAirports()
        {
            // TODO LAB: Implement airport display
            // Hint: Check if airports.Count == 0, display message and return
            // Hint: Display header with count using string interpolation
            // Hint: Use foreach loop over airports.Values.OrderBy(a => a.Code)
            // Hint: For each airport, get connection count from routes dictionary
            // Hint: Use string formatting: {airport.Code,-5} for left-aligned 5 chars
            // Hint: Display: code, city name, and connection count
            
            throw new NotImplementedException("DisplayAllAirports method not yet implemented");
        }

        /// <summary>
        /// TODO LAB #5: Get Airport by Code
        /// 
        /// Retrieve an airport from the graph by its code.
        /// Requirements:
        /// - Validate the code parameter
        /// - Convert code to uppercase for case-insensitive lookup
        /// - Return the Airport object if found
        /// - Return null if code is invalid or airport not found
        /// 
        /// Key Concepts:
        /// - Dictionary lookup provides O(1) retrieval
        /// - Null safety and validation
        /// - Case-insensitive search using ToUpperInvariant()
        /// - Ternary operator for concise conditional return
        /// </summary>
        /// <param name="code">Airport code</param>
        /// <returns>Airport object or null if not found</returns>
        public Airport? GetAirport(string code)
        {
            // TODO LAB: Implement airport retrieval
            // Hint: Check if code is null or whitespace, return null if so
            // Hint: Convert code to uppercase: code.ToUpperInvariant()
            // Hint: Check if airports.ContainsKey(upperCode)
            // Hint: If found, return airports[upperCode], otherwise return null
            // Hint: Can use ternary operator: condition ? valueIfTrue : valueIfFalse
            
            throw new NotImplementedException("GetAirport method not yet implemented");
        }

        #endregion

        #region Basic Search Operations (Student Implementation)

        /// <summary>
        /// TODO #1: Find Direct Flights Between Airports
        /// 
        /// Find all direct flight options between two airports.
        /// Requirements:
        /// - Validate that origin and destination are not null or empty
        /// - Convert airport codes to uppercase for consistent comparison
        /// - Check if the origin airport exists in the routes dictionary
        /// - Filter the flights from origin to find those going to destination
        /// - Return a list of matching Flight objects (empty list if none exist)
        /// 
        /// Key Concepts:
        /// - Adjacency list lookup - routes[origin] gives all outgoing flights
        /// - LINQ Where() for filtering based on destination
        /// - Case-insensitive string comparison
        /// </summary>
        /// <param name="origin">Departure airport code</param>
        /// <param name="destination">Arrival airport code</param>
        /// <returns>List of direct flights, or empty list if none exist</returns>
        public List<Flight> FindDirectFlights(string origin, string destination)
        {
            // TODO ASSIGNMENT: Implement direct flight search
            // Hint: Validate inputs first (check for null/empty strings)
            // Hint: Use ToUpperInvariant() for consistent airport code comparison
            // Hint: Check if routes.ContainsKey(origin) before accessing
            // Hint: Use LINQ .Where() to filter flights by destination
            // Hint: Return empty list if origin doesn't exist or no matches found
            
            throw new NotImplementedException("FindDirectFlights method not yet implemented");
        }

        /// <summary>
        /// TODO #2: Get All Direct Destinations from Airport
        /// 
        /// Get a sorted list of all airports reachable via direct flights from the origin.
        /// Requirements:
        /// - Validate the origin airport code
        /// - Get all flights from the origin airport
        /// - Extract unique destination airport codes
        /// - Sort the destinations alphabetically
        /// - Return the sorted list
        /// 
        /// Key Concepts:
        /// - Adjacency list traversal - examining all edges from a vertex
        /// - LINQ Select() to extract destination codes from Flight objects
        /// - Distinct() to eliminate duplicate destinations
        /// - OrderBy() for alphabetical sorting
        /// </summary>
        /// <param name="origin">Departure airport code</param>
        /// <returns>Sorted list of reachable airport codes</returns>
        public List<string> GetDestinationsFrom(string origin)
        {
            // TODO ASSIGNMENT: Implement destination listing
            // Hint: Similar validation as FindDirectFlights
            // Hint: Use .Select(f => f.Destination) to get destination codes
            // Hint: Use .Distinct() to remove duplicates (multiple flights to same airport)
            // Hint: Use .OrderBy(code => code) for alphabetical sorting
            // Hint: Convert to List with .ToList()
            
            throw new NotImplementedException("GetDestinationsFrom method not yet implemented");
        }

        /// <summary>
        /// TODO #3: Find Cheapest Direct Flight
        /// 
        /// Find the lowest-cost direct flight between two airports.
        /// Requirements:
        /// - Use FindDirectFlights() to get all direct flight options
        /// - Return null if no direct flights exist
        /// - Find and return the flight with the minimum cost
        /// 
        /// Key Concepts:
        /// - Code reuse - leverage existing methods
        /// - LINQ OrderBy() for sorting by cost
        /// - First() to get the minimum element
        /// </summary>
        /// <param name="origin">Departure airport code</param>
        /// <param name="destination">Arrival airport code</param>
        /// <returns>Cheapest flight, or null if no direct flight exists</returns>
        public Flight? FindCheapestDirectFlight(string origin, string destination)
        {
            // TODO ASSIGNMENT: Implement cheapest flight search
            // Hint: Call FindDirectFlights(origin, destination) to get all options
            // Hint: Check if the list is empty and return null if so
            // Hint: Use .OrderBy(f => f.Cost).First() to find minimum cost flight
            // Alternative: Use .MinBy(f => f.Cost) if available in your .NET version
            
            throw new NotImplementedException("FindCheapestDirectFlight method not yet implemented");
        }

        #endregion

        #region BFS Pathfinding (Student Implementation)

        /// <summary>
        /// TODO #4: Find Any Valid Route Using BFS
        /// 
        /// Use breadth-first search to find any valid route between two airports.
        /// Requirements:
        /// - Validate inputs and check that both airports exist in the graph
        /// - Handle special case where origin equals destination
        /// - Implement BFS using a Queue for exploration
        /// - Track visited airports with a HashSet to avoid cycles
        /// - Track parent relationships to reconstruct the path
        /// - Return the path from origin to destination, or null if no route exists
        /// 
        /// Key Concepts:
        /// - BFS explores level-by-level (closest airports first)
        /// - Queue ensures FIFO processing (breadth-first order)
        /// - Parent tracking enables path reconstruction
        /// - HashSet prevents revisiting airports (cycle detection)
        /// 
        /// Algorithm Steps:
        /// 1. Initialize: Queue with origin, visited set, parent dictionary
        /// 2. Loop: Dequeue current airport
        /// 3. Check: If current == destination, reconstruct and return path
        /// 4. Explore: For each outgoing flight from current
        /// 5. Visit: If neighbor unvisited, mark visited, record parent, enqueue
        /// 6. Repeat until queue empty or destination found
        /// </summary>
        /// <param name="origin">Starting airport code</param>
        /// <param name="destination">Ending airport code</param>
        /// <returns>List of airport codes in route order, or null if no route exists</returns>
        public List<string>? FindRoute(string origin, string destination)
        {
            // TODO ASSIGNMENT: Implement BFS pathfinding
            // Hint: Validate inputs (null/empty check)
            // Hint: Convert to uppercase and verify airports exist in graph
            // Hint: Handle special case: if origin == destination, return single-element list
            // Hint: Create Queue<string>, HashSet<string> visited, Dictionary<string, string> parents
            // Hint: Enqueue origin, mark as visited
            // Hint: While loop: while (queue.Count > 0)
            // Hint: Dequeue current airport
            // Hint: Check if current == destination, if so call ReconstructPath() helper
            // Hint: Loop through routes[current] to explore neighbors
            // Hint: For each unvisited neighbor: mark visited, record parent, enqueue
            // Hint: Return null if queue empties without finding destination
            
            throw new NotImplementedException("FindRoute method not yet implemented");
        }

        /// <summary>
        /// TODO #5: Find Shortest Route by Number of Stops
        /// 
        /// Find the route with the fewest number of stops (airports) using BFS.
        /// Requirements:
        /// - BFS naturally finds shortest path in unweighted graphs
        /// - Each edge (flight) has equal weight (one hop)
        /// - Can reuse FindRoute() since BFS guarantees shortest hop-count
        /// 
        /// Key Concepts:
        /// - BFS guarantees shortest path in unweighted graphs
        /// - Level-by-level exploration finds minimum hops automatically
        /// - This is different from FindCheapestRoute which considers edge weights (cost)
        /// </summary>
        /// <param name="origin">Starting airport code</param>
        /// <param name="destination">Ending airport code</param>
        /// <returns>List of airport codes representing shortest route, or null if no route exists</returns>
        public List<string>? FindShortestRoute(string origin, string destination)
        {
            // TODO ASSIGNMENT: Implement shortest route by stops
            // Hint: BFS already finds shortest path by hop count
            // Hint: Simply call and return FindRoute(origin, destination)
            // Note: This method exists to make the distinction clear between
            //       "shortest by stops" (BFS) and "cheapest by cost" (Dijkstra's)
            
            throw new NotImplementedException("FindShortestRoute method not yet implemented");
        }

        #endregion

        #region Dijkstra's Algorithm (Student Implementation)

        /// <summary>
        /// TODO #6: Find Cheapest Route by Total Cost Using Dijkstra's Algorithm
        /// 
        /// Use Dijkstra's algorithm to find the route with the lowest total cost.
        /// Requirements:
        /// - Validate inputs and handle special cases
        /// - Use PriorityQueue to always explore lowest-cost path first
        /// - Track shortest known distance to each airport
        /// - Update distances when shorter path is found (relaxation)
        /// - Track parent relationships for path reconstruction
        /// - Return cheapest path or null if no route exists
        /// 
        /// Key Concepts:
        /// - Dijkstra's finds optimal path in weighted graphs (considers edge costs)
        /// - PriorityQueue ensures we explore minimum-cost paths first
        /// - Distance tracking prevents exploring worse paths
        /// - Relaxation: if (newCost < knownCost) update distance and parent
        /// - Different from BFS which only counts hops (unweighted)
        /// 
        /// Algorithm Steps:
        /// 1. Initialize: All distances to infinity, origin to 0
        /// 2. Create: PriorityQueue, parent dictionary, visited set
        /// 3. Enqueue: origin with cost 0
        /// 4. Loop: While queue not empty
        /// 5. Dequeue: Airport with minimum total cost
        /// 6. Skip: If already visited (duplicate in queue)
        /// 7. Check: If current == destination, reconstruct path
        /// 8. Explore: For each outgoing flight
        /// 9. Calculate: newCost = current distance + flight cost
        /// 10. Relax: If newCost < neighbor distance, update and enqueue
        /// </summary>
        /// <param name="origin">Starting airport code</param>
        /// <param name="destination">Ending airport code</param>
        /// <returns>List of airport codes representing cheapest route, or null if no route exists</returns>
        public List<string>? FindCheapestRoute(string origin, string destination)
        {
            // TODO ASSIGNMENT: Implement Dijkstra's algorithm
            // Hint: Validate inputs similar to FindRoute
            // Hint: Create PriorityQueue<string, decimal> for min-cost extraction
            // Hint: Create Dictionary<string, decimal> for distance tracking
            // Hint: Create Dictionary<string, string> for parent tracking
            // Hint: Create HashSet<string> for visited tracking
            // Hint: Initialize all distances to decimal.MaxValue
            // Hint: Set distances[origin] = 0
            // Hint: Enqueue origin with priority 0
            // Hint: While loop: while (priorityQueue.Count > 0)
            // Hint: Dequeue current airport (minimum cost)
            // Hint: Skip if visited.Contains(current) - avoid reprocessing
            // Hint: Mark current as visited
            // Hint: Check if current == destination, return ReconstructPath if so
            // Hint: Loop through routes[current] for each flight
            // Hint: Calculate newCost = distances[current] + flight.Cost
            // Hint: Relaxation: if (newCost < distances[neighbor])
            // Hint:   Update distances[neighbor] = newCost
            // Hint:   Update parents[neighbor] = current
            // Hint:   Enqueue neighbor with priority newCost
            // Hint: Return null if destination never reached
            
            throw new NotImplementedException("FindCheapestRoute method not yet implemented");
        }

        #endregion

        #region Multi-Criteria Search (Student Implementation)

        /// <summary>
        /// TODO #7: Find All Routes Meeting Constraints (EXTRA CREDIT - Advanced)
        /// 
        /// Find all routes that satisfy both maximum stops and maximum cost constraints.
        /// Requirements:
        /// - Validate inputs (null checks, airport existence)
        /// - Use DFS with backtracking to explore all possible paths
        /// - Prune paths that exceed maxStops or maxCost (optimization)
        /// - Track visited airports to prevent cycles
        /// - Collect all valid routes that reach destination within constraints
        /// - Return list of route lists (each route is list of airport codes)
        /// 
        /// Key Concepts:
        /// - DFS explores deeply before backtracking (vs BFS which explores level-by-level)
        /// - Backtracking: undo choices to explore alternative paths
        /// - Pruning: stop exploring paths that can't possibly succeed
        /// - This finds ALL solutions, not just one optimal solution
        /// 
        /// Algorithm Strategy:
        /// 1. Create result list and validate inputs
        /// 2. Initialize starting path with origin, mark as visited
        /// 3. Call recursive helper method DFSWithConstraints
        /// 4. Helper method:
        ///    - Base case: if at destination, save path copy
        ///    - Prune: if stops >= maxStops, return
        ///    - Explore: for each outgoing flight
        ///    - Calculate: newCost = currentCost + flight.Cost
        ///    - Prune: if newCost > maxCost or neighbor visited, skip
        ///    - Recurse: add neighbor to path, mark visited, call helper
        ///    - Backtrack: remove neighbor from path and visited set
        /// </summary>
        /// <param name="origin">Starting airport code</param>
        /// <param name="destination">Ending airport code</param>
        /// <param name="maxStops">Maximum number of stops allowed</param>
        /// <param name="maxCost">Maximum total cost allowed</param>
        /// <returns>List of valid routes, each route is a list of airport codes</returns>
        public List<List<string>> FindRoutesByCriteria(string origin, string destination, int maxStops, decimal maxCost)
        {
            // TODO ASSIGNMENT (EXTRA CREDIT): Implement constrained route finding
            // Hint: Create empty List<List<string>> validRoutes for results
            // Hint: Validate inputs and return empty list if invalid
            // Hint: Create currentPath = new List<string> { originUpper }
            // Hint: Create visited = new HashSet<string> { originUpper }
            // Hint: Call DFSWithConstraints helper (see below)
            // Hint: Return validRoutes
            
            throw new NotImplementedException("FindRoutesByCriteria method not yet implemented");
        }

        /// <summary>
        /// TODO #7 (continued): Helper Method for DFS with Backtracking
        /// 
        /// Recursive helper that explores all paths within constraints.
        /// This is a private method that implements the core DFS logic.
        /// </summary>
        private void DFSWithConstraints(string current, string destination, int maxStops, decimal maxCost,
            decimal currentCost, List<string> currentPath, HashSet<string> visited, List<List<string>> validRoutes)
        {
            // TODO ASSIGNMENT (EXTRA CREDIT): Implement DFS helper
            // Hint: Base case - check if current == destination
            //   If so, add NEW copy of currentPath: validRoutes.Add(new List<string>(currentPath))
            //   Then return
            // Hint: Pruning - check if currentPath.Count - 1 >= maxStops
            //   If so, return (can't go deeper)
            // Hint: Check if routes.ContainsKey(current)
            // Hint: Loop through each flight in routes[current]
            // Hint: Calculate newCost = currentCost + flight.Cost
            // Hint: Prune if newCost > maxCost OR visited.Contains(neighbor)
            // Hint: Add neighbor to currentPath and visited
            // Hint: Recurse: DFSWithConstraints(neighbor, destination, maxStops, maxCost, newCost, currentPath, visited, validRoutes)
            // Hint: BACKTRACK: Remove last item from currentPath, remove from visited
            
            throw new NotImplementedException("DFSWithConstraints helper not yet implemented");
        }

        #endregion

        #region Network Analysis (Student Implementation)

        /// <summary>
        /// TODO #8: Find Hub Airports (Most Connected)
        /// 
        /// Find the airports with the most outgoing flight connections.
        /// Requirements:
        /// - Calculate the degree (number of outgoing flights) for each airport
        /// - Sort airports by degree in descending order
        /// - Return the top N airport codes
        /// - Handle edge case where topN <= 0
        /// 
        /// Key Concepts:
        /// - Vertex degree: number of edges (flights) from a vertex (airport)
        /// - Hub identification: high-degree vertices are central to network
        /// - LINQ for sorting and limiting results
        /// - In directed graphs, this measures out-degree specifically
        /// </summary>
        /// <param name="topN">Number of top airports to return</param>
        /// <returns>List of airport codes sorted by connection count (descending)</returns>
        public List<string> FindHubAirports(int topN)
        {
            // TODO ASSIGNMENT: Implement hub airport identification
            // Hint: Return empty list if topN <= 0
            // Hint: Use routes dictionary - each airport's Value.Count is its degree
            // Hint: Use LINQ: routes.OrderByDescending(kvp => kvp.Value.Count)
            // Hint: Use .Take(topN) to limit results
            // Hint: Use .Select(kvp => kvp.Key) to extract airport codes
            // Hint: Use .ToList() to convert to list
            
            throw new NotImplementedException("FindHubAirports method not yet implemented");
        }

        /// <summary>
        /// TODO #9: Calculate Comprehensive Network Statistics
        /// 
        /// Calculate and format detailed statistics about the flight network.
        /// Requirements:
        /// - Count total airports and flights
        /// - Calculate average connections per airport
        /// - Find most and least connected airports
        /// - Calculate average flight cost and duration
        /// - Format results in a readable multi-line string
        /// - Handle empty network gracefully
        /// 
        /// Key Concepts:
        /// - Aggregate operations across graph structure
        /// - LINQ for calculations (Sum, Average, Min, Max)
        /// - StringBuilder for efficient string concatenation
        /// - Graph metrics provide insights into network structure
        /// </summary>
        /// <returns>Formatted string with network metrics</returns>
        public string CalculateNetworkStatistics()
        {
            // TODO ASSIGNMENT: Implement network statistics calculation
            // Hint: Return "No airports in the network." if airports.Count == 0
            // Hint: Calculate totalFlights = routes.Values.Sum(flights => flights.Count)
            // Hint: Calculate avgConnections = (double)totalFlights / routes.Count
            // Hint: Find maxConnections = routes.Max(kvp => kvp.Value.Count)
            // Hint: Find mostConnected airports: routes.Where(kvp => kvp.Value.Count == maxConnections)
            // Hint: Find minConnections = routes.Min(kvp => kvp.Value.Count)
            // Hint: Find leastConnected airports: routes.Where(kvp => kvp.Value.Count == minConnections)
            // Hint: Get all flights: routes.Values.SelectMany(flights => flights).ToList()
            // Hint: Calculate avgCost = allFlights.Average(f => f.Cost)
            // Hint: Calculate avgDuration = allFlights.Average(f => f.Duration)
            // Hint: Use StringBuilder to build multi-line output
            // Hint: Format numbers with :F2 for 2 decimal places
            // Hint: Convert duration to hours by dividing by 60
            
            throw new NotImplementedException("CalculateNetworkStatistics method not yet implemented");
        }

        /// <summary>
        /// TODO #10: Find Isolated Airports
        /// 
        /// Find airports that have no incoming or outgoing flights.
        /// Requirements:
        /// - Build set of airports that have incoming flights (destinations)
        /// - Check each airport for both outgoing and incoming connections
        /// - An airport is isolated if it has NEITHER incoming NOR outgoing flights
        /// - Return sorted list of isolated airport codes
        /// 
        /// Key Concepts:
        /// - Graph connectivity analysis
        /// - In-degree vs out-degree in directed graphs
        /// - HashSet for efficient membership testing
        /// - Network health diagnostics
        /// </summary>
        /// <returns>List of isolated airport codes</returns>
        public List<string> FindIsolatedAirports()
        {
            // TODO ASSIGNMENT: Implement isolated airport detection
            // Hint: Create empty List<string> isolated for results
            // Hint: Create HashSet<string> hasIncoming to track airports with incoming flights
            // Hint: Loop through routes.Values (all flight lists)
            // Hint:   Loop through each flight in the list
            // Hint:   Add flight.Destination to hasIncoming set
            // Hint: Loop through airports.Keys to check each airport
            // Hint:   Check hasOutgoing: routes.ContainsKey(code) && routes[code].Count > 0
            // Hint:   Check hasIncomingFlights: hasIncoming.Contains(code)
            // Hint:   If BOTH are false (no outgoing AND no incoming), add to isolated list
            // Hint: Return isolated.OrderBy(code => code).ToList() for sorted output
            
            throw new NotImplementedException("FindIsolatedAirports method not yet implemented");
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Helper method to reconstruct a path from a parent map
        /// Used by BFS and Dijkstra's algorithms
        /// </summary>
        /// <param name="parents">Dictionary mapping each airport to its parent in the path</param>
        /// <param name="start">Starting airport code</param>
        /// <param name="end">Ending airport code</param>
        /// <returns>List of airport codes from start to end</returns>
        protected List<string> ReconstructPath(Dictionary<string, string> parents, string start, string end)
        {
            List<string> path = new List<string>();
            string current = end;

            while (current != start)
            {
                path.Add(current);
                
                if (!parents.ContainsKey(current))
                {
                    // Path reconstruction failed - no route exists
                    return new List<string>();
                }
                
                current = parents[current];
            }

            path.Add(start);
            path.Reverse();
            return path;
        }

        /// <summary>
        /// Gets the total cost of a route by summing flight costs
        /// </summary>
        /// <param name="route">List of airport codes in route order</param>
        /// <returns>Total cost, or -1 if route is invalid</returns>
        public decimal GetRouteCost(List<string> route)
        {
            if (route == null || route.Count < 2)
                return -1;

            decimal totalCost = 0;

            for (int i = 0; i < route.Count - 1; i++)
            {
                string from = route[i];
                string to = route[i + 1];

                Flight? cheapestFlight = FindCheapestDirectFlight(from, to);
                
                if (cheapestFlight == null)
                    return -1; // Invalid route

                totalCost += cheapestFlight.Cost;
            }

            return totalCost;
        }

        /// <summary>
        /// Displays a route with detailed flight information
        /// </summary>
        /// <param name="route">List of airport codes in route order</param>
        public void DisplayRoute(List<string> route)
        {
            if (route == null || route.Count == 0)
            {
                Console.WriteLine("No route to display.");
                return;
            }

            Console.WriteLine($"\nRoute: {string.Join(" → ", route)}");
            Console.WriteLine($"Total stops: {route.Count - 1}");

            if (route.Count < 2)
                return;

            Console.WriteLine("\nFlight Details:");
            decimal totalCost = 0;
            int totalDuration = 0;

            for (int i = 0; i < route.Count - 1; i++)
            {
                string from = route[i];
                string to = route[i + 1];

                Flight? cheapestFlight = FindCheapestDirectFlight(from, to);
                
                if (cheapestFlight != null)
                {
                    Console.WriteLine($"  {i + 1}. {cheapestFlight}");
                    totalCost += cheapestFlight.Cost;
                    totalDuration += cheapestFlight.Duration;
                }
            }

            Console.WriteLine($"\nTotal Cost: ${totalCost:F2}");
            Console.WriteLine($"Total Duration: {totalDuration} minutes ({totalDuration / 60}h {totalDuration % 60}m)");
        }

        #endregion
    }
}
