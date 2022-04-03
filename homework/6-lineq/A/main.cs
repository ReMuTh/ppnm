using System;

class main{
static void Main(){

	// var ma=new matrix(3,2);
	// ma.print();

	int n = 19;
	int m = 7;
	int max1 = 999;
	int max2 = 99;

	var T = random_int_matrix(n,m,max1);	
	T.print($"Homework A, part 1\nCreating {n}-by-{m} matrix of random integers between {-max1} and {max1}");

	WL("\nPerforming QR decomposisition");
	var TestQR = new QRGS(T);

	TestQR.Q.print("\nThis is Q.");

	TestQR.R.print("\nThis is R. Should be upper triangular.");
	if (TestQR.R.is_upper_triangular()) WL("PASSED");else WL("FAILED");


	matrix QR = TestQR.Q*TestQR.R.copy();
	QR.print("\nThis is Q*R. Should be equal to A");

	if (T.approx(QR)) WL("PASSED");else WL("FAILED");

	matrix QTQ = TestQR.Q.T*TestQR.Q;
	var Id = new matrix(m,m);
	Id.setid();
	QTQ.print("\nThis is QT*Q. Should be idendity matrix (U is unitary)");
	if (Id.approx(QTQ)) WL("PASSED");else WL("FAILED");


	var A = random_int_matrix(m,m,max2);	
	A.print($"\nHomework lineq A, part 2\nCreating {m}-by-{m} matrix, A of random integers between {-max2} and {max2}");

	WL("\nRunning QR decomposisition");
	var SolveQR = new QRGS(A);

	// Abusing random matrix function and graceful vector object column return from matrix class
	vector b = random_int_matrix(m,1,max1)[0];
	b.print($"\nCreating {m} length vector, b, of random integers between {-max1} and {max1}: ");
		
	vector x = SolveQR.solve(b);

	x.print($"\nSolving for Ax = b. x is: ");

	vector Ax = A * x;
	Ax.print("\nThis is A*x. Should be equal to b");
	if (Ax.approx(b)) WL("PASSED");else WL("FAILED");

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