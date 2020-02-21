using System;

class cmdline{
static int Main(string[] args){
	System.Console.Write("Part A:\n");
	Console.WriteLine("x	Sin(x)		Cos(x)");
	foreach(var s in args){
		double x = double.Parse(s);
		Console.WriteLine("{0} {1} {2}", x, Math.Sin(x), Math.Cos(x));
	}




	return 0;
}


}
