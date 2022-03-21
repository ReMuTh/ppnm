using static System.Console;
using System;

class main{
	static void Main(){
		// make sin(kx) delegate
		double k = 1;  // defining k as double for generality (even though only integer k's are needed in the exercise)
		System.Func<double,double> sinkx = delegate(double x){return Math.Sin(k*x);};	
	
		WriteLine("\nSin(x) from 0 to 2π in 21 steps");
		table.make_table(sinkx,0,Math.PI*2,Math.PI/10);

		k = 2;
		WriteLine("\nSin(2x) from 0 to 2π in 21 steps");
		table.make_table(sinkx,0,Math.PI*2,Math.PI/10);

		k = 3;
		WriteLine("\nSin(3x) from 0 to 2π in 21 steps");
		table.make_table(sinkx,0,Math.PI*2,Math.PI/10);

	}
}
