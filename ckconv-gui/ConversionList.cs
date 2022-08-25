using ImportedWPF.Collections;
using PropertyChanged;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.Specialized;
using System.ComponentModel;

namespace ckconv_gui
{
    [DoNotNotify]
    public class ConversionList : IList, ICollection, IEnumerable, IList<Conversion>, IImmutableList<Conversion>, ICollection<Conversion>, IEnumerable<Conversion>, IReadOnlyList<Conversion>, IReadOnlyCollection<Conversion>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        public object? this[int index] { get => ((IList)this.Items)[index]; set => ((IList)this.Items)[index] = value; }
        Conversion IList<Conversion>.this[int index] { get => ((IList<Conversion>)this.Items)[index]; set => ((IList<Conversion>)this.Items)[index] = value; }

        Conversion IReadOnlyList<Conversion>.this[int index] => ((IReadOnlyList<Conversion>)this.Items)[index];

        public ObservableImmutableList<Conversion> Items { get; set; } = new();

        public bool IsFixedSize => ((IList)this.Items).IsFixedSize;

        public bool IsReadOnly => ((IList)this.Items).IsReadOnly;

        public int Count => ((ICollection)this.Items).Count;

        public bool IsSynchronized => ((ICollection)this.Items).IsSynchronized;

        public object SyncRoot => ((ICollection)this.Items).SyncRoot;

        public event NotifyCollectionChangedEventHandler? CollectionChanged
        {
            add
            {
                ((INotifyCollectionChanged)this.Items).CollectionChanged += value;
            }

            remove
            {
                ((INotifyCollectionChanged)this.Items).CollectionChanged -= value;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged
        {
            add
            {
                ((INotifyPropertyChanged)this.Items).PropertyChanged += value;
            }

            remove
            {
                ((INotifyPropertyChanged)this.Items).PropertyChanged -= value;
            }
        }

        public int Add(object? value) => ((IList)this.Items).Add(value);
        public void Add(Conversion item) => ((ICollection<Conversion>)this.Items).Add(item);
        public IImmutableList<Conversion> AddRange(IEnumerable<Conversion> items) => ((IImmutableList<Conversion>)this.Items).AddRange(items);
        public void Clear() => ((IList)this.Items).Clear();
        public bool Contains(object? value) => ((IList)this.Items).Contains(value);
        public bool Contains(Conversion item) => ((ICollection<Conversion>)this.Items).Contains(item);
        public void CopyTo(Array array, int index) => ((ICollection)this.Items).CopyTo(array, index);
        public void CopyTo(Conversion[] array, int arrayIndex) => ((ICollection<Conversion>)this.Items).CopyTo(array, arrayIndex);
        public IEnumerator GetEnumerator() => ((IEnumerable)this.Items).GetEnumerator();
        public int IndexOf(object? value) => ((IList)this.Items).IndexOf(value);
        public int IndexOf(Conversion item) => ((IList<Conversion>)this.Items).IndexOf(item);
        public int IndexOf(Conversion item, int index, int count, IEqualityComparer<Conversion>? equalityComparer) => ((IImmutableList<Conversion>)this.Items).IndexOf(item, index, count, equalityComparer);
        public void Insert(int index, object? value) => ((IList)this.Items).Insert(index, value);
        public void Insert(int index, Conversion item) => ((IList<Conversion>)this.Items).Insert(index, item);
        public IImmutableList<Conversion> InsertRange(int index, IEnumerable<Conversion> items) => ((IImmutableList<Conversion>)this.Items).InsertRange(index, items);
        public int LastIndexOf(Conversion item, int index, int count, IEqualityComparer<Conversion>? equalityComparer) => ((IImmutableList<Conversion>)this.Items).LastIndexOf(item, index, count, equalityComparer);
        public void Remove(object? value) => ((IList)this.Items).Remove(value);
        public bool Remove(Conversion item) => ((ICollection<Conversion>)this.Items).Remove(item);
        public IImmutableList<Conversion> Remove(Conversion value, IEqualityComparer<Conversion>? equalityComparer) => ((IImmutableList<Conversion>)this.Items).Remove(value, equalityComparer);
        public IImmutableList<Conversion> RemoveAll(Predicate<Conversion> match) => ((IImmutableList<Conversion>)this.Items).RemoveAll(match);
        public void RemoveAt(int index) => ((IList)this.Items).RemoveAt(index);
        public IImmutableList<Conversion> RemoveRange(IEnumerable<Conversion> items, IEqualityComparer<Conversion>? equalityComparer) => ((IImmutableList<Conversion>)this.Items).RemoveRange(items, equalityComparer);
        public IImmutableList<Conversion> RemoveRange(int index, int count) => ((IImmutableList<Conversion>)this.Items).RemoveRange(index, count);
        public IImmutableList<Conversion> Replace(Conversion oldValue, Conversion newValue, IEqualityComparer<Conversion>? equalityComparer) => ((IImmutableList<Conversion>)this.Items).Replace(oldValue, newValue, equalityComparer);
        public IImmutableList<Conversion> SetItem(int index, Conversion value) => ((IImmutableList<Conversion>)this.Items).SetItem(index, value);
        IImmutableList<Conversion> IImmutableList<Conversion>.Add(Conversion value) => ((IImmutableList<Conversion>)this.Items).Add(value);
        IImmutableList<Conversion> IImmutableList<Conversion>.Clear() => ((IImmutableList<Conversion>)this.Items).Clear();
        IEnumerator<Conversion> IEnumerable<Conversion>.GetEnumerator() => ((IEnumerable<Conversion>)this.Items).GetEnumerator();
        IImmutableList<Conversion> IImmutableList<Conversion>.Insert(int index, Conversion element) => ((IImmutableList<Conversion>)this.Items).Insert(index, element);
        IImmutableList<Conversion> IImmutableList<Conversion>.RemoveAt(int index) => ((IImmutableList<Conversion>)this.Items).RemoveAt(index);
    }
}
