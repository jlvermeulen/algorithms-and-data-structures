using System;
using System.Text;

namespace Utility
{
    namespace Algorithms
    {
        namespace ComputationalGeometry
        {
            public class Matrix
            {
                private double[,] elems;

                public Matrix(int size) { this.elems = new double[size, size]; }

                public Matrix(int rows, int columns) { this.elems = new double[rows, columns]; }

                public Matrix(Matrix matrix) { this.elems = new double[matrix.Rows, matrix.Columns]; Array.Copy(matrix.elems, this.elems, matrix.elems.Length); }

                public double this[int row, int column] { get { return elems[row, column]; } set { elems[row, column] = value; } }

                public int Rows { get { return this.elems.GetLength(0); } }

                public int Columns { get { return this.elems.GetLength(1); } }

                public bool IsSquare { get { return this.Rows == this.Columns; } }

                public double Determinant
                {
                    get
                    {
                        if (!this.IsSquare)
                            throw new NotSupportedException("The determinant is only defined for square matrices.");

                        int signP;
                        Matrix l, u, p;
                        this.LUPDecomposition(out l, out u, out p, out signP);
                        double d = 1;
                        for (int i = 0; i < l.Rows; i++)
                            d *= l[i, i] * u[i, i];

                        return signP * d;
                    }
                }

                public bool Invertible { get { return this.IsSquare && this.Determinant != 0; } }

                public void Invert()
                {
                    if (!this.IsSquare)
                        throw new NotSupportedException("Only square matrices can be inverted.");

                    this.Augment(Matrix.CreateIdentity(this.Rows));
                    this.ToReducedRowEchelonForm();

                    if (this.Submatrix(0, 0, this.Rows, this.Rows) != Matrix.CreateIdentity(this.Rows))
                        throw new NotSupportedException("This matrix cannot be inverted.");

                    Matrix matrix = this.Submatrix(0, this.Rows, this.Rows, this.Columns);
                    this.elems = matrix.elems;
                }

                public void Invert2()
                {
                    if (!this.IsSquare)
                        throw new NotSupportedException("Only square matrices can be inverted.");

                    Matrix l, u, p;
                    this.LUPDecomposition(out l, out u, out p);

                    Matrix inverse = new Matrix(this.Rows);

                    double[] idCol;
                    for (int i = 0; i < this.Columns; i++)
                    {
                        idCol = new double[this.Rows];
                        for (int j = 0; j < this.Rows; j++)
                            idCol[j] = p[j, i];

                        double[] invCol = ForwardSubstitution(l, idCol);
                        invCol = BackwardSubstitution(u, invCol);

                        for (int j = 0; j < this.Rows; j++)
                            inverse[j, i] = invCol[j];
                    }

                    for (int i = 0; i < this.Rows; i++)
                        for (int j = 0; j < this.Columns; j++)
                            this[i, j] = inverse[i, j];
                }

                public void Transpose()
                {
                    double[,] newElems = new double[this.Columns, this.Rows];
                    for (int i = 0; i < this.Rows; i++)
                        for (int j = 0; j < this.Columns; j++)
                            newElems[j, i] = this.elems[i, j];

                    this.elems = newElems;
                }

                public double Cofactor(int row, int column)
                {
                    if (!this.IsSquare)
                        throw new NotSupportedException("The cofactor is only defined for square matrices.");

                    Matrix minor = new Matrix(this.Rows - 1);
                    int rOff = 0, cOff = 0;
                    for (int r = 0; r < minor.Rows; r++)
                    {
                        if (r == row)
                            rOff = 1;

                        for (int c = 0; c < minor.Columns; c++)
                        {
                            if (c == column)
                                cOff = 1;

                            minor[r, c] = this[r + rOff, c + cOff];
                        }

                        cOff = 0;
                    }

                    return Math.Pow(-1, row + column) * minor.Determinant;
                }

                public void Augment(Matrix matrix)
                {
                    if (matrix.Rows != this.Rows)
                        throw new ArgumentException("Both matrices must have the same number of rows.");

                    double[,] newElems = new double[this.Rows, this.Columns + matrix.Columns];
                    for (int row = 0; row < this.Rows; row++)
                        for (int column = 0; column < newElems.GetLength(1); column++)
                        {
                            if (column < this.Columns)
                                newElems[row, column] = this[row, column];
                            else
                                newElems[row, column] = matrix[row, column - this.Columns];
                        }

                    this.elems = newElems;
                }

