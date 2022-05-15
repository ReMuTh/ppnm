MATHDIR = ../../../matlib

cgamma.gnuplot.svg : cgamma.data.txt cgamma.plot.gpi Makefile
	gnuplot cgamma.plot.gpi

cgamma.data.txt: main.exe
	mono main.exe > cgamma.data.txt

main.exe: main.cs cmath.dll
	mcs main.cs -reference:cmath.dll

cmath.dll: $(MATHDIR)/complex/cmath.cs $(MATHDIR)/complex/complex.cs
	mcs -target:library -out:./cmath.dll $^ 

PHONY: clean
clean:
	rm *.exe
	rm *.svg
	rm cgamma.data.txt
