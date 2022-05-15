all: out.txt linear_interpolation.svg
	cat out.txt

linear_interpolation.svg : input_data.txt interpolated_data.txt linearplot.gpi
	gnuplot linearplot.gpi

interpolated_data.txt out.txt: main.exe input_data.txt Makefile
	mono main.exe -input:input_data.txt -output:interpolated_data.txt -z:2.56 >out.txt
	
main.exe : main.cs 
	mcs main.cs

PHONY : clean
clean:
	rm *.exe
	rm *.svg
	rm out.txt
	