using System;
using static System.Math;
// using static rk45;

class main{


static void Main(){
WL(@"HOMEWORK 9-ODE PART B

1. Improve driver to check the acceptence condition for every component of y etc.
This is implimented in the lib that ran also for the examples in A.

The debugging naturally included checking that  the driver behaviour was identical
in the 1D cases, pre/post the code update.

In the 2D cases the updated driver took more steps to complete. This is expected since
the element wise tolerance criterium is stricter.

2. Storing the points in my own Genlist class was also implimented and used in A,
since it's the natural way to output for plots.

Running the entire intgrator from a to x over and over for multiple x'es is wasteful.

The sourcecode is in ../lib/rk45.cs

USEFUL FUTURE IMPROVEMENTS:
double maxstep; maximum stepsize the driver will use.

xlist as argument - give x values that you'd like outputs for.
The driver will then start from a and use each element in xlist as a 'b'
to evaluate. The corresponding ylist is collected.

This option is highly useful if you're using a system of coupled ODEs 
as model function to fit to a dataset of experiment data.
The output ylist can then be flattened and least-square optimized to corresponding flattened
experimental data. (the optimizing parameter are the coupling parameters in the ODE)


");
}

static void WL(string s="") {
	System.Console.WriteLine(s);

} // lazy write
} // class main