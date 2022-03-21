using System;
using static System.Console;
using static System.Math;

class main{

public static double erf(double z){
	Func<double,double> f = x => Exp(-x*x);
	return integrate.quad(f,0,z)*2/Sqrt(PI);
}

public static void Main(){
	for(double x=-3;x<=3;x+=1.0/16)
		WriteLine($"{x} {erf(x)}");
	}
}
