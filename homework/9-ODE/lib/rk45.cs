// Two different drivers.
// driver_ab integrates the ODE from a to b and delivers y
// driver_ab_incr that does the same but returns arrays (matrices) with all the integration steps.
using System;
using static System.Math;

public struct rk45 {

public static (vector,int) driver_ab( Func<double,vector,vector> f,
// return y(b) and the number of integration steps used
	double a, double b, vector ya,
	double h=0.01, double acc=0.01, double eps=0.01){
	int n = ya.size;
	double x=a;
	vector y=ya;
	vector tol = new vector(n);
	vector ervabs = new vector(n);
	int c=0;
	while(true) {
	        if(x>=b) break;
	        if(x+h>b) h=b-x;
	        (vector yh,vector erv) = rk45step(f,x,y,h);
	        //  We've seen mentioned as using Max. But if both the acc and eps conditions areto be fulfilled     
 			//	shouldn't we then compare errors to the smaller of the two?
	        for(int i=0;i<n;i++) tol[i]=Min(acc,Abs(yh[i])*eps)*Sqrt(h/(b-a));
	        ervabs = erv.abs(); // we need this twice
	        if((ervabs-tol).max() < 0) { x+=h; y=yh; } // accept if every component of erv.abs()-tol is negative
	        double factor = (tol/ervabs).min();
			h *= Min(Pow(factor,0.25)*0.95 , 2); // adjust stepsize*/
			c++;
	        } // loop until break
	return (y,c);
} //driver



public static (genlist<double>,genlist<vector>) driver_ab_incr( Func<double,vector,vector> f,
	double a, double b, vector ya,
	double h=0.01, double acc=0.01, double eps=0.01){
	var xlist = new genlist<double>();
	var ylist = new genlist<vector>();
	int n = ya.size;
	double x=a;
	vector y=ya;
	vector tol = new vector(n);
	vector ervabs = new vector(n);
	int c=0;
	while(true) {
			xlist.push(x);
			ylist.push(y);
	        if(x>=b) break;
	        if(x+h>b) h=b-x;
	        (vector yh,vector erv) = rk45step(f,x,y,h);
	        for(int i=0;i<n;i++) tol[i]=Min(acc,Abs(yh[i])*eps)*Sqrt(h/(b-a));
	        ervabs = erv.abs();
	        if((ervabs-tol).max() < 0) { x+=h; y=yh; } // accept if every component of erv.abs()-tol is negative
	        double factor = (tol/ervabs).min();
			h *= Min(Pow(factor,0.25)*0.95 , 2); // adjust stepsize*/
			c++;
			// if (c>10000) {System.Console.Write("STUCK");break;}
	        } // loop until break
	return (xlist,ylist);
} //driver


// Trying my luck with the renowned FORTRAN rkf45 stepper
// I think there could be a performance boost in defining the rational coeffiecients from the butchers 
// table outside the function. If using them literal as coefficients inside the function means the divisions
// would maybe be carried out every time the stepper is called. Perhaps the compiler is optimized in a way
// that would remember the values but this feels safer.
// Not defining coefficients that are 0 or 1…

static double c2=1.0/4,		a21=1.0/4;
static double c3=3.0/8,		a31=3.0/32,			a32=9.0/32;
static double c4=12.0/13,	a41=1932.0/2197,	a42=-7200.0/2197,	a43=7296.0/2197;
static double 				a51=439.0/216,		a52=-8.0,			a53=3680.0/513,		a54=-845.0/4104;
static double c6=0.5,		a61=-8.0/27,		a62=2.0,			a63=-3544.0/2565,	a64=1859.0/4104,	a65=-11.0/40;
static double 				b1=16.0/135,							b3=6656.0/12825,	b4=28561.0/56430,	b5=-9.0/50,		b6=2.0/55;
static double 				bs1=25.0/216,							bs3=1408.0/2565,	bs4=2197.0/4104,	bs5=-1.0/5;


public static (vector,vector) rk45step(Func<double,vector,vector> f, double x, vector y, double h){
	vector k1 = h*f(x,y);
	vector k2 = h*f(x+c2*h, y +a21*k1);
	vector k3 = h*f(x+c3*h, y +a31*k1 +a32*k2);
	vector k4 = h*f(x+c4*h,	y +a41*k1 +a42*k2 +a43*k3);
	vector k5 = h*f(x+h,    y +a51*k1 +a52*k2 +a53*k3 +a54*k4);
	vector k6 = h*f(x+c6*h, y +a61*k1 +a62*k2 +a63*k3 +a64*k4 +a65*k5);

	// Embedded fourth order step
	vector yh4 = y+(bs1*k1 + bs3*k3 + bs4*k4 + bs5*k5);

	// Fifth order step
	vector yh5 = y+(b1*k1 + b3*k3 + b4*k4 + b5*k5 + b6*k6);

	// Error vector
	vector err = yh5 - yh4;

	return (yh5,err);
}





}