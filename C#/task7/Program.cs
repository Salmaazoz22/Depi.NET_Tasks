using System;

#region Generic Stack
class MyStack<T>
{
    private T[] items;
    private int top;

    public MyStack(int size)
    {
        items = new T[size];
        top = -1;
    }

    public void Push(T item)
    {
        if (top == items.Length - 1)
        {
            Console.WriteLine("Stack Overflow!");
            return;
        }
        items[++top] = item;
    }

    public T Pop()
    {
        if (IsEmpty())
            throw new Exception("Stack Underflow!");
        return items[top--];
    }

    public T Peek()
    {
        if (IsEmpty())
            throw new Exception("Stack Empty!");
        return items[top];
    }

    public bool IsEmpty()
    {
        return top == -1;
    }

    public int Count()
    {
        return top + 1;
    }

  
    public void PrintAll()
    {
        Console.Write("Stack: ");
        for (int i = 0; i <= top; i++)
        {
            Console.Write(items[i] + " ");
        }
        Console.WriteLine();
    }
}
#endregion

#region Generic List
class MyList<T>
{
    private T[] items;
    private int count;

    public MyList(int size = 10)
    {
        items = new T[size];
        count = 0;
    }

    public void Add(T item)
    {
        if (count == items.Length)
            Resize();
        items[count++] = item;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= count)
            throw new Exception("Invalid Index!");
        for (int i = index; i < count - 1; i++)
            items[i] = items[i + 1];
        count--;
    }

    public bool Contains(T item)
    {
        for (int i = 0; i < count; i++)
            if (items[i].Equals(item)) return true;
        return false;
    }

    public int Count()
    {
        return count;
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= count) throw new Exception("Invalid Index!");
            return items[index];
        }
    }

    private void Resize()
    {
        T[] newItems = new T[items.Length * 2];
        for (int i = 0; i < items.Length; i++)
            newItems[i] = items[i];
        items = newItems;
    }

 
    public void PrintAll()
    {
        Console.Write("List: ");
        for (int i = 0; i < count; i++)
        {
            Console.Write(items[i] + " ");
        }
        Console.WriteLine();
    }
}
#endregion

#region Generic Dictionary
class MyDictionary<K, V>
{
    private K[] keys;
    private V[] values;
    private int count;

    public MyDictionary(int size = 10)
    {
        keys = new K[size];
        values = new V[size];
        count = 0;
    }

    public void Add(K key, V value)
    {
        if (ContainsKey(key))
            throw new Exception("Duplicate key!");

        if (count == keys.Length)
            Resize();

        keys[count] = key;
        values[count] = value;
        count++;
    }

    public bool ContainsKey(K key)
    {
        for (int i = 0; i < count; i++)
            if (keys[i].Equals(key)) return true;
        return false;
    }

    public V GetValue(K key)
    {
        for (int i = 0; i < count; i++)
            if (keys[i].Equals(key)) return values[i];
        throw new Exception("Key not found!");
    }

    private void Resize()
    {
        K[] newKeys = new K[keys.Length * 2];
        V[] newValues = new V[values.Length * 2];
        for (int i = 0; i < keys.Length; i++)
        {
            newKeys[i] = keys[i];
            newValues[i] = values[i];
        }
        keys = newKeys;
        values = newValues;
    }

    public void PrintAll()
    {
        Console.WriteLine("Dictionary:");
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine(keys[i] + " -> " + values[i]);
        }
    }
}
#endregion

#region Sorting
class Sorter
{
    // Delegate way
    public delegate bool Compare(int a, int b);

    public static void SortWithDelegate(int[] arr, Compare cmp)
    {
        for (int i = 0; i < arr.Length - 1; i++)
        {
            for (int j = i + 1; j < arr.Length; j++)
            {
                if (cmp(arr[i], arr[j]))
                {
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }
        }
    }

    // Func way
    public static void SortWithFunc(int[] arr, Func<int, int, bool> cmp)
    {
        for (int i = 0; i < arr.Length - 1; i++)
        {
            for (int j = i + 1; j < arr.Length; j++)
            {
                if (cmp(arr[i], arr[j]))
                {
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }
        }
    }
}
#endregion

class Program
{
    static void Main()
    {
        // Stack
        MyStack<int> stack = new MyStack<int>(5);
        stack.Push(10);
        stack.Push(20);
        stack.Push(30);
        stack.PrintAll();

        // List
        MyList<string> list = new MyList<string>();
        list.Add("Apple");
        list.Add("Banana");
        list.Add("Orange");
        list.PrintAll();

        // Dictionary
        MyDictionary<int, string> dic = new MyDictionary<int, string>();
        dic.Add(1, "One");
        dic.Add(2, "Two");
        dic.Add(3, "Three");
        dic.PrintAll();

        // Sorting
        int[] arr = { 5, 3, 8, 1, 2 };

        Console.WriteLine("\n--- Sorting with Delegate ---");
        Sorter.SortWithDelegate(arr, (a, b) => a > b); // Ascending
        Console.WriteLine("Ascending: " + string.Join(", ", arr));
        Sorter.SortWithDelegate(arr, (a, b) => a < b); // Descending
        Console.WriteLine("Descending: " + string.Join(", ", arr));

        int[] arr2 = { 9, 7, 4, 6, 1 };
        Console.WriteLine("\n--- Sorting with Func ---");
        Sorter.SortWithFunc(arr2, (a, b) => a > b);
        Console.WriteLine("Ascending: " + string.Join(", ", arr2));
        Sorter.SortWithFunc(arr2, (a, b) => a < b);
        Console.WriteLine("Descending: " + string.Join(", ", arr2));
    }
}
