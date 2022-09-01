namespace TypeExtensions
{
    /// <summary>
    /// Extension methods for <see cref="object"/> types.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Checks if <paramref name="obj"/> is equal to any of the given <paramref name="other"/> objects.
        /// </summary>
        /// <param name="obj">An object instance.</param>
        /// <param name="other">Enumerable object instance(s).</param>
        /// <returns><see langword="true"/> when <paramref name="obj"/> is equal to any of the given comparison objects; otherwise <see langword="false"/></returns>
        public static bool EqualsAny(this object? obj, IEnumerable<object?> other)
        {
            foreach (object? o in other)
                if (obj == o)
                    return true;
            return false;
        }
        /// <summary>
        /// Checks if <paramref name="obj"/> is equal to any of the given <paramref name="other"/> objects.
        /// </summary>
        /// <param name="obj">An object instance.</param>
        /// <param name="other">Any number of object instances to compare.</param>
        /// <returns><see langword="true"/> when <paramref name="obj"/> is equal to any of the given comparison objects; otherwise <see langword="false"/></returns>
        public static bool EqualsAny(this object? obj, params object?[] other)
        {
            foreach (object? o in other)
                if (obj == o)
                    return true;
            return false;
        }
    }
}
