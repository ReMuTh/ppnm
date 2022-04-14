
using System;
using static System.Math;
using static rk45;

class main{


static void Main(){


WL("HOMEWORK 9-ODE PART A");
WL("Trying my luck with the renowned FORTRAN rkf45 stepper");

WL(@"
Implimented two different drivers.
1. driver_ab integrates the ODE from a to b and delivers y and the number of integration steps used.
2. driver_ab_incr that does the same but returns genlists with all the integration steps.
To reproduce graph data like in the scipy.odeint we of course want to use the latter,
since driver_ab would wastefully do the iteration all the way from a over and over for each datapoint.");

WL(@"
TEST 1:
driver_ab on the simple 1D uncoupled example

y' = 1 + y^2

Tan(x) satisfies this, so initiated with y(0)=0 the ODE should yield tan(x) values,
becoming increasingly difficult as x approaches PI/2.
So trying a=0, b=1.5, acc=1e-5, eps=1e-5. Initial h=0.1
(if h was constant, b would be reached in 15 steps)");

vector y0 = new vector(0.0);
double b = 1.5;
double h0 = 0.1;

(vector yb,int steps) = driver_ab(tan_as_ode,0,b,y0,h0,1e-5,1e-5);

WL($"\nResult is y({b})={yb[0]} reached in {steps} steps.");
WL($"Comparison: tan({b}) = {Tan(b)}");
WL($"Relative error: {yb[0]/Tan(b)-1:G3}      Absolute error: {yb[0]-Tan(b):G3} ");

WL("We notice that the driver shifts towards more granular steps than the inital h.");

WL(@"
TEST 2:
Arrive at Eulers constant using

y' = y from y(0) = 1

a=0, b=1, acc=1e-7, eps=1e-7. Initial h=0.001");

y0 = new vector(1.0);
b = 1.0;
h0 = 0.001;
(yb,steps) = driver_ab((x,y) => y,0,b,y0,h0,1e-5,1e-5);
WL($"\nResult is y({b})={yb[0]} reached in {steps} steps.");
WL($"Comparison: e = {E}");
WL($"Relative error: {yb[0]/E-1:G3}      Absolute error: {yb[0]-E:G3} ");

WL("We notice that the driver shifts towards more coarse steps than the inital h.");


WL(@"
TEST 3:
Handling vector arguments to solve y'' = -y
With boundary condition y(0) = 0, y'(0) = 1 the solution should be sin(x).
Double derivative packaged as
y1' = y2
y2' = -y1
y2 then becomes cos x with same boundary conditions");

y0 = new vector(0.0,1.0);
b = PI;
h0 = 0.01;
(yb,steps) = driver_ab(sin_as_ode,0,b,y0,h0,1e-5,1e-5);

WL($"\nResult is y(PI)={yb[0]},{yb[1]} reached in {steps} steps.");
WL($"Comparison: sin(PI)=0, cos(PI)=-1");
WL($"Absolute errors {yb[0]:G3} Absolute error: {yb[1]+1:G3}");

WL(@"
TEST 4:
Running the same ODE as in TEST 3 through driver_ab_incr to retrieve the increments.odeint.
a = 0, b = 2*PI, h0 = 0.5, acc=1e-3, eps=1e-3.");
(var xlist,var ylist) = driver_ab_incr(sin_as_ode,0,2*PI,y0,0.5,1e-3,1e-3);

WL($"\nIntegrated from x=0 to x=2*PI in {xlist.size} steps.\nSaving to datafile. plot shown in ode_sincos.svg.");

WL(@"The takeaway from the plot is just how few increments rk45 needs to do a full cycle
of sin and cos to good accuracy.");

var outstream=new System.IO.StreamWriter("ode_sincos_data.txt");
outstream.WriteLine($"x\ty1(x)\ty2(x)");

for (int i=0;i<xlist.size;i++) {
	vector y = ylist[i];
	outstream.WriteLine($"{xlist[i]}\t{y[0]}\t{y[1]}");	
}
outstream.Close();

WL(@"
TEST 5:
Replicate friction pendulum example from scipy.integrate.odeint
Not much to say, we have the tools.
Defining the functions and running with a higher accuracy goal,
acc=1e-7, eps=1e-7.
mostly to obtain more integration points for the plots.");

y0= new vector(PI-0.1,0.0);
(xlist,ylist) = driver_ab_incr(pend,0,10,y0,0.1,1e-7,1e-7);

WL($"\nIntegrated from t=0 to t=10 in {xlist.size} steps.\nSaving to datafile");

outstream=new System.IO.StreamWriter("pend_fric_data.txt");
outstream.WriteLine($"t\ttheta(x)\tomega(t)");

for (int i=0;i<xlist.size;i++) {
	vector y = ylist[i];
	outstream.WriteLine($"{xlist[i]}\t{y[0]}\t{y[1]}");	
}
outstream.Close();

}

public static vector pend(double t,vector y) {
	double b = 0.25,c=5.0;
	// y[0] is theta
	vector dydt = new vector(2);
	dydt[0] = y[1];
	dydt[1] = -b*y[1]-c*Sin(y[0]);
	return dydt;
}

public static vector tan_as_ode(double x,vector y) {

	vector dydx = new vector(y[0]*y[0]+1);
	return dydx;
}


public static vector sin_as_ode(double x,vector y) {
	vector dydx = new vector(2);
	dydx[0] = y[1];
	dydx[1] = -y[0];
	return dydx;
}


static void WL(string s="") {
	System.Console.WriteLine(s);

} // lazy write


}
