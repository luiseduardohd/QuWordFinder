using System;
namespace ChallengeQu
{
	public interface IMatrix<T>
    {
        IEnumerable<T> Rows { get; }
        IEnumerable<T> Columns { get; }

        int RowsCount { get; }
        int ColumnsCount { get; }
    }
}

