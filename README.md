A collection of various algorithms and datastructures made in C#

Currently has:

---

# ALGORITHMS

## Sorting
- Bubble sort — optimised by keeping track of the place of the last swap, everything after that is sorted
- Cocktail sort — one pass of Bubble sort left-to-right, followed by a pass right-to-left
- Comb sort — repeated Bubble sort with decreasing gaps, calls to Bubble sort when the gap reaches 1 for final sorting
- Comb-Insertion sort — same as Comb sort, but calls to Insertion sort for final sorting
- Gnome sort — optimised by making the gnome remember his position before walking back
- Heapsort — using a binary heap with Floyd's method for constructing a heap in O(n) time
- Insertion sort — no fancy stuff here, just plain old Insertion sort
- Merge sort — bottom-up merging, requires only one extra array of length n
- Odd-Even sort — swap odd-even pairs when out of order, then swap even-odd pairs
- Quicksort — dual-pivot randomized quicksort as proposed by Vladimir Yaroslavskiy
- Selection sort — uses an optimisation that finds the two smallest elements in 3n/2 - 2 comparisons
- Shellsort — repeated Insertion sort over decreasing gaps, using the gap sequence h(n) = ⌈2.25 * h(n - 1)⌉, with h(0) = 1
- Tree sort — uses an AVL Tree, adds all elements and retrieves them in order

---

# DATASTRUCTURES

## Binary search tree
- AVL tree — height-balanced tree with guaranteed O(log n) operations
- Binary search tree — base class for all other BSTs, no balancing, implements ICollection&lt;T>
- Splay tree — height-balanced tree with amortized O(log n) operations

## Disjoint-set
- Union Find — implements union by rank and path compression

## Priority queue
- Binary heap — uses Floyd's method for constructing a heap in O(n) time