namespace Utils.Collections
{
    /// <summary>
    /// Represents a tree structure where values can be added using a chain of keys.
    /// For example, with keys "source" and "path", the tree would look like: source -> path = value.
    /// </summary>
    /// <typeparam name="TKey">The type of keys used in the tree.</typeparam>
    /// <typeparam name="TValue">The type of values stored in the tree.</typeparam>
    public class MultipleKeyTree<TKey, TValue> : IKeyValueManager<TKey[], TValue>, IKeyValueManager<TKey, TValue>
    {
        private readonly TreeNode<TKey, TValue> m_RootNode;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleKeyTree{TKey, TValue}"/> class.
        /// </summary>
        public MultipleKeyTree()
        {
            m_RootNode = new TreeNode<TKey, TValue>();
        }

        /// <summary>
        /// Adds a value to the tree using a chain of keys.
        /// The value will be associated with the last key in the chain.
        /// </summary>
        /// <param name="keys">The chain of keys to navigate to the value.</param>
        /// <param name="value">The value to add.</param>
        /// <exception cref="ArgumentNullException">Thrown when keys is null.</exception>
        /// <exception cref="ArgumentException">Thrown when keys is empty.</exception>
        public void Add(TKey[] keys, TValue value)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            if (keys.Length == 0)
            {
                throw new ArgumentException("At least one key must be provided.", nameof(keys));
            }

            TreeNode<TKey, TValue> currentNode = m_RootNode;
            for (int i = 0; i < keys.Length - 1; i++)
            {
                TKey currentKey = keys[i];
                if (currentKey == null)
                {
                    throw new ArgumentNullException(nameof(keys), $"Key at index {i} is not specified.");
                }

                if (!currentNode.Children.TryGetValue(currentKey, out var nextNode))
                {
                    nextNode = new TreeNode<TKey, TValue>();
                    currentNode.Children[currentKey] = nextNode;
                }

                currentNode = nextNode;
            }

            TKey lastKey = keys[keys.Length - 1];
            if (lastKey == null)
            {
                throw new ArgumentNullException(nameof(keys), "Last key is not specified.");
            }

            if (!currentNode.Children.TryGetValue(lastKey, out var valueNode))
            {
                valueNode = new TreeNode<TKey, TValue>();
                currentNode.Children[lastKey] = valueNode;
            }

            valueNode.Value = value;
        }

        /// <summary>
        /// Adds a value to the tree using a single key.
        /// </summary>
        /// <param name="key">The key to associate with the value.</param>
        /// <param name="value">The value to add.</param>
        void IKeyValueManager<TKey, TValue>.Add(TKey key, TValue value)
        {
            Add(new[] { key }, value);
        }

        /// <summary>
        /// Tries to get a value from the tree using a chain of keys.
        /// </summary>
        /// <param name="keys">The chain of keys to navigate to the value.</param>
        /// <param name="value">The value.</param>
        /// <returns>True if the value was found, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown when keys is null.</exception>
        /// <exception cref="ArgumentException">Thrown when keys is empty.</exception>
        public bool TryGetValue(TKey[] keys, out TValue value)
        {
            value = default;
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            if (keys.Length == 0)
            {
                throw new ArgumentException("At least one key must be provided.", nameof(keys));
            }

            TreeNode<TKey, TValue> node = GetNodeForKeys(keys);
            if (node == null)
            {
                return false;
            }

            value = node.Value;
            return true;
        }

        /// <summary>
        /// Tries to get a value from the tree using a single key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>True if the value was found, false otherwise.</returns>
        bool IKeyValueManager<TKey, TValue>.TryGetValue(TKey key, out TValue value)
        {
            return TryGetValue([key], out value);
        }

        /// <summary>
        /// Removes a value from the tree using a chain of keys.
        /// </summary>
        /// <param name="keys">The chain of keys to navigate to the value.</param>
        /// <exception cref="ArgumentNullException">Thrown when keys is null.</exception>
        /// <exception cref="ArgumentException">Thrown when keys is empty.</exception>
        public void Remove(TKey[] keys)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            if (keys.Length == 0)
            {
                throw new ArgumentException("At least one key must be provided.", nameof(keys));
            }

            TKey[] penultimateKeys = keys.Take(keys.Length - 1).ToArray();
            TreeNode<TKey, TValue> currentNode = GetNodeForKeys(penultimateKeys);
            TKey lastKey = keys[keys.Length - 1];
            if (lastKey == null)
            {
                return;
            }

            currentNode.Children.Remove(lastKey);
        }

        /// <summary>
        /// Removes a value from the tree using a single key.
        /// </summary>
        /// <param name="key">The key.</param>
        void IKeyValueManager<TKey, TValue>.Remove(TKey key)
        {
            Remove([key]);
        }

        /// <summary>
        /// Gets the node for the specified keys.
        /// </summary>
        /// <param name="keys">The keys to navigate through.</param>
        /// <returns>The node at the specified path, or null if not found.</returns>
        private TreeNode<TKey, TValue> GetNodeForKeys(TKey[] keys)
        {
            TreeNode<TKey, TValue> currentNode = m_RootNode;
            foreach (TKey key in keys)
            {
                if (key == null)
                {
                    return null;
                }

                if (!currentNode.Children.TryGetValue(key, out var nextNode))
                {
                    return null;
                }

                currentNode = nextNode;
            }

            return currentNode;
        }

        /// <summary>
        /// Clears all values from the tree.
        /// </summary>
        public void Clear()
        {
            m_RootNode.Children.Clear();
        }
    }
}
