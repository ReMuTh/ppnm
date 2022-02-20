using static System.Console;
using System;

class main{
	static void Main(){

		WriteLine($"Testing various results from complex class:");


		complex minus_one = new complex(-1,0);
		complex sqrt_minus_one = cmath.sqrt(minus_one);

		WriteLine($"\nSquare root of minus one:");
		WriteLine($"cmath.sqrt(new complex(-1,0)) = {sqrt_minus_one}");
		WriteLine($"cmath.I has Re=0 and Im=1: {cmath.I}");
		WriteLine($"Compare using approx method: {sqrt_minus_one.approx(cmath.I)}");
		
		complex sqrt_i = cmath.sqrt(cmath.I);
		complex sqrt_i_man = new complex (cmath.sqrt(0.5),cmath.sqrt(0.5));
		
		WriteLine($"\nSquare root of i:");
		WriteLine($"cmath.sqrt(c.math.I) = {sqrt_i}");
		WriteLine($"Manual constructed result with Re = Im = √0.5: {sqrt_i_man}");		
		WriteLine($"Compare using approx method: {sqrt_i.approx(sqrt_i_man)}");

		complex exp_i = cmath.exp(cmath.I);
		complex exp_i_man = new complex(cmath.cos(1),cmath.sin(1));

		WriteLine($"\nexp(i):");
		WriteLine($"cmath.exp(cmath.I) = {exp_i}");
		WriteLine($"Manually constructed result with Re=cos(1) and Im=sin(1): {exp_i_man}");		
		WriteLine($"Compare using approx method: {exp_i.approx(exp_i_man)}");

		complex exp_i_pi = cmath.exp(cmath.I * Math.PI);
		complex exp_i_pi_man = new complex(-1,0);

		WriteLine($"\nexp(iπ) (Euler's formula):");
		WriteLine($"cmath.exp(cmath.I * Math.PI) = {exp_i_pi}");
		WriteLine($"Manually constructed result with Re=-1 and Im=0: {exp_i_pi_man}");		
		WriteLine($"Compare using approx method: {exp_i_pi.approx(exp_i_pi_man)}");


		complex i_to_i = cmath.pow(cmath.I,cmath.I);
		complex i_to_i_man = new complex(cmath.exp((-Math.PI / 2)),0);

		WriteLine($"\ni raised to i:");
		WriteLine($"cmath.pow(cmath.I,cmath.I) = {i_to_i}");
		WriteLine($"Manually constructed result with Re=exp(-π/2) and Im=0: {i_to_i_man}");		
		WriteLine($"Compare using apmethodprox : {i_to_i.approx(i_to_i_man)}");
	
		complex ln_i = cmath.log(cmath.I);
		complex ln_i_man = new complex(0,Math.PI/2);

		WriteLine($"\nln(i):");
		WriteLine($"cmath.log(cmath.I) = {ln_i}");
		WriteLine($"Manually constructed result with Re=0 and Im=π/2: {ln_i_man}");		
		WriteLine($"Compare using approx method: {ln_i.approx(ln_i_man)}");

		complex sin_i_pi = cmath.sin(cmath.I*Math.PI);
		complex sin_i_pi_man = new complex(0,Math.Sinh(Math.PI));

		WriteLine($"\nsin(iπ):");
		WriteLine($"cmath.sin(cmath.I*Math.PI) = {sin_i_pi}");
		WriteLine($"Manually constructed result with Re=0 and Im=sinh(π): {sin_i_pi_man}");		
		WriteLine($"Compare using approx method: {sin_i_pi.approx(sin_i_pi_man)}");

	
	}	
}