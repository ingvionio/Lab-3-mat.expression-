using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

class MathExpression
{

    public static void Main()
    {
        List<char> operationsList = new List<char>(); 
        string str = "12+6*  (4+3)+10*5+3";

        for (int i = 0; i < str.Length; i++)
        {
            if ((char.IsDigit(str[i]) == false) && (str[i] != ' '))
                operationsList.Add(str[i]);
        }

        foreach (char op in operationsList)
            Console.Write($"{op} ");

        List<double> numbersList = new List<double>();

        for (int i = 0; i < str.Length; i++)
        {
            if ((char.IsDigit(str[i]) == true) && (str[i]) != ' ')
            {
                string number = null;

                while (char.IsDigit(str[i]) == true)
                {
                    number += str[i];
                    i++;
                    if (i == str.Length)
                        break;
                }

                numbersList.Add(Convert.ToDouble(number));
                i--;

            }
        }

        Console.WriteLine();

        foreach (double number in numbersList)
            Console.Write($"{number} ");

        Console.WriteLine();

        (List<int> indexList, int maxindex,List<char> operationListWithoutBrackets) = IndexOperations(operationsList);
        double result = DoOperations(operationListWithoutBrackets, numbersList, indexList, maxindex);

        Console.WriteLine(result);
    }

    public static double DoOperations(List<char> operations,List<double> numbers,List<int> index, int maxindex)
    {

        while (operations.Count > 0)
        {
            int operatorIndex = -1;
            if (index.Contains(maxindex))
                operatorIndex = index.IndexOf(maxindex);

            else
            {
                while (index.Contains(maxindex) == false)
                    maxindex -= 1;

                operatorIndex = index.IndexOf(maxindex);
            }
            
            char op = operations[operatorIndex];

            double num1 = numbers[operatorIndex];
            double num2 = numbers[operatorIndex + 1];

            double result = Calculate(op, num1, num2);

            numbers.RemoveAt(operatorIndex + 1);
            numbers.RemoveAt(operatorIndex);
            operations.RemoveAt(operatorIndex);
            index.RemoveAt(operatorIndex);

            numbers.Insert(operatorIndex, result);
        }

        return numbers[0];
    }

    public static (List<int>,int,List<char>) IndexOperations(List<char> operations)
    {
        List<int> index = new List<int>();
        int interimIndex = 0;
        int maxindex = 0;
        int lenght = operations.Count;

        for (int i = 0;i < operations.Count;i++)
        {
            if ((operations[i] == '*') || (operations[i] == '/'))
                index.Add(interimIndex + 1);

            else if ((operations[i] == '+') || (operations[i] == '-'))
                index.Add(interimIndex);


            else if ((operations[i] == '(')|| (operations[i] == ')'))
            {
                if (operations[i] == '(')
                    interimIndex += 2;
                else
                    interimIndex -= 2;
                operations.RemoveAt(i);
                i --;
            }

            if (interimIndex>maxindex)
                maxindex = interimIndex;
        }

        return (index, maxindex,operations);
    }

    public static double Calculate(char operation, double firstNumber, double secondNumber)
    {
        switch (operation)
        {
            case '+': return firstNumber + secondNumber;
            case '-': return firstNumber - secondNumber;
            case '*': return firstNumber * secondNumber;
            case '/': return secondNumber * secondNumber;

            default:
                Console.WriteLine("unknow operation");
                return 0;

        }
    }
}