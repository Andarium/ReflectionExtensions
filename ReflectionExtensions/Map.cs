// ReSharper disable UnusedMember.Global
// ReSharper disable RedundantNullableDirective

#nullable enable

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ReflectionExtensions
{
    internal sealed class Map<TK1, TK2, TV>
    {
        private readonly Dictionary<TK1, Dictionary<TK2, TV>> _subMaps = new();

        public bool ContainsKey(TK1 key1, TK2 key2)
        {
            return _subMaps.TryGetValue(key1, out var map) && map.ContainsKey(key2);
        }

        public TV this[TK1 key1, TK2 key2]
        {
            get => _subMaps[key1][key2];
            set
            {
                if (!_subMaps.TryGetValue(key1, out var map))
                {
                    map = new Dictionary<TK2, TV>();
                    _subMaps[key1] = map;
                }

                map[key2] = value;
            }
        }

        public bool TryGetValue(TK1 key1, TK2 key2, [MaybeNullWhen(false)] out TV value)
        {
            if (_subMaps.TryGetValue(key1, out var map))
            {
                return map.TryGetValue(key2, out value);
            }

            value = default;
            return false;

        }

        public bool TryAdd(TK1 key1, TK2 key2, TV value)
        {
            if (!_subMaps.TryGetValue(key1, out var map))
            {
                map = new Dictionary<TK2, TV>();
                _subMaps[key1] = map;
            }
            else if (map.ContainsKey(key2))
            {
                return false;
            }

            map[key2] = value;
            return true;
        }

        public void Add(TK1 key1, TK2 key2, TV value)
        {
            if (!_subMaps.TryGetValue(key1, out var map))
            {
                map = new Dictionary<TK2, TV>();
                _subMaps[key1] = map;
            }

            map[key2] = value;
        }

        public void Clear()
        {
            foreach (var value in _subMaps.Values)
            {
                value.Clear();
            }

            _subMaps.Clear();
        }
    }
}