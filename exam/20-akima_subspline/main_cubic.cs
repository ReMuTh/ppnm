using System;
using static System.Console;

public class interpol {

	static double[] x;
    static double[] y;
	static string infile = null;
	static string outfile = null;
	static double z;
	static bool zset = false;
	static int N = 101;
	static int n;
	static bool verbose = false;
	static cubicspline qspline;
	public static int Main(string[] args) {

		foreach(var arg in args){
			// parsing input parameters as in io-homework 
			var w=arg.Split(':');
			// -input sets the input file to read data from that needs interpolating
			if(w[0]=="-input")infile=w[1];
			// -output sets the output file to put interpolated datapoints
			else if(w[0]=="-output")outfile=w[1];
			// -n sets the number of interpolation points
			// the first and last being the first and last in the inputdata 
			else if(w[0]=="-n"){N=int.Parse(w[1]);}
			// -z sets an x value that needs interpolation
			else if(w[0]=="-z"){z=double.Parse(w[1]);zset = true;}
			// -Verbose output more explicit
			else if(w[0]=="-verbose"){verbose = true;}

			else { Error.WriteLine("wrong argument"); return 1;}
		}
		
		// only proceed doing anything if -input is set

		if(!String.IsNullOrEmpty(infile)) {
			
			try {
				read_datafile();
			}
			catch(Exception ex) {
				Error.WriteLine(ex.Message);
				return 1;
			}
			
			// If null is passed, the akimaspline constructor will default to "akima" endpoints 

			try {
				qspline = new cubicspline(x,y);
			}
			catch(ArgumentException ex) {
				Error.WriteLine(ex.Message);
				return 1;	
			}
			
			// If outfile is set fill it with equidistant interpolation values 
			if(!String.IsNullOrEmpty(outfile)) {
				try{
					write_outputfile();
				}
				catch(Exception ex) {
					Error.WriteLine(ex.Message);
				}
			}

			// if a single z has been requested return interpolation value
			if(zset) {
				double yp,dp,sp;
				try{
					(yp,dp,sp) = qspline.eval(z);

					System.Console.WriteLine($"Akima spline interpolation of data from {infile}");
					System.Console.WriteLine($"Interpolated value: y({z}) = {yp}");
					System.Console.WriteLine($"Derivative at x={z}: {dp}");
					System.Console.WriteLine($"Integration from x={x[0]} to x={z}: {sp}");
				}
				catch(ArgumentException ex) {
					Error.WriteLine(ex.Message);	
				}
			}
		}
		
		else {
			Error.WriteLine("-input [name of datafile] needs to be set."); return 1;
		}
	
		return 0;	
		
	}
	
	static void read_datafile() {
		if(verbose) System.Console.WriteLine($"read_datafile called");
		string[] lines = System.IO.File.ReadAllLines(infile);
		n = lines.Length;
		x = new double[n];
		y = new double[n];
		
		// Parse file and fill up xy
		for(int i = 0;i<n;i++){			
				string[] linebits = lines[i].Split(' ');
				x[i] = double.Parse(linebits[0]);
				y[i] = double.Parse(linebits[1]);
				if(verbose) System.Console.WriteLine($"Reading line: {x[i]} {y[i]}");
		}
	}

	static void write_outputfile() {
		if(verbose) System.Console.WriteLine($"write_outputfile called");
		var outstream = new System.IO.StreamWriter(outfile);
		
		double dx = (x[n-1]-x[0])/(N-1);
		double xp = x[0];
		double yp,dp,sp;
		
		for(int i = 0;i<(N-1);i++) {
			// Getting spline values

			(yp,dp,sp) = qspline.eval(xp);
			
			outstream.WriteLine($"{xp} {yp} {dp} {sp}");
			xp += dx;
		}
		// final point taken as last point from input data, Otherwise in some cases
		// xp would can slighly past the last input point due to epsilon error
		// making the binsearch fail
		
		xp = x[n-1];
		(yp,dp,sp) = qspline.eval(xp);
		outstream.WriteLine($"{xp} {yp} {dp} {sp}");
		outstream.Close();

	}
	

}
