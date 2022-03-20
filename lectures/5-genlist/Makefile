SDKDIR=$(HOME)/.dotnet/sdk/6.0.102# or whatever your sdk is
CSCPATH=$(SDKDIR)/Roslyn/bincore/csc.dll
NETSTANDARD=$(SDKDIR)/ref/netstandard.dll
CONFIG=$(SDKDIR)/vstest.console.runtimeconfig.json
DOTNET=DOTNET_CLI_TELEMETRY_OPTOUT=1; dotnet
CSC=$(DOTNET) $(CSCPATH) -reference:$(NETSTANDARD)
RUN=$(DOTNET) exec --runtimeconfig $(CONFIG)
CSC = mcs
RUN = mono
all:Out.txt
	cat $<
Out.txt: main.exe
	$(RUN) main.exe > Out.txt
main.exe: main.cs genlist.dll
	$(CSC) -target:exe -out:$@ -reference:genlist.dll $<
genlist.dll: genlist.cs
	$(CSC) -target:library -out:$@ $<
clean:
	$(RM) *.dll *.exe [Oo]ut* [Ll]og*
