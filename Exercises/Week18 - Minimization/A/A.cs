using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;

public static class main{
public static int Main(){
	Func<vector, double> R = delegate(vector v){
			return Pow(1-v[0], 2) + 100 * Pow(v[1]-v[0]*v[0], 2);
	};
	Func<vector, double> H = delegate(vector v){
			return Pow(v[0]*v[0] + v[1]-11, 2) + Pow(v[0] + v[1]*v[1]-7, 2);
	};
	vector R0 = new vector(2.0,2.0);
	vector H0 = new vector(2.0,2.0);

	var Rres = minimize.qnewton(R,R0);
	vector Rmin = Rres.Item1;
	int Rn = Rres.Item2;

	var Hres = minimize.qnewton(H,H0);
	vector Hmin = Hres.Item1;
	int Hn = Hres.Item2;
	
	Write("For Rosenbrock's valley, the minimum was found at [{0},{1}] in {2} steps\n", Rmin[0],Rmin[1],Rn);
	Write("For Himmelblau's function, the minimum was found at [{0},{1}] in {2} steps\n", Hmin[0],Hmin[1],Hn);
	Write("Analytically, the minima should be at Rosenbrock: [1,1] and Himmelblau: [3,2]\n");

	return 0;
}
}
