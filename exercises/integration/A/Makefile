all : out.txt erf_integrated.png
	cat out.txt

erf_integrated.png: erf.data.txt erf.tab.txt erf.plot.gpi Makefile
	gnuplot erf.plot.gpi	

erf.data.txt: erf.exe
	mono $< | sed 's/,/./g' > $@

out.txt : main.exe
	mono $< > $@

erf.exe : erf.cs matlib.dll
	mcs -target:exe -reference:matlib.dll -out:$@ $<

main.exe : main.cs matlib.dll
	mcs -target:exe -reference:matlib.dll -out:$@ $<

matlib.dll : integrate.cs
	mcs -target:library -out:$@ $<

PHONY: clean
clean:
	rm *.exe
	rm *.png
	