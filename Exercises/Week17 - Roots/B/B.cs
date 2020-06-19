using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;

public class main{
public static int Main(){
	// Hydrogen atom, derivation of expression
	// e*f = -f''/2 - f/r
	// => f'' = -2(e*f+f/r)
	// Let y[0] =f, y[1]=f', y'[0]=y1, y'[1]=-2(e+1/r)f
	// Then we can write the ODE-system for our H-Equation, as seen in the auxilary function auxfun.

	Func<vector, vector> fix = delegate(vector w){
		double energy = w[0];
		return auxfun(energy);
	};

	vector e0 = new vector(-1.0);
	vector e_res = root.newton(fix, e0);

	Write("At the chosen maximal radius of 8 Bohr radii, we find the lowest energy to be {0}\n", e_res[0]);
	double q=0;
	for(int i=0; i<101; i++){
		Error.Write("{0}\t{1}\t{2}\n", q, auxfun(e_res[0], q)[0], q*Exp(-q));
		q+=8.0/100;
	}
	Write("As can be seen in B.svg, the solutions start to diverge close to the maximal R_max\n");
	return 0;

}

	// Setting a max r = 8
public static vector auxfun(double e, double r_max=8, double r_min=1e-3, double dr=1e-3){	
	Func<double, vector, vector> heq = delegate(double x, vector y){
		return new vector(y[1], -2.0*(e+1.0/x)*y[0]);
	};
	vector y0 = new vector(r_min*(1-r_min), 1-2*r_min);
	var res = rk.driver(heq, r_min, r_max, y0, dr);
	List<double> ts = res.Item1;
	List<vector> ys = res.Item2;
	// Ugly little fix
	vector goat = new vector(1);
	goat[0] = ys[ys.Count-1][0];
	return goat;
}
	











}
