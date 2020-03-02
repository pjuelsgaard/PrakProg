all : log out.txt
	cat log

PartA.svg : plot.gpi out.txt
	gnuplot $<

out.txt log: main.exe
	mono $< 1> out.txt 2> log

main.exe : main.cs matlib.dll
	mcs $< -reference:$(word 2, $^) > $@

matlib.dll : ~/matlib/matrix/vector.cs ~/matlib/ode/ode.cs
	mcs -target:library -out:$@ $^


clean:
	rm -f *.dll *.exe log* out* *.svg
