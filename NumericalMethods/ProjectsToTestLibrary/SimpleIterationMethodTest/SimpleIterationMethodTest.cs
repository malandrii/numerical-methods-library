﻿using System.IO;
using NumericalMethods;
using LinearEquationsSystems = NumericalMethods.LinearEquationsSystems;
using FilesController = NumericalMethods.FilesController;
using PrintType = NumericalMethods.PrintType;

namespace SimpleIterationMethodTest
{
    class SimpleIterationMethodTest
    {
        static void Main()
        {
            using (StreamWriter writer = FilesController.WriteResultToFile("SimpleIterationMethodResult.txt"))
            {
                const string testTableFile = "TestTable.txt";
                writer.WriteLine($"\r\n {testTableFile}");

                FilesController.ReadMatrixWithVectorFromFile(testTableFile, out Matrix A, out Vector b, out int n);
                writer.Write(TextFormater.GetMatrixAndVectorTextFormat(A, b, form: true,
                    forwardFormatIndent: 2, formatIndent: 3, "Matrix Ab"));

                writer.Write("\r\n Solving Linear Equations Systems with Simple Iteration Method:");

                Vector X = LinearEquationsSystems.SimpleIterationMethod(A, b, 1.0E-5, out int K);
                writer.Write(X.GetTextFormat(PrintType.Vertical, form: true,
                    forwardFormatIndent: 3, formatIndent: 10, "Vector X"));
                double error = LinearEquationsSystems.AccuracyError(A, X, b);
                writer.WriteLine($"\r\n Error = {error,10:E1} Iterations = {K}");

                X = LinearEquationsSystems.SimpleIterationMethod(A, b, 1.0E-10, out K);
                writer.Write(X.GetTextFormat(PrintType.Vertical, form: true,
                    forwardFormatIndent: 3, formatIndent: 10, "Vector X"));
                error = LinearEquationsSystems.AccuracyError(A, X, b);
                writer.WriteLine($"\r\n Error = {error,10:E1} Iterations = {K}");
            }
        }
    }
}
