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
	int n = z.Length;
	double[] S = new double[n];
	for(int i=0; i<n;i++){
		S[i] = Spline(xs, ys, z[i]);
	}
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
class qspline{






}
