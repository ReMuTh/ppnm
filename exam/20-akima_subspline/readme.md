Akima spline interpolation
==========================

René Thalund, 19920543
----------------------

akimaspline.exe takes the following arguments

-input [string]
	Relative path input file to read data from that needs interpolating.
	Format is two columns of x y data separated by space
	Must be ordered in incrementing x.
	Must be set for any further processing going on.

-output [string]
	Relative path to the output file in which put interpolated datapoints
	The output file will have four space-separated columns.
	Column 1: x values
	Column 2: interpolated y-value
	Column 3: first derivative of the interpolation curve. Calculated using an analytical expression on the cubic sub-spline.
	Column 4: definate integral from the first x point to the x-value in column 1. Calculated using an analytical expression on the cubic sub-spline.
	
-n [int]
	The number of interpolation points being put in the output file. Defaults to 101
	(100 interpolation intervals). The first and last being the first and last in the input data

-z [double]
	single x-value for interpolation
	The corresponding interpolated y value and 
	values for the first derivative and definate integral will be returned in the console
	z cannot be outside the limits of x values in the -input file


Optimization efforts:
	1. After reading in the data, the program calculates an array of coarse
	accumulated integration areas at the input x values.
	The array is kept and makes subsequent integration to interpoint x values faster.
	
	2. The last found binary search interval is kept, and checked as first option
	when a new integration point is requested.
	If z values all over the place are needed this adds two comparisons extra per point.
	However, when a large range of monotonic increasing or desceasing z values is requested
	like when writing n consequtive z-values to the output file it saves greatly on computation.