                public Matrix Submatrix(int startRow, int startColumn, int endRow, int endColumn)
                {
                    if (endRow <= startRow || endRow > this.Rows)
                        throw new ArgumentException("endRow must be larger than startRow and no larger than the total number of rows in this matrix.");

                    if (endColumn <= startColumn || endColumn > this.Columns)
                        throw new ArgumentException("endColumn must be larger than startColumn and no larger than the total number of rows in this matrix.");

                    Matrix matrix = new Matrix(endRow - startRow, endColumn - startColumn);
                    for (int i = startRow; i < endRow; i++)
                        for (int j = startColumn; j < endColumn; j++)
                            matrix[i - startRow, j - startColumn] = this[i, j];

                    return matrix;
                }

                public void ToReducedRowEchelonForm()
                {
                    int lead = 0;
                    for (int i = 0; i < this.Rows; i++)
                    {
                        if (lead > this.Columns)
                            break;

                        int k = i;
                        while (this[k, lead] == 0)
                        {
                            k++;
                            if (k == this.Rows)
                            {
                                k = i;
                                lead++;
                                if (this.Columns == lead)
                                {
                                    lead--;
                                    break;
                                }
                            }
                        }

                        this.SwapRows(i, k);

                        double div = this[i, lead];
                        for (int j = 0; j < this.Columns; j++)
                            this[i, j] /= div;

                        for (int j = 0; j < this.Rows; j++)
                            if (j != i)
                            {
                                double sub = this[j, lead];
                                for (int l = 0; l < this.Columns; l++)
                                    this[j, l] -= (sub * this[i, l]);
                            }

                        lead++;
                    }
                }

                public void LUPDecomposition(out Matrix l, out Matrix u, out Matrix p) { int signP; this.LUPDecomposition(out l, out u, out p, out signP); }

                private void LUPDecomposition(out Matrix l, out Matrix u, out Matrix p, out int signP)
                {
                    if (!this.IsSquare)
                        throw new NotSupportedException("LU decomposition is only possible for square matrices.");

                    Matrix a = new Matrix(this);

                    l = Matrix.CreateIdentity(a.Rows);
                    u = new Matrix(a.Rows);
                    p = Matrix.CreateIdentity(a.Rows);

                    int swaps = 0;
                    for (int j = 0; j < a.Rows; j++)
                    {
                        int maxRow = j;
                        for (int i = j; i < a.Rows; i++)
                            if (Math.Abs(a[i, j]) > Math.Abs(a[maxRow, j]))
                                maxRow = i;

                        if (maxRow != j)
                        {
                            p.SwapRows(maxRow, j);
                            swaps++;
                        }
                    }

                    Matrix a2 = p * a;
                    for (int j = 0; j < a.Rows; j++)
                        for (int i = 0; i < a.Rows; i++)
                        {
                            double val;
                            if (i <= j)
                            {
                                val = 0;
                                for (int k = 0; k < i; k++)
                                    val += l[i, k] * u[k, j];
                                u[i, j] = a2[i, j] - val;
                            }

                            if (i >= j)
                            {
                                val = 0;
                                for (int k = 0; k < i; k++)
                                    val += l[i, k] * u[k, j];
                                l[i, j] = (a2[i, j] - val) / u[j, j];
                            }
                        }

                    signP = swaps % 2 == 0 ? 1 : -1;
                }

                private static double[] ForwardSubstitution(Matrix matrix, double[] vector)
                {
                    double[] result = new double[vector.Length];

                    for (int i = 0; i < vector.Length; i++)
                    {
                        result[i] = vector[i];
                        for (int j = 0; j < i; j++)
                            result[i] -= matrix[i, j] * result[j];
                        result[i] /= matrix[i, i];
                    }

                    return result;
                }

                private static double[] BackwardSubstitution(Matrix matrix, double[] vector)
                {
                    double[] result = new double[vector.Length];

                    for (int i = vector.Length - 1; i >= 0; i--)
                    {
                        result[i] = vector[i];
                        for (int j = i + 1; j < vector.Length; j++)
                            result[i] -= matrix[i, j] * result[j];
                        result[i] /= matrix[i, i];
                    }

                    return result;
                }

                public static Matrix CreateIdentity(int size)
                {
                    Matrix matrix = new Matrix(size, size);
                    for (int i = 0; i < size; i++)
                        matrix[i, i] = 1;

                    return matrix;
                }

