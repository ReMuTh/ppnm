using System;
using static System.Math;

class main{
static void Main(){

	// var ma=new matrix(3,2);
	// ma.print();

	int n = 11;
	int m = 3;
	int max1 = 999;

	WL("Homework least sqared\n\n PART A.\nAFFIRMING that QRGS works for tall matrices:");

	var A = random_int_matrix(n,m,max1);	
	A.print($"\nCreating {n}-by-{m} matrix, A of random integers between {-max1} and {max1}");

	WL("\nPerforming QR decomposisition");
	var TestQR = new QRGS(A);

	TestQR.Q.print("\nThis is Q.");

	TestQR.R.print("\nThis is R. Should be upper triangular.");
	if (TestQR.R.is_upper_triangular()) WL("PASSED");else WL("FAILED");


	matrix QR = TestQR.Q*TestQR.R.copy();
	QR.print("\nThis is Q*R. Should be equal to A");

	if (A.approx(QR)) WL("PASSED");else WL("FAILED");

	matrix QTQ = TestQR.Q.T*TestQR.Q;
	var Id = new matrix(m,m);
	Id.setid();
	QTQ.print("\nThis is QT*Q. Should be identity matrix (proving U is unitary)");
	if (Id.approx(QTQ)) WL("PASSED");else WL("FAILED");

	WL("\nSTILL PART A\nTesting leastsq routing using Rutherford and Soddy ThX decay data on y(t) = A*exp(-λ*t) expectation.");

	vector time = new vector("1.0,  2.0,  3.0, 4.0, 6.0, 9.0,   10.0,  13.0,  15.0");
	vector act = new vector("117,100,88,72,53,29.5,25.2,15.2,11.1");
	vector d_act = new vector("5,5,5,5,5,1,1,1,1");

	time.print("Time [days]   ");
	act.print("Activity [arb]");
	d_act.print("Uncertainty  ");
	

	vector y = act.map(Log);
	y.print("\nTaking log of activity data to transform problem to a linear fit problem ln y = ln a - λ*t\n");
	vector dy = d_act/act;

	var fit_fs = new Func<double,double>[] { t => 1.0, t => t};

	(vector c,matrix cov) = leastsq(fit_fs,time,y,dy);

	c.print("\nFit Coefficients: a, -λ\n");

	cov.print("\nPART B:\nRoutine also outputs covariant matrix");

	// Uncertainties of the fit coeffs is the square root of the diaginal elements
	vector d_c = cov.diag().map(Sqrt);

	d_c.print("\nUncertainty of coeeffs a, λ are the square root of the diagonal elements in the covariant matrix:");
	WL($"\nThe decay constant is {-c[1]:n3} ± {d_c[1]:n3} per day");
	WL($"\nHalf life: {-Log(2)/c[1]:n1} ± {Log(2)*d_c[1]/(c[1]*c[1]):n1} days");
	WL(@"\nModern value is 	3.6319 ± 0.0023 days (Source Wikipedia).
		The uncertainty intervals of the modern and 1902 value do not overlap.
		However there may be errors in the 1902 data that we haven't taken into account");

	vector f_act = time.map(t => Exp(c[0]+t*c[1]));

	vector f_act_hi = time.map(t => Exp(c[0]+d_c[0]+t*(c[1]+d_c[1])));
	vector f_act_lo = time.map(t => Exp(c[0]-d_c[0]+t*(c[1]-d_c[1])));

	WL("\nWriting plot data file");
	WL("\nPart C:\nIncluding columns with fit curves based on high and low estimates of fit parameters");

	var outstream=new System.IO.StreamWriter("thx_data.txt");
	outstream.WriteLine($"#Time (days)	Activity(Arb)	ΔAct	Log(act)	ΔLog(activity)	Log(Act) Fit");

	for(int i = 0;i<time.size;i++) {
	
		outstream.WriteLine($"{time[i]}	{act[i]}	{d_act[i]}	{f_act[i]}	{f_act_hi[i]}	{f_act_lo[i]}");
	}
	outstream.Close();


}

static (vector,matrix) leastsq (Func<double,double>[]fs,vector x,vector y,vector dy){
	// fs is the array of fit functions
	// x is the list of independent variables
	// y are the corresponding datapoints
	// dy is the uncertainties of the items in y
	int n = x.size;
	int m = fs.Length;

	// Formulating the the least sq as a linear equation problem
	var A = new matrix(n,m);
	var b = new vector(n);
	// Contructing A and b as in the note equation 14.

	for(int i=0; i<n; i++){
		b[i]=y[i]/dy[i];
			for(int j=0;j<m;j++) {
					A[i,j]=fs[j](x[i])/dy[i];
				} 
		}

	var gsqr1 = new QRGS(A);
	// the solution vector c contains optimized coefficients of the input functions
	vector c = gsqr1.solve(b);
	// The covariance matrix as in the note eq. 22, the inverse of A.T*A.
	// Involves a new instance of GSQR

	matrix ATA = A.T * A;

	var gsqr2 = new QRGS(ATA);
	matrix Cov = gsqr2.inverse();

	return(c,Cov);
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


