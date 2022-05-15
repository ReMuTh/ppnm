all: jacobi_timing_opti.txt jacobi_timing.txt jacobi_timing.svg;

jacobi_timing.svg : jacobi_timing.txt jacobi_timing_plot.gpi Makefile
	echo "Plotting results and fitting to n^3 dependence."
	gnuplot jacobi_timing_plot.gpi


jacobi_timing_opti.txt: main_opti.exe
	echo "Performing jacobi of n x n matrices n = 5,10,15,…,200. Please hold."
	mono $< > $@

jacobi_timing.txt: main.exe
	echo "Performing jacobi of n x n matrices n = 5,10,15,…,200. Please hold again."
	mono $< > $@

main_opti.exe: main.cs matrix.dll jacobi_opti.dll
	mcs -reference:matrix.dll -reference:jacobi_opti.dll -out:$@ main.cs

main.exe: main.cs matrix.dll jacobi.dll
	mcs -reference:matrix.dll -reference:jacobi.dll -out:$@ main.cs

jacobi_opti.dll : ../matlib/jacobi.cs matrix.dll
	mcs -target:library -reference:matrix.dll -out:$@  ../matlib/jacobi_opti.cs

jacobi.dll : ../matlib/jacobi.cs matrix.dll
	mcs -target:library -reference:matrix.dll -out:$@  ../matlib/jacobi.cs

matrix.dll : ../matlib/vector.cs ../matlib/matrix.cs ../matlib/matrix_rene.cs
	mcs -target:library -out:$@ $^

clean:
	rm *.dll *.exe [Oo]ut*