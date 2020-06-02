using System;
using static System.Console;
using static matrix;
using System.Collections.Generic;

public static class main{
public static int Main(){
	
	// This part tested whether it worked at all
	Random rand = new Random();
	int nmax = 10;
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
	
	matrix A3 = new matrix(10,10);
	matrix V3 = A3.copy();
	vector e3 = new vector(A3.size1);
	// Create fixed symmetric random matrix
	Random fixrand = new Random(3);
	for(int i =0; i<A3.size1;i++){
		A3[i,i] = fixrand.NextDouble()*10;
		for(int j=0; j<i; j++){
			A3[j,i] = fixrand.NextDouble()*10;
			A3[i,j] = -A3[j,i];
		}
	}
	matrix.print2(A3,1);
	Error.Write($"{A3[0,1]:f2}\n");
	/*
	int sweeps2 = diag.jacobian(A3,V3,e3);
	for(int i =0; i<e3.size; i++){
		Error.Write($"e[{i}] = {e3[i]}\n");
	}
	*/

	return 0;
}
}

		


