﻿using System;

namespace NumericalMethods
{
    public enum VectorType { Empty, Identity, Zero }
    public enum PrintType { Horizontal, Vertical }

    public class Vector
    {
        public const double MaxValue = 10.0;

        internal Vector(int size) => DeclareVectorValues(size);

        public Vector(int size, VectorType type)
        {
            DeclareVectorValues(size);
            switch (type)
            {
                case VectorType.Empty:
                    SetVectorValues(double.NaN);
                    break;
                case VectorType.Identity:
                    SetVectorValues(1.0);
                    break;
                case VectorType.Zero:
                    SetVectorValues(0.0);
                    break;
            }
        }

        public Vector(string path)
        {
            if (!FilesController.SetVectorFromFile(vector: this, path))
            {
                VectorValues = null;
                Size = 0;
            }
        }

        internal double[] VectorValues { private get; set; }

        internal int Size { get; set; }

        internal double this[int index]
        {
            get
            {
                if (IndexInsideVector(index))
                    return VectorValues[index - 1];
                else
                    return double.NaN;
            }
            set
            {
                if (IndexInsideVector(index))
                    VectorValues[index - 1] = value;
            }
        }

        private bool IndexInsideVector(int index)
        {
            return index >= 1 && index <= Size;
        }

        private void DeclareVectorValues(int size)
        {
            Size = size;
            VectorValues = new double[Size];
        }

        private void SetVectorValues(double value)
        {
            for (int index = 1; index <= Size; index++)
            {
                this[index] = value;
            }
        }

        internal Vector Copy()
        {
            Vector vectorCopy = new Vector(Size);
            for (int i = 1; i <= vectorCopy.Size; i++)
            {
                vectorCopy[i] = this[i];
            }
            return vectorCopy;
        }

        public void SetExtrema(out double max, out int maxIndex,
            out double min, out int minIndex)
        {
            minIndex = -1; min = double.MaxValue;
            maxIndex = -1; max = double.MinValue;
            for (int index = 1; index <= Size; index++)
            {
                if (this[index] < min)
                {
                    min = this[index];
                    minIndex = index;
                }
                if (max < this[index])
                {
                    max = this[index];
                    maxIndex = index;
                }
            }
        }

        internal static Vector MathOperation(Vector firstVector, Vector secondVector,
            bool toSum)
        {
            if (firstVector.Size != secondVector.Size) return null;
            int vectorSize = firstVector.Size;
            Vector operatedVector = new Vector(vectorSize);
            for (int i = 1; i <= vectorSize; i++)
            {
                operatedVector[i] = firstVector[i];
                if (toSum) 
                    operatedVector[i] += secondVector[i];
                else 
                    operatedVector[i] -= secondVector[i];
            }
            return operatedVector;
        }

        public static Vector operator +(Vector vectorFirst, Vector vectorSecond)
        {
            return MathOperation(vectorFirst, vectorSecond, toSum: true);
        }

        public static Vector operator -(Vector vectorFirst, Vector vectorSecond)
        {
            return MathOperation(vectorFirst, vectorSecond, toSum: false);
        }

        public static double operator *(Vector vectorFirst, Vector vectorSecond)
        {
            if (vectorFirst.Size != vectorSecond.Size) return double.NaN;
            double sum = 0.0;
            for (int index = 1; index <= vectorFirst.Size; index++)
            {
                sum += vectorFirst[index] * vectorSecond[index];
            }
            return sum;
        }

        private static Vector MultiplyVectorByNumeric(Vector vector, double numeric)
        {
            Vector multipliedVector = new Vector(vector.Size);
            for (int index = 1; index <= vector.Size; index++)
            {
                multipliedVector[index] = numeric * vector[index];
            }
            return multipliedVector;
        }

        public static Vector operator *(double numeric, Vector vector)
            => MultiplyVectorByNumeric(vector, numeric);

        public static Vector operator *(Vector vector, double numeric)
            => MultiplyVectorByNumeric(vector, numeric);

        public static Vector Multiply(Matrix matrix, Vector vector)
        {
            if (matrix.Size != vector.Size) return null;
            int matrixSize = matrix.Size;
            Vector multipliedVector = new Vector(matrixSize);
            for (int widthIndex = 1; widthIndex <= matrixSize; widthIndex++)
            {
                multipliedVector[widthIndex] = 0.0;
                for (int heightIndex = 1; heightIndex <= matrixSize; heightIndex++)
                {
                    multipliedVector[widthIndex] += matrix[widthIndex, heightIndex] * vector[heightIndex];
                }
            }
            return multipliedVector;
        }

        public double Module()
        {
            double module = 0.0;
            for (int index = 1; index <= Size; index++)
            {
                module += this[index] * this[index];
            }
            return Math.Sqrt(module);
        }

        public string GetTextFormat(PrintType type, bool form, int forwardFormatIndent, 
            int formatIndent, string title)
        {
            return TextFormater.GetVectorTextFormat(vector: this, type, form, forwardFormatIndent,
                formatIndent, title);
        }
    }
}