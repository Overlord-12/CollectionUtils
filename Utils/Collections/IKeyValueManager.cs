namespace Utils.Collections
{
    /// <summary>
    /// It provides an abstraction for working with a collection that supports key-value data storage and management.
    /// </summary>
    /// <typeparam name="TValue">The type of values in the manager</typeparam>
    /// <typeparam name="TKey">The key</typeparam>
    public interface IKeyValueManager<in TKey, TValue>
    {
        /// <summary>
        /// Tries to get a value by key
        /// </summary>
        bool TryGetValue(TKey key, out TValue value);

        /// <summary>
        /// Adds a value to the manager by key
        /// </summary>
        /// <param name="key">The key to associate with the value</param>
        /// <param name="value">The value to add to the manager</param>
        void Add(TKey key, TValue value);

        /// <summary>
        /// Removes the value.
        /// </summary>
        /// <param name="key">The key.</param>
        void Remove(TKey key);

        /// <summary>
        /// Clears all items from the manage
        /// </summary>
        void Clear();
    }
}
