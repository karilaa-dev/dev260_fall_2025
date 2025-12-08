# Assignment 10: Flight Route Network Navigator - Implementation Notes

**Name:** Kyryl Andreiev

## Graph Data Structure Understanding

**How adjacency list representation works for flight networks:**

Answer: Dictionary<string, List<Flight>> gives O(1) airport lookups by code. Stores only existing connections, not all possible pairs like a matrix would. For 16 airports with 52 flights, this uses way less memory than a 16x16 matrix (256 cells vs 52 entries).

**Difference between BFS and Dijkstra's algorithms:**

Answer: BFS finds shortest path by number of stops (unweighted), using a regular queue. Dijkstra's finds cheapest path by cost (weighted), using a priority queue to always explore the lowest-cost option first. BFS is simpler but ignores edge weights.

## Challenges and Solutions

**Biggest challenge faced:**

Answer: Path reconstruction from parent maps - working backwards from destination to origin was confusing at first.

**How you solved it:**

Answer: Drew small graph examples on paper, traced through the parent map manually, then realized I needed to reverse the list at the end.

**Most confusing concept:**

Answer: Understanding why Dijkstra's priority queue needs to track cumulative costs and how the relaxation step updates distances.

## Algorithm Implementation Details

**BFS Implementation (FindRoute and FindShortestRoute):**

Answer: Used Queue for FIFO traversal, HashSet to track visited nodes, Dictionary for parent tracking. Enqueue origin, dequeue and check neighbors, mark visited, record parents. BFS guarantees shortest path because it explores level-by-level.

**Dijkstra's Implementation (FindCheapestRoute):**

Answer: PriorityQueue<string, decimal> orders by cumulative cost. Track distances in Dictionary, initialize origin to 0. For each node, check neighbors, calculate new cost, update if cheaper (relaxation). Priority queue ensures we always process cheapest path first.

**Path Reconstruction Logic:**

Answer: Start at destination, follow parent map backwards to origin. Build list in reverse, then reverse it to get origin→destination order. Handle null check if no path exists.

## Code Quality

**What you're most proud of in your implementation:**

Answer: Clean separation between BFS and Dijkstra's, and the network statistics methods that efficiently analyze hub airports and connectivity.

**What you would improve if you had more time:**

Answer: Add caching for frequently-queried routes, implement A* search with heuristics, better error messages for invalid inputs.

## Real-World Applications

**How this relates to actual routing systems:**

Answer: Same concepts as Google Flights (cheapest vs fastest routes), GPS navigation (shortest vs fastest path), network routing (packet paths), social networks (friend suggestions via graph traversal).

**What you learned about graph algorithms:**

Answer: Different algorithms optimize for different goals. BFS is simple and fast for unweighted graphs. Dijkstra's handles weighted graphs efficiently. Adjacency lists are perfect for sparse graphs.

## Testing and Verification

**Test cases you created:**

Answer: Tested LAX→JFK (multiple routes), SEA→MIA (long distance), SFO→SFO (same airport), LAX→XXX (invalid airport), checked that cheapest ≠ shortest routes.

**Interesting findings from testing:**

Answer: Cheapest routes often have more stops. Hub airports like ORD and DFW appear in many optimal paths. Some direct flights cost more than multi-stop routes.

## Optional Challenge

Answer: Implemented FindRoutesByCriteria with DFS, max stops constraint, and budget limit. Used recursive backtracking to explore all paths within constraints.

## Time Spent

**Total time:** 6 hours

**Breakdown:**

- Understanding graph concepts and assignment requirements: 0.5 hours
- Implementing basic search operations (TODO #1-3): 0.5 hours
- Implementing BFS pathfinding (TODO #4-5): 1.5 hours
- Implementing Dijkstra's algorithm (TODO #6): 2 hours
- Implementing network analysis (TODO #8-10): 1 hour
- Testing with flights.csv and edge cases: 0.5 hours
- Debugging graph traversal algorithms: 0.5 hours
- Writing these notes: 0.25 hours

**Most time-consuming part:** Dijkstra's algorithm - understanding priority queue mechanics and ensuring the relaxation step worked correctly.

## Key Takeaways

**Most important lesson learned:**

Answer: Choosing the right algorithm depends on what you're optimizing for. BFS for fewest hops, Dijkstra's for lowest cost - same problem, different solutions.

**How this changed your understanding of data structures:**

Answer: Graphs model relationships between entities, unlike linear structures (arrays/lists) or hierarchical ones (trees). They're more flexible and represent real-world networks naturally.
