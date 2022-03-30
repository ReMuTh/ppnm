all: out.txt linear_interpolation.eps
	cat out.txt

linear_interpolation.eps : input_data_sin.txt interpolated_data.txt quadspline_plot.gpi Makefile
	gnuplot quadspline_plot.gpi

interpolated_data.txt out.txt: main.exe input_data_sin.txt Makefile
	mono main.exe -input:input_data_sin.txt -output:interpolated_data.txt -n:200 -z:2.56 >out.txt
	

main.exe: main.cs quadspline.dll
	mcs -target:exe -out:main.exe -reference:quadspline.dll main.cs

quadspline.dll:quadspline.cs
	mcs -target:library -out:quadspline.dll quadspline.cs

clean:
	rm *.exe *.dll
