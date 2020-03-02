using System;

class cmdline{
static int Main(string[] args){
	string input = args[0];
	string output = args[1];


	/* The following commented section would be used to make the program interactive
	System.IO.TextReader stdin = System.Console.In;
	System.IO.TextWriter stdout = System.Console.Out;
	System.IO.TextWriter stderr = System.Console.Error; */
	
	
	System.IO.StreamReader inputfile = new System.IO.StreamReader(input);
	System.IO.StreamWriter outputfile = new System.IO.StreamWriter(output, true);
	outputfile.WriteLine("Part C:");
	do{
		string s = inputfile.ReadLine();
		if (s==null)break;
		string[] words = s.Split('\t', '\n', ' ');
		foreach(var word in words){
			double x = double.Parse(word);
			outputfile.WriteLine("{0} {1} {2}", x, Math.Sin(x), Math.Cos(x));
		}
	}while(true);
	outputfile.Close();
	inputfile.Close();


	
	return 0;
}


}
