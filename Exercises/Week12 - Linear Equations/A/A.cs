using System;
using static System.Console;
using static matrix;


public static class main{
public static int Main(){
	// Part 1: Decomposition
	Write("Part 1: Decomposition\n");
	// Create random matrix
	matrix A = new matrix(10, 6); // Size1 = #rows, Size2 = #columns
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

	// Checking if QR=A within eps=1e-6
	if((Q*R).equals(A,1e-6))Write("QR=A within eps=1e-6\n");

	// Part 2: Solving linear equation systems
	Write("\nPart 2: Solving linear equation systems\n");
	matrix A2 = new matrix(8,8);
	for(int i = 0; i<A2.size1; i++){
		for(int j = 0; j<A2.size2; j++){
			A2[i,j] = rand.NextDouble()*9.99;
			if(A2[i,j] > 5.0)A2[i,j]*=-1;
		}
	}
	vector b = new vector(A2.size1);
	for(int i = 0; i<b.size;i++)b[i] = rand.NextDouble()*9.99;
	matrix Q2 = A2.copy(); matrix R2 = new matrix(Q2.size1, Q2.size2);
	matrix.qr_gs_decomp(Q2,R2);
	Write("\nA2({0},{1}):\n", A2.size1, A2.size2);
	print2(A2);
	Write("B = ");
	for(int i=0;i<b.size;i++)Write("{0:f2}  ", b[i]);
	Write("\nR2({0},{1}):\n", R2.size1, R2.size2);
	print2(R2);

	vector x = qr_gs_solve(Q2, R2, b);

	// Checking if Ax=b within eps=1e-6
	double rest = 0;
	Write("x=");
	for(int i = 0; i<b.size; i++){
		rest += (A2*x)[i]-b[i]; 
		Write("{0:f2}  ", x[i]);
		}
	Write("\n");
	if(rest<1e-6)Write("A*x=b within eps=1e-6\n");
	
	vector restv = b-(A2*x); Write("The elementwise difference :\n");
	for(int i = 0; i<restv.size;i++)WriteLine($"{restv[i]}");


	return 0;
}
}
