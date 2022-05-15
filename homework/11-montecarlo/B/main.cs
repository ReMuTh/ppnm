using System;
using static System.Math;
using static montecarlo;

class main{

static void Main() {

WL(@"HOMEWORK 10-montecarlo
PART B

Testing quasirandom montecarlo routine on the same functions as in PART A
All run with 10.000 samples.


TEST 1:
f(x,y) = x*y^2, 0<=x<=2, y<=0<=1, 10.000 samples");

(double int_plain, double error_plain) = plain_mc(vec=>vec[0]*vec[1]*vec[1],new vector(0,0),new vector(2,1),100000);
(double int_qr, double error_qr) =    quasirand_mc(vec=>vec[0]*vec[1]*vec[1],new vector(0,0),new vector(2,1),100000);
double expected = 2.0/3;
WL($"Analytical result: 2/3 = {expected:n5}");
WL($"Result plain montecarlo: {(int_plain):n5} ± {error_plain:n5}");
WL($"Deviation from analytical result {int_plain-expected:n5}");
test(approx(int_plain,expected,error_plain));
WL($"Result quasirand mc:     {(int_qr):n5} ± {error_qr:n5}");
WL($"Deviation from analytical result {int_qr-expected:n5}");
test(approx(int_qr,expected,error_qr));
WL($"Ratio between plain and quasirand sigmas: {error_plain/error_qr:n0}");

WL(@"
TEST 2:
Unit halfsphere. -1<x<1, -1<y<1");

(int_plain, error_plain) = plain_mc(panton,new vector(-1,-1),new vector(1,1),100000);
(int_qr, error_qr) = quasirand_mc(panton,new vector(-1,-1),new vector(1,1),100000);
expected = 2.0/3*PI;
WL($"Analytical result: 2/3*PI = {expected:n5}");
WL($"Result plain montecarlo: {(int_plain):n5} ± {error_plain:n5}");
WL($"Deviation from analytical result {int_plain-expected:n5}");
test(approx(int_plain,expected,error_plain));
WL($"Result quasirand mc:     {(int_qr):n5} ± {error_qr:n5}");
WL($"Deviation from analytical result {int_qr-expected:n5}");
test(approx(int_qr,expected,error_qr));
WL($"Ratio between plain and quasirand sigmas: {error_plain/error_qr:n0}");


WL(@"
TEST 3:
(PI*PI*PI)^-1 * (1-Cos(x)*Cos(y)*Cos(z))^-1 integrated in 0<x<π, 0<y<π, 0<z<π");

(int_plain, error_plain) = plain_mc(singular,new vector(0,0,0),new vector(PI,PI,PI),100000);
(int_qr, error_qr) =  quasirand_mc(singular,new vector(0,0,0),new vector(PI,PI,PI),100000);
expected = 1.3932039296856768591842462603255;
int_plain /= (PI*PI*PI);error_plain /= (PI*PI*PI);
int_qr /= (PI*PI*PI);error_qr /= (PI*PI*PI);

WL($"Analytical result: Γ(1/4)^4/(4π^3) = {expected:n5}");
WL($"Result plain montecarlo: {(int_plain):n5} ± {error_plain:n5}");
WL($"Deviation from analytical result {int_plain-expected:n5}");
test(approx(int_plain,expected,error_plain));
WL($"Result quasirand mc:     {(int_qr):n5} ± {error_qr:n5}");
WL($"Deviation from analytical result {int_qr-expected:n5}");
test(approx(int_qr,expected,error_qr));
WL($"Ratio between plain and quasirand sigmas: {error_plain/error_qr:n0}");


WL(@"
Conclusion from the tests is that for TEST1 and TEST2 the quasirandom method comes much closer to the analytical results.
The sigma estimated by two different runs is pretty on the mark.

The higly singular in TEST3 is still challenging.
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