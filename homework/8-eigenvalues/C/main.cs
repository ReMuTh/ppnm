using System;
using static System.Math;

class main{


static void Main(){

	int n_min = 5;
	int n_max = 225;
	int dn = 5;
	int max1 = 999;

	var stopwatch = new System.Diagnostics.Stopwatch();
	for (int n=n_min; n<=n_max; n+=dn) {
		stopwatch.Reset();
	    matrix A = random_sym_matrix(n,max1);
		var Jac = new Jacobi(A);
		stopwatch.Start();
		(matrix D, matrix V) = Jac.diagonalize();
		stopwatch.Stop();
		var lap = stopwatch.ElapsedMilliseconds;
		WL($"{n} {lap}");
	}
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


