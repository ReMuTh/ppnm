all: QRGS_timing.txt QRGS_timing.svg;
	
QRGS_timing.svg : QRGS_timing.txt QRGS_timing_plot.gpi Makefile
	echo "Plotting results and fitting to n^3 dependence."
	gnuplot QRGS_timing_plot.gpi

QRGS_timing.txt: main.exe
	echo "Performing QRGS of n x n matrices n = 5,10,15,…,500. Please hold."
	mono $< > $@

main.exe: main.cs matrix.dll QRGS.dll
	mcs -reference:matrix.dll -reference:QRGS.dll main.cs

QRGS.dll : ../matlib/QRGS.cs matrix.dll
	mcs -target:library -reference:matrix.dll -out:$@  ../matlib/QRGS.cs

matrix.dll : ../matlib/vector.cs ../matlib/matrix.cs ../matlib/matrix_rene.cs
	mcs -target:library -out:$@ $^

clean:
	rm *.dll *.exe
	rm QRGS_timing.txt