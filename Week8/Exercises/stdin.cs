using System;

class cmdline{
static int Main(){
	System.Console.Write("Part B:\n");
	// Dmitri did some funky magic
	System.IO.TextReader stdin = System.Console.In;
	System.IO.TextWriter stdout = System.Console.Out;
	System.IO.TextWriter stderr = System.Console.Error;
	do{
		string s = stdin.ReadLine();
		if (s==null)break;
		string[] words = s.Split(' ',',','\t');
		foreach(var word in words){
			double x = double.Parse(s);
			stdout.WriteLine("{0} {1} {2}", x, Math.Sin(x), Math.Cos(x));
		}
	}while(true);


	
	return 0;
}


}
