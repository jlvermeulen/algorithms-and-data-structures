A collection of various algorithms and data structures made in C#

Currently has:

---

# ALGORITHMS

## Graphs
- Minimum cut — calculates the lightest set of edges that would divide the graph in two components should they be removed
- Minimum spanning tree — calculates the lightest tree that connects all the node in the graph using Kruskal's algorithm
- Maximum flow — calculates the maximum amount of flow that can move from the source to the sink
- Shortest path — find the shortest path between to nodes in a graph using Dijkstra's algorithm for weighted graphs and a simple breadth-first search for unweigthed graphs

## Searching
- Binary-Interpolation search — alternating passes of binary search and interpolation search
- Binary search — find an item by repeatedly halving the range it could be in
- Gallop search — search that favours finding elements close to the start of the search, looks for exponentially increasing ranges and then performs binary search on the range that contains the item
- Interpolation search — find an item by interpolation between the upper and lower bound of the search, giving O(log log n) search time for evenly distributed arrays
- Random search — find an item by repeatedly splitting the range it could be in at a random point

## Sorting
- Binary insertion sort — performs a binary search on the sorted part to determine where to place the next element
- Bubble sort — optimised by keeping track of the place of the last swap, everything after that is sorted
- Cocktail sort — one pass of Bubble sort left-to-right, followed by a pass right-to-left
- Comb sort — repeated Bubble sort with decreasing gaps, calls to Bubble sort when the gap reaches 1 for final sorting
- Comb-Insertion sort — same as Comb sort, but calls to Insertion sort for final sorting
- Cycle sort — achieves the theoretical minimum number of writes to the original array
- Gnome sort — optimised by making the gnome remember his position before walking back
- Heapsort — using a binary heap with Floyd's method for constructing a heap in O(n) time
- Insertion sort — no fancy stuff here, just plain old Insertion sort
- JSort — builds a min-heap from the lowest index followed by a max-heap from the highest index, then sorts completely with Insertion sort
- Merge sort — bottom-up merging, requires only one extra array of length n
- Odd-Even sort — swap odd-even pairs when out of order, then swap even-odd pairs
- Patience sort — build stacks with elements in order, then repeatedly extract the value of the stack with the smallest top
- Quicksort — dual-pivot randomized quicksort as proposed by Vladimir Yaroslavskiy
- Selection sort — uses an optimisation that finds the two smallest elements in 3n/2 - 2 comparisons
- Shellsort — repeated Insertion sort over decreasing gaps, using the gap sequence h(n) = ⌈2.25 * h(n - 1)⌉, with h(0) = 1
- Strand sort — looks for sequences of input that are already sorted and merges them with the sorted part of the array
- Timsort — highly adaptive merge sort, builds runs out of data already in order and merges those
- Tree sort — uses an AVL Tree, adds all elements and retrieves them in order

---

# DATASTRUCTURES

## Binary search tree
- AVL tree — height-balanced tree with worst-case O(log n) operations
- Binary search tree — base class for all other BSTs, no balancing, implemented as ICollection&lt;T> and IDictionary&lt;TKey, TValue>
- Red-black tree — height-balanced tree with worst-case O(log n) operations
- Scapegoat tree — weight-balanced tree with O(log n) lookup, amortized O(log n) insertion and deletion
- Splay tree — height-balanced tree with amortized O(log n) operations

## Disjoint-set
- Union Find — implements union by rank and path compression

## Priority queue
- d-ary heap — generalisation of a binary heap with d children, uses Floyd's method for constructing a heap in O(n) time, implements ICollection&lt;T>

## Probabilistic structures
- Bloom filter — test whether an element might be in a set (with a known margin of error), or that it is definitely not in the set, without storing the actual elements
- Skip list — an alternative to balanced BSTs, offers average case O(log n) operations, but worst case O(n)