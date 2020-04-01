// (C) 2020 Dmitri Fedorov; License: GNU GPL v3+; no warranty.
using System;
public partial class matrix{

public readonly int size1, size2;
public double[][] data;

public matrix(int n, int m){
	size1=n; size2=m; data = new double[size2][];
	for(int j=0;j<size2;j++) data[j]=new double[size1];
	}

public double this[int r,int c]{
	get{return data[c][r];}
	set{data[c][r]=value;}
	}

public vector this[int c]{
	get{return (vector)data[c];}
	set{data[c]=(double[])value;}
	}

public matrix(string s){
        string[] rows = s.Split(';');
        size1 = rows.Length;
        size2 = rows[0].Split(',',' ').Length;
        data = new double[size2][];
	for(int j=0;j<size2;j++) data[j]=new double[size1];
        for(int i=0;i<size1;i++){
                string[] ws = rows[i].Split(',',' ');
                for(int j=0; j<size2; j++){
//                        this[i,j]=System.Convert.ToDouble(ws[j]);
                        this[i,j]=double.Parse(ws[j]);
                        }
                }
        }

public static matrix operator+ (matrix a, matrix b){
	matrix c = new matrix(a.size1,a.size2);
	for(int j=0;j<a.size2;j++)
		for(int i=0;i<a.size1;i++)
			c[i,j]=a[i,j]+b[i,j];
	return c;
	}

public static matrix operator-(matrix a){
	matrix c = new matrix(a.size1,a.size2);
	for(int j=0;j<a.size2;j++)
		for(int i=0;i<a.size1;i++)
			c[i,j]=-a[i,j];
	return c;
	}

public static matrix operator- (matrix a, matrix b){
	matrix c = new matrix(a.size1,a.size2);
	for(int j=0;j<a.size2;j++)
		for(int i=0;i<a.size1;i++)
			c[i,j]=a[i,j]-b[i,j];
	return c;
	}

public static matrix operator/(matrix a, double x){
	matrix c=new matrix(a.size1,a.size2);
	for(int j=0;j<a.size2;j++)
		for(int i=0;i<a.size1;i++)
			c[i,j]=a[i,j]/x;
	return c;
}

public static matrix operator*(double x, matrix a){ return a*x; }
public static matrix operator*(matrix a, double x){
	matrix c=new matrix(a.size1,a.size2);
	for(int j=0;j<a.size2;j++)
		for(int i=0;i<a.size1;i++)
			c[i,j]=a[i,j]*x;
	return c;
}

public static matrix operator* (matrix a, matrix b){
        var c = new matrix(a.size1,b.size2);
        for (int k=0;k<a.size2;k++)
        for (int j=0;j<b.size2;j++)
		{
                double bkj=b[k,j];
                var cj=c.data[j];
                var ak=a.data[k];
		int n=a.size1;
                for (int i=0;i<n;i++){
                        //c[i,j]+=a[i,k]*b[k,j];
                      cj[i]+=ak[i]*bkj;
                	}
        	}
        return c;
        }

public static vector operator* (matrix a, vector v){
	var u = new vector(a.size1);
	for(int k=0;k<a.size2;k++)
	for(int i=0;i<a.size1;i++)
		u[i]+=a[i,k]*v[k];
	return u;
	}

public matrix(vector e) : this(e.size,e.size) { for(int i=0;i<e.size;i++)this[i,i]=e[i]; }

public void set(int r, int c, double value){ this[r,c]=value; }
public static void set(matrix A, int i, int j, double value){ A[i,j]=value; }
public double get(int i, int j){ return this[i,j]; }
public static double get(matrix A, int i, int j){ return A[i,j]; }

public matrix rows(int a, int b){
  matrix m = new matrix(b-a+1,size2);
  for(int i=0;i<m.size1;i++)
	for(int j=0;j<m.size2;j++)
    		m[i,j]=this[i+a,j];
  return m;
}

public matrix cols(int a, int b){
  matrix m = new matrix(size1,b-a+1);
  for(int i=0;i<m.size1;i++)for(int j=0;j<m.size2;j++)
    m[i,j]=this[i,j+a];
  return m;
  }

public void set_identity(){ this.set_unity(); }
public void set_unity(){
	for(int i=0;i<size1;i++){
		this[i,i]=1;
		for(int j=i+1;j<size2;j++){
			this[i,j]=0;this[j,i]=0;
		}
	}
}

public void set_zero(){
	for(int j=0;j<size2;j++)
		for(int i=0;i<size1;i++)
			this[i,j]=0;
	}

public static matrix outer(vector u, vector v){
	matrix c = new matrix(u.size,v.size);
	for(int i=0;i<c.size1;i++)for(int j=0;j<c.size2;j++) c[i,j]=u[i]*v[j];
	return c;
}

public matrix copy(){
	matrix c = new matrix(size1,size2);
	for(int j=0;j<size2;j++)
		for(int i=0;i<size1;i++)
			c[i,j]=this[i,j];
	return c;
	}


public matrix T{
		get{return this.transpose();}
		set{}
}

public matrix submatrix(int ia, int ib, int ja, int jb){
	matrix m = new matrix(ib-ia+1,jb-ja+1);
	for(int i=ia;i<=ib;i++)
	for(int j=ja;j<=jb;j++) m[i-ia,j-ja]=this[i,j];
	return m;
}

public matrix transpose(){
	matrix c = new matrix(size2,size1);
	for(int j=0;j<size2;j++)
		for(int i=0;i<size1;i++)
			c[j,i]=this[i,j];
	return c;
	}

public void print(){print("");}
public void print(string s){
	System.Console.WriteLine(s);
	for(int ir=0;ir<this.size1;ir++){
	for(int ic=0;ic<this.size2;ic++)
		System.Console.Write("{0,9:F3} ",this[ir,ic]);
		System.Console.WriteLine();
		}
	}

public static bool double_equal(double a, double b, double eps=1e-6){
	if(System.Math.Abs(a-b)<eps)return true;
	if(Math.Abs(a-b)/(Math.Abs(a)+Math.Abs(b)) < eps/2)return true;
	return false;
}

public bool equals(matrix B,double eps=1e-6){
	if(this.size1!=B.size1)return false;
	if(this.size2!=B.size2)return false;
	for(int i=0;i<size1;i++)
		for(int j=0;j<size2;j++)
			if(!double_equal(this[i,j],B[i,j],eps))
				return false;
	return true;
}

// Method for QR-decomposition via Gram-Schmidt ortogonalization
// Note: A[row,column] = A[column][row]
public static void qr_gs_decomp(matrix A, matrix R){
	//Assert.IsTrue(R.size1 == A.size1 && R.size2 == R.size1);
	// Constructing Q and R
	matrix Q = new matrix(A.size1, A.size2);
	
	for(int i = 0; i<A.size2; i++){
		R[i,i] = Math.Sqrt(A[i].dot(A[i]));
		Q[i] = A[i]/R[i,i];
		for(int j = i+1; j<A.size2; j++){
			R[i,j] = Q[i].dot(A[j]);
			A[j] -= Q[i] * Q[i,j];
		}
	}

	// Replacing A with Q
        for(int i = 0; i<Q.size1; i++){
                for(int j = 0; j<Q.size2; j++){
                        A[i,j] = Q[i,j];
                }
        }
		
}

// Method to write out matrix elements w. 2 decimal points, if wanted
public static void print2(matrix Q){
	for(int i = 0; i<Q.size1; i++){
                for(int j = 0; j<Q.size2; j++){
                        System.Console.Write("{0:f2}\t", Q[i,j]);
                }
                System.Console.Write("\n");
        }
}
// Method to write out matrix elements w. 16 decimal points, if wanted
public static void print16(matrix Q){
	for(int i = 0; i<Q.size1; i++){
                for(int j = 0; j<Q.size2; j++){
                        System.Console.Write("{0:f16}\t", Q[i,j]);
                }
                System.Console.Write("\n");
        }
}



}//matrix

