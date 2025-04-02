namespace Utils.Collections
{
    /// <summary>
    /// Represents a node in the tree
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class TreeNode<TKey, TValue>
    {
        /// <summary>
        /// Gets the children.
        /// </summary>
        public IDictionary<TKey, TreeNode<TKey, TValue>> Children { get; } = new Dictionary<TKey, TreeNode<TKey, TValue>>();

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public TValue Value { get; set; }
    }
}
