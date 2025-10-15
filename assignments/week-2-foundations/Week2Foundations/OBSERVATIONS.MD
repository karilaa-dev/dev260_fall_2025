# 1
My results identical to what was in [COMPLEXITY.MD](./COMPLEXITY.MD). HashSet and Dictionary `Contains` had O(1) complexity, while List had O(n)

# 2
Run time for List was the same for the last item, and item that don't exist.

# 3
For membership checks I would definitely use HashSet or Dictionary (depending on use case). But if I don't have a lot of data, or don't access it often, List is also fine.