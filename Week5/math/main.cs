using System;
using static System.Console;
using static System.Math;
using static cmath;
class main{
	static int Main(){
	double x=2;
	Write($"sqrt(2) = {Sqrt(x)}\n");
	
	complex i = new complex(0,1);
	Write($"e.pow(i) = {E.pow(i)}\n");

	Write($"i={i}\n");
	double y = Pow(x,2)*i;
	Write($"y={y}\n");
	complex J = new complex(0,1);
	Write($"J*J={J*J}\n");
	Write($"J.pow(J)={J.pow(J)}\n");
		return 0;
	}
}
