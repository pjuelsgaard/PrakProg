all : PartA.svg PartB.svg

PartA.svg : A.plot.gpi out.A.txt out1.A.txt
	gnuplot $<

PartB.svg : B.plot.gpi out0.B.txt out1.B.txt out2.B.txt
	gnuplot $<

out.A.txt out1.A.txt : A.exe
	mono $< 1> out.A.txt

out0.B.txt out1.B.txt out2.B.txt : B.exe
	mono $<

A.exe : A.cs matlib.dll
	mcs $< -reference:$(word 2, $^) > $@

B.exe : B.cs matlib.dll
	mcs $< -reference:$(word 2, $^) > $@

matlib.dll : vector.cs ode.cs
	mcs -target:library -out:$@ $^


clean:
	rm -f *.dll *.exe *.svg out*
