using System;
using System.Collections.Generic;

public class Program
{
	public static List<string> equationItems;
	public static void Main()
	{
		/*
			1 + 1
			2 * 2
			1 + 2 + 3
			6 / 2
			11 + 23
			11.1 + 23
			1 + 1 * 3
			( 11.5 + 15.4 ) + 10.1
			23 - ( 29.3 - 12.5 )
			( 1 / 2 ) - 1 + 1
			10 - ( 2 + 3 * ( 7 - 5 ) )
		*/
		string equation = Console.ReadLine();
		Console.WriteLine("Before " + equation);
		Calculate(equation);
	}

	public static double Calculate(string sum)
	{
		equationItems = new List<string>(sum.Split(new Char[]{' '}));
		//Math Rule 
		/* Always do () first, then Division/ M , A /S at left right order*/
		//Split them out
		//Find brackets recursively
		//Find Divide & Multi
		//Find add & Subtract
		while (equationItems.Contains("(") && equationItems.Contains(")"))
		{
			//Find the innermost bracket
			int startIndex = equationItems.LastIndexOf("(") + 1;
			int endIndex = equationItems.IndexOf(")");
			int rangeOfBracket = endIndex - startIndex;
			//Extract bracket formula for priority processing
			List<string> bracketItems = new List<string>(equationItems.GetRange(startIndex, rangeOfBracket));
			Console.WriteLine("BRC " + string.Join(" ", bracketItems)); //For checking bracket is correct
			//Replace bracket formula with result
			double result = MathList(bracketItems);
			equationItems.Insert(startIndex - 1, result.ToString());
			equationItems.RemoveRange(startIndex, rangeOfBracket + 2);
			Console.WriteLine("Formula Steps " + string.Join(" ", equationItems)); //for checking each steps of the loop
		}

		Console.WriteLine("B4 Final " + string.Join(" ", equationItems));
		double finalResult = MathList(equationItems);
		Console.WriteLine("Result " + finalResult);
		return finalResult;
	}

	public static double MathList(List<string> equationItems)
	{
		string div = "/";
		string mul = "*";
		string plus = "+";
		string sub = "-";
		while (equationItems.Count > 1)
		{
			Console.WriteLine("Formula Steps " + string.Join(" ", equationItems)); //for checking each steps of the loop
			while (equationItems.Contains(div) || equationItems.Contains(mul))
			{
				int operatorIndex = equationItems.FindIndex(e => e.Equals(div) || e.Equals(mul));
				double leftNum = Convert.ToDouble(equationItems[operatorIndex - 1]);
				double rightNum = Convert.ToDouble(equationItems[operatorIndex + 1]);
				double result = MathLeftRight(leftNum, rightNum, equationItems[operatorIndex]);
				equationItems.Insert(operatorIndex - 1, result.ToString());
				equationItems.RemoveRange(operatorIndex, 3);
				Console.WriteLine("Formula Steps " + string.Join(" ", equationItems)); //for checking each steps of the loop
			}

			while (equationItems.Contains(plus) || equationItems.Contains(sub))
			{
				int operatorIndex = equationItems.FindIndex(e => e.Equals(plus) || e.Equals(sub));
				double leftNum = Convert.ToDouble(equationItems[operatorIndex - 1]);
				double rightNum = Convert.ToDouble(equationItems[operatorIndex + 1]);
				double result = MathLeftRight(leftNum, rightNum, equationItems[operatorIndex]);
				equationItems.Insert(operatorIndex - 1, result.ToString());
				equationItems.RemoveRange(operatorIndex, 3);
				Console.WriteLine("Formula Steps " + string.Join(" ", equationItems)); //for checking each steps of the loop
			}
		}

		return Convert.ToDouble(equationItems[0]);
	}

	public static double MathLeftRight(double leftNum, double rightNum, string opr)
	{
		string error = "";
		double result = 0.00;
		switch (opr)
		{
			case "+":
				result = leftNum + rightNum;
				break;
			case "-":
				result = leftNum - rightNum;
				break;
			case "*":
				result = leftNum * rightNum;
				break;
			case "/":
				if (rightNum == 0)
				{
					error = "Cannot divide by zero";
				}
				else
				{
					result = leftNum / rightNum;
				}

				break;
		}

		return result;
	}
}