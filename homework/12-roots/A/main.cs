using System;
using static Roots;
using static System.Math;


class main{

static void Main() {
	WL("homework 12-roots part A:");

	WL("\nFirst testing root finder on a simple 1D system: (x-3)^3+17 = 0. ");
	WL("Analytival solution is 3-17^(1/3)) = ");

	WL("First with x0=[2.0]");

	vector x0 = new vector(3.0);
    vector solution = NewtonRaphson(oneDtest,x0);
    judge(solution,new vector(3-Pow(17,1.0/3)));

	WL("\nNext same but with x0=[3.0]. This is challenging since f'(x0) = 0,\nso naïvely the NewtonRaphson will take an infinate step. I've tried implimenting a circumvention to this situation");

	x0 = new vector(2.0);
    solution = NewtonRaphson(oneDtest,x0);
    judge(solution,new vector(3-Pow(17,1.0/3)));

    WL("Very well, lets try a 2D case. Extrenum of Rosenbrock's valley function f(x,y) = (1-x)^2 + 100(y-x^2)^2");
    WL("The derivative (gradient) is done Analytically f'(x,y) = [2*(200*x^3 - 200*x*y + x - 1), 200*(y - x^2)]");
    x0 = new vector(0.25,0.25);
    x0.print("x0");
    solution = NewtonRaphson(twoDtest,x0,1e-4,0);
    judge(solution,new vector(1.0,1.0));
    
    WL("It works, however the method gets stuck if x0=[0,0]. Hmm. Will return to see if I can remedy this. ");


}

static vector oneDtest(vector v) {
	vector ret = new vector(Pow(v[0]-3,3)+17);
	return ret;

}

static vector twoDtest(vector v) {
	vector ret = new vector(200*v[0]*v[0]*v[0] - 200*v[0]*v[1] + v[0] - 1, 200*(v[1] - v[0]*v[0]));
	return ret;
}


static bool approx(double a, double b, double acc=1e-4, double eps=1e-4){
	if(Abs(a-b)<acc)return true;
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


static void WL(string s="") {
	System.Console.WriteLine(s);
} // lazy write


}