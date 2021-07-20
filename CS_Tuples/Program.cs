using System;
using System.Collections.Generic;
using System.Linq;
namespace CS_Tuples
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Tuple Demo");
			 
			// Un-NAmed Tuple type declaration
			// Inernally the C# Compiler will invoke the 'Create()' method
			// by assigning default values to the item1, item2,item3....
			// Item1,Item2,.... will be defined by Roslyn Compiler
			var unamed = ("Mahesh","Rameshrao","Sabnis", 44);
			Console.WriteLine($"Values in Tupe {unamed.Item1} {unamed.Item2} {unamed.Item3} {unamed.Item4}");
			// Tuple with Field Names
			// Field Names will replaces item1, item2,....
			var named = (FirstName:"Mahesh", LastName:"Sabnis");
			Console.WriteLine($"Named Tuple {named.FirstName} {named.LastName}");

			// C# 7.1+ Typle Changes
			// Type Priojection Initializers
			// Declare variables and can generate Tuple Dynamically from it

			string fullname = "Tejas Mahesh Sabnis";
			int age = 17;
			string ocupation = "Student";

			var personalInfo = (fullname, age, ocupation);
			Console.WriteLine($"Persoanl Info {personalInfo.fullname} {personalInfo.age} {personalInfo.ocupation}");
			Console.WriteLine();
			Console.WriteLine($"Type of personalInfo =  {personalInfo.GetType()}");

			// Tuple<T1,T2,T3> type definition
			// The Tuple class, used to bundle Value Types into a Reference Type and will create a
			// encaposulated type at runtime
			// Roslyn compiler service will create an encapsulated type that contains
			// values of fullname,age,ocupation
			Tuple<string, int,string> tp = new Tuple<string, int,string>(fullname,age,ocupation);
			Console.WriteLine($"Tuple Type {tp.Item1} {tp.Item2} {tp.Item3}");
			Console.WriteLine();
			Console.WriteLine($"Type of tp {tp.GetType()}");

			// Check for equality
			Console.WriteLine($"Are tp and personalInfo equal {personalInfo.Equals(tp)}"); //false

			Console.WriteLine($"Completed TAsks {System.Text.Json.JsonSerializer.Serialize(GetCompletedTasks())}");

			Console.WriteLine("USing Tuple for Reading data");

			IEnumerable<(string,string)> Result = GetCompletedTasksWithTaskNameAndAssignTo();

			foreach (var item in Result)
			{
				Console.WriteLine($"{ item.Item1} and {item.Item2}" );
			}

			Console.ReadLine();
		}


		static IEnumerable<Projects> GetCompletedTasks()
		{
			var tasks = new ProjectTasks();
			IEnumerable<Projects> result = null;
			  result = from t in tasks
						 where t.Status == true
						 select t;
			return result;
		}

		/// <summary>
		/// Using Tuples to return only selected data from the Collection after the query
		/// </summary>
		/// <returns></returns>
		static IEnumerable<(string TaskName, string AssignedTo)> GetCompletedTasksWithTaskNameAndAssignTo()
		{
			var tasks = new ProjectTasks();
			 
			var result = from t in tasks
					 where t.Status == true
					 select (t.TaskName, t.AsignedTo);
			return result;
		}
	}
}
