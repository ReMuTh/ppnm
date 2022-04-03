using System;

class main{
static void Main(){

	// var ma=new matrix(3,2);
	// ma.print();

	int m = 11;
	int max1 = 999;
	int max2 = 99;
	
	matrix A = random_int_matrix(m,m,max2);	
	A.print($"\nHomework lineq B\nCreating {m}-by-{m} matrix, A of random integers between {-max2} and {max2}");

	WL("\nPerforming QR decomposisition.");
	var InvQR = new QRGS(A);
	matrix B = InvQR.inverse();

	B.print($"\nB is the inverse of A:");

	matrix check = A*B;
	matrix Id = new matrix(m,m);
	Id.setid();

	check.print($"\nChecking A * B. Should be identity");
	if (Id.approx(check)) WL("PASSED");else WL("FAILED");

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