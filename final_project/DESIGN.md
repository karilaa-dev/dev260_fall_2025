# Project Design & Rationale

**Instructions:** Replace prompts with your content. Be specific and concise. If something doesn't apply, write "N/A" and explain briefly.

---

## Data Model & Entities

**Core entities:**  
_List your main entities with key fields, identifiers, and relationships (1–2 lines each)._

**Your Answer:**

**Entity A:**

- Name: `PantryItem`
- Key fields: `Name`, `Quantity`, `Unit`, `Category`, `ExpirationDate`, `LowStockThreshold`, `DateAdded`
- Identifiers: `Name` (string, case-insensitive; this is the "primary key" for the pantry)
- Relationships: Standalone item stored in a `Dictionary<string, PantryItem>`

**Entity B (if applicable):**

- Name: `Recipe`
- Key fields: `Name`, `Ingredients` (`List<string>`), `Description`
- Identifiers: Index position in the recipes `List` (1-based in the console menu)
- Relationships: Each ingredient is basically a pantry item name string, so I can validate against the pantry `Dictionary`

**Identifiers (keys) and why they're chosen:**  
_Explain your choice of keys (e.g., string Id, composite key, case-insensitive, etc.)._

**Your Answer:**

I used the item `Name` as the key for `PantryItem` because that's how people actually think about pantry stuff ("do I have eggs?"). It's unique enough for a home pantry, and with `StringComparer.OrdinalIgnoreCase` I don't get duplicates like "Eggs" vs "eggs". It also keeps lookups fast (O(1) average) with a `Dictionary`.

---

## Data Structures — Choices & Justification

_List only the meaningful data structures you chose. For each, state the purpose, the role it plays in your app, why it fits, and alternatives considered._

### Structure #1

**Chosen Data Structure:**  
_Name the data structure (e.g., Dictionary<string, Customer>)._

**Your Answer:**

`Dictionary<string, PantryItem>` with `StringComparer.OrdinalIgnoreCase`

**Purpose / Role in App:**  
_What user action or feature does it power?_

**Your Answer:**

Powers all pantry inventory operations: Add item, View all items, Search item, Update quantity, Remove item, Low stock checks, Expiration checks.

**Why it fits:**  
_Explain access patterns, typical size, performance/Big-O, memory, simplicity._

**Your Answer:**

- The main thing the app does is "find item by name", so a `Dictionary` is the best fit
- Typical size is small (like 50-200 items), but even if it grows, lookups stay fast
- Performance: O(1) average for add/search/update/delete (`Add`, `TryGetValue`, `Remove`)
- It's built-in and straightforward, so I didn't need anything fancy

**Alternatives considered:**  
_List alternatives (e.g., List<T>, SortedDictionary, custom tree) and why you didn't choose them._

**Your Answer:**

- `List<PantryItem>`: I'd have to do O(n) searches all the time, which is annoying since lookups happen constantly
- `SortedDictionary<string, PantryItem>`: Keeps keys sorted, but I don't need that for the core feature and it costs O(log n)
- Custom tree/BST: Too much complexity for basically no gain at this scale

---

### Structure #2

**Chosen Data Structure:**  
_Name the data structure._

**Your Answer:**

`HashSet<string>` with `StringComparer.OrdinalIgnoreCase`

**Purpose / Role in App:**  
_What user action or feature does it power?_

**Your Answer:**

Powers the Smart Shopping List feature: Add to list (with duplicate prevention), View list, Remove from list, Clear list.

**Why it fits:**  
_Explain access patterns, typical size, performance/Big-O, memory, simplicity._

**Your Answer:**

- The shopping list is basically "a set of unique names"
- I add items, check duplicates, and remove items when they're bought
- Performance: O(1) average for `Add`/`Contains`/`Remove`, and `Add()` already tells me if it was a duplicate
- It only stores strings, so it's super lightweight

**Alternatives considered:**  
_List alternatives and why you didn't choose them._

**Your Answer:**

- `List<string>`: Would require an O(n) `Contains` check before every add to avoid duplicates
- `Dictionary<string, bool>`: Works, but it's too complex in comparison to `HashSet`
- `SortedSet<string>`: Sorting isn't worth paying O(log n); I can sort only when displaying if I want

---

### Structure #3

**Chosen Data Structure:**  
_Name the data structure._

**Your Answer:**

`List<Recipe>`

**Purpose / Role in App:**  
_What user action or feature does it power?_

**Your Answer:**

Stores predefined recipes for the Recipe Validator feature: Display available recipes, Check ingredient availability, Add missing ingredients to shopping list.

**Why it fits:**  
_Explain access patterns, typical size, performance/Big-O, memory, simplicity._

**Your Answer:**

- Users just scroll a numbered menu and pick a recipe, so a `List` fits that perfectly
- It's a small, fixed set (5-20 recipes)
- Performance: O(n) to display the menu, O(1) to grab a recipe by index
- Simple is better here since there's no real need for name-based lookup

**Alternatives considered:**  
_List alternatives and why you didn't choose them._

**Your Answer:**

- `Dictionary<string, Recipe>`: I don't really need name lookups since selection is by number
- `Array`: Too rigid if I want to add more recipes later
- `SortedList`: No real benefit because I'm not sorting recipes by anything

---

### Additional Structures (if applicable)

_Add more sections if you used additional structures like Queue for workflows, Stack for undo, HashSet for uniqueness, Graph for relationships, BST/SortedDictionary for ordered views, etc._

