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
public static complex crkstep45_single(Func<complex, List<complex>, List<complex>> f, complex z0, List<complex> y0, complex h, double err){
	
	List<complex> k_0 = f(z0,y0);
	List<complex> k_1 = f(z0 + h/4, y0+h/4*k_0);
	List<complex> k_2 = f(z0+h*3/8, y0 + h*3/32*k_0 + h*9/32*k_1);
	List<complex> k_3 = f(z0+h*12/13, y0 + h*1932/2197*k_0 - h*7200/2197*k_1 + h*7296/2197*k_2);
	List<complex> k_4 = f(z0+h, y0 + h*439/216*k_0 - h*8*k_1 + h*3680/513*k_2 - h*845/4104*k_3);
	List<complex> k_5 = f(z0+h/2, y0 - h*8/27*k_0 + h*2*k_1 - h*3544/2565*k_2 + h*1859/4104*k_3 - h*11/40*k_4);
	List<complex> k_sum = 16.0/135*k_0 + 6656.0/12825*k_2 + 28561.0/56430*k_3 - 9.0/50*k_4 + 2.0/55*k_5;

	// The embedded lower order method made with b_i^* instead of b_i
	List<complex> k_star = 25.0/216*k_0 + 1408.0/2565*k_2 + 2197.0/4104*k_3 - 1.0/5*k_4;
	err = h*(k_sum - k_star);
	for(int j=0; j<err.Count; j++)err[j]=Abs(err[j]);
	
	List<complex> y_next = y0 + h*k_sum;
	return y_next;
}

public static Tuple<List<complex>, List<complex>> cdrive_single(Func<complex,List<complex>, List<complex>> f, complex a, complex b, List<complex> ya, complex h= default(complex), double acc=1e-4, double eps=1e-4){
	// Return lists. I use lists because arrays and vectors don't play nice with complex
	List<complex> zs = new List<complex>(){a};
	List<complex> ys = new List<complex>(){ya[0]};
	int nsteps =0;
	if(h==default(complex))h=(b-a)/1000; // Starting guesstimate of stepsize
	while(true){
		if(abs(b-a)<acc)break; // End statement
		nsteps++;
		List<complex> err = new List<complex>();
		complex y_next = crkstep45_single(f,a,ya,h,err);
		break;
	}

	return Tuple.Create(zs,ys);
}

}

public class cvector{
// Operator overload to make my life easier. Well, until I got error CS0563
public static List<complex> times(this List<complex> b, complex a){
	List<complex> res = new List<complex>();
	foreach(complex thing in b)res.Add(a*thing);
	return res;}
public static List<complex> times(this List<complex> b, double a){
	List<complex> res = new List<complex>();
	foreach(complex thing in b)res.Add(a*thing);
	return res;}
public static List<complex> minus(this List<complex> a, List<complex> b){
	List<complex> res = new List<complex>();
	for(int i=0; i<a.Count; i++)res.Add(a[i]-b[i]);
	return res;}
}


