all: out.txt
	cat out.txt

out.txt: cmdline_to_stdout.exe
	mono cmdline_to_stdout.exe 1 2 3 4 5 6 7 8 9 10 > out.txt

cmdline_to_stdout.exe : cmdline_to_stdout.cs
	mcs -target:exe -out:cmdline_to_stdout.exe cmdline_to_stdout.cs

clean:
	rm *.exe [Oo]ut.txt