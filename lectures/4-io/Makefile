SDKDIR=$(HOME)/.dotnet/sdk/6.0.102# or whatever your sdk is
CSCPATH=$(SDKDIR)/Roslyn/bincore/csc.dll
NETSTANDARD=$(SDKDIR)/ref/netstandard.dll
CONFIG=$(SDKDIR)/vstest.console.runtimeconfig.json
DOTNET=DOTNET_CLI_TELEMETRY_OPTOUT=1 && dotnet
CSC=$(DOTNET) $(CSCPATH) -reference:$(NETSTANDARD)
RUN=$(DOTNET) exec --runtimeconfig $(CONFIG)
CSC = mcs
RUN = mono

all: Out-stdin.txt outfile.txt out.cmdline.txt
	cat out.cmdline.txt

out.cmdline.txt:cmdline.exe Makefile
	#$(RUN) cmdline.exe 1.23e-4    45  	3e5 >$@  
	#$(RUN) cmdline.exe $$(echo 1.23e-4    45  	3e5) >$@  
	$(RUN) cmdline.exe $$(cat input.txt) >$@  

cmdline.exe:cmdline.cs
	$(CSC) -target:exe -out:$@ $<

outfile.txt: fileio.exe
	$(RUN) $< < input.txt 1> $@

fileio.exe:fileio.cs
	$(CSC) -target:exe -out:$@ $<

Out-stdin.txt: stdin.exe input.txt
	$(RUN) $< < input.txt 1> $@ 2>log.txt

input.txt: Makefile
	echo -1.234e-5 99.9e+5 > $@

stdin.exe: stdin.cs
	$(CSC) -target:exe -out:$@ $<

.PHONEY:clean
clean:
	$(RM) [Oo]ut* *.exe [Ll]og*

test:
	echo $(RM)
