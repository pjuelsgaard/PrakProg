using System;
using System.Collections.Generic;
using static System.Math;
using static System.Console;

public static class main{
public static int Main(){
	// Using a gaussian function, since the integral from -infty to infty is sqrt(pi)
	Func<double, double> g = delegate(double x){
		return Exp(-x*x);
	};

	vector g1 = integrator.recint_inf(g, Double.NegativeInfinity, Double.PositiveInfinity);
	vector g2 = integrator.recint_inf(g, 0, Double.PositiveInfinity);
	vector g3 = integrator.recint_inf(g, 0, Double.NegativeInfinity);

	Write($"The result, which should be {Sqrt(PI)} gave {g1[0]}+-{g1[1]} in {g1[2]} iterations\n");
	Write($"The result, which should be {Sqrt(PI)/2} gave {g2[0]}+-{g2[1]} in {g2[2]} iterations\n");
	Write($"The result, which should be {-Sqrt(PI)/2} gave {g3[0]}+-{g3[1]} in {g3[2]} iterations\n");


	double o1 = integrator.o8av(g,Double.NegativeInfinity, Double.PositiveInfinity);
	double o2 = integrator.o8av(g,0,Double.PositiveInfinity);
	double o3 = -integrator.o8av(g,Double.NegativeInfinity,0);

	Write($"The values returned for the same 3 integrals by o8av are\n{o1}\n{o2}\n{o3}\n Although the integral from 0-> NegInfty had to be manually rearranged for o8av\n");

	return 0;
}
}
