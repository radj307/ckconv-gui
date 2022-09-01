using ImportedWPF.Collections;
using PropertyChanged;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.Specialized;
using System.ComponentModel;

namespace ckconv_gui.Measurement
{
    [DoNotNotify]
    public class TwoWayConversionList : IList, ICollection, IEnumerable, IList<TwoWayConversion>, IImmutableList<TwoWayConversion>, ICollection<TwoWayConversion>, IEnumerable<TwoWayConversion>, IReadOnlyList<TwoWayConversion>, IReadOnlyCollection<TwoWayConversion>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        public object? this[int index] { get => ((IList)this.Items)[index]; set => ((IList)this.Items)[index] = value; }
        TwoWayConversion IList<TwoWayConversion>.this[int index] { get => ((IList<TwoWayConversion>)this.Items)[index]; set => ((IList<TwoWayConversion>)this.Items)[index] = value; }

        TwoWayConversion IReadOnlyList<TwoWayConversion>.this[int index] => ((IReadOnlyList<TwoWayConversion>)this.Items)[index];

        public ObservableImmutableList<TwoWayConversion> Items { get; set; } = new();

        public bool IsFixedSize => ((IList)this.Items).IsFixedSize;

        public bool IsReadOnly => ((IList)this.Items).IsReadOnly;

        public int Count => ((ICollection)this.Items).Count;

        public bool IsSynchronized => ((ICollection)this.Items).IsSynchronized;

        public object SyncRoot => ((ICollection)this.Items).SyncRoot;

        public event NotifyCollectionChangedEventHandler? CollectionChanged
        {
            add => this.Items.CollectionChanged += value;
            remove => this.Items.CollectionChanged -= value;
        }

        public event PropertyChangedEventHandler? PropertyChanged
        {
            add => this.Items.PropertyChanged += value;
            remove => this.Items.PropertyChanged -= value;
        }

        public int Add(object? value) => ((IList)this.Items).Add(value);
        public void Add(TwoWayConversion item) => ((ICollection<TwoWayConversion>)this.Items).Add(item);
        public IImmutableList<TwoWayConversion> AddRange(IEnumerable<TwoWayConversion> items) => ((IImmutableList<TwoWayConversion>)this.Items).AddRange(items);
        public void Clear() => ((IList)this.Items).Clear();
        public bool Contains(object? value) => ((IList)this.Items).Contains(value);
        public bool Contains(TwoWayConversion item) => ((ICollection<TwoWayConversion>)this.Items).Contains(item);
        public void CopyTo(Array array, int index) => ((ICollection)this.Items).CopyTo(array, index);
        public void CopyTo(TwoWayConversion[] array, int arrayIndex) => ((ICollection<TwoWayConversion>)this.Items).CopyTo(array, arrayIndex);
        public IEnumerator GetEnumerator() => ((IEnumerable)this.Items).GetEnumerator();
        public int IndexOf(object? value) => ((IList)this.Items).IndexOf(value);
        public int IndexOf(TwoWayConversion item) => ((IList<TwoWayConversion>)this.Items).IndexOf(item);
        public int IndexOf(TwoWayConversion item, int index, int count, IEqualityComparer<TwoWayConversion>? equalityComparer) => ((IImmutableList<TwoWayConversion>)this.Items).IndexOf(item, index, count, equalityComparer);
        public void Insert(int index, object? value) => ((IList)this.Items).Insert(index, value);
        public void Insert(int index, TwoWayConversion item) => ((IList<TwoWayConversion>)this.Items).Insert(index, item);
        public IImmutableList<TwoWayConversion> InsertRange(int index, IEnumerable<TwoWayConversion> items) => ((IImmutableList<TwoWayConversion>)this.Items).InsertRange(index, items);
        public int LastIndexOf(TwoWayConversion item, int index, int count, IEqualityComparer<TwoWayConversion>? equalityComparer) => ((IImmutableList<TwoWayConversion>)this.Items).LastIndexOf(item, index, count, equalityComparer);
        public void Remove(object? value) => ((IList)this.Items).Remove(value);
        public bool Remove(TwoWayConversion item) => ((ICollection<TwoWayConversion>)this.Items).Remove(item);
        public IImmutableList<TwoWayConversion> Remove(TwoWayConversion value, IEqualityComparer<TwoWayConversion>? equalityComparer) => ((IImmutableList<TwoWayConversion>)this.Items).Remove(value, equalityComparer);
        public IImmutableList<TwoWayConversion> RemoveAll(Predicate<TwoWayConversion> match) => ((IImmutableList<TwoWayConversion>)this.Items).RemoveAll(match);
        public void RemoveAt(int index) => ((IList)this.Items).RemoveAt(index);
        public IImmutableList<TwoWayConversion> RemoveRange(IEnumerable<TwoWayConversion> items, IEqualityComparer<TwoWayConversion>? equalityComparer) => ((IImmutableList<TwoWayConversion>)this.Items).RemoveRange(items, equalityComparer);
        public IImmutableList<TwoWayConversion> RemoveRange(int index, int count) => ((IImmutableList<TwoWayConversion>)this.Items).RemoveRange(index, count);
        public IImmutableList<TwoWayConversion> Replace(TwoWayConversion oldValue, TwoWayConversion newValue, IEqualityComparer<TwoWayConversion>? equalityComparer) => ((IImmutableList<TwoWayConversion>)this.Items).Replace(oldValue, newValue, equalityComparer);
        public IImmutableList<TwoWayConversion> SetItem(int index, TwoWayConversion value) => ((IImmutableList<TwoWayConversion>)this.Items).SetItem(index, value);
        IImmutableList<TwoWayConversion> IImmutableList<TwoWayConversion>.Add(TwoWayConversion value) => ((IImmutableList<TwoWayConversion>)this.Items).Add(value);
        IImmutableList<TwoWayConversion> IImmutableList<TwoWayConversion>.Clear() => ((IImmutableList<TwoWayConversion>)this.Items).Clear();
        IEnumerator<TwoWayConversion> IEnumerable<TwoWayConversion>.GetEnumerator() => ((IEnumerable<TwoWayConversion>)this.Items).GetEnumerator();
        IImmutableList<TwoWayConversion> IImmutableList<TwoWayConversion>.Insert(int index, TwoWayConversion element) => ((IImmutableList<TwoWayConversion>)this.Items).Insert(index, element);
        IImmutableList<TwoWayConversion> IImmutableList<TwoWayConversion>.RemoveAt(int index) => ((IImmutableList<TwoWayConversion>)this.Items).RemoveAt(index);
    }
}
