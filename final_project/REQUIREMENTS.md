# Core Requirements (What You Must Deliver)

## 1) Original Application
- Console or minimal-UI app in C#/.NET.
- Performs a meaningful task (not just method demos or toy snippets).
- Must be distinct from labs/assignments used in this course.

## 2) Data Structures (≥ 3 total)
- At least 1 linear (e.g., List<T>, array, LinkedList<T>, Queue<T>, Stack<T>).
- At least 1 non-linear (e.g., Dictionary<TKey,TValue>, HashSet<T>, Tree/BST, Graph).
- Use .NET collections and/or your own implementations where appropriate.
- In docs, explain why each DS was chosen (trade-offs, Big-O for key ops, expected data sizes).

## 3) User Menu / UX
- Clear, easy-to-navigate menu (text UI is fine).
- Supports: create/add, view/search, update, delete, and quit.
- Include input validation and graceful error handling (no crashes on bad input).

## 4) Documentation (Short & Focused; place in your repo)
README.md — How to run + STUDY_NOTES section (your understanding & reflections).
Include:
- What I built (1–2 paragraphs in plain language)
- Why these data structures (brief rationale & key operations)
- Complexity notes (expected Big-O for your core operations)
- Manual testing summary (how you verified behavior; 3–5 scenarios)
- What I’d do next (future improvements, edge cases to handle)

DESIGN.md — Technical details:
- Data model, data structures used, key operations & trade-offs
- Any comparers (e.g., StringComparer.OrdinalIgnoreCase) and why
- Performance expectations (two input sizes; note slow paths)
- File format if you persist data (optional)

## 5) Presentation (Extra Credit, Optional)
- + up to 20 pts: Live finals week presentation or a 5–10 min recorded demo (OK to replay in class is optional).
- Cover: problem, demo, DS choices, performance notes, lessons learned.

# Scope Guidance (How to Plan & Grow Your Project)
Start small. Ship a thin slice. Iterate. Aim to get a minimal vertical slice working early (menu → one data structure → one feature end-to-end), then add complexity incrementally.

## Recommended Iteration Path

### MVP (Day 1–2):
- One entity type (e.g., Item, Book, Task)
- Store in one DS (e.g., Dictionary<string, Item>)
- Implement Add, List, Quit

### Core Utilities (Day 2–3):
- Add Search (by key or predicate)
- Add Update/Delete + input validation
- Add a second DS that optimizes a common action (e.g., HashSet for uniqueness or tags; List<T> for recent items)

### Data Structure Depth (Day 3–4):
- Add a third DS (e.g., SortedDictionary for ordered views; Queue/Stack for workflows; simple BST for custom ordering; tiny Graph for relationships)

### Performance & Polish (Day 4–5):
- Try two input sizes (e.g., ~100 vs ~5,000)
- Note observed behavior vs Big-O expectations (brief notes in DESIGN.md)
- Add helpful menu prompts, error messages, and “Stats” screen

## Scope Targets

### Bronze (baseline):
- Single-user console app
- CRUD + search
- 3 data structures used thoughtfully
- README.md with STUDY_NOTES + DESIGN.md

### Silver (strong):
- Adds indexing, caching, or a custom DS
- Basic persistence (file/JSON)
- Clear performance notes (two input sizes; brief timing or observation)

### Gold (ambitious):
- Adds a non-trivial DS/algorithm (tree/graph ops, custom comparer, range queries)
- Robust input datasets
- Measured comparisons of two approaches or data structures (and short interpretation)

> Tip: If you feel behind, de-scope features, not quality: keep the menu small but solid; keep data structures correct; document real trade-offs.

## Suggested Project Ideas (or propose your own)
- Campus Directory — SortedDictionary or BST for A–Z; Dictionary for fast lookup; HashSet to prevent duplicates.
- Study Planner — graph for prerequisites; PriorityQueue/SortedSet for scheduling; Queue for today’s tasks.
- Recipe Manager — Dictionary keyed by recipe; HashSet for ingredients; small graph for substitutions; tag/range queries.
- Media Library — Dictionary by title/ID; List<T> for playlists; HashSet for favorites; optional BST for sorted export.
- Choose something different from our labs/assignments.

## Milestones & Suggested Timeline
- This Week (Pitch due in Canvas): 1 paragraph — problem, intended data structures, target scope (Bronze/Silver/Gold).

### Week 11 (Open Lab):
- Milestone 1: Menu skeleton + one core DS works end-to-end.
- Milestone 2: All DS integrated; core workflows complete; performance notes drafted.
- Finals Week: Code freeze; docs complete; submission + (optional) presentation.
