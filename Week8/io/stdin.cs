using System;

class cmdline{
static int Main(){
	System.IO.TextReader stdin = System.Console.In;
	System.IO.TextWriter stdout = System.Console.Out;
	System.IO.TextWriter stderr = System.Console.Error;
	System.IO.TextReader inputfile = new System.IO.TextReader("input.txt");
	System.IO.TextWriter outputfile = new System.IO.TextWriter("output.txt", append:"false");
	do{
		//string s=Console.ReadLine();
		string s = stdin(ReadLine());
		if (s==null)break;
		string[] words = s.Split('\t');
		foreach(var word in words){
			double x = double.Parse(s);
			//Console.WriteLine("{0} {1} {2}", x, Math.Sin(x), Math.Cos(x));
			stdout.WriteLine("{0} {1} {2}", x, Math.Sin(x), Math.Cos(x));
			stderr.WriteLine("{0} {1} {2}", x, Math.Sin(x), Math.Cos(x));
			output.WriteLine("{0} {1} {2}", x, Math.Sin(x), Math.Cos(x));
			// Stuff
		}
	}
	while(true);
	outputfile.Close();
	inputfile.Close();


	
	return 0;
}


}
