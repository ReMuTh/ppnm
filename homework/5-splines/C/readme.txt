Homework 5 C. Cubic spline interpolation 
René Thalund

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
	1. After reading in the data, the program calculates an array of coarse
	accumulated integration areas at the input x values.
	The array is kept making subsequent integration to interpoint x values faster
	
	2. The last found binary search interval is kept, and checked as first option
	when a new integration point is requested.
	If z values all over the place were needed this adds two comparisons extra per point.
	However, when a large range of monotonic increasing or desceasing z values is requested
	like when writing the output file it saves greatly on computation.
	
	
Testing validity:

	For all tests my cubic routine is compared to Gnuplot smooth cspline routine.
	In all tests, the resemblance is cunning.

	TEST 1
	A test input file with 14 scattered values of sin(x) in the interval [-pi,pi], endpoints included.
	101 even spaced interpolation points and integration are created.
	Results are plotted in cubic_spline_sin.svg

	result is excellent and smoother than the quadratic splines

	TEST 2
	Random input data y values uniform in [-1,1]
	Results are plotted in cubic_spline_sin.svg
	Also handles this well. Integration ends up around 0 as would be expected

	TEST 3
	Step function artefact.
	Compared to the quadratic case, I notice that the ringing is still there but is thouroghly
	supressed in the points before and following the step-up.

	