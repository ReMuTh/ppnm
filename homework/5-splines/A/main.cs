using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;

public class interpol {

	static List<double> x = new List<double>();
    static List<double> y = new List<double>();
    static List<double> s = new List<double>();
	static string infile = null;
	static string outfile = null;
	static double z;
	static bool zset = false;
	static int n = 101;
	static int len;
	static bool verbose = false;
	static int lastbin = 0;
	
	public static int Main(string[] args) {
		foreach(var arg in args){
			// parsing input parameters as in io-homework 
			var w=arg.Split(':');
			// -input sets the input file to read data from that needs interpolating
			// 
			if(w[0]=="-input")infile=w[1];
			// -output sets the output file to put interpolated datapoints
			else if(w[0]=="-output")outfile=w[1];
			// -n sets the number of interpolation points
			// the first and last being the first and last in the inputdata 
			else if(w[0]=="-n"){n=int.Parse(w[1]);}
			// -z sets an x value that needs interpolation
			else if(w[0]=="-z"){z=double.Parse(w[1]);zset = true;}
			// -Verbose output more explicit
			else if(w[0]=="-verbose"){verbose = true;}
			else { Error.WriteLine("wrong argument"); return 1;}
		}
		
		// only proceed doing anything if -input is set

		if(!String.IsNullOrEmpty(infile)) {
			
			read_datafile();
			
			// A coarse integration between the imput points is performed and kept
			// saves on calculations when integrating individual points later
			coarse_integration();
			
			// If outfile is set fill it with equidistant interpolation values 
			if(!String.IsNullOrEmpty(outfile)) write_outputfile();
			
			// if a single z has been requested return interpolation value
			if(zset) {
				double yp = linterp(z);
				double sp = fine_integration(z,yp);
				System.Console.WriteLine($"Interpolated value: y({z}) = {yp}");
				System.Console.WriteLine($"Integrated value at x={z}: {sp}");
			}
		}
		
		else {
			Error.WriteLine("-input [name of datafile] needs to be set. "); return 1;
		}
	
		return 0;	
		
	}
	
	static void read_datafile() {
		var instream = new System.IO.StreamReader(infile);
		// Parse file and fill up xy
		for(string line=instream.ReadLine();line!=null;line=instream.ReadLine()){			
				string[] linebits = line.Split(' ');
				double x_read = double.Parse(linebits[0]);
				double y_read = double.Parse(linebits[1]);
				x.Add (x_read);
				y.Add (y_read);
				if(verbose) System.Console.WriteLine($"Reading line: {x_read} {y_read}");
		}
		instream.Close();	
		// Keep number of input points
		len = x.Count;
	}

	static void write_outputfile() {
		var outstream = new System.IO.StreamWriter(outfile);
		
		double dx = (x[len - 1]-x[0])/(n-1);
		double xp = x[0];
		double yp,sp;
		// reusing i, no harm.
		for(int i = 0;i<(n-1);i++) {
			// Getting interpolated values
			yp = linterp(xp);
			sp = fine_integration(xp,yp);
			outstream.WriteLine($"{xp} {yp} {sp}");
			xp += dx;
		}
		// final point taken as last point from input data, Otherwise in some cases
		// xp would can slighly past the last input point due to rounding error, making the binsearch fail
		// integration is of couse just last element in the coarse integration list
		outstream.WriteLine($"{x[len-1]} {y[len-1]} {s[len-1]}");
		outstream.Close();
	}
	
	static double trapez_area(double x1,double x2,double y1,double y2) {
		return (x2-x1)*(y1+y2)/2;
	}
	
	static void coarse_integration() {
		double sum = 0;
		// adding a zero as first item makes the use of the course register more general later
		s.Add(sum);
		for(int i = 0;i<len-1;i++) {
			sum += trapez_area(x[i],x[i+1],y[i],y[i+1]);
		    if(verbose) System.Console.WriteLine($"Coarse: step {i}, sum is {sum}");
			s.Add(sum);	
		}
			
	}
	
	static double linterp(double z){
        int i=binsearch(z);
        double dx=x[i+1]-x[i]; if(!(dx>0)) throw new Exception("Overlapping x values");
        double dy=y[i+1]-y[i];
        return y[i]+dy/dx*(z-x[i]);
    }
    
	// fine_integration rely on both precalculated coarse integratation
	// also, it relies on the interpolation at z, so linterp only has to
	// be called once per interpolation point
	static double fine_integration(double z,double yz) {
		int i=binsearch(z);
 		return s[i]+trapez_area(x[i],z,y[i],yz);
	}

	static int binsearch(double z) {
		/* locates the interval for z by bisection */ 
		if(x[lastbin] <= z && z <= x[lastbin+1]) return lastbin;
		
		if(!(x[0]<=z && z<=x[len-1])) throw new Exception("binsearch: z out of x range");
		int i=0, j=len-1;
		while(j-i>1){
			int mid=(i+j)/2;
			if(z>x[mid]) i=mid; else j=mid;
			}
		if(verbose) System.Console.WriteLine($"Binsearch: {z} is in interval {i}");
		lastbin = i;
		return i;
	}

}
