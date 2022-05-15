using System;
using static System.Console;

public class main {
	
	public static void Main() {
	    double[] x = {1,2,3,4,5};
	    double[] y1 = {1,1,1,1,1};
	    double[] y2 = {1,2,3,4,5};
	    double[] y3 = {1,4,9,16,25};

	    WriteLine("TESTING quadspline coefficients");

	   	WriteLine("\nTEST 1. Constant function");
	   	print_coord("x",x);
	    print_coord("y",y1);
	    WriteLine("\nWe expect all b and c spline coefficients to be zero");

	   	var s1 = new quadspline(x,y1);
	   	print_coord("b",s1.b);
	    print_coord("c",s1.c);

	    WriteLine("\nTEST 2. Linear function");
	   	print_coord("x",x);
	    print_coord("y",y2);
	    WriteLine("\nWe expect all b coefficients to be 1, and all c to be 0");

	   	var s2 = new quadspline(x,y2);
	   	print_coord("b",s2.b);
	    print_coord("c",s2.c);

	    WriteLine("\nTEST 3. Quadratic function");
	   	print_coord("x",x);
	    print_coord("y",y3);
	    WriteLine("\nWe expect b coeffs to be linearly rising {2,4,6,8} and all c coefficients to be 1.");

	   	var s3 = new quadspline(x,y3);
	   	print_coord("b",s3.b);
	    print_coord("c",s3.c);

	}

	static void print_coord(string name, double[] x) {
		Write($"{name} = ");
		Write("{");
		for (int i=0;i<x.Length-1;i++) {
			Write($"{x[i]}, ");
		}
		Write($"{x[x.Length-1]}");
		Write("}\n");
	}
	

}
