using System;

namespace CS_Discards
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Discards Demo");
			// explict declaration of the Varible 'result'
			// use the 'discard', the local variable that is bound to the scope
			// and declare using _
			var _ = ProcessData(10,20);
			 
			Console.WriteLine($"Result = {_}");
			// implcit inline call of the method
			Console.WriteLine($"Result of Addition = {ProcessData(300,400)}");
			Console.ReadLine();
		}

		static int ProcessData(int x,int y)
		{
			var _ = x + y;
			// inline return with logic
			return _;
		}
	}
}
