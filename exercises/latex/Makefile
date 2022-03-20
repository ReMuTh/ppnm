FIG = figures/

report.pdf: report.tex $(FIG)ex_plot.eps $(FIG)exerror_plot.eps Makefile
	pdflatex report.tex
	
$(FIG)exerror_plot.eps $(FIG)ex_plot.eps $(FIG)exerror_plot.tex $(FIG)ex_plot.tex: \
ex.data.txt $(FIG)exerror.plot.gpi $(FIG)ex.plot.gpi
	gnuplot $(FIG)ex.plot.gpi
	gnuplot $(FIG)exerror.plot.gpi

ex.data.txt: main.exe
	mono $< | sed 's/,/./g' > $@

%.exe: %.cs
	mcs $<

PHONY: clean
clean:
	rm *.exe
	rm $(FIG)*.eps
	rm $(FIG)*.tex
	
