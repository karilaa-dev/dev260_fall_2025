# 📚 Book Catalog — Step 3 Explained: 2D Index for Fast Lookups

## The problem we’re solving

You have **N** book titles in a **sorted array**. If you search with binary search over the **entire** array, each lookup costs **O(log N)** comparisons. That’s already good—but we can often do **even less work** by jumping directly to the **tiny region** of the array where a title can possibly live, then running binary search **only within that small slice**.

Think of it like a phone book: you don’t scan from A to Z; you jump to the **AA..AZ** section first, then scan a handful of entries.

---

## The idea: a tiny 2D “table of contents”

We precompute a **26 × 26** grid (A–Z × A–Z) where each cell stores a **start** and **end** index into the **sorted titles array** for all titles that begin with that **two-letter prefix**.

- `start[L1,L2]` = first index in the sorted array whose normalized title starts with letters `L1L2`
- `end[L1,L2]` = one past the last index for that prefix (so the slice is `[start, end)`)

This is your **multi-dimensional array index**.

Because titles are already **sorted**, all titles that start with `"HA"` (e.g., “Hamlet”, “Happy Potter…”) sit **contiguously** in one block. We just remember the boundaries of that block.

**Memory cost:**  
Two `int[26,26]` arrays = 26×26×2 integers ≈ **1,352 ints** (a few KB).  
Build once, use many times.

---

## What happens at lookup time

1. **Normalize** the user’s query (same rules you used for sorting: uppercase, trimmed, maybe remove leading `THE ` etc.).
2. Take the **first two letters** (handle non-letters as a special case).
3. Convert letters ‘A’..‘Z’ to indices `0..25` (e.g., `'H' => 7`, `'A' => 0`).
4. Fetch `lo = start[h,a]`, `hi = end[h,a]`.
   - If `lo == -1` (or `lo == hi`), there are **no titles** with that prefix → return “no exact match” + **suggestions**.
5. Run **binary search only on `titles[lo .. hi)`**, not the entire array.
   - This is **O(log k)** where `k = hi - lo` (often much smaller than `N`).

**Net effect:** `O(1)` to jump to the right slice + `O(log k)` inside the slice.  
When prefixes are reasonably distributed, `k << N`, so lookups feel **instant**.

---

## How to build the 2D index (one scan)

1. Initialize all `start[r,c] = -1`, `end[r,c] = -1`.
2. Scan the sorted titles **once** from `i = 0..N-1`:
   - Normalize the title; take its first two letters (or treat missing/invalid as a chosen bucket).
   - Map to `(r, c)` in `0..25`.
   - If `start[r,c] == -1`, set `start[r,c] = i` (first time we see this prefix).
   - Always set `end[r,c] = i + 1` (so it ends one past the current match).
3. A
