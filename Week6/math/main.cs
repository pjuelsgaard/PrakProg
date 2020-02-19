using System;
using static System.Console;
using static System.Math;
using static cmath;
class main{
	static int Main(){
	Write("Exercise: math");
	Write($"sqrt(2) = {Sqrt(2)}\n");
	
	complex i = new complex(0,1);
	Write($"e^i = {cmath.exp(i)}\n");

	Write($"e^(i*pi) = {cmath.exp(i*PI):f2}\n");

	Write($"i^i = {cmath.pow(i, i):f2}\n");

	Write($"Sin(i*pi) = {cmath.sin(i*PI):f2}\n");

	Write($"Sinh(i) = {sinh(i):f2}\n");

	Write($"Cosh(i) = {cosh(i):f2}\n");
	
	complex J = new complex(-1,0);
	Write($"sqrt(-i) = {sqrt(-i)}\n");

	Write($"When entered as negative double: sqrt(-1) = {sqrt(-1)}\n");
	Write($"When entered as negative complex, y=0: sqrt(-1) = {sqrt(J)}\n");

	
		return 0;
	}
}
