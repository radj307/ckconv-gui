using ImportedWPF.Collections;
using PropertyChanged;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.Specialized;
using System.ComponentModel;

namespace ckconv_gui.Collections
{
    [DoNotNotify]
    public class EnumList<T> : IList, ICollection, IEnumerable, IList<T>, IImmutableList<T>, ICollection<T>, IEnumerable<T>, IReadOnlyList<T>, IReadOnlyCollection<T>, INotifyCollectionChanged, INotifyPropertyChanged where T : struct, Enum
    {
        public object? this[int index] { get => ((IList)this.Items)[index]; set => ((IList)this.Items)[index] = value; }
        T IList<T>.this[int index] { get => ((IList<T>)this.Items)[index]; set => ((IList<T>)this.Items)[index] = value; }

        T IReadOnlyList<T>.this[int index] => ((IReadOnlyList<T>)this.Items)[index];

        public ObservableImmutableList<T> Items { get; } = new ObservableImmutableList<T>(Enum.GetValues<T>());

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
        public void Add(T item) => ((ICollection<T>)this.Items).Add(item);
        public IImmutableList<T> AddRange(IEnumerable<T> items) => ((IImmutableList<T>)this.Items).AddRange(items);
        public void Clear() => ((IList)this.Items).Clear();
        public bool Contains(object? value) => ((IList)this.Items).Contains(value);
        public bool Contains(T item) => ((ICollection<T>)this.Items).Contains(item);
        public void CopyTo(Array array, int index) => ((ICollection)this.Items).CopyTo(array, index);
        public void CopyTo(T[] array, int arrayIndex) => ((ICollection<T>)this.Items).CopyTo(array, arrayIndex);
        public IEnumerator GetEnumerator() => ((IEnumerable)this.Items).GetEnumerator();
        public int IndexOf(object? value) => ((IList)this.Items).IndexOf(value);
        public int IndexOf(T item) => ((IList<T>)this.Items).IndexOf(item);
        public int IndexOf(T item, int index, int count, IEqualityComparer<T>? equalityComparer) => ((IImmutableList<T>)this.Items).IndexOf(item, index, count, equalityComparer);
        public void Insert(int index, object? value) => ((IList)this.Items).Insert(index, value);
        public void Insert(int index, T item) => ((IList<T>)this.Items).Insert(index, item);
        public IImmutableList<T> InsertRange(int index, IEnumerable<T> items) => ((IImmutableList<T>)this.Items).InsertRange(index, items);
        public int LastIndexOf(T item, int index, int count, IEqualityComparer<T>? equalityComparer) => ((IImmutableList<T>)this.Items).LastIndexOf(item, index, count, equalityComparer);
        public void Remove(object? value) => ((IList)this.Items).Remove(value);
        public bool Remove(T item) => ((ICollection<T>)this.Items).Remove(item);
        public IImmutableList<T> Remove(T value, IEqualityComparer<T>? equalityComparer) => ((IImmutableList<T>)this.Items).Remove(value, equalityComparer);
        public IImmutableList<T> RemoveAll(Predicate<T> match) => ((IImmutableList<T>)this.Items).RemoveAll(match);
        public void RemoveAt(int index) => ((IList)this.Items).RemoveAt(index);
        public IImmutableList<T> RemoveRange(IEnumerable<T> items, IEqualityComparer<T>? equalityComparer) => ((IImmutableList<T>)this.Items).RemoveRange(items, equalityComparer);
        public IImmutableList<T> RemoveRange(int index, int count) => ((IImmutableList<T>)this.Items).RemoveRange(index, count);
        public IImmutableList<T> Replace(T oldValue, T newValue, IEqualityComparer<T>? equalityComparer) => ((IImmutableList<T>)this.Items).Replace(oldValue, newValue, equalityComparer);
        public IImmutableList<T> SetItem(int index, T value) => ((IImmutableList<T>)this.Items).SetItem(index, value);
        IImmutableList<T> IImmutableList<T>.Add(T value) => ((IImmutableList<T>)this.Items).Add(value);
        IImmutableList<T> IImmutableList<T>.Clear() => ((IImmutableList<T>)this.Items).Clear();
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => ((IEnumerable<T>)this.Items).GetEnumerator();
        IImmutableList<T> IImmutableList<T>.Insert(int index, T element) => ((IImmutableList<T>)this.Items).Insert(index, element);
        IImmutableList<T> IImmutableList<T>.RemoveAt(int index) => ((IImmutableList<T>)this.Items).RemoveAt(index);
    }
}