/*
public static Tuple<List<complex>, List<List<complex>>> cdriver(Func<complex, List<complex>, List<complex>> f, complex a, complex b, List<complex> y0, double h=0 , double acc=1e-6, double eps=1e-6){
	List<complex> zs = new List<complex>(){a};
	List<List<complex>> ys = new List<List<complex>>(){y0};
	int nsteps = 0;

	complex dir = b-a; // Direction along which the ODE is solved

	// First, we translate the system, so the new starting point a' is in 0+i0. This is possible because it is a straight line along which the ODE is solved
	complex a_pretrans = a;
	complex b_pretrans = b;
	a -= a_pretrans;
	b -= a_pretrans;

	// Then we rotate the system, so the line over which we solve is the real line (ie. z.Im=0 forall z)
	double phi;
	if(b.Im > 0)phi = Acos(b.Re/abs(b));
	else phi = -Acos(b.Re/abs(b));
	complex b_prerot = b;
	complex rot = new complex(Cos(-phi), Sin(-phi));
	complex rotback = new complex(Cos(phi), Sin(phi)); // For rotating values back at the end
	b*= rot;

	// NOTE: The parts marked with (???) are preeeetty sketchy, but it's the best hack I could think of
	// ???Translating and rotating initial conditions???
	vector ya = new vector(y0.Count);
	for(int j=0; j<ya.size; j++){
		ya[j] = ((y0[j]-a_pretrans)*rot).Re;
	}
	Write($"{b.Im}\n");
	
	if(h==0)h=b.Re/1000.0;
	// Loop until we are within acc of b
	while(a.Re<b.Re){

		// ???Translate and rotate ODE???
		Func<double, vector, vector> g = delegate(double q, vector v){
			complex zg = q*complex.One*rotback+a_pretrans;
			List<complex> yg = new List<complex>();
			for(int j=0; j<v.size; j++){
				yg.Add(v[j]*complex.One*rotback+a_pretrans);
			}
			List<complex> resg = f(zg, yg);
			vector res = new vector(resg.Count);
			for(int j=0; j<resg.Count; j++){
				res[j] = ((resg[j]-a_pretrans)*rot).Re;
			}
			return res;
		};


		nsteps++;
		vector err = new vector(ya.size);
		vector y_next = crkstep45(g,a.Re,ya,h,err);

		//Errors
		vector tol = new vector(ya.size);
		for(int j=0; j<y_next.size; j++){
			tol[j] = Sqrt(h/(a.Re-b.Re))*(eps*Abs(y_next[j]) + acc);
			if(err[j]==0)err[j]=tol[j]/4;
		}
		// Relative error
		double[] rel_arr = new double[err.size];
		for(int j=0; j<err.size; j++)rel_arr[j]=tol[j]/err[j];

		// Check for bad step
		bool crap =false;
		for(int j=0; j<rel_arr.Length; j++){
			if(rel_arr[j]<1.0){
				crap=true;
				break;
			}
		}
		if(!crap){
			a+=h;
			ya = y_next;
			zs.Add(a*rotback + a_pretrans);
			List<complex> yAdd = new List<complex>();
			for(int j=0; j<ya.size; j++){
				yAdd.Add(ya[j]*rotback+a_pretrans);
			}
			ys.Add(yAdd);
		}

		// Calculating the next step size, using eq. 40 
		h *= Pow(rel_arr.Min(),0.25)*0.95;
	} //End of while-loop
	
	return Tuple.Create(zs,ys);

	/*
	if(h.Equals(complex.Zero)){
		h = dir/1000.0;

	}
	// Loop goes on until we are within acc of b
	while( cmath.abs(b-a) > acc){
		if(abs(b-a) < abs(h))h=abs(b-a); // Make sure we end on b
		nsteps++;
		// I treat the imaginary and real parts separately
		double ar = a.Re;
		double ai = a.Im;

		vector err_R = new vector(ya.size);
		vector err_I = new vector(ya.size);
		vector err_step = new vector(ya.size);
		vector y_nextR = crkstep45(f,a,ya,h.Re, err_R, "R");
		vector y_nextI = crkstep45(f,a,ya,h.Im, err_I, "I");
		// Errorstuffs
		vector tol = new vector(ya.size);
		for(int j=0; j<ya.size; j++){
			complex y_next = new complex(y_nextR[j], y_nextI[j]);
			complex err_c = new complex(err_R[j], err_I[j]);
			err_step[j] = abs(err_c);
			tol[j] = abs(sqrt(h/(b-a)))*(eps* abs(y_next)+acc);
			if(err_step[j] == 0)err_step[j] = tol[j]/4;
		}
		// Relative errors
		double[] rel_arr = new double[err_step.size];
		for(int j=0; j<err_step.size; j++) rel_arr[j] = tol[j]/err_step[j];

		// Check for bad steps
		bool crap=false;
		for(int j=0; j<tol.size; j++){
			if(rel_arr[j]<1.0){
				crap=true;
				break;
			}
		}
		vector y_nextstep = new vector(ya.size);
		// Since the y vector always should contain both real and imaginary parts, ya.size will always be even
		for(int j=0;j<ya.size/2; j++)y_nextstep[j]=y_nextR[j];
		for(int j=ya.size/2;j<ya.size; j++)y_nextstep[j]=y_nextI[j];

		// For good steps we move to next step
		if(!crap){
			a+=h;
			ya = y_nextstep;
			zs.Add(a);
			ys.Add(ya);
		}

		// Calculating next step size;
		//h.Re *= Pow(rel_arr.Min(), 0.25)*0.95;
		//h.Im *= Pow(rel_arr.Min(),0.25)*0.95;
		if(nsteps>10000)break;
	}// End while loop
	Write($"It took {nsteps} steps\n");
	return Tuple.Create(zs, ys);
}
	*/


