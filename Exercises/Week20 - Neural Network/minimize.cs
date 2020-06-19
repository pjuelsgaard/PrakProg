using System;
using static System.Math;
using static System.Double;
using System.Collections.Generic;
using static System.Console;

public static class minimize{
public static vector gradient(Func<vector, double> f, vector x, double eps=1.0/4194304){
	
	double fx = f(x);
	double dx;
	vector grad = new vector(x.size);
	for(int i=0; i<x.size; i++){
		dx = Abs(x[i])*eps;
		if(Abs(x[i])<Sqrt(eps))dx=eps;
		x[i] += dx;
		grad[i] = (f(x)-fx)/dx;
		x[i] -= dx;
	}
	return grad;


}
	

public static Tuple<vector,int> qnewton(Func<vector, double> f, vector x0, double acc=1e-3, double eps=1.0/4194304){
	
	vector x = x0.copy();
	double fx = f(x);
	matrix B = new matrix(x.size, x.size);
	B.set_unity();
	int n = 0;
	vector gx = gradient(f,x, eps);
	
	
	while(n<1000){
		vector Dx = -B*gx;
		n++;
		if(gx.norm() < acc || Dx.norm()<eps*x.norm())break;
		double lam=1.0;
		// Commence backtracking
		vector z;
		double fz;
		do{
			z = x + Dx*lam;
			fz = f(z);
			if(fz < fx)break; // Defines a good step
			if(lam<eps){ // Here we just don't care
				B.set_unity();
				break;
			}
			lam/=2;
		}while(true);
		vector s = z-x;
		vector gz = gradient(f,z, eps);
		vector y=gz-gx;
		vector u = s-B*y;
		double uTy = u.dot(y);
		// Updating B
		if(Abs(uTy)>1e-6){
			for(int i=0; i<B.size1;i++){
				for(int j=0;j<B.size2;j++){
					B[i,j]+=u[i]*u[j]*(1/uTy);
				}
			}
		}
		x=z;
		gx=gz;
		fx=fz;
	}
	return Tuple.Create(x,n);

}






}
