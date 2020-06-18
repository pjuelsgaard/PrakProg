// Must be used together with matrix class
using System;
public partial class diag{
public static int jacobian(matrix A, matrix V, vector e){
	// A must be symmetric
	int p, q, sweep =0;
	int n = A.size1;
	
	// Create V matrix, and initialize it as the identity matrix
	//matrix V = matrix(n, n);
	for(int i = 0; i<n; i++)for(int j = 0; j<n; j++){
		if(i==j){V[i,j]=1.0;}
		else{V[i,j]=0.0;}
	}
	
	//vector e = vector(n);
	for(int i = 0; i<n; i++)e[i]=A[i,i];
	bool change;
	do{
		sweep++;
		for(int i = 0; i<n; i++)e[i]=A[i,i];
		for(p=0; p<n; p++){
			//e[p] = A[p,p];
			for(q=p+1; q<n;q++){
			operation(A,V,p,q);
			}
		}
		
		change = vectorchange(A,e);
	}while(change);
return sweep;
}

public static bool vectorchange(matrix A, vector e){
	vector e_temp = new vector(A.size2);
	for(int i =0; i<A.size1; i++)e_temp[i]=A[i,i];
	return !e_temp.approx(e);
}


public static void operation(matrix A, matrix V, int p, int q){
	int n = A.size1;
	// Saving the old values for App, Aqq and Apq, and using these to ocmpute phi
	double[] Aold = {A[p,p], A[q,q], A[p,q]};
	double phi = Math.Atan2(2*Aold[2], Aold[1]-Aold[0])/2;
	double c = Math.Cos(phi);
	double s = Math.Sin(phi);

	// Creating the new values for App, Aqq and Apq (well, not really with the last one)
	//double[] Anew = new double[2];
	A[p,p] = c*c*Aold[0] - 2*s*c*Aold[2] + s*s*Aold[1];
	A[q,q] = s*s*Aold[0] + 2*s*c*Aold[2] + c*c*Aold[1];
	A[p,q] = s*c*(Aold[0]-Aold[1])+(c*c-s*s)*Aold[2];

	// Updates entries p,0->p,p and q,0->q,q
	for(int i=0; i<p;i++){          	
        	double A_ip = A[i,p];
        	double A_iq = A[i,q];
        	A[i,p] = c*A_ip-s*A_iq;
        	A[i,q] = c*A_iq+s*A_ip;
	}
	// Updates entries p,p->p,q and p,q->q,q. It utilizes the symmetry A_iq=A_qi
	for(int i=p+1; i<q;i++){
		double A_pi = A[p,i];
		double A_iq = A[i,q];
		A[p,i] = c*A_pi-s*A_iq;
		A[i,q] = s*A_pi+c*A_iq;
	}
	// Updates entries p,q->p,n and q,q->q,n
	for(int i=q+1;i<n;i++){
		double A_pi = A[p,i];
		double A_qi = A[q,i];
		A[p,i] = c*A_pi-s*A_qi;
		A[q,i] = s*A_pi+c*A_qi;
	}
	// Updating the values of the eigenvectors
	for(int i=0;i<n; i++){
		double V_ip = V[i,p];
		double V_iq = V[i,q];
		V[i,p] = c*V_ip-s*V_iq;
		V[i,q] = s*V_ip+c*V_iq;
	}
}




// Value-by-value method
public static int jacobian_partial(matrix A, matrix V, vector e, int deg=0){
	if(deg==0 || deg < A.size1 ){
		return jacobian(A,V,e);
	}
	int sweep=0, n=A.size1;


	V.set_unity(); // Sets V matrix to identity matrix
	vector ev = new vector(n); // Vector for eigenvalues
	double tol = 1e-6; // Tolerance for evaluating whether the algorithm has converged
	
	// Actually does stuff
	for(int p = 0; p<deg; p++){
		bool change; // Figure out some way to define change happening
		do{
			sweep++;
			e[p] = A[p,p];
			for(int q= p+1; q<n; q++){
				operation(A,V,p,q);
			}
			// If old eigenval is outside of tolerance interval around new eigenval, there has been significant change
			change = e[p]<(A[p,p]-tol) | e[p] > (A[p,p]+tol);
		}while(change); // As long as there are significant changes to the eigenvalues, keep sweeping across the same row
	}

	return sweep;

}

}
