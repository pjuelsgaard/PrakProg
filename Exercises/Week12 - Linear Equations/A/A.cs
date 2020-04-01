using System;
using static System.Console;
using static matrix;


public static class main{
public static int Main(){
	// Create random matrix
	matrix A = new matrix(12, 8); // Size1 = #rows, Size2 = #columns
	Random rand = new Random();
	for(int i = 0; i<A.size1; i++){
		for(int j = 0; j<A.size2; j++){
			A[i,j] = rand.NextDouble()*9.99;
			if(A[i,j] > 5.0)A[i,j]*=-1;
		}
	}
	
	// Writing out matrix A
	Write("\nA({0},{1}):\n", A.size1, A.size2);
	print2(A);

	// Preparing the R matrix
	matrix R = new matrix(A.size2, A.size2);
	matrix Q = A.copy();
	matrix.qr_gs_decomp(Q,R);
	
	// Writing out matrix R
	Write("\nR({0},{1}):\n", R.size1, R.size2);
	print2(R);

	// Writing out Q
	Write("\nQ({0},{1}):\n", Q.size1, Q.size2);
	print2(Q);
	matrix QT = Q.T ;
	Write("\nQ.T({0},{1}):\n", QT.size1, QT.size2);
	print2(QT);

	matrix QTQ = QT*Q;
	Write("\nQTQ:\n");
	print2(QTQ);

	return 0;
}
}
