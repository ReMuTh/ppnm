CSC = mcs
RUN = mono
OUT = out.txt

all:$(OUT)
	cat $<

$(OUT): main.exe
	mono main.exe > out.txt

main.exe:main.cs genlist.dll
	$(CSC) -t:exe -out:$@ $(addprefix -r:,$(filter %.dll,$^)) $(filter %.cs,$^)

genlist.dll:genlist.cs
	$(CSC) -target:library -out:$@ $<

clean:
	$(RM) *.dll *.exe
