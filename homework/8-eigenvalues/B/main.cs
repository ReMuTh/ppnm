using System;
using static System.Math;

class main{
static void Main(){

	WL("Homework Eigenvalues\nPART B. Hydrogen s wave");

	// building vector of analytical levels
	vector E_ana = new vector(5);
	for (int n=1;n<=5;n++) E_ana[n-1] = -0.5*1/(n*n);

	solve_hydrogen(10,0.3,2);
	WL("Analytical bound energies in a.u are 1/(2*n^2)");
	WL("Two bound states are found. The ground state is found to decent accuracy, n=2 is more than 10% off");

	WL("\nDoubling r_max and halfing dr both leads to twice the side n of Hamiltionian");
	WL("Let's see which improves result the most");

	solve_hydrogen(20,0.3,1);
	WL("Larger r_max not surprisingly finds more bound levels. Error in n=2 improves a lot, error in n=1 is changed by very little");

	solve_hydrogen(10,0.15,1);
	WL("Smaller dr yields a much more precise n=1 value, but does not reveal new levels");

	WL("\nCranking up both to return acceptable values for n=1,2,3,4");
	(vector E_bound, vector r, matrix V) = solve_hydrogen(40,0.10,1);


	WL("The function vectors in V are discrete normalized. To have them integrate to unity, they're multiplied with sqrt(1/dr).");


	WL(@"

Writing datafiles for four bound states, both matrix solutions and analytical.

In the plots I notice fine resemblance to the analytical functions for n=1,2.
For n=3 the signs are opposite, which can expectedly happen, -f3 can take the place of f3 as ortho-normal eigenfunction.
For n=4 the matrix solution is forced to 0 at r=40 by the boundary condition. The analytical f4 still has amplitude at r=40. ");

	var outstream1=new System.IO.StreamWriter("hydrogen_matrix_data.txt");
	var outstream2=new System.IO.StreamWriter("hydrogen_analytic_data.txt");

	outstream1.WriteLine($"#r (a.u)	f1(r)	f2(r)	f3(r)	f4(r)");
	outstream2.WriteLine($"#r (a.u)	f1(r)	f2(r)	f3(r)	f4(r)");

	for(int i = 0;i<r.size;i++) {
		outstream1.WriteLine($"{r[i]}	{V[i,0]}	{V[i,1]}	{V[i,2]}	{V[i,3]}");
		outstream2.WriteLine($"{r[i]}	{rrf(r[i],1)}	{rrf(r[i],2)}	{rrf(r[i],3)}	{rrf(r[i],4)}");
	}
	outstream2.Close();
	outstream1.Close();

}

static (vector,vector,matrix) solve_hydrogen(double r_max=20,double dr=0.2,int verbose=0) {

	int npoints = (int)(r_max/dr)-1;

	if(verbose >= 1) WL($"\nBuilding Hamiltonian matrix with r_max = {r_max}, dr = {dr}, n = {npoints}");


	vector r = new vector(npoints);
	for(int i=0;i<npoints;i++)r[i]=dr*(i+1);
	matrix H = new matrix(npoints,npoints);
	for(int i=0;i<npoints-1;i++){
		matrix.set(H,i,i,-2);
 		matrix.set(H,i,i+1,1);
		matrix.set(H,i+1,i,1);
	}
	matrix.set(H,npoints-1,npoints-1,-2);
	H*=-0.5/dr/dr;
	for(int i=0;i<npoints;i++)H[i,i]+=-1/r[i];

	if(verbose >= 3) H.print("H:");
	else if(verbose >= 2) H.submatrix(0,10,0,10).print("Top-left 10x10 of H:");

	var Hyd = new Jacobi(H);
	(matrix D,matrix V) = Hyd.diagonalize();

	vector E = D.diag;
	int num_bound = 0;
	while(E[num_bound]<0) num_bound++;
	vector E_bound = new vector(num_bound);
	for(int i=0;i<num_bound;i++) E_bound[i]=E[i];

	if(verbose >= 1) {
		E_bound.print("Bound energies [a.u]:   ");
		// building vector of analytical levels
		vector E_ana = new vector(num_bound);
		for (int n=1;n<=num_bound;n++) E_ana[n-1] = -0.5*1/(n*n);
		E_ana.print("Analytical values [a.u]:");
		// vector diff = 
		(E_bound/E_ana).print("Relative difference:    ");
	}
	// The 

	return (E_bound,r,V.submatrix(0,npoints-1,0,num_bound-1)*Sqrt(1/dr));

}

// Reduced radial function
static double rrf(double r,int order) {
	double rho = 2*r/order;
	if(order == 1) return r*Exp(-rho/2)*2;
	if(order == 2) return r*Exp(-rho/2)*1/(2*Sqrt(2))*(2-rho);
	if(order == 3) return r*Exp(-rho/2)*1/(9*Sqrt(3))*(6-6*rho+rho*rho);
	if(order == 4) return r*Exp(-rho/2)*1/96*(24-36*rho+12*rho*rho-rho*rho*rho);
	return 0;

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


