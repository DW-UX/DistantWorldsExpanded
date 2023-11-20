using System.Collections.Generic;

namespace DistantWorlds.Types;

public readonly struct ReverseComparer<T> : IComparer<T> {

  public static ReverseComparer<T> Default = new(Comparer<T>.Default);

  private readonly IComparer<T> _comparer;

  public ReverseComparer(IComparer<T> comparer)
    => _comparer = comparer;

  public int Compare(T x, T y)
    => _comparer.Compare(y, x) * -1;

}