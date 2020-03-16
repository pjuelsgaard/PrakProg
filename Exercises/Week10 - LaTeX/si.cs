using System;
using static System.Console;
using static System.Math;

class main{
	public static int Main(){
		// The Sine integral
		double i = 0;
		
		while(i<=900){
			Write("{0}\t{1}\t{2}\n", i, Si(i), si(i));
			i+=0.05;
		};
		
		return 0;
	}

	private static double Si(double x){
		// Using the definition Si(x) = int_0^x sin(t)/t dt
		if(x==0){return 0;};
		if(x>700){return PI/2;};
		Func<double, double> f = (t) => Sin(t)/t;	
		double res = quad.o8av(f,0,x);
		return res;
	}

	private static double si(double x){
		// Using the definition si(x) = -int_x^inf sin(t)/t dt = Si(x)-PI/2
		return Si(x)-PI/2;
	}

}
