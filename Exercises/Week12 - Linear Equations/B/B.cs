using System;
using static System.Console;
using static matrix;


public static class main{
public static int Main(){
	Random rand = new Random();
	// Creating the inverse of a matrix
        Write("Inverse of matrix\n");
        matrix A = new matrix(10,10);
        for(int i = 0; i<A.size1; i++){
                for(int j = 0; j<A.size2; j++){
                        A[i,j] = rand.NextDouble()*9.99;
                        if(A[i,j] > 5.0)A[i,j]*=-1;
                }
        }
	Write("A=\n");
	print2(A);
	matrix Q = A.copy(); matrix R = new matrix(A.size1, A.size2);
	matrix.qr_gs_decomp(Q,R);
	matrix B = qr_gs_inverse(Q,R);
	Write("\nA*B=\n");
	print2(A*B);


	return 0;
}
}
