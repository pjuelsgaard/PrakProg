all: plot2.svg plot1.svg plot3.svg plot4.svg plot5.svg plot6.svg

plot6.svg : plot6.gpi out.txt
	gnuplot $<

plot5.svg : plot5.gpi out.txt
	gnuplot $<

plot4.svg : plot4.gpi out.txt
	gnuplot $<

plot3.svg : plot3.gpi out.txt
	gnuplot $<

plot2.svg : plot2.gpi out.txt
	gnuplot $<

plot1.svg : plot1.gpi out.txt
	gnuplot $<

out.txt : main.exe
	mono $< 2> $@

main.exe : main.cs matlib.dll
	mcs $< -reference:$(word 2, $^) -out:$@

matlib.dll : rk.cs matrix.cs vector.cs cmath.cs complex.cs erf.cs
	mcs -target:library -out:$@ $^

clean:
	rm -f *.dll *.txt *.exe *.svg
