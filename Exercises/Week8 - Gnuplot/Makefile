all: Gamma.svg Gamma-error.svg B.svg C.svg

Gamma-error.svg: Gamma-error.gpi out.gamma.txt out.gamma.error.txt
	gnuplot $<
Gamma.svg: gamma.gpi out.gamma.txt out.gamma.test.txt
	gnuplot $<

Erf.svg : erf.gpi out.erf.txt out.erf.test.txt
	gnuplot $<


out.gamma.txt out.gamma.error.txt: main-gamma.exe
	mono $< > out.gamma.txt 2> out.gamma.error.txt

out.erf.txt : main-erf.exe
	mono $< > $@

out.gamma.test.txt: gamma.c
	tcc -run $< > $@
out.erf.test.txt: erf.c
	tcc -run $< > $@


main-gamma.exe : main-gamma.cs math.dll
	mcs $< -out:$@ -reference:$(word 2, $^)


main-erf.exe : main-erf.cs math.dll
	mcs $< -out:$@ -reference:$(word 2, $^)





math.dll: gamma.cs erf.cs complex.cs cmath.cs
	mcs -target:library -out:$@ $^





.PHONEY:clean
clean: 
	rm -f *.dll *.exe out* *.svg


