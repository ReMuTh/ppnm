SDKDIR=$(HOME)/.dotnet/sdk/6.0.102# or whatever your sdk is
CSCPATH=$(SDKDIR)/Roslyn/bincore/csc.dll
NETSTANDARD=$(SDKDIR)/ref/netstandard.dll
CONFIG=$(SDKDIR)/vstest.console.runtimeconfig.json
DOTNET=DOTNET_CLI_TELEMETRY_OPTOUT=1; dotnet
CSC=$(DOTNET) $(CSCPATH) -reference:$(NETSTANDARD)
RUN=$(DOTNET) exec --runtimeconfig $(CONFIG)
CSC = mcs
RUN = mono

all:ms.pdf

ms.pdf:ms.tex gamma_pyx.pdf gamma_gnu.tex
	pdflatex $<
	pdflatex $<
#	bibtex $<
#	pdflatex $<

gamma_gnu.tex:fig.gnuplot.gpi out.gamma.txt tab.gamma.txt
	gnuplot $<


out.gamma.txt:main.exe
	$(RUN) $< > $@

main.exe: main.cs sfuns.dll

%.exe: %.cs
	$(CSC) -t:exe $(addprefix -r:,$(filter %.dll,$^)) $(filter %.cs,$^)

%.dll: %.cs
	$(CSC) -t:library $(addprefix -r:,$(filter %.dll,$^)) $(filter %.cs,$^)
