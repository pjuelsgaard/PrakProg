using System;
using static System.Math;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
//using static complex;
using static cmath;

public class rk{

public static vector rkstep45(Func<double,vector, vector>f, double t0, vector y0, double h, vector err){
	// Using the RKF45 butcher's table
	// t is current argument; y=y(t); h is stepsize;
	// Generate the k_i vectors
	vector k_0 = f(t0,y0);
	vector k_1 = f(t0+h/4, y0+h/4*k_0);
	vector k_2 = f(t0+h*3/8, y0 + h*3/32*k_0 + h*9/32*k_1);
	vector k_3 = f(t0+h*12/13, y0 + h*1932/2197*k_0 - h*7200/2197*k_1 + h*7296/2197*k_2);
	vector k_4 = f(t0+h, y0 + h*439/216*k_0 - h*8*k_1 + h*3680/513*k_2 - h*845/4104*k_3);
	vector k_5 = f(t0+h/2, y0 - h*8/27*k_0 + h*2*k_1 - h*3544/2565*k_2 + h*1859/4104*k_3 - h*11/40*k_4);
	vector k_sum = 16.0/135*k_0 + 6656.0/12825*k_2 + 28561.0/56430*k_3 - 9.0/50*k_4 + 2.0/55*k_5;
	
	// The embedded lower order method made with b_i^* instead of b_i
	vector k_star = 25.0/216*k_0 + 1408.0/2565*k_2 + 2197.0/4104*k_3 - 1.0/5*k_4;
	err = (k_sum - k_star)*h;
	for(int j =0; j<err.size; j++)err[j]=Abs(err[j]); // The error estimate is set to always be positive (because that juts makes more sense to me)
	vector y_next = y0 + h*k_sum;
	
	return y_next;
}




// acc is the absolute precision, and eps is the relative precision on each step
public static Tuple<List<double>, List<vector>> driver(Func<double, vector, vector> f, double a, double b, vector ya, double h, double acc=1e-6, double eps=1e-6){
	// Initialize lists for ts and ys with t=a and y(a) already in them
	List<double> ts = new List<double>(){a};
	List<vector> ys = new List<vector>(){ya};
	
	// Within this loop a serves as t_current and ya serves as y_current
	while(a<b){
		// Check for overshoot, to make sure the function actually ends at b
		if(b<= a+h)h = b-a;
	
	// Do the actual work
	vector err_step = new vector(ya.size);
	vector y_next = rkstep45(f, a, ya, h, err_step);


	// Do error stuffs using eq.s 40 and onwards from ODE reading
	vector tol = new vector(y_next.size);
	for(int j=0;j<y_next.size; j++){
		tol[j] = Sqrt(h/(b-a))*(eps*Abs(y_next[j]) + acc);
		// Dividing by zero is a bad idea, so...
		if(err_step[j]==0)err_step[j]=tol[j]/4; 
	}
	// Array for relative errors
	double[] rel_arr = new double[err_step.size];
	for(int j=0; j<err_step.size; j++) rel_arr[j]=tol[j]/err_step[j];

	// Check if step is crap
	bool crap = false; // Of course I hope that the step works
	for(int j=0; j<tol.size; j++){
		if(rel_arr[j]<1.0){
			crap= true;
			break;
		}
	}
	
	// If step is good, move the starting point for the next iteration to the end point of the current
	if(!crap){
		a+=h;
		ya = y_next;
		ts.Add(a);
		ys.Add(ya);
	}

	// Calculating the next step size, using eq. 40 
	h *= Pow(rel_arr.Min(),0.25)*0.95;
	} //End of while-loop
	return Tuple.Create(ts,ys);
}

/*
// Operator overload to make my life easier. Well, until I got error CS0563
public static List<complex> operator*(complex a, List<complex> b){
	List<complex> res = new List<complex>();
	foreach(complex thing in b)res.Add(a*thing);
	return res;}
public static List<complex> operator*(double a, List<complex> b){
	List<complex> res = new List<complex>();
	foreach(complex thing in b)res.Add(a*thing);
	return res;}
public static List<complex> operator-(List<complex> a, List<complex> b){
	List<complex> res = new List<complex>();
	for(int i=0; i<a.Count; i++)res.Add(a[i]-b[i]);
	return res;}
*/

// Create a complex stepper NOTE: So far only for 1st order ODE
public static complex crkstep45_single(Func<complex, complex, complex> f, complex z0, complex y0, complex h, double err){
	
	complex k_0 = f(z0,y0);
	complex k_1 = f(z0 + h/4.0, y0+h/4.0*k_0);
	complex k_2 = f(z0+h*3.0/8, y0 + h*3.0/32*k_0 + h*9.0/32*k_1);
	complex k_3 = f(z0+h*12.0/13, y0 + h*1932.0/2197*k_0 - h*7200.0/2197*k_1 + h*7296.0/2197*k_2);
	complex k_4 = f(z0+h, y0 + h*439.0/216*k_0 - h*8.0*k_1 + h*3680.0/513*k_2 - h*845.0/4104*k_3);
	complex k_5 = f(z0+h/2.0, y0 - h*8.0/27*k_0 + h*2.0*k_1 - h*3544.0/2565*k_2 + h*1859.0/4104*k_3 - h*11/40*k_4);
	complex k_sum = 16.0/135*k_0 + 6656.0/12825*k_2 + 28561.0/56430*k_3 - 9.0/50*k_4 + 2.0/55*k_5;

	// The embedded lower order method made with b_i^* instead of b_i
	complex k_star = 25.0/216*k_0 + 1408.0/2565*k_2 + 2197.0/4104*k_3 - 1.0/5*k_4;
	err = abs(h*(k_sum - k_star));
	//for(int j=0; j<err.Count; j++)err[j]=Abs(err[j]);
	
	complex y_next = y0 + h*k_sum;
	return y_next;
}

public static Tuple<List<complex>, List<complex>, int, List<double>> cdrive_single(Func<complex,complex, complex> f, complex a, complex b, complex ya, complex h= default(complex), double acc=1e-4, double eps=1e-4, int nmax=100000){
	// Return lists. I use lists because arrays and vectors don't play nice with complex
	List<complex> zs = new List<complex>(){a};
	List<complex> ys = new List<complex>(){ya};
	List<double> errs = new List<double>();
	int nsteps =0;
	if(h.Equals(default(complex)))h=(b-a)/1000; // Starting guesstimate of stepsize
	double dist = abs(b-a);
	double newdist = abs(b-a)-acc;
	do{
		if(abs(b-a)<acc)break; // End statement
		if(nsteps>nmax){
			Write("Error: Did not converge within iteration limit\n");
			break;
		}
		if(abs(a+h)>abs(b-a))h=b-a;
		nsteps++;
		double err=0; // = new complex();
		complex y_next = crkstep45_single(f,a,ya,h,err);
	
		double tol = abs(sqrt(h/(b-a)))*(eps*abs(y_next)+acc);
		if(err==0)err=tol/4; 
		// Relative error, and checking for bad steps
		double rel_arr = tol/err;
		bool crap = false;
		if(rel_arr<1.0)crap=true;

		if(!crap){
			dist = abs(b-a);
			a+=h;
			newdist = abs(b-a);
			ya = y_next;
			zs.Add(a);
			ys.Add(ya);
			errs.Add(rel_arr);
		}

		// Calculating the next step size, using eq. 40 
		h = h*(Pow(rel_arr,0.25)*0.95);
	}while(newdist<dist); //End of while-loop


	return Tuple.Create(zs,ys, nsteps, errs);
}

// Create driver/stepper that treats real and imaginary parts separately before recombining
public static Tuple<List<complex>, List<complex>, int, List<double>> cdrive_split(Func<complex,complex, complex> f, complex a, complex b, complex ya, complex h= default(complex), double acc=1e-4, double eps=1e-4, int nmax=100000){
	// Return lists. I use lists because arrays and vectors don't play nice with complex
	List<complex> zs = new List<complex>(){a};
	List<complex> ys = new List<complex>(){ya};
	List<double> errs = new List<double>();
	int nsteps=0;
	complex ar = new complex(a.Re,0); complex ai = new complex(0,a.Im);
	complex br = new complex(b.Re,0); complex bi = new complex(0,b.Im);
	if(h.Equals(default(complex)))h=(b-a)/1000;
	double dist = abs(b-a);
	double newdist = abs(b-a)-acc;

	do{
		if(abs(b-a)<acc)break; // End statement
		if(nsteps>nmax){
			Write("Error: Did not converge within iteration limit\n");
			break;
		}
		if(abs(a+h)>abs(b-a))h=b-a;
		nsteps++;
		double err=0; // = new complex();
		complex y_re = crkstep45_single(f,ar,ya,h,err);
		double tol_re = abs(sqrt(h/(br-ar)))*(eps*abs(y_re)+acc);
		complex y_im = crkstep45_single(f,ai,ya,h,err);
		double tol_im = abs(sqrt(h/(bi-ai)))*(eps*abs(y_im)+acc);
		complex y_next = new complex(y_re.Re, y_im.Im);
		double tol = Sqrt(Pow(tol_re,2)+Pow(tol_im,2));
	
		if(err==0)err=tol/4; 
		// Relative error, and checking for bad steps
		double rel_arr = tol/err;
		bool crap = false;
		if(rel_arr<1.0)crap=true;

		if(!crap){
			dist = abs(b-a);
			a+=h;
			newdist = abs(b-a);
			ya = y_next;
			zs.Add(a);
			ys.Add(ya);
			errs.Add(rel_arr);
		}

		// Calculating the next step size, using eq. 40 
		h = h*(Pow(rel_arr,0.25)*0.95);
	}while(newdist<dist); //End of while-loop





	return Tuple.Create(zs, ys, nsteps, errs);
}
}


