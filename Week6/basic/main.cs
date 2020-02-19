using static System.Console;
using static System.Math;
class qarsten{ // main class is public and static no matter what. Static means thatall funs in this class use the same variables w. the same names
	string s = "Old string\n";
	double qw = PI;
	static int Main(){
		double x = 5.87;
		int nmax = 100;
		string s = "ªºß§ðÐđªŋŊħĦΩłŁ€¢®®þÞ←¥↓↑→ıœŒþÞ¨¡¹@²£³$¼½¢¥⅝{÷[«]»}°±¿«>»©“‘”’”’nNµº\n";
		{double y = 0;}
		Write(s); // Writes the innermost s
		// if-statements
		if(2>1){Write("2 is larger than 1\n");
			Write("Ya idjit...\n");}
		else Write("We broke math, my dudes!\n");

		// for-loops
		int i = 0;
		for(i=0;
				i<10; // Init. condition
				i++){// Increments i by adding 1
		      //Write("i={0}, and not {1} or {2}\n",i, i+43*i, i-200*i);
		}	
		i = 0;
		//while(i <666){Write("Ain't metal yet\n");i++;}

		// air-rays n' shit
		double[] v = new double[10];
		for (i=0; i<10;i++){v[i] = Sin(i*PI);
		Write($"v[i]={v[i]:f2}\n");
		}
		System.Array.Resize<double>(ref v,v.Length+1);
		v[10]=PI;
		Write("v.Length= {0}\n",v.Length);
		Write("v = [\n");
		foreach(var item in v)
		{
			Write(item.ToString());
			Write("\n");
		}
		Write("]\n");
		
			
		return 0;
	}


}
