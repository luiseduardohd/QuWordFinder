using System;
namespace ChallengeQu
{
    public class CharMatrix //: Matrix<char>
    {
        public readonly IEnumerable<IEnumerable<char>> Rows;
        public readonly IEnumerable<IEnumerable<char>> Columns;
        public readonly int rowsCount;
        public readonly int columnsCount;

        // Constructor that receives a list of strings as the matrix
        public CharMatrix(IEnumerable<IEnumerable<char>> matrix)
        {
            var matrixList = matrix.ToList();
            rowsCount = matrixList.Count();
            columnsCount = matrixList[0].Count();

            // Preprocess matrix into horizontal and vertical strings
            //Rows = matrixList;
            Rows = matrix;
            Columns = new List<List<char>>();

            // Convert columns into vertical strings
            for (int col = 0; col < columnsCount; col++)
            {
                char[] verticalWord = new char[rowsCount];
                for (int row = 0; row < rowsCount; row++)
                {
                    verticalWord[row] = matrixList[row][col];
                }
                ((List<string>)Columns).Add(new string(verticalWord));
            }
        }
    }
}

