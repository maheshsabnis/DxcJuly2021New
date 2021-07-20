using System;
using System.Collections.Generic;
namespace CS_Patterns
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Pattern Matching Demo!");

			InputPropertyPatternCheck(1000);
			InputPropertyPatternCheck("Mahesh");


			List<object> values = new List<object>() 
			{
			   10,20, "Mahesh", "Sabnis", 40,50,90.8, "Tejas",
			   80.2, 56.32, "Leena", true, false, null, "Rameshrao",
			   76.55,30,70,-9, new List<int>() { 40,80,46,90}, null,
			   new List<string> { "Anay", "Vijay", "Arjun"}, -8,
			   new DateTime()
			};

			SwitchCasePaternMatching(values);

			Console.ReadLine();
		}
		/// <summary>
		/// The vaue will be input property that will be check and compatred using the 
		/// 'is' operator
		/// </summary>
		/// <param name="value"></param>
		static void InputPropertyPatternCheck(object value)
		{
			// the 'val' is the left-hand-side type check for the input value
			// the 'val' is inline declaration to the condition C# 7.0+
			// the 'val' will be inline declred and its type will be compared using
			// the GetType() access on input value
			if (value is int val)
			{
				val = 3000;
				Console.WriteLine($"The Received Value is Integer {Convert.ToInt32(value) + val}");
			}
			// the 'str' will be inline declred and its type will be compared using
			// the GetType() access on input value
			if (value is string str)
			{
				str = "Sabnis";
				Console.WriteLine($"The Received value is String {value + str}");
			}
		}

		static void SwitchCasePaternMatching(List<object> values)
		{
			int intSum =0;
			string stringConcat = "";
			double doubleSum = 0;

			foreach (var item in values)
			{
				try
				{
					switch (item)
					{
						//List of Integers
						case IEnumerable<int> intValues:
							{
								foreach (var v in intValues)
								{
									intSum += v;
								}
								break;
							}
						//List of String		
						case IEnumerable<string> strValues:
							{
								foreach (var str in strValues)
								{
									stringConcat += str;
								}
								break;
							}
						//List of Doubles
						case IEnumerable<double> dobValues:
							{
								foreach (var v in dobValues)
								{
									doubleSum += v;
								}
								break;
							}
							// read only positive values
							// the 'when' will invoke the 'if' statement internally
						case int v when v > 0:
							{
								intSum += v;
								break;
							}
						case int v when v < 0:
							{
								Console.WriteLine($"Negative value is found in input {v}");
								break;
							}
						case string str: 
							{
								stringConcat += str;
								break;
							}
						case double dob:
							{
								doubleSum += dob;
								break;
							}
						case null:
							{
								Console.WriteLine("The null vakue is discovered");
								break;
							}
						case bool b:
							{
								Console.WriteLine($"The boolean value {b}");
								break;
							}
						default:
							throw new InvalidOperationException("Type is UnReognized");
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Exception Thrown {ex.Message}");
				}
			}

			Console.WriteLine($"Sum of Integers {intSum}");
			Console.WriteLine($"Sum of Doubles {doubleSum}");
			Console.WriteLine($"Concatination of strings {stringConcat}");
		}
	}
}
