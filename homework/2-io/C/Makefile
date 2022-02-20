all: out.txt
	cat out.txt

out.txt: filein_to_fileout.exe
	mono filein_to_fileout.exe -input:in.txt -output:out.txt

filein_to_fileout.exe : filein_to_fileout.cs
	mcs -target:exe -out:filein_to_fileout.exe filein_to_fileout.cs
	
clean:
	rm *.exe [Oo]ut.txt