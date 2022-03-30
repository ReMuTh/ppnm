erf.gnuplot.png: erf.data.txt erf.tab.txt erf.plot.gpi Makefile
	gnuplot erf.plot.gpi	

erf.data.txt: main.exe
	mono $< | sed 's/,/./g' > $@

%.exe: %.cs
	mcs $<

PHONY: clean
clean:
	rm *.exe
	rm *.eps
	rm *.png
	
