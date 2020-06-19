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
	Write("Part B:\n");
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
	Write("As can be seen in B.svg, the solutions start to diverge close to the maximal R_max\n\n");

	Write("Part C:\n");
	Error.Write("\n\n");
	double r = 0.1;
	for(int i = 0; i<10; i++){
		Func<vector, vector> newfix = delegate(vector w){
			double energy = w[0];
			return auxfun(energy, r);
		};	
		Func<vector, vector> newfiximprove = delegate(vector w){
			double energy = w[0];
			return auxfun(energy, r,improve: true);
		};


		vector e0new = new vector(-1.0);
		vector e_res1 = root.newton(newfix, e0new);
		vector e_res2 = root.newton(newfiximprove, e0);
		Error.Write("{0}\t{1}\t{2}\n", r, e_res1[0], e_res2[0]); // r_max, e0_simple, e0_improved
		r+=0.3;
	}

	Write("A comparison of how the lowest energy approaches the exact result (-1/2) for the simple and improved conditions\n It is evident that the improved condition converges on much smaller R_max.\n");
	
	return 0;

}

	// Setting a max r = 8
public static vector auxfun(double e, double r_max=8, double r_min=1e-3, double dr=1e-3, bool improve = false){	
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
	if(improve)goat[0]-=r_max*Exp(-Sqrt(-e*2)*r_max);
	return goat;
}
	











}
