A collection of various algorithms and datastructures made in C#

Currently has:

---

# ALGORITHMS

## Sorting
- Binary Insertion sort — performs a binary search on the sorted part to determine where to place the next element
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
- Tree sort — uses an AVL Tree, adds all elements and retrieves them in order

---

# DATASTRUCTURES

## Binary search tree
- AVL tree — height-balanced tree with worst-case O(log n) operations
- Binary search tree — base class for all other BSTs, no balancing, implements ICollection&lt;T>
- Red-black tree — height-balanced tree with worst-case O(log n) operations
- Scapegoat tree — weight-balanced tree with O(log n) lookup, amortized O(log n) insertion and deletion
- Splay tree — height-balanced tree with amortized O(log n) operations

## Disjoint-set
- Union Find — implements union by rank and path compression

## Priority queue
- d-ary heap — generalisation of a binary heap with d children, uses Floyd's method for constructing a heap in O(n) time, implements ICollection&lt;T>

## Probabilistic structures
- Skip list — an alternative to balanced BSTs, offers average case O(log n) operations, but worst case O(n)