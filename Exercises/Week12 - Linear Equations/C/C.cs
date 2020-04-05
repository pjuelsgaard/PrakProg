using System;
using static System.Console;
using static matrix;


public static class main{
public static int Main(){
	Random rand = new Random();
	// Creating the inverse of a matrix
        Write("Givens rotation QR-decomposition\n");
        matrix A = new matrix(10,10);
        for(int i = 0; i<A.size1; i++){
                for(int j = 0; j<A.size2; j++){
                        A[i,j] = rand.NextDouble()*99.9;
                        if(A[i,j] > 5.0)A[i,j]*=-1;
                }
        }
	vector b = new vector(A.size1);
	for(int i=0; i<b.size;i++)b[i] = rand.NextDouble()*99.9;
	vector b_orig = b.copy();


	Write("A=\n");
	print2(A);
	Write("b=\n");
	for(int i=0; i<b.size;i++)Write("{0}\n", b[i]);
	matrix Q = A.copy(); matrix R = new matrix(A.size1, A.size2);
	matrix.givens_qr(Q);
	vector x = givens_qr_solve(Q,b);
	
	Write("Ax=\n");
	for(int i=0; i<x.size;i++)Write("{0}\n", (A*x)[i]);
	
       // Checking if Ax=b within eps=1e-6
       double rest = 0;
       Write("x=");
       for(int i = 0; i<b_orig.size; i++){
               rest += (A*x)[i]-b_orig[i]; 
               Write("{0:f2}  ", x[i]);
               }
       Write("\n");
       if(rest<1e-6)Write("A*x=b within eps=1e-6\n");
       
       vector restv = b_orig-(A*x); Write("The elementwise difference :\n");
       for(int i = 0; i<restv.size;i++)WriteLine($"{restv[i]}");



	return 0;
}
}
