using System;
using System.Collections.Generic;
using static System.Math;
using static matrix;


public class root{
public static vector newton(Func<vector,vector> f, vector x0, double eps=1e-6, double dx=1e-7){
	// Prepare a bunch of stuff
	int n=x0.size;
	vector x = x0.copy();
	vector fx = new vector(n);
	vector df = new vector(n);
	vector y = new vector(n);
	vector fy = new vector(n);
	vector Dx = new vector(n);
	matrix J = new matrix(n,n);
	matrix R = J.copy();
	bool conin; // Condition for inner loop
	bool conout; // Condition for outer loop


	do{
		conin= true;
		fx = f(x);
		for(int i=0; i<n; i++){
			x[i]+=dx;
			df = f(x)-fx;
			for(int j=0; j<n; j++){
				J[j,i] = df[j]/dx;
			}
			x[i]-=dx;
		}
		matrix.givens_qr(J);
		Dx = matrix.givens_qr_solve(J,(-1)*fx);
		double lam = 1.0; // Commence the backtracking
		do{
			y = x+Dx*lam;
			fy = f(y);
			lam/=2;
			conin = (fy.norm()>(1-lam/2)*fx.norm() && lam>1.0/128);
		}while(conin);
	x=y;
	fx=fy;
	conout = (Dx.norm()>dx && fx.norm()>eps);
	}while(conout);
	return x;
}





}
