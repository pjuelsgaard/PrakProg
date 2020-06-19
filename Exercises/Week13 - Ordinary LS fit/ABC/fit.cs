//Package for fitting data. Requires working with matrix and vector classes

using System;
//using static matrix;
public partial class fit{
public static vector lsfit(matrix A, vector b){
	matrix Q = A.copy();
	matrix R = new matrix(A.size2, A.size2);
	matrix.qr_gs_decomp(Q,R);
	
	vector c = matrix.qr_gs_solve(Q,R,b);
	return c;
}
public static matrix cov(matrix A){
	/*	
	matrix R = new matrix(A.size2, A.size2);
	matrix Q = A.copy();
	matrix.qr_gs_decomp(Q,R);
	matrix invR = matrix.rtri_invert(R);
	matrix S = invR*invR.T;
	*/

	
	matrix ATA = A.T * A;
	matrix Q = ATA.copy();
	matrix R = new matrix(ATA.size2, ATA.size2);
	matrix.qr_gs_decomp(Q,R);
	matrix S = matrix.qr_gs_inverse(Q, R);
	
	return S;
}
}
