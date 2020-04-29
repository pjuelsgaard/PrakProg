using System;
using static System.Console;
using static matrix;
using System.Collections.Generic;

public static class main{
public static int Main(){
	
	// This part tested whether it worked at all
	Random rand = new Random();
	int nmax = 100;
	var watch = new System.Diagnostics.Stopwatch();
	List<double> ts = new List<double>();
	List<double> ns = new List<double>();
	for(int n = 2;n<nmax; n++){
	matrix A = new matrix(n,n);
	matrix V = A.copy();
	vector e = new vector(A.size1);
	// Create symmetric random matrix
	for(int i =0; i<A.size1;i++){
		A[i,i] = rand.NextDouble()*10;
		for(int j=0; j<i; j++){
			A[j,i] = rand.NextDouble()*10;
			A[i,j] = A[j,i];
		}
	}
	
	watch.Restart();
	int sweeps = diag.jacobian(A, V, e);
	watch.Stop();
	// Saving the n^3 and elapsed time
	ts.Add(watch.ElapsedTicks);
	ns.Add(n);
	}
	
	// Utilizing the LS-fit algorithm to determine coefficient for a*x^3
	matrix A2 = new matrix(ts.Count, 2);
	vector b = new vector(ts.Count);
	for(int i =0; i<ts.Count; i++){
		b[i] = ts[i];
		A2[i,0] = 1.0;
		A2[i,1] = ns[i]*ns[i]*ns[i];
	}
	vector c = fit.lsfit(A2,b);
	Func<double, double> f = (x) => c[0] + c[1]*x*x*x;
	for(int i =0; i<ts.Count; i++){
		Write($"{ns[i]}\t{ts[i]}\t{f(ns[i])}\n");
	}



	return 0;
}
}

		


