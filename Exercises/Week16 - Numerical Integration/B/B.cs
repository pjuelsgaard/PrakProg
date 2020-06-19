using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;

public static class main{
public static int Main(){
	Func<double, double> f = delegate(double x){
		return 1.0/Sqrt(x);
	};
	Func<double,double> g = delegate(double x){
		return Log(x)/Sqrt(x);
	};
	Func<double,double> h = delegate(double x){
		return 4*Sqrt(1-x*x);
	};
	
	// Comparing how well each routine calculates the integrals
	double acc =1e-10;
	double eps =1e-10;

	vector f1 = integrator.recint(f,0,1, acc, eps);
	vector g1 = integrator.recint(g,0,1, acc, eps);
	vector f2 = integrator.CC(f,0,1, acc, eps);
	vector g2 = integrator.CC(g,0,1);
	double f3 = integrator.o8av(f,0,1, acc, eps);
	double g3 = integrator.o8av(g,0,1, acc, eps);
	
	var watch = new System.Diagnostics.Stopwatch();

	watch.Restart();
	vector h1 = integrator.recint(h,0,1, acc, eps);
	watch.Stop();
	double recintTicks = watch.ElapsedTicks;
	watch.Restart();
	vector h2 = integrator.CC(h,0,1, acc, eps);
	watch.Stop();
	double CCTicks = watch.ElapsedTicks;
	watch.Restart();
	double h3 = integrator.o8av(h,0,1, acc, eps);
	watch.Stop();
	double o8Ticks = watch.ElapsedTicks;
	

	Write("Test of integral 1/sqrt(x) from 0->1\n");
	Write("Method\tValue\t\t\tErr\t\tIterations\n");
	Write($"RecInt\t{f1[0]}\t\t\t{f1[1]}\t{f1[2]}\n");
	Write($"CC\t{f2[0]}\t{f2[1]}\t{f2[2]}\n");
	Write($"o8av\t{f3}\n\n");

	Write("Test of integral ln(x)/sqrt(x) from 0->1\n");
	Write("Method\tValue\t\t\tErr\t\tIterations\n");
	Write($"RecInt\t{g1[0]}\t\t\t{g1[1]}\t{g1[2]}\n");
	Write($"CC\t{g2[0]}\t{g2[1]}\t{g2[2]}\n");
	Write($"o8av\t{g3}\n\n");
	
	Write("Test of integral 4*sqrt(1-x*x) from 0->1\n");
	Write("Method\tValue\t\t\tErr\t\tIterations\tTime\n");
	Write($"RecInt\t{h1[0]}\t\t{h1[1]}\t{h1[2]}\t{recintTicks}\n");
	Write($"CC\t{h2[0]}\t{h2[1]}\t{h2[2]}\t{CCTicks}\n");
	Write($"o8av\t{h3}\t\t\t\t\t\t{o8Ticks}\n\n");
	Write("We see that the CC integration routine takes much fewer iterations to reach a desirable result, although it lacksa bit of accuracy compared to the recursive.\n However, the last integral seemed rather difficult for the CC routine.\n The o8av routine is less precise, but is MUCH faster.\n");	
	return 0;
}
}
