using System;
using System.Collections.Generic;
using System.Linq;

// Linear splines and integrals
public class lspline{
// Binary search
public static int binsearch(double[] x, double z){ // Assumes ordered array
	int i=0;
	int j=x.Length-1;
	while(j-i>1){
		int mid = (i+j)/2;
		if(z>x[mid]) i=mid ; else j=mid;
	}
	return i;
}

public static double Spline(double[] xs, double[] ys, double z){
	if(z<xs.Min() || z>xs.Max()){
		System.Console.Error.Write("z is out of known bounds\n");
		return Double.NaN;
	}
	// First: Find the correct interval for z
	int i = binsearch(xs, z);
	double DyDx = (ys[i+1]-ys[i])/(xs[i+1]-xs[i]);
	double Si_z = ys[i] + DyDx*(z-xs[i]);
	return Si_z;
}
public static double Spline(List<double> xs, List<double> ys, double z){
	return Spline(xs.ToArray(), ys.ToArray(), z);
}
public static double[] Spline(double[] xs, double[] ys, double[] z){
	int n = z.Length; double[] S = new double[n]; for(int i=0; i<n;i++){ S[i] = Spline(xs, ys, z[i]); }
	return S;
}
public static double[] Spline(List<double> xs, List<double> ys, List<double> z){
	return Spline(xs.ToArray(), ys.ToArray(), z.ToArray());
}

public static double Int(double[] xs, double[] ys, double z){
	if(z<xs.Min() || z>xs.Max()){
		System.Console.Error.Write("z is out of known bounds\n");
		return Double.NaN;
	}
	double A=0;
	int i = binsearch(xs, z);
	double Dx = xs[i+1]-xs[i];
	double Dy = ys[i+1]-ys[i];
	double B = Dy/Dx/2 * (z-xs[i])*(z-xs[i]) + ys[i]*(z-xs[i]);
	for(int j=0; j<i; j++){A+=(ys[j+1]+ys[j])*(xs[j+1]-xs[j])/2;}
	return A+B;
}
public static double Int(List<double> xs, List<double> ys, double z){
	return Int(xs.ToArray(), ys.ToArray(), z);
}

public static double[] Int(double[] xs, double[] ys, double[] z){
	int n = z.Length;
	double[] S = new double[n];
	for(int i=0; i<n;i++){
		S[i] = Int(xs, ys, z[i]);
	}
	return S;
}
public static double[] Int(List<double> xs, List<double> ys, List<double> z){
	return Int(xs.ToArray(), ys.ToArray(), z.ToArray());
}

}


// Quadratic splines
public class qspline{

public static int binsearch(double[] x, double z){ // Assumes ordered array
	int i=0;
	int j=x.Length-1;
	while(j-i>1){
		int mid = (i+j)/2;
		if(z>x[mid]) i=mid ; else j=mid;
	}
	return i;
}

public static double Spline(double[] xs, double[] ys, double z, int d){
	if(z<xs.Min() || z>xs.Max()){
		System.Console.Error.Write("z is out of known bounds\n");
		return Double.NaN;
	}
	int n = xs.Length -2; // Index of 2ndfinal entry of xs
	
	// Arrays for a-coefficients of different routesof recursion
	double[] c_up = new double[n+1]; c_up[0]=0;
	double[] c_down = new double[n+1]; c_down[n]=0;
	double[] ps = new double[n+1];
	double[] bs = new double[n+1];
	
	// Create all values of p
	for(int j = 0; j<=n; j++){ps[j] = (ys[j+1]-ys[j])/(xs[j+1]-xs[j]);}
	// Create values of c from both recursions
	for(int k = 1; k<=n; k++){
		c_up[k] = (ps[k]-ps[k-1]-c_up[k-1]*(xs[k-1]-xs[k]))/(xs[k+1]-xs[k]);
		c_down[n-k] = (ps[n-k+1]-ps[n-k]-c_down[n-k+1]*(xs[n-k+2]-xs[n-k+1]))/(xs[n-k+1]-xs[n-k]);
	}
	// Retrieving elementwise average of c
	double[] cs = new double[n+1];
	for(int l =0; l<=n; l++){
		cs[l]=(c_up[l]+c_down[l])/2;
		bs[l] = ps[l]-cs[l]*(xs[l+1]-xs[l]);
	}
	int i = binsearch(xs, z);
	if(d == 1){return bs[i] + 2*cs[i]*(z-xs[i]);} // Derivative
	if(d == -1){ // Integral from 0-> z
		double A = 0;
		for(int m=0; m<i; m++){
			A += ys[m]*(xs[m+1]-xs[m])+bs[m]/2 *(xs[m+1]-xs[m])*(xs[m+1]-xs[m]) + cs[m]/3 *(xs[m+1]-xs[m])*(xs[m+1]-xs[m])*(xs[m+1]-xs[m]);
		}
		double B = ys[i]*(z-xs[i])+bs[i]/2 *(z-xs[i])*(z-xs[i]) + cs[i]/3 *(z-xs[i])*(z-xs[i])*(z-xs[i]);
		return A+B;}
	return ys[i] + bs[i]*(z-xs[i])+cs[i]*(z-xs[i])*(z-xs[i]); // Spline value at z
	
	//return Convert.ToDouble(n);
}
public static double Spline(List<double> xs, List<double> ys, double z, int d){
	return Spline(xs.ToArray(), ys.ToArray(), z, d);
}
	



}
