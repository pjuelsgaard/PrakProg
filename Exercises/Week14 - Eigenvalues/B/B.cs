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
	List<double> ts_vbv = new List<double>();
	List<double> ns = new List<double>();
	List<int> sweeplist = new List<int>();
	List<int> sweeplist_vbv = new List<int>();

	// For increasing sizes of matrix, I measure the ampount of time it takes each method to diagonalize the matrix and find eigenvalues
	for(int n = 10;n<nmax; n+=10){
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

	// Copying the matrix for the value-by-value test
	matrix A_vbv = A.copy();
	matrix V_vbv = A_vbv.copy();
	vector e_vbv = new vector(A_vbv.size1);

	watch.Restart();
	int sweeps = diag.jacobian(A, V, e);
	watch.Stop();
	// Saving the n^3 and elapsed time
	ts.Add(watch.ElapsedTicks);
	ns.Add(n);
	sweeplist.Add(sweeps);

	watch.Restart();
	int sweeps_vbv = diag.jacobian_partial(A_vbv, V_vbv, e_vbv);
	watch.Stop();
	ts_vbv.Add(watch.ElapsedTicks);
	sweeplist_vbv.Add(sweeps_vbv);
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
	// By plotting the times it takes for the different versions of the routine, we see that they're are equally fast
	for(int i =0; i<ts.Count; i++){
		Write($"{ns[i]}\t{ts[i]}\t{f(ns[i])}\t{sweeplist[i]}\t{ts_vbv[i]}\t{sweeplist_vbv[i]}\n");
	}
	







	// In this section I test the speed of the different algorithms depending on how many eigenvalues are required
	
	matrix A3 = new matrix(30,30);
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

	List<double> new_ts = new List<double>();
	List<double> new_ts_vbv = new List<double>();
	List<int> degree = new List<int>();
	
	// We demonstrate that the partial method (value-by-value) method will end in a triangular matrix when finding all eigenvalues
	matrix.print2(A3,1);
	int sweeps2 = diag.jacobian_partial(A3.copy(),V3.copy(),e3.copy(), 0);
	matrix.print2(A3,1);

	// Find computiation time as function of amount of eigenvalues. Degree is the number of eigenvalues.
	for(int i=1; i<A3.size1+1;i++){
	
		watch.Restart();
		int sweeps = diag.jacobian(A3.copy(), V3.copy(), e3.copy());
		watch.Stop();
		// Saving the n^3 and elapsed time
		new_ts.Add(watch.ElapsedTicks);
		degree.Add(i);

		watch.Restart();
		int sweeps_vbv = diag.jacobian_partial(A3.copy(), V3.copy(), e3.copy(), i);
		watch.Stop();
		new_ts_vbv.Add(watch.ElapsedTicks);
	}

	for(int i =0; i<new_ts.Count; i++){
		Error.Write($"{degree[i]}\t{new_ts[i]}\t{new_ts_vbv[i]}\n");
	}

	return 0;
}
}

		


