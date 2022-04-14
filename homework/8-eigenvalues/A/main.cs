using System;
using static System.Math;

class main{
static void Main(){

	WL("Homework Eigenvalues\nPART A");
	int n = 11;
	matrix A = random_sym_matrix(n,99);

	A.print($"Testing jacobi routines routines on A, {n}-by-{n} random symmetric matrix");

	WL("If A is symmetric then A^T = A");
	test(A.approx(A.T));

	
	WL("\nComparing timesJ and Jtimes routines with matrix multiplications with literal constructed Jacobi rotation matrix");
	var r = new Random();
	int p = r.Next(0,n-1);
	int q = r.Next(p+1,n-1);
	double theta=0.5*Atan2(2*A[p,q],A[q,q]-A[p,p]);

	matrix J = new matrix(n,n);
	J.setid();
	J[p,p]=J[q,q]= Cos(theta);
	J[p,q]= Sin(theta);
	J[q,p]= -J[p,q];

	J.print($"J with p={p} q={q}. Angle to eliminate matrix element ({p},{q}) in A is theta: {theta:G4}:");
	matrix AJ = A*J;
	AJ.print("A*J by literal matrix mult is:");
	var Jac = new Jacobi(A);
	Jac.timesJ(Jac.D,p,q,theta);
	Jac.D.print("timesJ(A,p,q,theta) result is:");
	test(AJ.approx(Jac.D));

	matrix JTAJ=J.T*AJ;
	JTAJ.print("J^T*S*J by literal matrix mult is:");
	Jac.Jtimes(Jac.D,p,q,-theta);
	Jac.D.print("timesJ(A,p,q,-theta) result is:");
	test(JTAJ.approx(Jac.D));

	WL($"Are elements ({p},{q}) and ({q},{p}) indeed ≈ 0?");
	test(vector.approx(Jac.D[p,q],0));
	test(vector.approx(Jac.D[q,p],0));

	WL("\nReinitialize and proceed with diagonalize");
	Jac = new Jacobi(A);
	(matrix D, matrix V) = Jac.diagonalize();

	D.print($"Result after {Jac.num_sweeps} sweeps:\nD = ");
	V.print("V = ");

	WL("\n Is D indeed diagonal?");
	test(D.is_diagonal());

	matrix VTAV = V.T*A*V;

	VTAV.print("\nV.Checking T*A*V – should be equal to Eigenvalue matrix D");
	test(VTAV.approx(D));

	matrix VDVT = V*(D*V.T);
	VDVT.print("\nChecking V*D*V.T – should be equal to original matrix A");
	test(VDVT.approx(A));

	matrix I = new matrix(n,n);
	I.setid();
	matrix VTV = V.T*V;
	VTV.print("\nChecking V.T*V – should be Id matrix");
	test(VTV.approx(I));

	matrix VVT = V*V.T;
	VVT.print("\nV*V.T should also be Id matrix");
	test(VVT.approx(I));

}


static matrix random_sym_matrix(int n,int max = 999) {

	var random = new Random();

	matrix rsm = new matrix(n,n);
	for(int i=0;i<n;i++) {
		for(int j=i;j<n;j++) {
			rsm[i,j] = random.Next(-max, max);
			rsm[j,i] = rsm[i,j];
			//diagonal elements gets writtes twice but alas…
		}

	}

	return rsm;
} // random_int_matrix

static void WL(string s="") {
	System.Console.WriteLine(s);

} // lazy write

static void test(bool condition) {
	if (condition) WL("PASSED");else WL("FAILED");
}

} // class main


