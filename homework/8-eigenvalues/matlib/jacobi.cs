/*
 Rene Thalund jacobi class

 [i][j] addressing is column i row j
 [i,j] addressing is ROW i, column j (other way aroung)
 n is counting rows
 m is counting column
*/
using System;
using static System.Math;

public  class Jacobi{
	public matrix D,V;
	public int n;
	public int num_sweeps;
	public int num_rotations;

	public Jacobi(matrix A) {
		if(A.size1 != A.size2) throw new ArgumentException("Jacobi class only on square matrix");
		// Saving local copy of a
		this.D = A.copy();
		this.n = A.size1;

		this.V = new matrix(this.n,this.n);
		this.V.setid();

	} // Constructor

	public (matrix,matrix) diagonalize(){
		this.cycle();
		return (this.D,this.V); 
	}

	// To comply with exercise, functions Jtimes and timesJ accepts angle theta.
	// However the meat of the diaginalization is done with subfunctions
	// Jtimes_cs and timesJ_cs
	// In this way Sin(theta) and Cos(theta) is only calculated once per
	// rotation during the sweeps instead of four times.

	public void timesJ(matrix A,int p,int q,double theta) {
		double c = Cos(theta), s = Sin(theta);
		timesJ_cs(A, p,q,c,s);
	}

	public void Jtimes(matrix A,int p,int q,double theta) {
		double c = Cos(theta), s = Sin(theta);
		Jtimes_cs(A, p,q,c,s);
	}


	public void timesJ_cs(matrix A,int p,int q,double c,double s) {
		for(int i=0;i<this.n;i++) {
			// pull these two elements out, because we're going to overwrite them
			// but still need both for second calculation
			double Aip = A[i,p], Aiq = A[i,q];
			A[i,p] = c*Aip - s*Aiq;
			A[i,q] = s*Aip + c*Aiq;
		}
		// this only becomes O(n) complex
	}


	public void Jtimes_cs(matrix A,int p,int q,double c,double s) {
		for(int j=0;j<this.n;j++) {
			// pull these two elements out, because we're going to overwrite them
			// but still need both for second calculation
			double Apj = A[p,j], Aqj = A[q,j];
			A[p,j] =  c*Apj + s*Aqj;
			A[q,j] = -s*Apj + c*Aqj;
		}
	}


	public void cycle() {
		vector last_diag=new vector(this.n);
		this.num_sweeps = 0;
		this.num_rotations = 0;
		while(true){
			if (this.D.diag.approx(last_diag)) break;
			last_diag = this.D.diag;
			this.sweep();
			this.num_sweeps++;
		} // while until break
		Console.WriteLine($"Sweeps {this.num_sweeps} Rots {this.num_rotations}");

	} // cycle

	public void sweep() {

		for(int p=0;p<n-1;p++) for(int q=p+1;q<n;q++) this.make_zero(p,q);

	}

	public void make_zero(int p,int q) {
		double theta=0.5*Atan2(2*this.D[p,q],this.D[q,q]-this.D[p,p]);
		double c=Cos(theta),s=Sin(theta);
		this.timesJ_cs(this.D,p,q, c,s);
		this.Jtimes_cs(this.D,p,q, c,-s); // J^T is just flipping the sign on the s elements
		this.timesJ_cs(this.V,p,q, c,s);
		this.num_rotations += 3;
	}
}


