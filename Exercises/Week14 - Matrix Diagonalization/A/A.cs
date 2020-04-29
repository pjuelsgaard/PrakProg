using System;
using static System.Console;
using static matrix;

public static class main{
public static int Main(){
	
	int n = 50;
	double s = 1.0/(n+1);
	matrix H = new matrix(n,n);
	for(int i=0; i<n-1; i++){
		H[i,i] = -2;
		H[i,i+1] = 1;
		H[i+1,i] = 1;
	}
	H[n-1,n-1]=-2;
	H = -1/s/s *H;
	matrix V = new matrix(n,n);
	vector e = new vector(n);
	int sweeps = diag.jacobian(H,V, e);
	
	Func<double, int,double> f = (x,m) => Math.Cos(m*Math.PI)*0.2*Math.Sin((m+1)*Math.PI*x);


	/*Write("A matrix of size {0} took {1} sweeps\n", n, sweeps);
	Write("k\tCalculated\tExact\n");
	// Checks for accuracy
	for(int k=0; k<n/3; k++){
		double exact = Math.PI*Math.PI*(k+1)*(k+1);
		double calc = e[k];
		Write($"{k}\t{calc:f8}\t{exact:f8}\n");
	}
	*/
	for(int k=0;k<3;k++){
		Write($"{0}\t{0}\t{0}\n");
		for(int i=0;i<n;i++){
			double x = (i+1.0)/(n+1);
			Write($"{x}\t{V[i,k]}\t{f(x, k)}\n");
		}
		Write($"{1}\t{0}\t{0}\n\n\n");
	}



	/*
	// This part tested whether it worked at all
	Random rand = new Random();
	matrix A = new matrix(3,3);
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
	matrix A_orig = A.copy();
	
	int sweeps = diag.jacobian(A, V, e);
	Write($"It took {sweeps} sweeps to diagonalize A\n");
	Write("e\t\tV.T*A*V=D\n");
	for(int i =0; i<e.size;i++){
		Write("{0:f6}\t{1:f6}\n", e[i], (V.T*A_orig*V)[i,i]);
	}
	matrix.print2(A-A_orig);
	*/

	return 0;
}
}

		