**Your Answer:**

Inside each `Recipe`, I store ingredients as a `List<string>` because it's just a short, ordered list of names. Then I can loop through it and check each one against the pantry `Dictionary` (O(1) per lookup), and it's also easy to use LINQ to figure out what's missing.

---

## Comparers & String Handling

**Comparer choices:**  
_Explain what comparers you used and why (e.g., StringComparer.OrdinalIgnoreCase for keys)._

**Your Answer:**

**For keys:**

I used `StringComparer.OrdinalIgnoreCase` for both the `Dictionary` and the `HashSet`. Basically means "Eggs" and "eggs" are the same thing.

**For display sorting (if different):**

For sorting names in display, I also used `StringComparer.OrdinalIgnoreCase` in `OrderBy` so the output looks consistent.

**Normalization rules:**  
_Describe how you normalize strings (trim whitespace, collapse duplicates, canonicalize casing)._

**Your Answer:**

I normalize user input by trimming it (`input.Trim()`), mainly so "Eggs" and "Eggs " don't become different keys. I don't force everything to lowercase because the case-insensitive comparer already handles that, and it lets me keep whatever casing the user typed for display.

**Bad key examples avoided:**  
_List examples of bad key choices and why you avoided them (e.g., non-unique names, culture-varying text, trailing spaces, substrings that can change)._

**Your Answer:**

- Trailing spaces: I trim input so "Eggs" and "Eggs " don't end up as two different items
- Case differences: handled by `OrdinalIgnoreCase`
- Empty/whitespace-only names: rejected up front (validation)
- Culture-specific comparisons: avoided by using Ordinal instead of `CurrentCulture`

---

## Performance Considerations

**Expected data scale:**  
_Describe the expected size of your data (e.g., 100 items, 10,000 items)._

**Your Answer:**

- Pantry: 50-200 items (typical home kitchen)
- Shopping List: 10-30 items per shopping trip
- Recipes: 5-20 predefined recipes

**Performance testing with two input sizes:**

| Operation | 100 Items | 5,000 Items | Expected | Observed |
|-----------|-----------|-------------|----------|----------|
| Add item | Instant | Instant | O(1) | Matches - hash insertion is constant |
| Search by name | Instant | Instant | O(1) | Matches - direct hash lookup |
| View all (sorted) | Instant | ~50ms | O(n log n) | Matches - sorting dominates at scale |
| Low stock scan | Instant | ~20ms | O(n) | Matches - linear scan of all items |
| Expiring soon scan | Instant | ~25ms | O(n) | Matches - linear with date comparison |
| Recipe check (5 ingredients) | Instant | Instant | O(m) where m=5 | Matches - 5 O(1) lookups |

**Observations:**
- At ~100 items (realistic pantry), everything feels instant
- At 5,000 items (just stress testing), the O(1) stuff is still instant and the O(n) scans/sorts are still fine (~20-50ms)
- Sorting for "View All" is the slowest at big sizes, but that's expected since sorting dominates

**Performance bottlenecks identified:**  
_List any potential performance issues and how you addressed them._

**Your Answer:**

- `GetLowStockItems()`: O(n) scan of the pantry - totally fine for <200 items
- `GetExpiringSoonItems()`: O(n) scan with date comparisons - also fine at this scale
- Recipe check: O(m) where m = ingredients (usually 5-10), and each ingredient lookup is O(1) in the pantry `Dictionary`

For the realistic size, there aren't any real bottlenecks. If this ever had to scale to thousands of items, I'd probably add extra indexing.

**Big-O analysis of core operations:**  
_Provide time complexity for your main operations (Add, Search, List, Update, Delete)._

**Your Answer:**

- Add: O(1) average (Dictionary.Add, HashSet.Add)
- Search: O(1) average (Dictionary.TryGetValue)
- List: O(n log n) for sorted display (LINQ OrderBy)
- Update: O(1) average (Dictionary.TryGetValue + property set)
- Delete: O(1) average (Dictionary.Remove, HashSet.Remove)

---

## Design Tradeoffs & Decisions

**Key design decisions:**  
_Explain major design choices and why you made them._

**Your Answer:**

1. **Separation of concerns**: I kept the data logic in `PantryTracker` and the console/menu UI in `PantryNavigator`. That made it way easier to work on features without everything being mixed together.

2. **Case-insensitive keys**: I went for user-friendliness. Nobody wants the app to act like "Eggs" and "eggs" are different things.

3. **Predefined recipes**: I kept recipes predefined/read-only for the MVP so the main focus stays on pantry + shopping list features.

4. **In-memory storage**: No persistence was mostly a scope/time decision. If I wanted persistence next, I'd serialize the same structures to JSON.

**Tradeoffs made:**  
_Describe any tradeoffs between simplicity vs performance, memory vs speed, etc._

**Your Answer:**

- **Simplicity over extra features**: I skipped custom recipe creation so the UI and flow stay clean
- **Memory for speed**: `Dictionary` uses more overhead than a plain list/array, but O(1) lookups are worth it
- **Nullable `ExpirationDate`**: Real pantry items don't always have a date, but it does mean some null checks

**What you would do differently with more time:**  
_Reflect on what you might change or improve._

**Your Answer:**

- Add JSON file persistence so the data doesn't reset every run
- Add partial name matching (fuzzy-ish search)
- Let users create/save custom recipes
- Track quantities in recipes (like "2 eggs" not just "eggs")
- Add undo using a `Stack`
