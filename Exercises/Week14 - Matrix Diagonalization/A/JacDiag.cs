// Must be used together with matrix class
using System;
public partial class diag{
	public static int jacobian(matrix A, matrix V, vector e){
		// A must be symmetric
		int p, q, change, sweep =0;
		int n = A.size1;
		// Create V matrix, and initialize it as the identity matrix
		//matrix V = matrix(n, n);
		for(int i = 0; i<n; i++)for(int j = 0; j<n; j++){
			if(i==j){V[i,j]=1.0;}
			else{V[i,j]=0.0;}
		}
		
		//vector e = vector(n);
		for(int i = 0; i<n; i++)e[i]=A[i,i];

		do{
			change=0;
			sweep++;
			for(p=0; p<n; p++)for(q=p+1; q<n;q++){
				// Array of [A_pp, A_qq, A_pq]
				double[] Aold = {e[p], e[q], A[p,q]};
				double phi = Math.Atan2(2*Aold[2], Aold[1]-Aold[0])/2;
				double c = Math.Cos(phi);
				double s = Math.Sin(phi);
				// Array for [A_pp'. A_qq', A_pq']
				double[] Anew = new double[3];
				Anew[0] = c*c*Aold[0] - 2*s*c*Aold[2]+s*s*Aold[1];
				Anew[1] = s*s*Aold[0]+2*s*c*Aold[2]+c*c*Aold[1];
				Anew[2] = s*c*(Aold[0]-Aold[1])+(c*c-s*s)*Aold[2];
				// Start updating matrix A
				if(Anew[0]!=Aold[0] || Anew[1]!=Aold[1]){
					change=1;
					e[p] = Anew[0];
					e[q] = Anew[1];
					A[p,q]=0.0;
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
					for(int i=0;i<n; i++){
						double V_ip = V[i,p];
						double V_iq = V[i,q];
						V[i,p] = c*V_ip-s*V_iq;
						V[i,q] = s*V_ip+c*V_iq;
					}
				}
			}
		}while(change!=0);
	return sweep;
}
}
