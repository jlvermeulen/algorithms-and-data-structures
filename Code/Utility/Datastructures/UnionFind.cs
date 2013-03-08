namespace Utility
{
    public static class UnionFind<T>
    {
        public static UnionNode<T> Make(T value)
        {
            UnionNode<T> node = new UnionNode<T>();
            node.Value = value;
            node.Parent = node;
            node.Rank = 0;
            return node;
        }

        public static UnionNode<T> Find(UnionNode<T> node)
        {
            if (node.Parent != node)
                node.Parent = Find(node.Parent);
            return node.Parent;
        }

        public static void Union(UnionNode<T> node1, UnionNode<T> node2)
        {
            UnionNode<T> root1 = Find(node1), root2 = Find(node2);
            if (root1 == root2)
                return;

            if (root1.Rank > root2.Rank)
                root2.Parent = root1;
            else if (root2.Rank > root1.Rank)
                root1.Parent = root2;
            else
            {
                root2.Parent = root1;
                root1.Rank++;
            }
        }
    }

    class UnionNode<T>
    {
        public T Value { get; set; }
        public UnionNode<T> Parent { get; set; }
        public int Rank { get; set; }
    }
}