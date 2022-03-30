Homework 5 A. linear interpolation 
René Thalund

Everything put in the main.cs.
Anything above this complexity should be organised with separate libraries…

main.exe takes the following arguments

-input [string]
	Relative path input file to read data from that needs interpolating.
	Format is two columns of x y data separated by space
	Must be ordered in incrementing x.
	Must be set for any further processing going on.
	

-output [string]
	Relative path to the output file to put interpolated datapoints
	
-n [int]
	The number of interpolation points being put in the output file. Defaults to 101
	(100 interpolation intervals)
	The first and last being the first and last in the -input data

-x [double]
	x value for interpolation
	The corresponding interpolated y value and 
	interpolated integration value will be returned in the console
	Cannot be outside the limits of x values in the -input file


Optimization efforts:
	1. After reading in the data, the program calculates a list of coarse
	integration values at the input points.
	The list is kept for later, since any (interpolated) integration value
	can then be found by calculating just one trapezoid area and summing
	with the appropriate accumulated value
	
	2. The last found binary search interval is kept, and checked as first option
	when a new integration point is requested.
	If z values all over the place were needed this adds two comparisons extra per point.
	However, when a large range of monotonic increasing or desceasing z values is requested
	like when writing the output file it saves greatly on computation.
	
	
Testing validity of method:
	A test input file with 14 scattered values of sin(x) in the interval [-pi,pi], endpoints included.
	101 even spaced interpolation points and integration are created.
	Results are plotted in linear_interpolation.eps
	
	The interpolation should appear as straight lines between the input points. Confirmed.
	The plot of integration segments should be smoother, as they are parabolas. Confirmed.
	The integration should begin in y=0. Confirmed.
	As sin(x) is an odd functions the integration should approximately end back in 0 at x=pi. Confirmed
	The analytic integral function from x = -pi is -cos(x)-1
	Thus the interpolation integral should have a minimum of approximately -2 at x=0. Confirmed
	[If anything it should be slighly above -2 as sin(x) 	is concave compared to the y = 0 line, thus
	the trapezoid area is less than the real sin(x) area]