all: out.txt fig/akima_spline_sin.pdf fig/cubic_spline_sin.pdf fig/akima_spline_step.pdf fig/akima_spline_endpoints.pdf akima_report.pdf fig/python_comparison.pdf Makefile #
	cat out.txt
	echo "See readme.md and akima_report.pdf"

# Comparing akima spline to cubic spline




akima_report.pdf:akima_report.tex fig/cubic_spline_sin.pdf fig/akima_spline_sin.pdf fig/akima_spline_step.pdf fig/akima_spline_endpoints.pdf fig/python_comparison.pdf
	pdflatex akima_report.tex

fig/python_comparison.pdf:scipy_comparison.py
	python3 scipy_comparison.py

# Test 1 (sinus): input data following a sine curve with irregular x values
# and also outputting one akima interpolation value to out.txt

fig/cubic_spline_sin.pdf: data/input_data_sin.txt data/interpol_cubic_sin.txt cubicspline_plot_sin.gpi
	gnuplot cubicspline_plot_sin.gpi

fig/akima_spline_sin.pdf: data/input_data_sin.txt data/interpol_akima_sin.txt akimaspline_plot_sin.gpi
	gnuplot akimaspline_plot_sin.gpi

data/interpol_akima_sin.txt out.txt: akimaspline.exe data/input_data_sin.txt
	mono akimaspline.exe -input:data/input_data_sin.txt -output:data/interpol_akima_sin.txt -n:200 -endpoints:akima -z:1.13 >out.txt

data/interpol_cubic_sin.txt: cubicspline.exe data/input_data_sin.txt
	mono cubicspline.exe -input:data/input_data_sin.txt -output:data/interpol_cubic_sin.txt -n:200

# Test 2 (step function): constant input data doing a sudden step

fig/akima_spline_step.pdf: data/input_data_step.txt data/interpol_akima_step.txt data/interpol_cubic_step.txt  akimaspline_plot_step.gpi
	gnuplot akimaspline_plot_step.gpi

data/interpol_akima_step.txt: akimaspline.exe data/input_data_sin.txt
	mono akimaspline.exe -input:data/input_data_step.txt -output:data/interpol_akima_step.txt -endpoints:akima -n:200

data/interpol_cubic_step.txt: cubicspline.exe data/input_data_sin.txt
	mono cubicspline.exe -input:data/input_data_step.txt -output:data/interpol_cubic_step.txt -n:200


# Test 3 (endpoint schemes): three different schemes of akima endpoint slopes

fig/akima_spline_endpoints.pdf: data/input_data_endpoints.txt data/interpol_akima_endpoints.txt data/interpol_naive_endpoints.txt data/interpol_bica_endpoints.txt data/interpol_cubic_endpoints.txt akimaspline_plot_endpoints.gpi
	gnuplot akimaspline_plot_endpoints.gpi

data/interpol_cubic_endpoints.txt: akimaspline.exe data/input_data_endpoints.txt
	mono cubicspline.exe -input:data/input_data_endpoints.txt -output:data/interpol_cubic_endpoints.txt -n:200

data/interpol_akima_endpoints.txt: akimaspline.exe data/input_data_endpoints.txt
	mono akimaspline.exe -input:data/input_data_endpoints.txt -output:data/interpol_akima_endpoints.txt -endpoints:akima -n:200

data/interpol_naive_endpoints.txt: akimaspline.exe data/input_data_endpoints.txt
	mono akimaspline.exe -input:data/input_data_endpoints.txt -output:data/interpol_naive_endpoints.txt -endpoints:naive -n:200

data/interpol_bica_endpoints.txt: akimaspline.exe data/input_data_endpoints.txt
	mono akimaspline.exe -input:data/input_data_endpoints.txt -output:data/interpol_bica_endpoints.txt -endpoints:bica -n:200


cubicspline.exe: main_cubic.cs cubiclib.dll
	mcs -target:exe -out:cubicspline.exe -reference:cubiclib.dll main_cubic.cs

akimaspline.exe: main_akima.cs akimalib.dll
	mcs -target:exe -out:akimaspline.exe -reference:akimalib.dll  main_akima.cs

cubiclib.dll:cubicspline.cs
	mcs -target:library -out:cubiclib.dll cubicspline.cs

akimalib.dll:akimaspline.cs
	mcs -target:library -out:akimalib.dll akimaspline.cs

clean:
	rm *.exe *.dll fig/*.pdf data/interpol*.txt *.log *.pdf
