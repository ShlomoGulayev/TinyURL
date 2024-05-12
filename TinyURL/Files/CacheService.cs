namespace TinyURL.Files;

public class CacheService<TKey, TValue>
{
    private readonly int _capacity;
    private readonly Dictionary<TKey, CacheNode<TKey, TValue>> _cache;
    private readonly LinkedList<CacheNode<TKey, TValue>> _accessOrder;

    public CacheService(int capacity)
    {
        if (capacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than zero.");
        }

        _capacity = capacity;
        _cache = new Dictionary<TKey, CacheNode<TKey, TValue>>(capacity);
        _accessOrder = new LinkedList<CacheNode<TKey, TValue>>();
    }

    public int Count => _cache.Count;

    public bool ContainsKey(TKey key)
    {
        return _cache.ContainsKey(key);
    }

    public void Add(TKey key, TValue value)
    {
        if (_cache.ContainsKey(key))
        {
            throw new ArgumentException($"Key '{key}' already exists in the cache.");
        }

        if (_cache.Count >= _capacity)
        {
            EvictLRU();
        }

        var newNode = new CacheNode<TKey, TValue>(key, value);
        _cache[key] = newNode;
        _accessOrder.AddFirst(newNode);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        if (_cache.TryGetValue(key, out var node))
        {
            MoveNodeToFront(node);
            value = node.Value;
            return true;
        }

        value = default;
        return false;
    }

    private void EvictLRU()
    {
        var lruNode = _accessOrder.Last;
        _cache.Remove(lruNode.Value.Key);
        _accessOrder.RemoveLast();
    }

    private void MoveNodeToFront(CacheNode<TKey, TValue> node)
    {
        _accessOrder.Remove(node);
        _accessOrder.AddFirst(node);
    }

    private class CacheNode<TKey, TValue>
    {
        public TKey Key { get; }
        public TValue Value { get; }

        public CacheNode(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
