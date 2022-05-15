using System;
using static System.Math;
using static montecarlo;

class main{

static void Main() {

WL(@"HOMEWORK 10-montecarlo

Testing plain montecarlo routine

TEST 1:
Let's start out simple and integrate f(x,y) = x*y^2
in the area 0<=x<=2, y<=0<=1, 10.000 samples");

(double int1, double error1) = plain_mc(vec=>vec[0]*vec[1]*vec[1],new vector(0,0),new vector(2,1),10000);
double expected1 = 2.0/3;
WL($"Analytical result:  2/3 = {expected1:n4}");

WL($"Montecarlo Result:   {(int1):n4} ± {error1:n4}");
WL($"Deviation from analytical result {int1-expected1:n5}");

test(approx(int1,expected1,error1));

WL(@"
TEST 2:
Let's do a halfsphere. Function that is zero except within the unit circle
where it forms half a unit sphere. Graph looks like a Verner Panton panel from the 1960s
1.000.000 samples in the area -1<x<1, -1<y<1");


(double int2, double error2) = plain_mc(panton,new vector(-1,-1),new vector(1,1),1000000);
double expected2 = 2.0/3*PI;
WL($"Analytical result: 2/3*PI = {expected2:n4}");
WL($"Montecarlo Result:   {int2:n4} ± {error2:n4}");
WL($"Deviation from analytical result {int2-expected2:n5}");

test(approx(int2,expected2,error2));


WL(@"
TEST 3:
The more challenging singular function from the exercise

(PI*PI*PI)^-1 * (1-Cos(x)*Cos(y)*Cos(z))^-1 integrated in 0<x<π, 0<y<π, 0<z<π

For efficiency reasons, there is no reason to calculate the (PI*PI*PI)^-1
factor in every function evaluation, as both output and error estimation
scales with this front factor. So just multiplying it after the fact.
10.000.000 evaluations.
");


(double int3, double error3) = plain_mc(singular,new vector(0,0,0),new vector(PI,PI,PI),10000000);
double expected3 = 1.3932039296856768591842462603255;
int3 /= (PI*PI*PI);
error3 /= (PI*PI*PI);
WL($"Analytical resuls: Γ(1/4)^4/(4π^3) = {expected3:n4}");
WL($"Montecarlo result: {int3:n4} ± {error3:n4}");
WL($"Deviation from analytical result {int3-expected3:n5}");

test(approx(int3,expected3,error3));

WL(@"
Conclusion from the tests is that while the integrations certainly hits in the 
ballpark of the correct result, they often underestimate the error.
");

} // main

static double singular(vector v) {
	return 1/((1-Cos(v[0])*Cos(v[1])*Cos(v[2])));

}

static double panton(vector v) {
	double norm_sq = v[0]*v[0] + v[1]*v[1];
	if (norm_sq <= 1) return Sqrt(1-norm_sq);
	else return 0;
}


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