using System;
using static System.Math;
using static Minimization;

class main{

static void Main() {

	// var Multimin = new Minimization();

	WL("homework 13-minimization part A:");
	WL("Implimented Quasi-Newton minimization method with SR1 update");
	WL("\nTest 1. Find a minimum of the Rosenbrock's valley function:");

	vector x0 = new vector(4,5);
	x0.print("Start point:");

	vector ros_min = Minimization.qnewton(Rosenbruck,x0,1e-12);

	judge(ros_min,new vector(1,1));

	WL("(Using a pretty crude approx with acc=eps=1e-3)");


	WL("\nTest 1. Find local minimum of the Himmelblau's function:");

	x0 = new vector(4,5);
	x0.print("Start point:");
	vector him_min = Minimization.qnewton(Himmelblau,x0,1e-12);
	judge(him_min,new vector(3.584428,-1.848125));

	x0 = new vector(4,2);
	x0.print("Start point:");
	him_min = Minimization.qnewton(Himmelblau,x0,1e-12);
	judge(him_min,new vector(3,2));

	x0 = new vector(-3,4);
	x0.print("Start point:");
	him_min = Minimization.qnewton(Himmelblau,x0,1e-12);
	judge(him_min,new vector(-2.805118,3.131312));

	x0 = new vector(-3,-4);
	x0.print("Start point:");
	him_min = Minimization.qnewton(Himmelblau,x0,1e-12);
	judge(him_min,new vector(-3.779310,-3.283186));

	WL("\nThe routine finds the local minima, but starting points need to be rather close to identify them.");
}

static double Rosenbruck(vector x) {
	return Pow(1-x[0],2) + 100 * Pow(x[1]-x[0]*x[0],2);
}

static double Himmelblau(vector x) {
	return Pow(x[0]*x[0]+x[1]-11,2)+Pow(x[0]+x[1]*x[1]-7,2);
}

static void WL(string s="") {
	System.Console.WriteLine(s);
} // lazy write


static bool approx(double a, double b, double acc=1e-3, double eps=1e-3) {
	if(Abs(a-b)/Max(Abs(a),Abs(b)) < eps)return true;
	return false;
} // approx

static void judge(vector result,vector reference) {
	result.print("Result is:");
	reference.print("Reference is:");
	bool passed = true;

	for (int i=0;i<reference.size;i++) {
		if (!approx(result[i],reference[i])) passed = false;
	}

	if (passed) WL("PASSED");
	else WL("FAILED");
}

}