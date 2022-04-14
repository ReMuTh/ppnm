using System;
using static System.Math;

class main{

static void Main() {

WL(@"HOMEWORK 10-montecarlo

Testing plain montecarlo routine

Let's start out simple and integrate f(x,y) = x*y^2
in the area 0<=x<=2, y<=0<=1, 10.000 samples");

(double int1, double error1) = plain_mc(vec=>vec[0]*vec[1]*vec[1],new vector(0,0),new vector(2,1),10000);

WL($"Result:   {(int1):n4} ± {error1:n4}");
WL($"Analytical:  2/3 = 0.66666");
test(approx(int1,2.0/3,error1));

WL(@"
Let's do a halfsphere. Function that is zero except within the unit circle
where it forms half a unit sphere. Graph looks like a Verner Panton panel from the 1960s
1.000.000 samples in the area -1<x<1, -1<y<1");


(double int2, double error2) = plain_mc(panton,new vector(-1,-1),new vector(1,1),1000000);
WL($"Result: {int2:n4} ± {error2:n4}");
WL($"Analytical: 2/3*PI = 2.0943");
test(approx(int2,2.0/3*PI,error2));


} // main

static double panton(vector v) {
	double norm_sq = v[0]*v[0] + v[1]*v[1];
	if (norm_sq <= 1) return Sqrt(1-norm_sq);
	else return 0;
}



static (double,double) plain_mc(Func<vector,double> f,vector a,vector b,int N){
		int dim=a.size;
		// Assuming all elements of b are bigger than a.
		// Integration volume is an n-dim cube, so calculating the volume is simple
		double V =((b-a).prod());

		var x = new vector(dim);

		double fx,sum=0,sum_sq=0;
        for(int i=0;i<N;i++) {
                	x.random(a,b);
                	fx = f(x);
                	sum += fx; sum_sq+=fx*fx;
                }
        double mean=sum/N, sigma=Sqrt(sum_sq/N-mean*mean);
        return (mean*V,V*sigma/Sqrt(N));
} // plain_mc


static void WL(string s="") {
	System.Console.WriteLine(s);
} // lazy write

static void test(bool condition) {
	if (condition) WL("PASSED");else WL("FAILED");
} // test

static bool approx(double a, double b, double acc=1e-6, double eps=1e-6){
	if(Abs(a-b)<acc)return true;
	if(Abs(a-b)/Max(Abs(a),Abs(b)) < eps)return true;
	return false;
} // approx
} // main class