                public static Matrix Transpose(Matrix matrix)
                {
                    Matrix result = new Matrix(matrix);
                    result.Transpose();
                    return result;
                }

                public static Matrix Invert(Matrix matrix)
                {
                    Matrix result = new Matrix(matrix);
                    result.Invert();
                    return result;
                }

                public static Matrix Invert2(Matrix matrix)
                {
                    Matrix result = new Matrix(matrix);
                    result.Invert2();
                    return result;
                }

                public static Matrix operator *(Matrix left, Matrix right)
                {
                    if (left.Columns != right.Rows)
                        throw new ArgumentException("Number of columns in the left operand must be equal to the number of rows in the right operand.");

                    Matrix result = new Matrix(left.Rows, right.Columns);

                    for (int i = 0; i < result.Rows; i++)
                        for (int j = 0; j < result.Columns; j++)
                            for (int k = 0; k < left.Columns; k++)
                                result[i, j] += left[i, k] * right[k, j];

                    return result;
                }

                public static Matrix operator *(double left, Matrix right)
                {
                    Matrix matrix = new Matrix(right.Rows, right.Columns);

                    for (int i = 0; i < right.Rows; i++)
                        for (int j = 0; j < right.Columns; j++)
                            matrix[i, j] = left * right[i, j];

                    return matrix;
                }

                public static Matrix operator +(Matrix left, Matrix right)
                {
                    if (left.Columns != right.Columns || left.Rows != right.Rows)
                        throw new ArgumentException("Dimensions of the left and right operands must be the same.");

                    Matrix result = new Matrix(left.Rows, left.Columns);

                    for (int i = 0; i < left.Rows; i++)
                        for (int j = 0; j < left.Columns; j++)
                            result[i, j] = left[i, j] + right[i, j];

                    return result;
                }

                public static Matrix operator -(Matrix left, Matrix right)
                {
                    if (left.Columns != right.Columns || left.Rows != right.Rows)
                        throw new ArgumentException("Dimensions of the left and right operands must be the same.");

                    Matrix result = new Matrix(left.Rows, left.Columns);

                    for (int i = 0; i < left.Rows; i++)
                        for (int j = 0; j < left.Columns; j++)
                            result[i, j] = left[i, j] - right[i, j];

                    return result;
                }

                public static bool operator ==(Matrix left, Matrix right)
                {
                    if (left.Columns != right.Columns || left.Rows != right.Rows)
                        return false;

                    for (int row = 0; row < left.Rows; row++)
                        for (int column = 0; column < left.Columns; column++)
                            if (left[row, column] != right[row, column])
                                return false;

                    return true;
                }

                public static bool operator !=(Matrix left, Matrix right) { return !(left == right); }

                public override string ToString()
                {
                    StringBuilder builder = new StringBuilder();
                    int[] pad = new int[this.Columns];
                    for (int column = 0; column < this.Columns; column++)
                        for (int row = 0; row < this.Rows; row++)
                            pad[column] = Math.Max(pad[column], Math.Max(1, DoubleToString(this[row, column]).Length));

                    int size, pre;
                    for (int row = 0; row < this.Rows; row++)
                    {
                        for (int column = 0; column < this.Columns; column++)
                        {
                            size = Math.Max(1, DoubleToString(this[row, column]).Length);
                            pre = (pad[column] - size) / 2;

                            for (int i = 0; i < pre; i++)
                                builder.Append(' ');

                            builder.Append(DoubleToString(this[row, column]));

                            for (int i = pre + size; i < pad[column]; i++)
                                builder.Append(' ');
                            
                            builder.Append(' ');
                        }

                        builder.AppendLine();
                    }

                    return builder.ToString();
                }

                private void SwapRows(int row1, int row2)
                {
                    double[,] temp = new double[this.Columns, 1];
                    Array.Copy(this.elems, row1 * this.Columns, temp, 0, this.Columns);
                    Array.Copy(this.elems, row2 * this.Columns, this.elems, row1 * this.Columns, this.Columns);
                    Array.Copy(temp, 0, this.elems, row2 * this.Columns, this.Columns);
                }

                private static string DoubleToString(double value) { return value.ToString("F10").TrimEnd(new char[] { '0' }).TrimEnd(new char[] { '.' }); }
            }
        }
    }
}