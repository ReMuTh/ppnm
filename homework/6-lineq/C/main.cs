using System;

class main{
static void Main(){

	int n_min = 5;
	int n_max = 500;
	int dn = 5;
	int max1 = 999;

	var stopwatch = new System.Diagnostics.Stopwatch();
	for (int n=n_min; n<=n_max; n+=dn) {
		stopwatch.Reset();
		matrix A = random_int_matrix(n,n,max1);
		stopwatch.Start();
		var timeQRGS = new QRGS(A);
		stopwatch.Stop();
		var lap = stopwatch.ElapsedMilliseconds;
		WL($"{n} {lap}");
	}
}

static matrix random_int_matrix(int n,int m,int max = 1000) {

	var random = new Random();

	matrix rim = new matrix(n,m);
	for(int i=0;i<n;i++) {
		for(int j=0;j<m;j++) {
			rim[i,j] = random.Next(-max, max);
		}

	}

	return rim;
} // random_int_matrix

static void WL(string s="") {
	System.Console.WriteLine(s);

} // lazy write

}