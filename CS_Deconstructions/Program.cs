using System;

namespace CS_Deconstructions
{
	class Program
	{

		static void Main(string[] args)
		{
			Console.WriteLine("Desconstuctions Example");
			// if the variables are used to store related data then use the deconstructions instead of
			// defining a structure or class
			// variables will be declared from left to right and its initial value will be set from left to right
			var (id,name,lname) = (101, "Mahesh", "Sabnis");
			Console.WriteLine($"Id = {id} and name = {name} , lastname {lname}");
			id = 102;
			Console.WriteLine(id);

			Console.ReadLine();
		}
	}
}
