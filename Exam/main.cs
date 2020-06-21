using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;
using static cmath;


public class main{
public static int Main(){
	// Using erf derivate as definition definition
	// Sadly I am, as of yet, unable to generalise my complex solver to higher orders of ODE, due to the complex class not playing nice with neither arrays nor vectors, and Lists are their whole own piece of problematic work
	Func<complex, complex, complex> f = delegate(complex z, complex y){ 
		return exp(z); // This should give the function exp(z.re)*(cos(z.im)+isin(z.im))-1 with our choice of starting point
	};


	// Initial conditions
	complex a = complex.Zero;
	complex b = new complex(4.0, 2.0);
	//List<complex> ya = new List<complex>(){complex.One, complex.Zero}; // I guess?
	complex ya = complex.Zero;
	
	var res = rk.cdrive_single(f,a,b,ya);
	List<complex> zs = res.Item1;
	List<complex> ys = res.Item2;
	Write("First test: Complex exponential f'(z)=exp(z)\n");
	Write($"Found in {res.Item3} steps\n");
	/*Func<double,double> test = delegate(double t){
		return t*t*t/3+t+8.0/3;};*/
	
	for(int i=0; i<zs.Count; i++){
		Error.Write("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\n", zs[i].Re, ys[i].Re, Exp(zs[i].Re)*Cos(zs[i].Im)-1, zs[i].Im, ys[i].Im, Exp(zs[i].Re)*Sin(zs[i].Im));
	}

	Write("In the images plot1.svg and plot2.svg we see the real and imaginary parts respectively of the solution to y'=exp(z), y0=(0,0). As is evident, the real part of the solution is spot on, while the imaginary part diverges\n\n");
	
	Error.Write("\n\n");
	Func<complex, complex, complex> g = delegate(complex z, complex y){ 
		return 2/Sqrt(PI)*exp(-z*z);  // This should give the function exp(z.re)*(cos(z.im)+isin(z.im))-1 with our choice of starting point
	};

	var res2 = rk.cdrive_single(g,a,b,ya);

	zs = res2.Item1;
	ys = res2.Item2;
	Write("Second test: Error function (as far as I could find) f'(z)=2/Sqrt(pi)*exp(-z^2)\n");
	Write($"Found in {res2.Item3} steps.\n");
	Write("In plot3.svg and plot4.svg we see the real and imaginary parts of the error function (at least resulting from the best guesstimate of a differential equation for it as I could find), compared to the approximation used in the gnuplot exercise.\n\n");

	for(int i=0; i<zs.Count; i++){
		Error.Write("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\n", zs[i].Re, ys[i].Re, math.erf(zs[i].Re), zs[i].Im, ys[i].Im, math.erf(zs[i].Im));
	}

	Error.Write("\n\n");
	/*Func<complex, complex, complex> w = delegate(complex z, complex y){
		return 2*complex.I/Sqrt(PI)-2*z*y;
	};
	Func<complex, complex> fad = delegate(complex z){
		return (1-z*z)*(1+2*complex.I*z/Sqrt(PI));
	};*/


	var res3 = rk.cdrive_single(g,a,b,ya);
	zs = res3.Item1;
	ys = res3.Item2;
	Write("Third test: Error function f'(z)=2i/Sqrt(pi)-2*z*f(z), imaginary and real parts treated separately, then recombined\n");
	Write($"Found in {res3.Item3} steps\n");

	
	for(int i=0; i<zs.Count; i++){
		Error.Write("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\n", zs[i].Re, ys[i].Re, math.erf(zs[i].Re), zs[i].Im, ys[i].Im, math.erf(zs[i].Im));
	}

	Write("It is evident from the graph of the complex exponential function f'=exp(z) in plot1.svg and plot2.svg that the solver works quite well.\n");
	Write("Note that the straight part comes from the linear interpolation between the points z.Re=2 and z.Re=4. The reason it looks like there is only one line is because both the analytical expression and the numerical result lie on top of each other.\n\n");
	Write("For the error function, we see in plot3.svg and plot5.svg that the real part suffers a bit of overshoot from the result, which it should converge to (namely 1), but then converges to the approximate result again.\n");
	Write("The imaginary part seems worse. For small z.Im it holds pretty well, but then diverges. However I don not know if this may be because the numerical approximation we used in the gnuplot exercises holds for imaginary arguments as well, and I could not find much data on the internet about numerical approximations to the imaginary part of the complex errorfunction.\n");
	Write("Also, an interesting discovery is how the two different treatments of the differential equation gave the same result. In plot3.svg and plot4.svg the equation is treated as a single, complex differential equation, whereas plot5.svg and plot6.svg show the result of splitting the equation into it's real and imaginary parts at each step, treating them separately, and the recombining them at the end of each step.\n");
	Write("For the intents and purposes in this project, the two methods seem to be equally accurate.\n");


	return 0;




}
}
