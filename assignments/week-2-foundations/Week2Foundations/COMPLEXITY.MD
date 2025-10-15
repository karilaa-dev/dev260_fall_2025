| Structure | Operation | Big-O (Avg) | One-sentence rationale |
|-----------|-----------|-------------|-------------------------|
| Array | Access by index | O(1) | Direct indexing computes address in constant time. |
| Array | Search (unsorted) | O(n) | Must scan each element until found or end. |
| List<T> | Add at end | O(1) amortized | Appends are constant except when resizing underlying array (which happens rarely due to capacity doubling). |
| List<T> | Insert at index | O(n) | Elements after the index shift right one position on average. |
| Stack<T> | Push / Pop / Peek | O(1) | Operates only on the top element (array or node pointer adjustment). |
| Queue<T> | Enqueue / Dequeue / Peek | O(1) | Circular array or linked pointers adjusted at head/tail only. |
| Dictionary<K,V> | Add / Lookup / Remove | O(1) | Hashing maps key to bucket; average constant-time assuming good hash distribution. |
| HashSet<T> | Add / Contains / Remove | O(1) | Hash-based structure with constant-time bucket operations on average. |

