namespace Mladim.Client.Extensions;

public static class IEnumrableExtensions
{

    public static IEnumerable<(T element1, K element2)> Merge<T, K>(this IEnumerable<T> sequence1, IEnumerable<K> sequence2)
    {
        var enumSeq1 = sequence1.GetEnumerator();
        var enumSeq2 = sequence2.GetEnumerator();

        do
        {
            yield return (enumSeq1.Current, enumSeq2.Current);

        } while (enumSeq1.MoveNext() && enumSeq2.MoveNext());
    }
}
